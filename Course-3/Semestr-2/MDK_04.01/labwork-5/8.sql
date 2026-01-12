-- Пример заполнения ВСЕХ таблиц с EER (немного данных, но связно по FK)
-- Вставляй в MySQL Workbench SQL Editor и выполняй целиком.

SET NAMES utf8mb4;

-- Чтобы пример можно было запускать повторно:
SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE order_services;
TRUNCATE TABLE order_components;
TRUNCATE TABLE orders;

TRUNCATE TABLE component_specs;
TRUNCATE TABLE components;
TRUNCATE TABLE manufacturers;
TRUNCATE TABLE component_types;

TRUNCATE TABLE services;

TRUNCATE TABLE customers;
TRUNCATE TABLE employees;

TRUNCATE TABLE passports;
TRUNCATE TABLE addresses;
TRUNCATE TABLE positions;
SET FOREIGN_KEY_CHECKS = 1;

-- 1) positions
INSERT INTO positions (position_id, position_name, salary, duties, requirements) VALUES
(1, 'Менеджер по продажам', 65000.00, 'Консультации, оформление заказов', 'Коммуникабельность, аккуратность'),
(2, 'Инженер-сборщик',      80000.00, 'Сборка ПК, тестирование',         'Опыт сборки, внимательность');

-- 2) addresses
INSERT INTO addresses (address_id, country, region, city, street, house, apartment, postal_code, comment) VALUES
(1, 'Россия', 'Московская область', 'Москва', 'Тверская', '10', '12', '125009', NULL),
(2, 'Россия', 'Ленинградская область', 'Санкт-Петербург', 'Невский проспект', '20', '5', '191186', NULL),
(3, 'Россия', 'Республика Татарстан', 'Казань', 'Баумана', '7', '44', '420111', 'домофон 44'),
(4, 'Россия', 'Свердловская область', 'Екатеринбург', 'Ленина', '15', NULL, '620014', NULL);

-- 3) passports
INSERT INTO passports (passport_id, series, number, issued_by, issued_date, division_code) VALUES
(1, '4501', '123456', 'ОВД Тверского района', '2016-05-10', '770-001'),
(2, '4012', '987654', 'ОВД Центрального района', '2018-09-21', '780-002');

-- 4) employees
INSERT INTO employees (
  employee_id, second_name, first_name, middle_name, birth_date, gender,
  phone_number, position_id, passport_id, address_id
) VALUES
(1, 'Иванов', 'Пётр', 'Сергеевич', '1994-03-12', 'M', '+79990001122', 1, 1, 1),
(2, 'Смирнова', 'Анна', 'Игоревна', '1990-11-02', 'F', '+79990003344', 2, 2, 2);

-- 5) customers
INSERT INTO customers (
  customer_id, second_name, first_name, middle_name, phone_number, address_id
) VALUES
(1, 'Петров', 'Алексей', 'Николаевич', '+79995550011', 3),
(2, 'Кузнецова', 'Марина', 'Олеговна', '+79995550022', 4);

-- 6) services
INSERT INTO services (service_id, service_name, description, cost) VALUES
(1, 'Сборка ПК', 'Сборка системного блока с кабель-менеджментом', 3500.00),
(2, 'Установка ОС', 'Установка ОС и драйверов', 2000.00),
(3, 'Диагностика', 'Базовая диагностика и тесты', 1500.00);

-- 7) component_types
INSERT INTO component_types (type_id, type_name, description) VALUES
(1, 'Процессор', 'CPU для настольных ПК'),
(2, 'Материнская плата', 'Motherboard ATX/mATX'),
(3, 'Оперативная память', 'DDR4/DDR5'),
(4, 'SSD', 'Накопители SSD');

-- 8) manufacturers
INSERT INTO manufacturers (manufacturer_id, manufacturer_name, country) VALUES
(1, 'Intel', 'США'),
(2, 'ASUS',  'Тайвань'),
(3, 'Kingston', 'США'),
(4, 'Samsung', 'Южная Корея');

-- 9) components
INSERT INTO components (
  component_id, type_id, manufacturer_id, brand, release_date, warranty_months, description, price
) VALUES
(1, 1, 1, 'Core i5-12400F', '2022-01-01', 36, '6 ядер / 12 потоков', 14500.00),
(2, 2, 2, 'PRIME B660M-K D4', '2022-02-01', 24, 'mATX, LGA1700', 9800.00),
(3, 3, 3, 'FURY Beast 16GB DDR4 3200', '2021-06-01', 60, '1x16GB', 4200.00),
(4, 4, 4, '970 EVO Plus 1TB', '2020-03-01', 60, 'NVMe M.2', 8900.00);

-- 10) component_specs
INSERT INTO component_specs (spec_id, component_id, spec_name, spec_value) VALUES
(1, 1, 'Socket', 'LGA1700'),
(2, 1, 'Cores', '6'),
(3, 1, 'Threads', '12'),

(4, 2, 'Socket', 'LGA1700'),
(5, 2, 'FormFactor', 'mATX'),
(6, 2, 'Chipset', 'B660'),

(7, 3, 'Type', 'DDR4'),
(8, 3, 'Capacity', '16GB'),
(9, 3, 'Frequency', '3200MHz'),

(10, 4, 'Interface', 'NVMe'),
(11, 4, 'Capacity', '1TB'),
(12, 4, 'ReadSpeed', '3500MB/s');

-- 11) orders (шапка)
INSERT INTO orders (
  order_id, order_date, due_date, customer_id, employee_id,
  prepayment_share, payment_status, execution_status
) VALUES
(1, '2026-01-05', '2026-01-08', 1, 1, 30.00, 'partial', 'in_work'),
(2, '2026-01-06', '2026-01-10', 2, 1, 50.00, 'paid',    'done'),
(3, '2026-01-07', NULL,         1, 2, 0.00,  'unpaid',  'new');

-- 12) order_components (позиции комплектующих)
INSERT INTO order_components (
  order_component_id, order_id, component_id, quantity, unit_price, warranty_months
) VALUES
(1, 1, 1, 1, 14500.00, 36),
(2, 1, 2, 1, 9800.00,  24),
(3, 1, 3, 2, 4200.00,  60),

(4, 2, 4, 1, 8900.00,  60),
(5, 2, 3, 1, 4200.00,  60),

(6, 3, 2, 1, 9800.00,  24);

-- 13) order_services (позиции услуг)
INSERT INTO order_services (
  order_service_id, order_id, service_id, quantity, unit_cost
) VALUES
(1, 1, 1, 1, 3500.00),  -- сборка
(2, 1, 2, 1, 2000.00),  -- установка ОС

(3, 2, 3, 1, 1500.00),  -- диагностика
(4, 2, 1, 1, 3500.00);  -- сборка
