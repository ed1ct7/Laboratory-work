-- MySQL Workbench: всё, что связано с таблицей "Комплектующие"
-- (Виды комплектующих + Производители + Комплектующие + Характеристики)
-- Строгий вариант: 1НФ (характеристики отдельной таблицей) и 3НФ (страна у производителя)

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- Сначала удаляем зависимые таблицы
DROP TABLE IF EXISTS component_specs;
DROP TABLE IF EXISTS components;
DROP TABLE IF EXISTS manufacturers;
DROP TABLE IF EXISTS component_types;

SET FOREIGN_KEY_CHECKS = 1;

-- 3) Виды комплектующих
CREATE TABLE component_types (
  type_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  type_name VARCHAR(120) NOT NULL,
  description TEXT NULL,
  PRIMARY KEY (type_id),
  UNIQUE KEY uq_component_types_name (type_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Производители (чтобы не хранить страну в components и не нарушать 3НФ)
CREATE TABLE manufacturers (
  manufacturer_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  manufacturer_name VARCHAR(160) NOT NULL,
  country VARCHAR(120) NULL,
  PRIMARY KEY (manufacturer_id),
  UNIQUE KEY uq_manufacturers_name (manufacturer_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 4) Комплектующие
CREATE TABLE components (
  component_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  type_id INT UNSIGNED NOT NULL,               -- Код вида
  manufacturer_id INT UNSIGNED NOT NULL,       -- Фирма производитель

  brand VARCHAR(120) NOT NULL,                 -- Марка/модель
  release_date DATE NULL,                      -- Дата выпуска
  warranty_months INT UNSIGNED NULL,           -- Срок гарантии (в месяцах)
  description TEXT NULL,                       -- Описание
  price DECIMAL(12,2) NOT NULL DEFAULT 0.00,   -- Цена

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

-- Характеристики (строго 1НФ: параметр–значение)
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
