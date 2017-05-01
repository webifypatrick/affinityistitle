-- increase permissions for admin role to include new affinity staff and affinity manager permissions

update role set r_permission_bit = 127 where r_code = 'admin';


-- insert new Affinity Staff and Affinity Manager roles

insert into role (r_code, r_description, r_permission_bit) VALUES ('AffinityStaff', 'Affinity Staff', 39);
insert into role (r_code, r_description, r_permission_bit) VALUES ('AffinityManager', 'Affinity Manager', 71);

update system_setting set ss_data='008' where ss_code='VERSION';