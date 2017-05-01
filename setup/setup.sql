-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.24a-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema affinity
--

CREATE DATABASE IF NOT EXISTS affinity;
USE affinity;

--
-- Definition of table `affinity`.`account`
--

DROP TABLE IF EXISTS `affinity`.`account`;
CREATE TABLE  `affinity`.`account` (
  `a_id` int(10) unsigned NOT NULL auto_increment,
  `a_username` varchar(75) NOT NULL,
  `a_password` varchar(250) NOT NULL,
  `a_first_name` varchar(45) NOT NULL,
  `a_last_name` varchar(45) NOT NULL,
  `a_status_code` varchar(15) NOT NULL,
  `a_created` datetime NOT NULL,
  `a_modified` datetime NOT NULL,
  `a_password_hint` varchar(100) NOT NULL,
  `a_preferences_xml` longtext NOT NULL,
  `a_role_code` varchar(15) NOT NULL,
  `a_company_id` int(10) unsigned NOT NULL,
  `a_internal_id` varchar(45) NOT NULL,
  `a_email` varchar(75) NOT NULL,
  PRIMARY KEY  (`a_id`),
  KEY `account_role` (`a_role_code`),
  KEY `account_company` (`a_company_id`),
  CONSTRAINT `account_company` FOREIGN KEY (`a_company_id`) REFERENCES `company` (`c_id`),
  CONSTRAINT `account_role` FOREIGN KEY (`a_role_code`) REFERENCES `role` (`r_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`account`
--

/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `affinity`.`account` (`a_id`,`a_username`,`a_password`,`a_first_name`,`a_last_name`,`a_status_code`,`a_created`,`a_modified`,`a_password_hint`,`a_preferences_xml`,`a_role_code`,`a_company_id`,`a_internal_id`,`a_email`) VALUES 
 (1,'admin','%ô!L˜™º“ùû_PàŠ','Affinity','Administrator','Active','2007-01-01 01:01:01','2007-02-20 01:49:02','None','<response><field name=\"EmailOnFilePost\">Yes</field><field name=\"EmailOnStatusChange\">Yes</field><field name=\"ShowBuyer\"></field><field name=\"ShowSeller\"></field><field name=\"ShowAdditional\"></field><field name=\"BuyersAttorneyName\"></field><field name=\"BuyersAttorneyAddress\"></field><field name=\"BuyersAttorneyCity\"></field><field name=\"BuyersAttorneyState\"></field><field name=\"BuyersAttorneyZip\"></field><field name=\"BuyersAttorneyPhone\"></field><field name=\"BuyersAttorneyFax\"></field><field name=\"BuyersAttorneyAttentionTo\"></field><field name=\"SellersAttorneyName\"></field><field name=\"SellersAttorneyAddress\"></field><field name=\"SellersAttorneyCity\"></field><field name=\"SellersAttorneyState\"></field><field name=\"SellersAttorneyZip\"></field><field name=\"SellersAttorneyPhone\"></field><field name=\"SellersAttorneyFax\"></field><field name=\"SellersAttorneyAttentionTo\"></field><field name=\"AdditionalCopyName\"></field><field name=\"AdditionalCopyAddress\"></field><field name=\"AdditionalCopyCity\"></field><field name=\"AdditionalCopyState\"></field><field name=\"AdditionalCopyZip\"></field><field name=\"AdditionalCopyPhone\"></field><field name=\"AdditionalCopyFax\"></field><field name=\"AdditionalCopyAttentionTo\"></field><field name=\"ApplicantName\">Guy Shoo</field><field name=\"ApplicantAddress\">Affinity Title Services</field><field name=\"ApplicantCity\">Des Plaines</field><field name=\"ApplicantState\">IL</field><field name=\"ApplicantZip\"></field><field name=\"ApplicantPhone\"></field><field name=\"ApplicantFax\"></field><field name=\"ApplicantAttentionTo\"></field></response>','Admin',1,'','gschoo@gmail.com'),
 (2,'jason','’Pa²g©næd½)à2','Jason','Hinkle','Active','2007-02-14 02:33:23','2007-02-18 18:03:09','password hint','<response><field name=\"EmailOnFilePost\">Yes</field><field name=\"EmailOnStatusChange\">Yes</field><field name=\"ShowBuyer\"></field><field name=\"ShowSeller\"></field><field name=\"ShowAdditional\"></field><field name=\"BuyersAttorneyName\"></field><field name=\"BuyersAttorneyAddress\"></field><field name=\"BuyersAttorneyCity\"></field><field name=\"BuyersAttorneyState\"></field><field name=\"BuyersAttorneyZip\"></field><field name=\"BuyersAttorneyPhone\"></field><field name=\"BuyersAttorneyFax\"></field><field name=\"BuyersAttorneyAttentionTo\"></field><field name=\"SellersAttorneyName\"></field><field name=\"SellersAttorneyAddress\"></field><field name=\"SellersAttorneyCity\"></field><field name=\"SellersAttorneyState\"></field><field name=\"SellersAttorneyZip\"></field><field name=\"SellersAttorneyPhone\"></field><field name=\"SellersAttorneyFax\"></field><field name=\"SellersAttorneyAttentionTo\"></field><field name=\"AdditionalCopyName\"></field><field name=\"AdditionalCopyAddress\"></field><field name=\"AdditionalCopyCity\"></field><field name=\"AdditionalCopyState\"></field><field name=\"AdditionalCopyZip\"></field><field name=\"AdditionalCopyPhone\"></field><field name=\"AdditionalCopyFax\"></field><field name=\"AdditionalCopyAttentionTo\"></field><field name=\"ApplicantName\">Jason Hinkle</field><field name=\"ApplicantAddress\">Originator Address</field><field name=\"ApplicantCity\">City</field><field name=\"ApplicantState\">ST</field><field name=\"ApplicantZip\">60606</field><field name=\"ApplicantPhone\">123-456-7890</field><field name=\"ApplicantFax\">123-456-7890</field><field name=\"ApplicantAttentionTo\">Jason Hinkle ATTN</field></response>','Attorney',1,'','jason@verysimple.com');
INSERT INTO `affinity`.`account` (`a_id`,`a_username`,`a_password`,`a_first_name`,`a_last_name`,`a_status_code`,`a_created`,`a_modified`,`a_password_hint`,`a_preferences_xml`,`a_role_code`,`a_company_id`,`a_internal_id`,`a_email`) VALUES 
 (3,'maranatha','ô¼Èà|¼’&Ëøt˜¯','Maranatha','Poirier','Active','2007-02-18 18:01:16','2007-02-18 18:02:54','','<response><field name=\"EmailOnFilePost\">Yes</field><field name=\"EmailOnStatusChange\">Yes</field><field name=\"ShowBuyer\"></field><field name=\"ShowSeller\"></field><field name=\"ShowAdditional\"></field><field name=\"BuyersAttorneyName\"></field><field name=\"BuyersAttorneyAddress\"></field><field name=\"BuyersAttorneyCity\"></field><field name=\"BuyersAttorneyState\"></field><field name=\"BuyersAttorneyZip\"></field><field name=\"BuyersAttorneyPhone\"></field><field name=\"BuyersAttorneyFax\"></field><field name=\"BuyersAttorneyAttentionTo\"></field><field name=\"SellersAttorneyName\"></field><field name=\"SellersAttorneyAddress\"></field><field name=\"SellersAttorneyCity\"></field><field name=\"SellersAttorneyState\"></field><field name=\"SellersAttorneyZip\"></field><field name=\"SellersAttorneyPhone\"></field><field name=\"SellersAttorneyFax\"></field><field name=\"SellersAttorneyAttentionTo\"></field><field name=\"AdditionalCopyName\"></field><field name=\"AdditionalCopyAddress\"></field><field name=\"AdditionalCopyCity\"></field><field name=\"AdditionalCopyState\"></field><field name=\"AdditionalCopyZip\"></field><field name=\"AdditionalCopyPhone\"></field><field name=\"AdditionalCopyFax\"></field><field name=\"AdditionalCopyAttentionTo\"></field><field name=\"ApplicantName\"></field><field name=\"ApplicantAddress\"></field><field name=\"ApplicantCity\"></field><field name=\"ApplicantState\"></field><field name=\"ApplicantZip\"></field><field name=\"ApplicantPhone\"></field><field name=\"ApplicantFax\"></field><field name=\"ApplicantAttentionTo\"></field></response>','Attorney',1,'','maranatha@aspir.com');
/*!40000 ALTER TABLE `account` ENABLE KEYS */;


--
-- Definition of table `affinity`.`attachment`
--

DROP TABLE IF EXISTS `affinity`.`attachment`;
CREATE TABLE  `affinity`.`attachment` (
  `att_id` int(10) unsigned NOT NULL auto_increment,
  `att_request_id` int(10) unsigned NOT NULL,
  `att_name` varchar(250) NOT NULL,
  `att_mime_type` varchar(35) NOT NULL,
  `att_size_kb` int(10) unsigned NOT NULL,
  `att_created` datetime NOT NULL,
  `att_filepath` varchar(100) NOT NULL,
  `att_purpose_code` varchar(15) NOT NULL,
  PRIMARY KEY  (`att_id`),
  KEY `attachment_request` (`att_request_id`),
  KEY `attachment_purpose` (`att_purpose_code`),
  CONSTRAINT `attachment_purpose` FOREIGN KEY (`att_purpose_code`) REFERENCES `attachment_purpose` (`ap_code`),
  CONSTRAINT `attachment_request` FOREIGN KEY (`att_request_id`) REFERENCES `request` (`r_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`attachment`
--

/*!40000 ALTER TABLE `attachment` DISABLE KEYS */;
/*!40000 ALTER TABLE `attachment` ENABLE KEYS */;


--
-- Definition of table `affinity`.`attachment_purpose`
--

DROP TABLE IF EXISTS `affinity`.`attachment_purpose`;
CREATE TABLE  `affinity`.`attachment_purpose` (
  `ap_code` varchar(15) NOT NULL,
  `ap_description` varchar(100) NOT NULL,
  `ap_send_notification` tinyint(3) unsigned NOT NULL,
  `ap_change_status_to` varchar(15) NOT NULL,
  PRIMARY KEY  (`ap_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`attachment_purpose`
--

/*!40000 ALTER TABLE `attachment_purpose` DISABLE KEYS */;
INSERT INTO `affinity`.`attachment_purpose` (`ap_code`,`ap_description`,`ap_send_notification`,`ap_change_status_to`) VALUES 
 ('Committment','Commitment Package',1,'0'),
 ('ExamSheet','Exam Sheet',0,'0'),
 ('Invoice','Invoice',0,'0'),
 ('SearchPkg','Search Package',1,'0');
/*!40000 ALTER TABLE `attachment_purpose` ENABLE KEYS */;


--
-- Definition of table `affinity`.`company`
--

DROP TABLE IF EXISTS `affinity`.`company`;
CREATE TABLE  `affinity`.`company` (
  `c_id` int(10) unsigned NOT NULL auto_increment,
  `c_name` varchar(100) NOT NULL,
  `c_created` datetime NOT NULL,
  PRIMARY KEY  (`c_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`company`
--

/*!40000 ALTER TABLE `company` DISABLE KEYS */;
INSERT INTO `affinity`.`company` (`c_id`,`c_name`,`c_created`) VALUES 
 (1,'Affinity Title Services, LLC','2007-01-01 01:01:01');
/*!40000 ALTER TABLE `company` ENABLE KEYS */;


--
-- Definition of table `affinity`.`location`
--

DROP TABLE IF EXISTS `affinity`.`location`;
CREATE TABLE  `affinity`.`location` (
  `l_code` varchar(15) NOT NULL,
  `l_description` varchar(100) NOT NULL,
  PRIMARY KEY  (`l_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`location`
--

/*!40000 ALTER TABLE `location` DISABLE KEYS */;
/*!40000 ALTER TABLE `location` ENABLE KEYS */;


--
-- Definition of table `affinity`.`order`
--

DROP TABLE IF EXISTS `affinity`.`order`;
CREATE TABLE  `affinity`.`order` (
  `o_id` int(10) unsigned NOT NULL auto_increment,
  `o_internal_id` varchar(45) NOT NULL,
  `o_customer_id` varchar(45) NOT NULL,
  `o_pin` varchar(45) NOT NULL,
  `o_property_address` varchar(45) NOT NULL,
  `o_property_city` varchar(45) NOT NULL,
  `o_property_state` varchar(2) NOT NULL,
  `o_property_zip` varchar(10) NOT NULL,
  `o_property_county` varchar(45) NOT NULL,
  `o_internal_status_code` varchar(15) NOT NULL,
  `o_customer_status_code` varchar(15) NOT NULL,
  `o_originator_id` int(10) unsigned NOT NULL,
  `o_created` datetime NOT NULL,
  `o_modified` datetime NOT NULL,
  `o_closing_date` datetime NOT NULL,
  `o_client_name` varchar(100) NOT NULL,
  PRIMARY KEY  (`o_id`),
  KEY `internal_status` (`o_internal_status_code`),
  KEY `customer_status` (`o_customer_status_code`),
  KEY `account_order` (`o_originator_id`),
  CONSTRAINT `account_order` FOREIGN KEY (`o_originator_id`) REFERENCES `account` (`a_id`),
  CONSTRAINT `customer_status` FOREIGN KEY (`o_customer_status_code`) REFERENCES `order_status` (`os_code`),
  CONSTRAINT `internal_status` FOREIGN KEY (`o_internal_status_code`) REFERENCES `order_status` (`os_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`order`
--

/*!40000 ALTER TABLE `order` DISABLE KEYS */;
/*!40000 ALTER TABLE `order` ENABLE KEYS */;


--
-- Definition of table `affinity`.`order_assignment`
--

DROP TABLE IF EXISTS `affinity`.`order_assignment`;
CREATE TABLE  `affinity`.`order_assignment` (
  `oa_account_id` int(10) unsigned NOT NULL,
  `oa_order_id` int(10) unsigned NOT NULL,
  `oa_permission_bit` varchar(45) NOT NULL,
  PRIMARY KEY  (`oa_account_id`,`oa_order_id`),
  KEY `assigned_account` (`oa_order_id`),
  CONSTRAINT `assigned_account` FOREIGN KEY (`oa_order_id`) REFERENCES `order` (`o_id`),
  CONSTRAINT `assigned_order` FOREIGN KEY (`oa_account_id`) REFERENCES `account` (`a_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`order_assignment`
--

/*!40000 ALTER TABLE `order_assignment` DISABLE KEYS */;
/*!40000 ALTER TABLE `order_assignment` ENABLE KEYS */;


--
-- Definition of table `affinity`.`order_status`
--

DROP TABLE IF EXISTS `affinity`.`order_status`;
CREATE TABLE  `affinity`.`order_status` (
  `os_code` varchar(15) NOT NULL,
  `os_description` varchar(100) NOT NULL,
  `os_permission_bit` int(10) unsigned NOT NULL,
  `os_internal_external` tinyint(3) unsigned NOT NULL,
  `os_is_closed` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY  (`os_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`order_status`
--

/*!40000 ALTER TABLE `order_status` DISABLE KEYS */;
INSERT INTO `affinity`.`order_status` (`os_code`,`os_description`,`os_permission_bit`,`os_internal_external`,`os_is_closed`) VALUES 
 ('Changed','Change Requested From Customer',2,1,0),
 ('Closed','Order is Closed',0,1,1),
 ('InProgress','Requests are Being Processed',2,1,0),
 ('New','New Order From Customer',2,1,0),
 ('Ready','All Requests are Processed',1,1,0);
/*!40000 ALTER TABLE `order_status` ENABLE KEYS */;


--
-- Definition of table `affinity`.`request`
--

DROP TABLE IF EXISTS `affinity`.`request`;
CREATE TABLE  `affinity`.`request` (
  `r_id` int(10) unsigned NOT NULL auto_increment,
  `r_request_type_code` varchar(15) NOT NULL,
  `r_order_id` int(10) unsigned NOT NULL,
  `r_originator_id` int(10) unsigned NOT NULL,
  `r_created` datetime NOT NULL,
  `r_status_code` varchar(15) NOT NULL,
  `r_xml` longtext NOT NULL,
  `r_is_current` tinyint(3) unsigned NOT NULL default '1',
  `r_note` varchar(250) NOT NULL,
  PRIMARY KEY  (`r_id`),
  KEY `status_request` (`r_status_code`),
  KEY `type_request` (`r_request_type_code`),
  KEY `order_request` (`r_order_id`),
  KEY `originator` (`r_originator_id`),
  CONSTRAINT `order_request` FOREIGN KEY (`r_order_id`) REFERENCES `order` (`o_id`),
  CONSTRAINT `originator` FOREIGN KEY (`r_originator_id`) REFERENCES `account` (`a_id`),
  CONSTRAINT `status_request` FOREIGN KEY (`r_status_code`) REFERENCES `request_status` (`rs_code`),
  CONSTRAINT `type_request` FOREIGN KEY (`r_request_type_code`) REFERENCES `request_type` (`rt_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`request`
--

/*!40000 ALTER TABLE `request` DISABLE KEYS */;
/*!40000 ALTER TABLE `request` ENABLE KEYS */;


--
-- Definition of table `affinity`.`request_status`
--

DROP TABLE IF EXISTS `affinity`.`request_status`;
CREATE TABLE  `affinity`.`request_status` (
  `rs_code` varchar(15) NOT NULL,
  `rs_description` varchar(100) NOT NULL,
  `rs_permission_bit` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`rs_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`request_status`
--

/*!40000 ALTER TABLE `request_status` DISABLE KEYS */;
INSERT INTO `affinity`.`request_status` (`rs_code`,`rs_description`,`rs_permission_bit`) VALUES 
 ('Changed','Change Requested by Customer',2),
 ('Committment','Commitment is Posted',1),
 ('Complete','Request is Complete',1),
 ('ExamComplete','Exam Sheet is Submitted',2),
 ('ExamRequied','Exam Sheet is Required',1),
 ('InProgress','Request is In Progress',2),
 ('New','New Submission From Customer',2),
 ('SearchPackage','Search Package Has Posted',1);
/*!40000 ALTER TABLE `request_status` ENABLE KEYS */;


--
-- Definition of table `affinity`.`request_type`
--

DROP TABLE IF EXISTS `affinity`.`request_type`;
CREATE TABLE  `affinity`.`request_type` (
  `rt_code` varchar(15) NOT NULL,
  `rt_description` varchar(250) NOT NULL,
  `rt_definition` longtext NOT NULL,
  `rt_is_active` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY  (`rt_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`request_type`
--

/*!40000 ALTER TABLE `request_type` DISABLE KEYS */;
INSERT INTO `affinity`.`request_type` (`rt_code`,`rt_description`,`rt_definition`,`rt_is_active`) VALUES 
 ('ClosingRequest','Closing Schedule Request','<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<request>\r\n  <group name=\"general\" legend=\"Schedule Information\">\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"1stChoiceLocation\" label=\"1st Choice: Location\"  input=\"select\" class=\"horizontal\" labelclass=\"label\">\r\n        <option value=\"NONE\" label=\"Select One...\" />\r\n        <option value=\"Affinity Des Plaines\" />\r\n        <option value=\"Affinity Downtown Chicago\" />\r\n      </field>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"1stChoiceDate\" label=\"Date\" input=\"date\" class=\"horizontal\" labelclass=\"label\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"1stChoiceTime\" label=\"Time\"  input=\"select\" class=\"horizontal\" labelclass=\"label\">\r\n        <option value=\"8:00 AM\" />\r\n        <option value=\"9:00 AM\" />\r\n        <option value=\"10:00 AM\" />\r\n        <option value=\"11:00 AM\" />\r\n        <option value=\"12:00 PM\" />\r\n        <option value=\"1:00 PM\" />\r\n        <option value=\"2:00 PM\" />\r\n        <option value=\"3:00 PM\" />\r\n        <option value=\"4:00 PM\" />\r\n        <option value=\"5:00 PM\" />\r\n        <option value=\"6:00 PM\" />\r\n        <option value=\"7:00 PM\" />\r\n        <option value=\"8:00 PM\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"2ndChoiceLocation\" label=\"2nd Choice: Location\"  input=\"select\" class=\"horizontal\" labelclass=\"label\">\r\n        <option value=\"NONE\" label=\"Select One...\" />\r\n        <option value=\"Affinity Des Plaines\" />\r\n        <option value=\"Affinity Downtown Chicago\" />\r\n      </field>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"2ndChoiceDate\" label=\"Date\" input=\"date\" class=\"horizontal\" labelclass=\"label\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"2ndChoiceTime\" label=\"Time\"  input=\"select\" class=\"horizontal\" labelclass=\"label\">\r\n        <option value=\"8:00 AM\" />\r\n        <option value=\"9:00 AM\" />\r\n        <option value=\"10:00 AM\" />\r\n        <option value=\"11:00 AM\" />\r\n        <option value=\"12:00 PM\" />\r\n        <option value=\"1:00 PM\" />\r\n        <option value=\"2:00 PM\" />\r\n        <option value=\"3:00 PM\" />\r\n        <option value=\"4:00 PM\" />\r\n        <option value=\"5:00 PM\" />\r\n        <option value=\"6:00 PM\" />\r\n        <option value=\"7:00 PM\" />\r\n        <option value=\"8:00 PM\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"3rdChoiceLocation\" label=\"3rd Choice: Location\"  input=\"select\" class=\"horizontal\" labelclass=\"label\">\r\n        <option value=\"NONE\" label=\"Select One...\" />\r\n        <option value=\"Affinity Des Plaines\" />\r\n        <option value=\"Affinity Downtown Chicago\" />\r\n      </field>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"3rdChoiceDate\" label=\"Date\" input=\"date\" class=\"horizontal\" labelclass=\"label\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"3rdChoiceTime\" label=\"Time\"  input=\"select\" class=\"horizontal\" labelclass=\"label\">\r\n        <option value=\"8:00 AM\" />\r\n        <option value=\"9:00 AM\" />\r\n        <option value=\"10:00 AM\" />\r\n        <option value=\"11:00 AM\" />\r\n        <option value=\"12:00 PM\" />\r\n        <option value=\"1:00 PM\" />\r\n        <option value=\"2:00 PM\" />\r\n        <option value=\"3:00 PM\" />\r\n        <option value=\"4:00 PM\" />\r\n        <option value=\"5:00 PM\" />\r\n        <option value=\"6:00 PM\" />\r\n        <option value=\"7:00 PM\" />\r\n        <option value=\"8:00 PM\" />\r\n      </field>\r\n    </line>\r\n  </group>\r\n</request>',1),
 ('Order','Order','<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<request>\r\n  <group name=\"general\" legend=\"General Information\">\r\n    <line>\r\n      <field sp_id=\"CMTDL\" rei_id=\"\" name=\"CommittmentDeadline\" label=\"Commitment Needed By:\" input=\"date\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"SS1\" rei_id=\"\" name=\"SurveyServices\" label=\"Survey Services to Order Plat of Survey:\" input=\"radio\" class=\"horizontal\" repeat=\"horizontal\" default=\"No\">\r\n        <option label=\"Yes\" value=\"Yes\">\r\n          <attribute name=\"onclick\">setVisibility(\'line_PlatOfSurvey\',false)</attribute>\r\n        </option>\r\n        <option label=\"No\" value=\"No\">\r\n          <attribute name=\"onclick\">setVisibility(\'line_PlatOfSurvey\',true)</attribute>\r\n        </option>\r\n      </field>\r\n    </line>\r\n    <line name=\"line_PlatOfSurvey\" hidden=\"SurveyServices=Yes\">\r\n      <field sp_id=\"\" rei_id=\"\" name=\"PlatOfSurvey\" label=\"Please specify Plat of Survey required:\" input=\"checkbox\" class=\"horizontal\" repeat=\"horizontal\">\r\n        <option label=\"Standard\" value=\"Standard\" />\r\n        <option label=\"Staked\" value=\"Staked\" />\r\n        <option label=\"ALTA\" value=\"ALTA\" />\r\n        <option label=\"Tolpo\" value=\"Tolpo\" />\r\n        <option label=\"Other\" value=\"Other\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"PreviousTitleEvidence\" label=\"Does Previous Title Evidence Exist?\" tip=\"If Yes, please fax documentation to Affinity\" default=\"No\"  input=\"radio\" class=\"horizontal\" repeat=\"horizontal\">\r\n        <option label=\"Yes\" value=\"Yes\" />\r\n        <option label=\"No\" value=\"No\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"TransactionType\" label=\"Transaction Type:\" input=\"checkbox\" class=\"horizontal\" repeat=\"horizontal\">\r\n        <option value=\"Purchase\" />\r\n        <option value=\"Cash Purchase\" />\r\n        <option value=\"Refinance\" />\r\n        <option value=\"Tract Search\" />\r\n        <option value=\"Construction Escrow\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"Endorsements\" label=\"Endorsements:\" input=\"checkbox\" class=\"horizontal\" repeat=\"horizontal\">\r\n        <option value=\"EPA\" />\r\n        <option value=\"Location\" />\r\n        <option value=\"Condo\" />\r\n        <option value=\"ARM\" />\r\n        <option value=\"PUD\" />\r\n        <option value=\"Balloon\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"TypeOfProperty\" label=\"Type of Property:\" input=\"checkbox\" class=\"horizontal\" repeat=\"horizontal\">\r\n        <option value=\"Existing\" />\r\n        <option value=\"New Construction\" />\r\n        <option value=\"Vacant Land\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"Containing\" label=\"Containing:\" input=\"checkbox\" class=\"horizontal\" repeat=\"horizontal\">\r\n        <option value=\"Single Family\" />\r\n        <option value=\"Condo\" />\r\n        <option value=\"Multi-Family\" />\r\n        <option value=\"Commercial\" />\r\n        <option value=\"Townhouse\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"PurchasePrice\" label=\"Purchase Price $\" input=\"text\" width=\"150\" class=\"horizontal\" labelclass=\"label width_125\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"MortgageAmount\" label=\"Mortgage Amount $\" input=\"text\" width=\"150\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SecondMortgage\" label=\"Is There a 2nd Mortgage?\" input=\"radio\" class=\"horizontal\" labelclass=\"label\" repeat=\"horizontal\" default=\"No\">\r\n        <option label=\"Yes\" value=\"Yes\">\r\n          <attribute name=\"onclick\">setVisibility(\'outer_SecondMortgageAmount\',false)</attribute>\r\n        </option>\r\n        <option label=\"No\" value=\"No\">\r\n          <attribute name=\"onclick\">setVisibility(\'outer_SecondMortgageAmount\',true)</attribute>\r\n        </option>\r\n      </field>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SecondMortgageAmount\" label=\"2nd Mortgage Amount $\" hidden=\"SecondMortgage=Yes\"  input=\"text\" width=\"150\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"Seller\" label=\"Seller\" input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_125\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"Buyer\" label=\"Buyer/Borrower\" input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_125\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"LenderName\" label=\"Lender\" input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_125\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"LenderContact\" label=\"Lender Contact\" input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_125\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"LenderAddress\" label=\"Lender Address\" input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_125\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"LenderPhone\" label=\"Lender Phone #\" input=\"text\" class=\"horizontal\" labelclass=\"label width_125\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"LenderFax\" label=\"Fax #\" input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BrokerName\" label=\"Mrtg. Broker\" input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_125\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BrokerPhone\" label=\"Mrtg. Broker Phone #\" input=\"text\" class=\"horizontal\" labelclass=\"label width_125\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BrokerFax\" label=\"Fax #\" input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"Underwriter\" label=\"Underwriter\" input=\"select\" optioncallback=\"UNDERWRITER\"  class=\"horizontal\" labelclass=\"label width_125\" />\r\n    </line>\r\n  </group>\r\n  <group name=\"applicant\" legend=\"Originator\">\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ApplicantName\" label=\"Name\"  input=\"text\" width=\"450\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ApplicantAddress\" label=\"Address\" width=\"450\"  input=\"text\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ApplicantCity\" label=\"City\" input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ApplicantState\" label=\"State\" width=\"30\"  input=\"text\" class=\"horizontal\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ApplicantZip\" label=\"Zip\" width=\"75\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ApplicantPhone\" label=\"Phone\"  input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ApplicantFax\" label=\"Fax\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ApplicantAttentionTo\" label=\"Attention\"  input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n    </line>\r\n\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"CopyApplicationTo\" label=\"Use this same address for\"  input=\"checkbox\" class=\"horizontal\" labelclass=\"label\" repeat=\"horizontal\">\r\n        <option value=\"Buyers Attorney\">\r\n          <attribute name=\"onclick\">copyAddress(this, \'Applicant\',\'BuyersAttorney\')</attribute>\r\n        </option>\r\n        <option value=\"Sellers Attorney\">\r\n          <attribute name=\"onclick\">copyAddress(this, \'Applicant\',\'SellersAttorney\')</attribute>\r\n        </option>\r\n        <option value=\"Additional Copy To\">\r\n          <attribute name=\"onclick\">copyAddress(this, \'Applicant\',\'AdditionalCopy\')</attribute>\r\n        </option>\r\n      </field>\r\n    </line>\r\n  </group>\r\n  <group name=\"buyersattorney\" legend=\"Buyers Attorney\">\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BuyersAttorneyName\" label=\"Name\"  input=\"text\" width=\"450\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BuyersAttorneyAddress\" label=\"Address\" width=\"450\"  input=\"text\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BuyersAttorneyCity\" label=\"City\" input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BuyersAttorneyState\" label=\"State\" width=\"30\"  input=\"text\" class=\"horizontal\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BuyersAttorneyZip\" label=\"Zip\" width=\"75\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BuyersAttorneyPhone\" label=\"Phone\"  input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BuyersAttorneyFax\" label=\"Fax\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"BuyersAttorneyAttentionTo\" label=\"Attention\"  input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n    </line>\r\n  </group>\r\n  <group name=\"sellersattorney\" legend=\"Sellers Attorney\">\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SellersAttorneyName\" label=\"Name\"  input=\"text\" width=\"450\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SellersAttorneyAddress\" label=\"Address\" width=\"450\"  input=\"text\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SellersAttorneyCity\" label=\"City\" input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SellersAttorneyState\" label=\"State\" width=\"30\"  input=\"text\" class=\"horizontal\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SellersAttorneyZip\" label=\"Zip\" width=\"75\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SellersAttorneyPhone\" label=\"Phone\"  input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SellersAttorneyFax\" label=\"Fax\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"SellersAttorneyAttentionTo\" label=\"Attention\"  input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n    </line>\r\n  </group>\r\n  <group name=\"additionalcopiesto\" legend=\"Additional Copies To\">\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"AdditionalCopyName\" label=\"Name\"  input=\"text\" width=\"450\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"AdditionalCopyAddress\" label=\"Address\" width=\"450\"  input=\"text\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"AdditionalCopyCity\" label=\"City\" input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"AdditionalCopyState\" label=\"State\" width=\"30\"  input=\"text\" class=\"horizontal\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"AdditionalCopyZip\" label=\"Zip\" width=\"75\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"AdditionalCopyPhone\" label=\"Phone\"  input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"AdditionalCopyFax\" label=\"Fax\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"AdditionalCopyAttentionTo\" label=\"Attention\"  input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n    </line>\r\n  </group>\r\n</request>',1);
INSERT INTO `affinity`.`request_type` (`rt_code`,`rt_description`,`rt_definition`,`rt_is_active`) VALUES 
 ('PropertyChange','Change Property Information','<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<request>\r\n  <group name=\"general\" legend=\"Property Information\">\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ClientName\" label=\"Client Name:\" input=\"text\" class=\"horizontal\" labelclass=\"label width_175\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"CustomerId\" label=\"Tracking Code:\" input=\"text\" class=\"horizontal\" width=\"150\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"PIN\" label=\"Tax ID (PIN)\" input=\"text\" class=\"horizontal\" labelclass=\"label width_175\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"PropertyCounty\" label=\"County\" input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"PropertyAddress\" label=\"Property Address\" input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_175\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"PropertyCity\" label=\"City\" input=\"text\" class=\"horizontal\" labelclass=\"label width_175\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"PropertyState\" label=\"State\" width=\"30\"  input=\"text\" class=\"horizontal\" />\r\n      <field sp_id=\"\" rei_id=\"\" name=\"PropertyZip\" label=\"Zip\" width=\"75\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"ClosingDate\" label=\"Estimated Closing Date:\" input=\"date\" class=\"horizontal\" labelclass=\"label width_175\" />\r\n    </line>\r\n  </group>\r\n</request>',0),
 ('SystemSettings','System Settings','<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<system>\r\n\r\n  <group name=\"path\" legend=\"File Path and URL Settings\">\r\n    <line>\r\n      <field name=\"RootUrl\" input=\"textbox\" class=\"horizontal\" labelclass=\"label width_125\"></field>\r\n    </line>\r\n    <line>\r\n      <field name=\"RootUrlSSL\" input=\"textbox\" class=\"horizontal\" labelclass=\"label width_125\"></field>\r\n    </line>\r\n  </group>\r\n  \r\n  <group name=\"email\" legend=\"Email Settings\">\r\n    <line>\r\n      <field name=\"SmtpHost\" input=\"textbox\" class=\"horizontal\" labelclass=\"label width_125\"></field>\r\n    </line>\r\n    <line>\r\n      <field name=\"SendFromEmail\" input=\"textbox\" class=\"horizontal\" labelclass=\"label width_125\"></field>\r\n    </line>\r\n    <line>\r\n      <field name=\"SendFromRealName\" input=\"textbox\" class=\"horizontal\" labelclass=\"label width_125\"></field>\r\n    </line>\r\n    <line>\r\n      <field name=\"EmailFooter\" input=\"textarea\" class=\"horizontal\" width=\"450\"  labelclass=\"label width_125\"></field>\r\n    </line>\r\n  </group>\r\n\r\n  <group name=\"path\" legend=\"Notifications\">\r\n    <line>\r\n      <field name=\"NewOrderEmail\" input=\"textbox\" class=\"horizontal\" labelclass=\"label width_125\"></field>\r\n    </line>\r\n    <line>\r\n      <field name=\"ClosingRequestEmail\" input=\"textbox\" class=\"horizontal\" labelclass=\"label width_125\"></field>\r\n    </line>\r\n  </group>\r\n  \r\n</system>',0);
INSERT INTO `affinity`.`request_type` (`rt_code`,`rt_description`,`rt_definition`,`rt_is_active`) VALUES 
 ('UserPreferences','User Preferences','<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<request>\r\n  <group name=\"email\" legend=\"Email Preferences\">\r\n    <line>\r\n      <field name=\"EmailOnFilePost\" label=\"Notify me by email when a new file is posted\"  input=\"radio\" labelclass=\"label\" class=\"horizontal\" default=\"Yes\" repeat=\"horizontal\">\r\n        <option value=\"Yes\" />\r\n        <option value=\"No\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field name=\"EmailOnStatusChange\" label=\"Notify me by email when an order status is updated\"  input=\"radio\" labelclass=\"label\" class=\"horizontal\" default=\"Yes\" repeat=\"horizontal\">\r\n        <option value=\"Yes\" />\r\n        <option value=\"No\" />\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field name=\"ShowBuyer\" label=\"Specify a Default Address for Buyer Attorney\"  input=\"checkbox\" class=\"horizontal\" labelclass=\"label\">\r\n        <option value=\"Yes\">\r\n          <attribute name=\"onclick\">setVisibility(\'group_buyersattorney\',!this.checked);setVisibility(\'group_buyersattorney_header\',!this.checked)</attribute>\r\n        </option>\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field name=\"ShowSeller\" label=\"Specify a Default Address for Seller Attorney\"  input=\"checkbox\" class=\"horizontal\" labelclass=\"label\">\r\n        <option value=\"Yes\">\r\n          <attribute name=\"onclick\">setVisibility(\'group_sellersattorney\',!this.checked);setVisibility(\'group_sellersattorney_header\',!this.checked)</attribute>\r\n        </option>\r\n      </field>\r\n    </line>\r\n    <line>\r\n      <field name=\"ShowAdditional\" label=\"Specify a Default Address for Additional Copies\"  input=\"checkbox\" class=\"horizontal\" labelclass=\"label\">\r\n        <option value=\"Yes\">\r\n          <attribute name=\"onclick\">setVisibility(\'group_additionalcopiesto\',!this.checked);setVisibility(\'group_additionalcopiesto_header\',!this.checked)</attribute>\r\n        </option>\r\n      </field>\r\n    </line>\r\n  </group>\r\n\r\n  <group name=\"buyersattorney\" hidden=\"ShowBuyer=Yes\" legend=\"Default Buyers Attorney Information\">\r\n    <line>\r\n      <field name=\"BuyersAttorneyName\" label=\"Name\"  input=\"text\" width=\"450\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"BuyersAttorneyAddress\" label=\"Address\" width=\"450\"  input=\"text\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"BuyersAttorneyCity\" label=\"City\" input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field name=\"BuyersAttorneyState\" label=\"State\" width=\"30\"  input=\"text\" class=\"horizontal\" />\r\n      <field name=\"BuyersAttorneyZip\" label=\"Zip\" width=\"75\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"BuyersAttorneyPhone\" label=\"Phone\"  input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field name=\"BuyersAttorneyFax\" label=\"Fax\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"BuyersAttorneyAttentionTo\" label=\"Attention\"  input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n    </line>\r\n  </group>\r\n  <group name=\"sellersattorney\" hidden=\"ShowSeller=Yes\" legend=\"Default Sellers Attorney Information\">\r\n    <line>\r\n      <field name=\"SellersAttorneyName\" label=\"Name\"  input=\"text\" width=\"450\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"SellersAttorneyAddress\" label=\"Address\" width=\"450\"  input=\"text\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"SellersAttorneyCity\" label=\"City\" input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field name=\"SellersAttorneyState\" label=\"State\" width=\"30\"  input=\"text\" class=\"horizontal\" />\r\n      <field name=\"SellersAttorneyZip\" label=\"Zip\" width=\"75\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"SellersAttorneyPhone\" label=\"Phone\"  input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field name=\"SellersAttorneyFax\" label=\"Fax\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"SellersAttorneyAttentionTo\" label=\"Attention\"  input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n    </line>\r\n  </group>\r\n  <group name=\"additionalcopiesto\" hidden=\"ShowAdditional=Yes\" legend=\"Default Additional Copy Information\">\r\n    <line>\r\n      <field name=\"AdditionalCopyName\" label=\"Name\"  input=\"text\" width=\"450\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"AdditionalCopyAddress\" label=\"Address\" width=\"450\"  input=\"text\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"AdditionalCopyCity\" label=\"City\" input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field name=\"AdditionalCopyState\" label=\"State\" width=\"30\"  input=\"text\" class=\"horizontal\" />\r\n      <field name=\"AdditionalCopyZip\" label=\"Zip\" width=\"75\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"AdditionalCopyPhone\" label=\"Phone\"  input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field name=\"AdditionalCopyFax\" label=\"Fax\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"AdditionalCopyAttentionTo\" label=\"Attention\"  input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n    </line>\r\n  </group>\r\n  <group name=\"applicant\" legend=\"Default Originator Information\">\r\n    <line>\r\n      <field name=\"ApplicantName\" label=\"Name\"  input=\"text\" width=\"450\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"ApplicantAddress\" label=\"Address\" width=\"450\"  input=\"text\" labelclass=\"label width_75\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"ApplicantCity\" label=\"City\" input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field name=\"ApplicantState\" label=\"State\" width=\"30\"  input=\"text\" class=\"horizontal\" />\r\n      <field name=\"ApplicantZip\" label=\"Zip\" width=\"75\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"ApplicantPhone\" label=\"Phone\"  input=\"text\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n      <field name=\"ApplicantFax\" label=\"Fax\"  input=\"text\" class=\"horizontal\" />\r\n    </line>\r\n    <line>\r\n      <field name=\"ApplicantAttentionTo\" label=\"Attention\"  input=\"text\" width=\"450\" class=\"horizontal\" labelclass=\"label width_75\" />\r\n    </line>\r\n  </group>\r\n</request>',0);
/*!40000 ALTER TABLE `request_type` ENABLE KEYS */;


--
-- Definition of table `affinity`.`role`
--

DROP TABLE IF EXISTS `affinity`.`role`;
CREATE TABLE  `affinity`.`role` (
  `r_code` varchar(15) NOT NULL,
  `r_description` varchar(100) NOT NULL,
  `r_permission_bit` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`r_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`role`
--

/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `affinity`.`role` (`r_code`,`r_description`,`r_permission_bit`) VALUES 
 ('Admin','System Administrator',7),
 ('Anonymous','Unauthenticated User',0),
 ('Attorney','Attorney',3),
 ('Broker','Mortgage Broker',3);
/*!40000 ALTER TABLE `role` ENABLE KEYS */;


--
-- Definition of table `affinity`.`system_setting`
--

DROP TABLE IF EXISTS `affinity`.`system_setting`;
CREATE TABLE  `affinity`.`system_setting` (
  `ss_code` varchar(25) NOT NULL,
  `ss_description` varchar(100) NOT NULL,
  `ss_data` text NOT NULL,
  PRIMARY KEY  (`ss_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`system_setting`
--

/*!40000 ALTER TABLE `system_setting` DISABLE KEYS */;
INSERT INTO `affinity`.`system_setting` (`ss_code`,`ss_description`,`ss_data`) VALUES 
 ('SYSTEM','System Settings','<response><field name=\"RootUrl\">http://www.dev-test.net/affinity/</field><field name=\"RootUrlSSL\">http://www.dev-test.net/affinity/</field><field name=\"SmtpHost\">relay.verysimple.com</field><field name=\"SendFromEmail\">auto@verysimple.com</field><field name=\"SendFromRealName\">Affinity Title</field><field name=\"EmailFooter\">--\r\nAffinity Title Services</field></response>');
/*!40000 ALTER TABLE `system_setting` ENABLE KEYS */;


--
-- Definition of table `affinity`.`underwriter`
--

DROP TABLE IF EXISTS `affinity`.`underwriter`;
CREATE TABLE  `affinity`.`underwriter` (
  `u_code` varchar(15) NOT NULL,
  `u_description` varchar(100) NOT NULL,
  PRIMARY KEY  (`u_code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `affinity`.`underwriter`
--

/*!40000 ALTER TABLE `underwriter` DISABLE KEYS */;
/*!40000 ALTER TABLE `underwriter` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
