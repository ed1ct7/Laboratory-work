-- Один скрипт: нормализованные таблицы для "Заказы" (шапка + позиции)
-- Ожидается, что уже существуют таблицы:
--   customers(customer_id), employees(employee_id), components(component_id), services(service_id)

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS order_services;
DROP TABLE IF EXISTS order_components;
DROP TABLE IF EXISTS orders;

SET FOREIGN_KEY_CHECKS = 1;

-- 1) Шапка заказа
CREATE TABLE orders (
  order_id INT UNSIGNED NOT NULL AUTO_INCREMENT,

  order_date DATE NOT NULL,          -- Дата заказа
  due_date   DATE NULL,              -- Дата исполнения (план/факт можно разделить позже)

  customer_id INT UNSIGNED NOT NULL, -- Код заказчика
  employee_id INT UNSIGNED NOT NULL, -- Код сотрудника (кто оформил/ведёт)

  prepayment_share DECIMAL(5,2) NOT NULL DEFAULT 0.00, -- Доля предоплаты (например 30.00 = 30%)

  payment_status ENUM('unpaid','partial','paid') NOT NULL DEFAULT 'unpaid',   -- Отметка об оплате
  execution_status ENUM('new','in_work','done','canceled') NOT NULL DEFAULT 'new', -- Отметка об исполнении

  PRIMARY KEY (order_id),

  KEY idx_orders_customer (customer_id),
  KEY idx_orders_employee (employee_id),
  KEY idx_orders_order_date (order_date),

  CONSTRAINT fk_orders_customer
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
    ON UPDATE CASCADE ON DELETE RESTRICT,

  CONSTRAINT fk_orders_employee
    FOREIGN KEY (employee_id) REFERENCES employees(employee_id)
    ON UPDATE CASCADE ON DELETE RESTRICT

  -- Примечание: "Общая стоимость" и "Срок общей гарантии" тут НЕ храним,
  -- потому что это производные значения (нарушают 3НФ и дают аномалии обновления).
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 2) Позиции заказа: комплектующие
CREATE TABLE order_components (
  order_component_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  order_id INT UNSIGNED NOT NULL,
  component_id INT UNSIGNED NOT NULL,

  quantity INT UNSIGNED NOT NULL DEFAULT 1,

  -- Если нужно фиксировать цену на момент заказа (иначе история "сломается", если цена в components изменится)
  unit_price DECIMAL(12,2) NOT NULL,

  -- Если нужно фиксировать гарантию на момент заказа (иначе при изменении в components история тоже "поедет")
  warranty_months INT UNSIGNED NULL,

  PRIMARY KEY (order_component_id),

  KEY idx_order_components_order (order_id),
  KEY idx_order_components_component (component_id),
  UNIQUE KEY uq_order_component_once (order_id, component_id),

  CONSTRAINT fk_order_components_order
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
    ON UPDATE CASCADE ON DELETE CASCADE,

  CONSTRAINT fk_order_components_component
    FOREIGN KEY (component_id) REFERENCES components(component_id)
    ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 3) Позиции заказа: услуги
CREATE TABLE order_services (
  order_service_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  order_id INT UNSIGNED NOT NULL,
  service_id INT UNSIGNED NOT NULL,

  quantity INT UNSIGNED NOT NULL DEFAULT 1,
  unit_cost DECIMAL(12,2) NOT NULL,

  PRIMARY KEY (order_service_id),

  KEY idx_order_services_order (order_id),
  KEY idx_order_services_service (service_id),
  UNIQUE KEY uq_order_service_once (order_id, service_id),

  CONSTRAINT fk_order_services_order
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
    ON UPDATE CASCADE ON DELETE CASCADE,

  CONSTRAINT fk_order_services_service
    FOREIGN KEY (service_id) REFERENCES services(service_id)
    ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
