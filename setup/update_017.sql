-- Increased size of the column rt_code 

alter table `affinity`.`attachment` modify column `att_purpose_code` varchar(50);

update system_setting set ss_data='017' where ss_code='VERSION';