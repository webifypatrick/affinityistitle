-- Table used to store which roles have permission to view which attachment purposes

CREATE TABLE `attachment_roles` (
`r_code` varchar(15) NOT NULL,
`ap_code` varchar(15) NOT NULL,
PRIMARY KEY  (`r_code`, `ap_code`)
);

-- View used to relate the attachment purpose descriptions and role descriptions

CREATE VIEW attachment_roles_descriptions AS
SELECT ar.r_code, ar.ap_code, ap.ap_description, r.r_description
FROM attachment_roles ar
LEFT JOIN attachment_purpose ap ON (ar.ap_code = ap.ap_code)
LEFT JOIN role r ON (ar.r_code = r.r_code);


 update system_setting set ss_data='011' where ss_code='VERSION';