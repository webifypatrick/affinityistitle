-- Increased size of the column rt_code 

alter table `affinity`.`attachment_roles` modify column `ap_code` varchar(50);
alter table `affinity`.`attachment_purpose` modify column `ap_code` varchar(50);

update system_setting set ss_data='016' where ss_code='VERSION';