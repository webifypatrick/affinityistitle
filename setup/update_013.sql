-- Added Export_Log table to track exports of requests

CREATE TABLE `affinity`.`export_log` (
  `el_id` INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  `a_id` INTEGER UNSIGNED NOT NULL,
  `o_id` INTEGER UNSIGNED NOT NULL,
  `r_id` INTEGER UNSIGNED NOT NULL,
  `export_format` VARCHAR(45) NOT NULL,
  `el_created` DATETIME NOT NULL,
  `el_modified` DATETIME NOT NULL,
  PRIMARY KEY (`el_id`)
)
ENGINE = InnoDB;

update system_setting set ss_data='013' where ss_code='VERSION';