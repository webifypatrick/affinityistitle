-- update 005 adds permission column for attachment purpose to restrict certain files
-- from being viewable by the user

ALTER TABLE `attachment_purpose`
ADD COLUMN `ap_permission_required` INTEGER UNSIGNED NOT NULL
AFTER `ap_change_status_to`;

update system_setting set ss_data='005' where ss_code='VERSION';