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
