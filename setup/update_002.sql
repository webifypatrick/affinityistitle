-- adds field used for underwriter assignment & sets defaults

ALTER TABLE `account` ADD COLUMN `a_underwriter_codes` VARCHAR(150) NOT NULL AFTER `a_email`;
update account set a_underwriter_codes = 'ATG,NLTIC';

update system_setting set ss_data='002' where ss_code='VERSION';