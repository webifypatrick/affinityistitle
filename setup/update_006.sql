-- adds "Incomplete" record when order has not yet been fully completed yet

insert into order_status values ('Pending','Order is Incomplete','0','0','1');

update system_setting set ss_data='006' where ss_code='VERSION';