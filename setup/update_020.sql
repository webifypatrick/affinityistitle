﻿-- Add column to upload history to track the user that uploads the file 

alter table `affinity`.`account` add column `a_signature` VARCHAR(50) NULL;


update system_setting set ss_data='020' where ss_code='VERSION';
