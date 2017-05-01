-- update permission bits for Admin, AffinityManager, AffinityStaff, and Attorney roles to include access to Attorney Services

insert role (r_code, r_description, r_permission_bit) values ('BrokerAgent', 'Broker Agent', 259);

update role set r_permission_bit = 511 where r_code = 'admin';
update role set r_permission_bit = 455 where r_code = 'AffinityManager';
update role set r_permission_bit = 423 where r_code = 'AffinityStaff';
update role set r_permission_bit = 387 where r_code = 'Attorney';



 update system_setting set ss_data='014' where ss_code='VERSION';