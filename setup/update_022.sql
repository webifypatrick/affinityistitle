-- Insert 2 new underwriters

insert into underwriter values ('STGC', 'Stewart Title Guaranty Company');
insert into underwriter values ('DIR-STGC', 'Stewart Title Guaranty Company (Direct)');


update system_setting set ss_data='022' where ss_code='VERSION';