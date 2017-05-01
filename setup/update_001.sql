-- adds the content table to the schema for managing basic web content
-- adds additional pin and address line 2 to order
-- increases size of address fields for order
-- adds db schema version id

DROP TABLE IF EXISTS `affinity`.`content`;
CREATE TABLE  `affinity`.`content` (
  `ct_code` varchar(40) NOT NULL,
  `ct_meta_title` varchar(150) NOT NULL,
  `ct_meta_keywords` varchar(150) NOT NULL,
  `ct_meta_description` varchar(150) NOT NULL,
  `ct_header` varchar(150) NOT NULL,
  `ct_body` text NOT NULL,
  `ct_modified` datetime NOT NULL,
  PRIMARY KEY  (`ct_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `affinity`.`content` (`ct_code`,`ct_meta_title`,`ct_meta_keywords`,`ct_meta_description`,`ct_header`,`ct_body`,`ct_modified`) VALUES
 ('about','About Affinity','','','About Affinity','Affinity Title provides services for mortgage companies and real estate agents from its central processing center located in the Chicago suburb of Des Plains, IL. Affinity Title\'s closing specialists handle every aspect of the title process including title abstracts, issuing title commitments, preparing HUD/Settlement statements, scheduling real estate signings/closings, disbursement of funds and issuing title insurance policies.','2007-02-20 12:30:15'),
 ('contact','Contact Us','','','Contact Us','<p>Our Location:</p>\r\n<p>\r\n2454 E. Dempster Street Suite 401<br />\r\nDes Plaines, Illinois 60016<br />\r\nPhone (847) 296-9287<br />\r\nFax: (847) 296-7890<br />\r\n</p>\r\n\r\n<p>Sales please contact msolley@affinityistitle.com</p>','2007-02-20 12:36:51'),
 ('home','Welcome to Affinity Title Services','','','Welcome to Affinity Title Services','Affinity Title provides services for mortgage companies and real estate agents from its central processing center located in the Chicago suburb of Des Plains, IL. Affinity Title\'s closing specialists handle every aspect of the title process including title abstracts, issuing title commitments, preparing HUD/Settlement statements, scheduling real estate signings/closings, disbursement of funds and issuing title insurance policies.','2007-02-20 12:36:41'),
 ('news','Affinity News','','','Affinity News','<p>The Affinity Title Services site has officially been launched!</p>\r\n','2007-02-20 12:38:31');

ALTER TABLE `affinity`.`order` ADD COLUMN `o_additional_pins` TEXT NOT NULL AFTER `o_pin`;

ALTER TABLE `affinity`.`order` MODIFY COLUMN `o_property_address` VARCHAR(200) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
 MODIFY COLUMN `o_property_city` VARCHAR(75) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
 ADD COLUMN `o_property_address_2` VARCHAR(200) NOT NULL AFTER `o_property_address`;
 
insert into system_setting values ("VERSION","DB Schema Version","001");