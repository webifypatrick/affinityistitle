-- Add export formats column to store export formats for each request type

ALTER TABLE `affinity`.`request_type` ADD COLUMN `rt_export_formats` 
VARCHAR(255) NOT NULL AFTER `rt_description`;

-- Set each request type to contain all available export formats

UPDATE `affinity`.`request_type` SET `rt_export_formats` = 'PFT=Export.aspx?id={ID},PFT (Changes Only)=Export.aspx?id={ID}&format=change,Generic XML=Export.aspx?id={ID}&format=xml&key=ts_id,REI XML=Export.aspx?id={ID}&format=rei&key=ts_id';

update system_setting set ss_data='012' where ss_code='VERSION';