﻿-- Add column to upload history to track the user that uploads the file 

insert into request_type (rt_code, rt_description, rt_export_formats, rt_definition, rt_is_active) VALUES ('Refinance', 'Refinance', 'PFT=Export.aspx?id={ID},PFT (Changes Only)=Export.aspx?id={ID}&format=change,Generic XML=Export.aspx?id={ID}&format=xml&key=ts_id,REI XML=Export.aspx?id={ID}&format=rei&key=ts_id', '', 1);

update system_setting set ss_data='019' where ss_code='VERSION';
