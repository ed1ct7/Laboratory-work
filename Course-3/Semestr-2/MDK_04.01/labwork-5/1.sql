-- MySQL 8.0+ (подходит для MySQL Workbench)
-- Таблицы, “покрывающие” Сотрудников: positions, passports, addresses, employees

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS employees;
DROP TABLE IF EXISTS passports;
DROP TABLE IF EXISTS addresses;
DROP TABLE IF EXISTS positions;

SET FOREIGN_KEY_CHECKS = 1;

-- 1) Должности
CREATE TABLE positions (
  position_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  position_name VARCHAR(100) NOT NULL,
  salary DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  duties TEXT NULL,
  requirements TEXT NULL,
  PRIMARY KEY (position_id),
  UNIQUE KEY uq_positions_name (position_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 2) Паспорта (как отдельная сущность)
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

-- 3) Адреса (если адрес хочешь хранить структурировано, а не одной строкой)
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

-- 4) Сотрудники
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

  -- если хочешь, чтобы один паспорт не мог быть привязан к двум сотрудникам:
  UNIQUE KEY uq_employees_passport (passport_id),

  -- если хочешь запретить одинаковые телефоны у сотрудников:
  UNIQUE KEY uq_employees_phone (phone_number),

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
