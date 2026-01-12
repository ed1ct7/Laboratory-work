-- Полный скрипт MySQL Workbench для всей БД "Компьютерная фирма"
-- (Нормализовано: сотрудники/должности/паспорта/адреса,
--  комплектующие/виды/производители/характеристики,
--  заказчики, услуги, заказы (шапка+позиции))

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- Сначала удаляем таблицы-дети
DROP TABLE IF EXISTS order_services;
DROP TABLE IF EXISTS order_components;
DROP TABLE IF EXISTS orders;

DROP TABLE IF EXISTS component_specs;
DROP TABLE IF EXISTS components;
DROP TABLE IF EXISTS manufacturers;
DROP TABLE IF EXISTS component_types;

DROP TABLE IF EXISTS customers;
DROP TABLE IF EXISTS employees;

DROP TABLE IF EXISTS passports;
DROP TABLE IF EXISTS addresses;
DROP TABLE IF EXISTS positions;

DROP TABLE IF EXISTS services;

SET FOREIGN_KEY_CHECKS = 1;

-- 1) Должности
CREATE TABLE positions (
  position_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  position_name VARCHAR(120) NOT NULL,
  salary DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  duties TEXT NULL,
  requirements TEXT NULL,
  PRIMARY KEY (position_id),
  UNIQUE KEY uq_positions_name (position_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 2) Адреса (общий справочник адресов)
CREATE TABLE addresses (
  address_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  country VARCHAR(80) NULL,
  region  VARCHAR(120) NULL,
  city    VARCHAR(120) NOT NULL,
  street  VARCHAR(160) NOT NULL,
  house   VARCHAR(20)  NOT NULL,
  apartment VARCHAR(20) NULL,
  postal_code VARCHAR(20) NULL,
  comment VARCHAR(255) NULL,
  PRIMARY KEY (address_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 3) Паспорта
CREATE TABLE passports (
  passport_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  series VARCHAR(10) NOT NULL,
  number VARCHAR(20) NOT NULL,
  issued_by VARCHAR(255) NULL,
  issued_date DATE NULL,
  division_code VARCHAR(20) NULL,
  PRIMARY KEY (passport_id),
  UNIQUE KEY uq_passports_series_number (series, number)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 4) Сотрудники (телефон один)
CREATE TABLE employees (
  employee_id INT UNSIGNED NOT NULL AUTO_INCREMENT,

  second_name VARCHAR(60) NOT NULL,
  first_name  VARCHAR(60) NOT NULL,
  middle_name VARCHAR(60) NULL,

  birth_date DATE NOT NULL,
  gender ENUM('M','F') NOT NULL,

  phone_number VARCHAR(30) NOT NULL,

  position_id INT UNSIGNED NOT NULL,
  passport_id INT UNSIGNED NULL,
  address_id  INT UNSIGNED NULL,

  PRIMARY KEY (employee_id),

  UNIQUE KEY uq_employees_phone (phone_number),
  UNIQUE KEY uq_employees_passport (passport_id),

  KEY idx_employees_position (position_id),
  KEY idx_employees_address  (address_id),

  CONSTRAINT fk_employees_positions
    FOREIGN KEY (position_id) REFERENCES positions(position_id)
    ON UPDATE CASCADE ON DELETE RESTRICT,

  CONSTRAINT fk_employees_passports
    FOREIGN KEY (passport_id) REFERENCES passports(passport_id)
    ON UPDATE CASCADE ON DELETE SET NULL,

  CONSTRAINT fk_employees_addresses
    FOREIGN KEY (address_id) REFERENCES addresses(address_id)
    ON UPDATE CASCADE ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 5) Заказчики (телефон один, адрес через addresses)
CREATE TABLE customers (
  customer_id INT UNSIGNED NOT NULL AUTO_INCREMENT,

  second_name VARCHAR(60) NOT NULL,
  first_name  VARCHAR(60) NOT NULL,
  middle_name VARCHAR(60) NULL,

  phone_number VARCHAR(30) NOT NULL,
  address_id INT UNSIGNED NULL,

  PRIMARY KEY (customer_id),

  UNIQUE KEY uq_customers_phone (phone_number),
  KEY idx_customers_address (address_id),

  CONSTRAINT fk_customers_addresses
    FOREIGN KEY (address_id) REFERENCES addresses(address_id)
    ON UPDATE CASCADE ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 6) Услуги
CREATE TABLE services (
  service_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  service_name VARCHAR(150) NOT NULL,
  description TEXT NULL,
  cost DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  PRIMARY KEY (service_id),
  UNIQUE KEY uq_services_name (service_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 7) Виды комплектующих
CREATE TABLE component_types (
  type_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  type_name VARCHAR(120) NOT NULL,
  description TEXT NULL,
  PRIMARY KEY (type_id),
  UNIQUE KEY uq_component_types_name (type_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 8) Производители (страна у производителя, чтобы не нарушать 3НФ в комплектующих)
CREATE TABLE manufacturers (
  manufacturer_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  manufacturer_name VARCHAR(160) NOT NULL,
  country VARCHAR(120) NULL,
  PRIMARY KEY (manufacturer_id),
  UNIQUE KEY uq_manufacturers_name (manufacturer_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 9) Комплектующие
CREATE TABLE components (
  component_id INT UNSIGNED NOT NULL AUTO_INCREMENT,

  type_id INT UNSIGNED NOT NULL,
  manufacturer_id INT UNSIGNED NOT NULL,

  brand VARCHAR(120) NOT NULL,          -- марка/модель
  release_date DATE NULL,
  warranty_months INT UNSIGNED NULL,   -- срок гарантии (в месяцах)
  description TEXT NULL,
  price DECIMAL(12,2) NOT NULL DEFAULT 0.00,

  PRIMARY KEY (component_id),

  KEY idx_components_type (type_id),
  KEY idx_components_manufacturer (manufacturer_id),

  CONSTRAINT fk_components_type
    FOREIGN KEY (type_id) REFERENCES component_types(type_id)
    ON UPDATE CASCADE ON DELETE RESTRICT,

  CONSTRAINT fk_components_manufacturer
    FOREIGN KEY (manufacturer_id) REFERENCES manufacturers(manufacturer_id)
    ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 10) Характеристики комплектующих (строго 1НФ: параметр–значение)
CREATE TABLE component_specs (
  spec_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  component_id INT UNSIGNED NOT NULL,
  spec_name VARCHAR(120) NOT NULL,
  spec_value VARCHAR(255) NOT NULL,

  PRIMARY KEY (spec_id),
  UNIQUE KEY uq_component_spec (component_id, spec_name),

  CONSTRAINT fk_specs_component
    FOREIGN KEY (component_id) REFERENCES components(component_id)
    ON UPDATE CASCADE ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 11) Заказы (шапка)
CREATE TABLE orders (
  order_id INT UNSIGNED NOT NULL AUTO_INCREMENT,

  order_date DATE NOT NULL,
  due_date   DATE NULL,

  customer_id INT UNSIGNED NOT NULL,
  employee_id INT UNSIGNED NOT NULL,

  prepayment_share DECIMAL(5,2) NOT NULL DEFAULT 0.00, -- 30.00 = 30%

  payment_status ENUM('unpaid','partial','paid') NOT NULL DEFAULT 'unpaid',
  execution_status ENUM('new','in_work','done','canceled') NOT NULL DEFAULT 'new',

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 12) Позиции заказа: комплектующие
CREATE TABLE order_components (
  order_component_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  order_id INT UNSIGNED NOT NULL,
  component_id INT UNSIGNED NOT NULL,

  quantity INT UNSIGNED NOT NULL DEFAULT 1,

  -- фиксируем цену и гарантию на момент заказа (иначе история "поедет" при изменении components)
  unit_price DECIMAL(12,2) NOT NULL,
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

-- 13) Позиции заказа: услуги
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
