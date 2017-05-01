-- Add column to upload history to track the user that uploads the file 

alter table `affinity`.`upload_log` add column `ua_id` int(10) unsigned NOT NULL;
update `affinity`.`taxing_district` set `address` = '5225 Old Orchard Road Suite 27B' where `taxing_district` = 'Skokie'

update system_setting set ss_data='018' where ss_code='VERSION';