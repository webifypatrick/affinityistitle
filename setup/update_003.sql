-- this update changes the underwriter code from ATG to ATGF
-- this operation requires updated several tables to preserve the relationship integrity

update underwriter set u_code = replace(u_code,'ATG','ATGF');
update account set a_underwriter_codes = replace(a_underwriter_codes,'ATG','ATGF');
update account set a_preferences_xml = replace(a_preferences_xml,'<field name="Underwriter">ATG</field>','<field name="Underwriter">ATGF</field>');
update request set r_xml = replace(r_xml,'<field name="Underwriter">ATG</field>','<field name="Underwriter">ATGF</field>');

update system_setting set ss_data='003' where ss_code='VERSION';