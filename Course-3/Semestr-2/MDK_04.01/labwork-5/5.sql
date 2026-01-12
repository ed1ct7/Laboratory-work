CREATE TABLE services (
  service_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  service_name VARCHAR(150) NOT NULL,
  description TEXT NULL,
  cost DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  PRIMARY KEY (service_id),
  UNIQUE KEY uq_services_name (service_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
