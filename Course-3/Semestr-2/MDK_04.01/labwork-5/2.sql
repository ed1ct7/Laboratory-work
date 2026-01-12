CREATE TABLE positions (
  position_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  position_name VARCHAR(120) NOT NULL,
  salary DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  PRIMARY KEY (position_id),
  UNIQUE KEY uq_positions_name (position_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE position_duties (
  duty_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  position_id INT UNSIGNED NOT NULL,
  duty_text VARCHAR(255) NOT NULL,
  PRIMARY KEY (duty_id),
  UNIQUE KEY uq_position_duty (position_id, duty_text),
  CONSTRAINT fk_duties_position
    FOREIGN KEY (position_id) REFERENCES positions(position_id)
    ON UPDATE CASCADE ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE position_requirements (
  req_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
  position_id INT UNSIGNED NOT NULL,
  requirement_text VARCHAR(255) NOT NULL,
  PRIMARY KEY (req_id),
  UNIQUE KEY uq_position_req (position_id, requirement_text),
  CONSTRAINT fk_requirements_position
    FOREIGN KEY (position_id) REFERENCES positions(position_id)
    ON UPDATE CASCADE ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
