-- update permission bits for Admin, AffinityManager, AffinityStaff, and Attorney roles to include access to Attorney Services

update role set r_permission_bit = 255 where r_code = 'admin';
update role set r_permission_bit = 199 where r_code = 'AffinityManager';
update role set r_permission_bit = 167 where r_code = 'AffinityStaff';
update role set r_permission_bit = 131 where r_code = 'Attorney';



 update system_setting set ss_data='010' where ss_code='VERSION';