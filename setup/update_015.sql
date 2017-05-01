-- Added Import_Log table to track file uploads

CREATE TABLE `affinity`.`upload_log` (
  `ul_id` INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  `a_id` INTEGER UNSIGNED NOT NULL,
  `o_id` INTEGER UNSIGNED NOT NULL,
  `r_id` INTEGER UNSIGNED NOT NULL,
  `att_id` INTEGER UNSIGNED NOT NULL,
  `ul_created` DATETIME NOT NULL,
  `ul_modified` DATETIME NOT NULL,
  PRIMARY KEY (`ul_id`)
)
ENGINE = InnoDB;

update system_setting set ss_data='015' where ss_code='VERSION';