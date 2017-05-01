-- insert clerking services request type

INSERT INTO `affinity`.`request_type` (`rt_code`,`rt_description`,`rt_definition`,`rt_is_active`) VALUES 
 ('ClerkingRequest','Clerking Services Request','<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<request>\r\n  <group name=\"general\" legend=\"CITY OF CHICAGO SERVICES\">\r\n    <line>\r\n      <field sp_id=\"\" rei_id=\"\" name=\"1stChoiceLocation\" label=\"1st Choice: Location\"  input=\"checkbox\" class=\"horizontal\" labelclass=\"label\" />\r\n      </line>\r\n  </group>\r\n</request>',1);

-- create taxing_district table for looking up cities with tax stamp requirements and then populate the table

-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.41-community-nt


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
-- Definition of table `taxing_district`
--

DROP TABLE IF EXISTS `taxing_district`;
CREATE TABLE `taxing_district` (
  `taxing_district` varchar(255) NOT NULL default '',
  `type` varchar(50) default NULL,
  `county` varchar(255) default NULL,
  `liable_party` varchar(255) default NULL,
  `amount` varchar(255) default NULL,
  `where` varchar(255) default NULL,
  `address` varchar(255) default NULL,
  `csz` varchar(255) default NULL,
  `phone` varchar(50) default NULL,
  `website` varchar(200) default NULL,
  `stamp_exempt` varchar(50) default NULL,
  `notes` varchar(255) default NULL,
  `stamp_required` tinyint(3) unsigned NOT NULL default '0',
  PRIMARY KEY  (`taxing_district`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `taxing_district`
--

/*!40000 ALTER TABLE `taxing_district` DISABLE KEYS */;
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Addison','Township','DuPage','Buyer','$2.50/$1000.00','Village Hall','1 Friendship Plaza','Addison, IL 60101','(630) 693-7564','www.addisonadvantage.org','Yes','Water bill must be paid, sewer inspection required, copy of state declaration and deed required if exempt',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Alsip','Township','Cook','Seller','$3.50/$1000.00','Village Clerks Office','4500 W. 123rd Street','Alsip, IL 60803','(708) 385-6902','www.alsip.il.us','Yes','Final water reading and bill must be paid, copy of sales contract required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Aurora','Township','DuPage, Kane, Kendall, Will','Seller','$3.00/$1000.00','City Hall','44 E. Downer Place','Aurora, IL 60507','(630) 906-7414','www.aurora-il.org','No','Final water reading and bill must be paid, copy or original state declaration, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Bartlett','Township','Cook, DuPage, Kane','Seller','$3.00/$1000.00','Village Hall','228 S. Main Street','Bartlett, IL 60103','(630) 837-0800','www.village.bartlett.il.us','Yes','Final water reading and bill must be paid, copy of deed & state declaration or contract required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Bedford Park','Township','Cook','Seller','$50.00/Trans.','Village Clerk','6701 S. Archer Avenue','Bedford Park, IL 60501','(708) 458-2067',NULL,'Yes','Water bills must be paid, inspection required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Bellwood','Township','Cook','Seller','$5.00/$1000.00','Village Hall/Water Dept.','3200 Washington Blvd.','Bellwood, IL 60104','(708) 547-3500',NULL,'Yes ($10.00)','Final water reading, home inspection required, has village form, original or copy of deed & state declaration required',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Bensenville','Township','Cook, DuPage',NULL,'None','Village Hall','12 S. Center Street','Bensenville, IL 60106','(630) 766-8200','www.bensenville.il.us','No','Inspection required to obtain certificate of occupancy, $55 fee',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Berkeley','Township','Cook','Seller','$25.00/Trans.','Village Hall','5819 Electric Avenue','Berkeley, IL 60163','(708) 449-8840','www.berkeley.bobnovak.net','No','Water bills, fines, etc. must be paid, original deed required, inspection required, compliance stamp',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Berwyn','Township','Cook','Seller','$10.00/$1000.00','City Hall','6700 W. 26th Street','Berwyn, IL 60402','(708) 788-2660','www.berwyn-il.gov','Yes ($25.00)','Final water reading, original deed required, state & cook declaration required, inspection required, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Blue Island','Township','Cook',NULL,'None','City Hall','13051 S. Greenwood Avenue','Blue Island, IL 60406','(708) 597-8602','www.blueisland.org','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Bolingbrook','Township','Dupage, Will','Seller & Buyer split equally','$7.50/$1000.00','Village Hall/Finance Dept.','375 West Briarcliff Road','Bolingbrook, IL 60440','(630) 226-8455','www.bolingbrook.com','Yes ($50.00)','Final meter reading, sums due must be paid, original or copy of deed required, state declaration required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Broadview','Township','Cook',NULL,'None','Village Hall/Building Dept.','2350 S. 25th Avenue','Broadview, IL 60155','(708) 345-8174','www.villageofbroadview.com','No','Inspection required to obtain certificate of occupancy, $15-$35',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Brookfield','Township','Cook',NULL,'None','Village Hall/Building Dept.','8820 Brookfield Avenue','Brookfield, IL 60513','(708) 485-7734','www.villageofbrookfield.com','No','Compliance inspection',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Buffalo Grove','Township','Cook, Lake','Seller','$3.00/$1000.00','Village Hall','50 Raupp Blvd.','Buffalo Grove, IL 60089','(847) 459-2500','www.vgb.org','Yes','Water bill, tickets, etc. must be paid, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Burbank','Township','Cook','Seller','$5.00/$1000.00','City Hall','6530 W. 79th Street','Burbank, IL 60459','(708) 599-5500','www.burbankil.gov','No','Has city form, original deed required if exempt',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Burnham','Township','Cook','Buyer','$5.00/$1000.00','Village Hall','14450 Manistee Avenue','Burnham, IL 60633','(708) 862-9150',NULL,'Yes','Final water reading, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Calumet City','Township','Cook','Seller & Buyer split equally','$8.00/$1000.00','City Hall','204 Pulaski Road','Calumet City, IL 60409','(708) 891-8116','www.calumetcity.org','Yes','Water bill must be paid, inspection required, state declaration required, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Calumet Park','Township','Cook','Buyer','$5.00/$1000.00','Village Hall','12409 S. Throop','Calumet Park, IL 60827','(708) 389-0851',NULL,'Yes','Final water reading, inspection required $100, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Carol Stream','Township','DuPage','Seller','$3.00/$1000.00','Village Hall','500 N. Gary Avenue','Carol Stream, IL 60188','(630) 665-7050','www.carolstream.org','Yes','Final water reading, water, sewer, etc. must be paid, copy of sales contract required, state declaration required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Channahon','Township','Grundy, Will','Buyer','$3.00/$1000.00','Village Hall','24555 S. Navajo Drive','Channahon, IL 60410','(815) 467-6644','www.channahon.org','Yes','Final water reading, water & sums due must be paid, original deed required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Chicago','Township','Cook','Buyer','$3.75/$500.00','City Hall','121 N. LaSalle Street, Rm. 107','Chicago, IL 60602','(312) 747-9723','www.cityofchicago.org','Yes','Water & sewer bills must be paid, zoning certificate, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Chicago Heights','Township','Cook','Seller','$4.00/$1000.00','City Clerks Office','1601 Chicago Road','Chicago Heights, IL 60411','(708) 756-5300','www.chicagoheights.net','Yes','Water bill must be paid, inspection required, copy of deed required, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Cicero','Township','Cook','Seller','$10.00/$1000.00','Town Collector','4936 W. 25th Place','Cicero, IL 60804','(708) 656-3600','www.thetownofcicero.com','Yes ($25.00)','Water bill must be paid, original deed & state & county declarations required, certificate of compliance & inspection required (including refinances); has town form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Clarendon Hills','Township','DuPage',NULL,'None','Village Hall','1 N. Prospect Avenue','Clarendon Hills, IL 60514','(630) 323-3500','www.clarendon-hills.il.us','No','Yes',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Cook County','County',NULL,'Seller','$0.25/$500.00','Cook County Recorder','118 N. Clark St., #120','Chicago, IL 60602','(312) 603-5050','www.cookctyrecorder.com',NULL,'Cook County Declaration Form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Country Club Hills','Township','Cook','Seller','$5.00/$1000.00','City Hall','4200 W. 183rd Street','Country Club Hills, IL 60478','(708) 798-2616','www.countryclubhills.org','Yes','Final water reading, water, etc. must be paid, copy of contract required, state declaration required, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Countryside','Township','Cook','Buyer','$50.00/Trans.','City Hall','5550 East Avenue','Countryside, IL 60525','(708) 354-7270','www.countryside-il.org','Yes','Water, sewer, etc. must be paid, receipts requred, final $20 reading, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('DeKalb County','County',NULL,'Either party(seller customary)','$0.25/$500.00','DeKalb County Recorder','110 E. Sycamore St.','Sycamore, IL 60178','(815) 895-7156','www.dekalbcounty.org',NULL,'State Declaration Form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Des Plaines','Township','Cook','Seller','$2.00/$1000.00','City Hall','1420 Miner Street','Des Plaines, IL 60016','(847) 391-5382','www.desplaines.org','Yes ($10.00)','Final water reading, update code violations, inspection required on rental property only, original deed, survey, state declaration & copy of title commitment required',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Dolton','Township','Cook','Seller','$10.00/Trans.','Village Hall','14014 Park Avenue','Dolton, IL 60419','(708) 849-4000','www.villageofdolton.com','Yes ($10.00)','Water bill must be paid, inspection required $150, has inspection form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Downers Grove','Township','DuPage',NULL,'None','Village Hall','801 Burlington Avenue','Downers Grove, IL 60515','(630) 434-5500','www.downers.us','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('DuPage County','County',NULL,'Either party(seller customary)','$0.25/$500.00','DuPage County Recorder','421 N. County Farm Rd.','Wheaton, IL 60187','(630) 682-7200','www.co.dupage.il.us',NULL,'State Declaration Form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('East Hazel Crest','Township','Cook','Buyer','$25.00/Trans.','Village Clerk','1904 W. 174th Street','East Hazel Crest, IL 60429','(708) 798-0213','http://easthazelcrest.com','Yes','Final water reading, water bills, etc. must be paid, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Elgin','Township','Cook, Kane',NULL,'None','City Hall','150 Dexter Court','Elgin, IL 60120','(847) 931-5639','www.cityofelgin.org','Yes','Final water reading, water, etc. must be paid, inspection required if violations exist, stamp required, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Elk Grove Village','Township','Cook, DuPage','Seller','$3.00/$1000.00','Village Hall','901 Wellington Avenue','Elk Grove Village, IL 60007','(847) 439-3900','www.elk-grove-village.il.us','Yes ($10.00)','Final water reading, water, sewer, etc. must be paid, copy of deed & state declaration required, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Elmhurst','Township','DuPage, Cook','Seller','$1.50/$1000.00','Village Hall','209 N. York Street','Elmhurst, IL 60126','(630) 530-3117','www.elmhurst.org','No','Final water reading, water, etc. bills must be paid, inspection required, copy of signed sales contract required, copy of deed & state declaration required, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Elmwood Park','Township','Cook','Seller','$5.00/$1000.00','Village Hall','11 Conti Parkway','Elmwood Park, IL 60707','(708) 452-7300','www.elmwoodpark.org','Yes ($35.00)','Final water reading, water, sewer, etc. must be paid, two inspections required, copy of deed & state declaration required, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Evanston','Township','Cook','Seller','$5.00/$1000.00','City Clerks Office','2100 Ridge','Evanston, IL 60201','(847) 866-2925','www.cityofevanston.org','Yes ($20.00)','Water bill must be paid, has city form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Evergreen Park','Township','Cook','Seller','$5.00/$1000.00','Village Hall','9418 S. Kedzie','Evergreen Park, IL 60805','(708) 229-8215','www.evergreenpark-ill.com','Yes','Final water reading, inspection required $15, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Flossmoor','Township','Cook',NULL,'None','Village Hall','2800 Flossmoor Road','Flossmoor, IL 60422','(708) 798-2300','www.flossmoor.org','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Forest Park','Township','Cook',NULL,'None','Village Hall','517 Des Plaines Avenue','Forest Park, IL 60130','(708) 366-2323','www.forestpark.net','No','Final water reading, water bill must be paid, inspection required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Franklin Park','Township','Cook','Seller','$25.00 minimum','Village Hall','9500 W. Belmont Avenue','Franklin Park, IL 60131','(847) 671-8795','www.villageoffranklinpark.com','Yes','Final water reading, water bill must be paid, zoning inspection & approval required, violations must be corrected, current survey & original deed required',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Glen Ellyn','Township','DuPage','Seller','$3.00/$1000.00','Village Hall','535 Duane Street','Glen Ellyn, IL 60137','(630) 547-5235','www.glenellyn.org','Yes','Water, sewer, etc. bills must be paid, sales contract or deed required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Glendale Heights','Township','DuPage','Seller','$3.00/$1000.00','Village Hall','300 E. Civic Center Plaza','Glendale Heights, IL 60139','(630) 260-6000','www.glendaleheights.org','Yes ($25.00)','Water, sewer, etc. bills must be paid, inspection required $50, copy of deed & state declaration required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Glenwood','Township','Cook','Seller','$5.00/$1000.00','Village Hall','1 Asselborn Way','Glenwood, IL 60425','(708) 753-2400','www.glenwood-il.com','Yes','Water bill must be paid, occupancy inspection required $35, copy of contract required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Hainesville','Township','Lake',NULL,'None','Village Hall/Building Dept.','83 W. Belvidere Road','Hainesville, IL 60030','(847) 223-1675','www.hainesville.org','No','Inspection required $40',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Hanover Park','Township','Cook, DuPage','Seller','$1.50/$500.00','Village Hall','2121 W. Lake Street','Hanover Park, IL 60133','(630) 372-4200','www.hanoverparkillinois.org','Yes ($10.00)','Water, sewer, etc. bills must be paid, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Harvey','Township','Cook','Seller & Buyer split equally','$4.00/$1000.00','City Clerks Office','15320 Broadway Avenue','Harvey, IL 60426','(708) 210-5300','www.cityofharvey.org','Yes ($45.00)','Water bill must be paid, inspection required, copy of deed required, has city form, $45 buyer processing fee',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Harwood Heights','Township','Cook','Buyer','$10.00/$1000.00','Village Hall','7300 W. Wilson Avenue','Harwood Heights, IL 60706','(708) 867-7200','www.harwoodheights.org','Yes ($50.00)','Water bill must be paid, inspection required $50, deed & state declaration required, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Hazel Crest','Township','Cook',NULL,'None','Village Hall','3000 W. 170th Place','Hazel Crest, IL 60429','(708) 335-9600','www.villageofhazelcrest.com','No','Inspection required $75',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Hickory Hills','Township','Cook',NULL,'None','Village Hall','8652 W. 95th Street','Hickory Hills, IL 60457','(708) 598-4800','www.hickoryhillsil.org','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Highland Park','Township','Cook','Seller','$5.00/$1000.00','Village Hall','1707 St. Johns Avenue','Highland Park, IL 60035','(847) 432-0800','www.cityhpil.com','Yes','Water bill must be paid, original deed required for exempt only, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Highwood','Township','Lake','Seller','$5.00/$1000.00','City Hall','17 Highwood Avenue','Highwood, IL 60040','(847) 432-1924','www.cityofhighwood.com','Yes','The transfer fee is applicable for the town of Fort Sheridan subdivision, copy of deed required, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Hillside','Township','Cook','Buyer','$3.75/$500.00','Village Clerk','452 Hillside Avenue','Hillside, IL 60162','(708) 449-6450','www.hillside-il.org','Yes','Final water reading, inspection required $20, original state declaration required, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Hoffman Estates','Township','Cook','Seller','$3.00/$1000.00','Village Hall','1900 Hassell Road','Hoffman Estates, IL 60195','(847) 882-9100','www.hoffmanestates.com','Yes ($10.00)','Water bill must be paid, state declaration required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Hometown','Township','Cook',NULL,'None','Village Hall','4331 Southwest Highway','Hometown, IL 60456','(708) 424-7500',NULL,'No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Illinois','State',NULL,'Either party(seller customary)','$0.50/$500.00','County Recorders Office',NULL,NULL,NULL,'www.revenue.state.il.us',NULL,'See 35 ILCS 200/31-1 ET. SEQ.',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Island Lake','Township','McHenry, Lake',NULL,'None','Village Hall','3720 Greenleaf Avenue','Island Lake, IL 60042','(847) 526-8764','www.villageofislandlake.com','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Itasca','Township','DuPage',NULL,'None','Village Hall','550 W. Irving Park Road','Itasca, IL 60143','(630) 773-0835','www.itasca.com','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Joliet','Township','Kendall, Will','Seller','$3.00/$1000.00','City Hall','150 W. Jefferson Street','Joliet, IL 60432','(815) 724-3902','www.cityofjoliet.info','Yes','Residential transfer information & disclosure form required before contract entered into if residential, final water reading, water, etc. bills must be paid, original deed required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Justice','Township','Cook',NULL,'None','Village Hall','7800 Archer Road','Justice, IL 60458','(708) 458-2520','www.villageofjustice.com','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Kane County','County',NULL,'Either party(seller customary)','$0.25/$500.00','Kane County Recorder','719 S. Batavia Ave., Bldg. C','Geneva, IL 60134','(630) 232-5935','www.co.kane.il.us',NULL,'State Declaration Form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Kendall County','County',NULL,'Either party(seller customary)','$0.25/$500.00','Kendall County Recorder','111 W. Fox St.','Yorkville, IL 60560','(630) 553-4112','www.co.kendall.il.us',NULL,'State Declaration Form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Lake County','County',NULL,'Either party(seller customary)','$0.25/$500.00','Lake County Recorder','18 N. County St.','Waukegan, IL 60085','(847) 360-6673','www.co.lake.il.us',NULL,'State Declaration Form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Lincolnshire','Township','Lake','Buyer','$3.00/$1000.00','Village Hall','1 Olde Halfday Road','Lincolnshire, IL 60069','(847) 883-8600','www.village.lincolnshire.il.us','Yes','Water bill must be paid, state declaration required',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Lockport','Township','Will',NULL,NULL,'City Hall','222 E. 9th Street','Lockport, IL 60441','(815) 838-0549','www.lockport.org','No','Some properties with a Lockport address are actually incorporated into an adjacent municipality that may require transfer tax. Call the city clerk with address to verify.',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Lynwood','Township','Cook',NULL,'None','Village Hall','21460 Lincoln Highway','Lynwood, IL 60411','(708) 758-6101',NULL,'No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Lyons','Township','Cook',NULL,'None','Village Hall','7801 W. Ogden Avenue','Lyons, IL 60534','(708) 447-8886',NULL,'No','Inspection required $100',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Markham','Township','Cook',NULL,'None','City Hall','16313 Kedzie Avenue','Markham, IL 60426','(708) 596-345','www.cityofmarkham.org','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Matteson','Township','Cook',NULL,'None','Village Hall','4900 Village Commons','Matteson, IL 60443','(708) 283-4900','www.vil.matteson.il.us','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Maywood','Township','Cook','Seller','$4.00/$1000.00','Village Hall','40 Madison Street','Maywood, IL 60153','(708) 450-4405','www.maywood-il.org','Yes','Water bill must be paid, inspection required $75, original deed required, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('McCook','Township','Cook','Seller','$5.00/$1000.00','Village Hall','5000 Glencoe Avenue','McCook, IL 60525','(708) 447-9030','www.villageofmccook.org','Yes','Water bill must be paid, inspection required $50, original deed required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('McHenry County','County',NULL,'Either party(seller customary)','$0.25/$500.00','McHenry County Recorder','2200 N. Seminary Ave.','Woodstock, IL 60098','(815) 334-4110','www.co.mchenry.il.us',NULL,'State Declaration Form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Melrose Park','Township','Cook',NULL,'None','Village Hall','1000 N. 25th Street','Melrose Park, IL 60160','(708) 343-4000','www.melrosepark.org','No','Inspection required $75 and up',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Mettawa','Township','Lake','Buyer','$5.00/$1000.00','Village Clerk','1000 Allanson Road','Mettawa, IL 60060','(847) 949-4015',NULL,'Yes','Original deed, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Morton Grove','Township','Cook','Seller','$3.00/$1000.00','Village Hall','6101 Capulina Street','Morton Grove, IL 60053','(847) 965-4100','http://www.mortongroveil.org/','Yes ($25.00)','Water, sewer, etc. bills must be paid, copy of state declaration, original deed required, copy of sales contract required',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Mount Prospect','Township','Cook','Buyer','$3.00/$1000.00','Village Hall','50 S. Emerson','Mt. Prospect, IL 60056','(847) 392-6000','www.mountprospect.org','Yes ($15.00)','Water, sewer, etc. bills must be paid, property violations must be resolved, copy of state declaration, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Naperville','Township','DuPage, Will, Kane, Kendall','Buyer','$3.00/$1000.00','Village Hall','400 S. Eagle Street','Naperville, IL 60566','(630) 420-4116','www.naperville.il.us','Yes','Copy of contract, has village form, if non-residentail, village requires all utility bills to be paid including final prorated bill, certificate required for exemption',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('New Lenox','Township','Will',NULL,'None','Village Hall','701 W. Haven Avenue','New Lenox, IL 60451','(815) 485-6452','www.newlenox.net','No','Some properties with a New Lenox address are actually incorporated into an adjacent municipality that may require transfer tax. Call the village clerk with address to verify.',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Niles','Township','Cook','Buyer','$3.00/$1000.00','Village Hall','1000 Civic Center Drive','Niles, IL 60714','(847) 588-8000','www.vniles.com','Yes','Water bill must be paid, inspection required, contract required, has village form, $25 occupancy permit fee',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Norridge','Township','Cook',NULL,'None','Village Hall','4000 N. Olcott Avenue','Norridge, IL 60706','(708) 453-0800','www.villageofnorridge.com','No','Inspection required $25 in order to issue Transfer Certificate',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('North Aurora','Township','Kane',NULL,'None','Village Hall','25 E. State Street','North Aurora, IL 60542','(630) 897-8228','www.vil.north-aurora.il.us','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('North Chicago','Township','Lake','Buyer','$5.00/$1000.00','City Clerk','1850 Lewis Avenue','North Chicago, IL 60064','(847) 596-8625','www.northchicago.org','Yes','Final water reading, water bill must be paid, inspection by code enforcer required, copy of contract or deed required, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('North Riverside','Township','Cook',NULL,'None','Village Hall','2401 Des Plaines Avenue','North Riverside, IL 60546','(708) 447-4211','www.northriverside-il.org','No','Pre-sale inspection required $250, final water reading, current survey required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Northlake','Township','Cook',NULL,'None','City Hall','55 E. North Avenue','Northlake, IL 60164','(708) 343-8700','www.northlakecity.com','No','Inspection required to obtain stamp, no fee for stamp, contact village for inspection fee',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Oak Forest','Township','Cook',NULL,'None','City Hall','15440 South Central Avenue','Oak Forest, IL 60452','(708) 687-4050','www.oak-forest.org','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Oak Lawn','Township','Cook','Seller','$5.00/$1000.00','Village Office','9446 Raymond Avenue','Oak Lawn, IL 60453','(708) 499-7761','www.oaklawn-il.gov','Yes','Water bill must be paid, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Oak Park','Township','Cook','Seller','$8.00/$1000.00','Village Hall','123 Madison Street','Oak Park, IL 60302','(708) 358-5675','www.oak-park.us','Yes','Final water & sewer reading, inspection required if 4+ units, copy of contract or state declaration required, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Olympia Fields','Township','Cook',NULL,'None','Village Hall','20701 Governors Highway','Olympia Fields, IL 60461','(708) 503-8000','http://olympia-fields.com','No','Point of sale inspection required $50, certificate of occupancy issued after inspection passed',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Park Forest','Township','Cook, Will','Seller','$5.00/$1000.00','Village Hall','350 Victory Drive','Park Forest, IL 60466','(708) 748-1112','www.villageofparkforest.net','Yes ($15.00)','Water, etc. bills must be paid, inspection required $50-$100, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Park Ridge','Township','Cook','Seller','$2.00/$1000.00','City Hall','505 Butler Place','Park Ridge, IL 60068','(847) 318-5289','www.parkridge.us','Yes ($25.00)','Water bill must be paid, zoning inspection required, original deed required, has city form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Phoenix','Township','Cook',NULL,'None','Village Hall','633 E. 151st Street','Phoenix, IL 60426','(708) 331-2636','http://villageofphoenix.com','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Plainfield','Township','Will',NULL,'None','Village Hall','24000 W. Lockport Street','Plainfield, IL 60544','(815) 436-7093','www.plainfield-il.org','No','Some properties with a Plainfield address are actually incorporated into an adjacent municipality that may require transfer tax. Call the village clerk with address to verify.',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Posen','Township','Cook',NULL,'None','Village Hall','2440 Walter Zimny Drive','Posen, IL 60469','(708) 389-5293',NULL,'No','Must give notice to village clerk of intent to sell any residential property, inspection required to obtain certificate of compliance',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Richton Park','Township','Cook',NULL,'None','Village Hall','4455 Sauk Trail','Richton Park, IL 60471','(708) 481-8950','www.richtonpark.org','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('River Forest','Township','Cook','Seller','$1.00/$1000.00','Village Clerk','400 Park Avenue','River Forest, IL 60305','(708) 366-8500','www.ci.river-forest.il.us','Yes','Final water reading, water bill must be paid, inspection required to obtain certificate of occupancy, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('River Grove','Township','Cook','Seller','$50.00/Trans.','Village Hall','2621 N. Thatcher Avenue','River Grove, IL 60171','(708) 453-8000','www.villageofrivergrove.org','No','Water bill & any sums due must be paid, inspection required, copy of deed required, additional fees may apply',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Riverdale','Township','Cook',NULL,'None','Village Hall','157 W. 144th Street','Riverdale, IL 60827','(708) 841-2200',NULL,'No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Riverside','Township','Cook',NULL,'None','Village Hall','27 Riverside Road','Riverside, IL 60546','(708) 447-2700','http://riverside.il.us','No','Inspection required $150, final water reading, water & any sums due must be paid',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Robbins','Township','Cook',NULL,'None','Village Hall','3327 W. 137th Street','Robbins, IL 60472','(708) 385-8940',NULL,'No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Rolling Meadows','Township','Cook','Seller','$3.00/$1000.00','City Hall','3600 Kirchoff','Rolling Meadows, IL 60008','(847) 394-8500','www.ci.rolling-meadows.il.us','Yes ($20.00)','Water & sewer bills must be paid, copy of deed & state declaration required, has city form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Romeoville','Township','Will','Buyer','$1.75/$500.00','Village Hall','13 Montrose Drive','Romeoville, IL 60446','(815) 866-7200','www.romeoville.org','Yes','Final meter reading, any sums due must be paid, must obtain clearance letter for all transactions, copy or original deed & state declaration required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Round Lake Beach','Township','Lake',NULL,'None','Village Hall/Community Devlp.','1937 N. Municipal Way','Round Lake Beach, IL 60073','(847) 546-8919','www.villageofroundlakebeach.com','No','Occupancy permit inspection required $25',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Round Lake Heights','Township','Lake',NULL,'None','Village Hall','619 Pontiac Court','Round Lake Heights, IL 60073','(847) 546-1206','www.villageofroundlakeheights.com','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Round Lake Park','Township','Lake',NULL,'None','Village Hall','203 E. Lake Shore Drive','Round Lake Park, IL 60073','(847) 546-2790','www.roundlakepark.us','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Sauk Village','Township','Cook',NULL,'None','Katz Corber Building','Burnham Avenue','Sauk Village, IL 60411','(708) 758-3016',NULL,'No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Schaumburg','Township','Cook, DuPage','Seller','$1.00/$1000.00','Village Hall','101 Schaumburg Court','Schaumburg, IL 60193','(847) 895-4500','www.ci.schaumburg.il.us','Yes ($10.00)','Water bill must be paid, copy of state declaration required, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Schiller Park','Township','Cook',NULL,'None','Village Hall','9526 Irving Park Road','Schiller Park, IL 60176','(847) 678-2550','www.villageofschillerpark.com','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Skokie','Township','Cook','Seller','$3.00/$1000.00','Village Hall','5127 Oakton Street','Skokie, IL 60077','(847) 673-0500','www.skokie.org','Yes ($15.00)','Water bill must be paid, copy of state declaration required, has village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('South Chicago Heights','Township','Cook',NULL,'None','Village Hall','3317 Chicago Road','South Chicago Heights, IL 60411','(708) 755-1880','www.southchicagoheights.com','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Steger','Township','Cook, Will',NULL,'None','Village Hall','35 W. 34th Street','Steger, IL 60475','(708) 754-9415',NULL,'No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Stickney','Township','Cook','Seller','$5.00/$1000.00','Village Hall','6533 W. Pershing Road','Stickney, IL 60402','(708) 749-4400','www.villageofstickney.com','Yes ($25.00)','Final water reading required, original deed, state and county declarations required, copy of contract required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Stone Park','Township','Cook','Seller','$2.00/$500.00','Village Hall','1629 N. Mannheim Road','Stone Park, IL 60165','(708) 345-5550',NULL,'Yes','Final water reading required, home inspection required $50, state declaration required',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Streamwood','Township','Cook','Seller','$3.00/$1000.00','Village Hall','301 E. Irving Park Road','Streamwood, IL 60107','(630) 837-0200','www.streamwood.org','Yes ($10.00)','Water and sewer bills must be paid, copy of state declaration required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Summit','Township','Cook',NULL,'None','Village Hall','7321 W. 59th Street','Summit, IL 60501','(708) 563-4800',NULL,'No','Inspection for occupancy required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('University Park','Township','Cook, Will','Seller','$1.00/$1000.00','Village Hall','698 Burnham Drive','University Park, IL 60466','(708) 534-6451','https://university-park-il.com','Yes','Inspection required $250, all sums due must be paid, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Villa Park','Township','DuPage',NULL,'None','Village Hall','20 S. Ardmore Avenue','Villa Park, IL 60181','(630) 834-8505','www.invillapark.com','No','Final meter reading requried, sump pump inspection required to obtain certificate of compliance $50',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Wauconda','Township','Lake',NULL,'None','Village Hall','101 N. Main Street','Wauconda, IL 60084','(847) 526-9609','www.villageofwauconda.com','No','Inspection required',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Waukegan','Township','Lake',NULL,'None','City Hall','100 N. Martin Luther King Jr. Ave.','Waukegan, IL 60085','(847) 360-9000','www.waukeganweb.net','No','Inspection required, zoning certificate required on residential properties consisting of 3 or more apartments and commercial buildings $75',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('West Chicago','Township','DuPage',NULL,'None','City Hall','475 Main Street','West Chicago, IL 60185','(630) 293-2200','www.westchicago.org','Yes','Inspection required $200-$1500, final meter reading, water and sewer bills must be paid, any sums due must be paid',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Westchester','Township','Cook','Seller','$50.00/Trans.','Village Hall','10300 W. Roosevelt Road','Westchester, IL 60154','(708) 345-0020','www.westchester-il.org','No','Inspection required $75, final water reading required, original deed required',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Wheaton','Township','DuPage','Buyer','$2.50/$1000.00','City Hall','303 W. Wesley','Wheaton, IL 60187','(630) 260-2027','www.wheaton.il.us','Yes','Water & sewer bills must be paid, copy of contract or state declaration required, has city form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Wheeling','Township','Cook, Lake','Seller/Owner','None','Village Hall','255 West Dundee Road','Wheeling, IL 60090','(847) 459-2600','www.vi.wheeling.il.us','No','Real estate transfer certificate stating water, sewer, and garbage disposal bills are paid in full',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Will County','County',NULL,'Either party(seller customary)','$0.25/$500.00','Will County Recorder','58 E. Clinton, Suite 100','Joliet, IL 60432','(815) 740-4646','www.willcountyillinois.com',NULL,'State Declaration Form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Wilmette','Township','Cook','Buyer','$3.00/$1000.00','Village Hall','1200 Wilmette Avenue','Wilmette, IL 60091','(847) 251-2700','www.wilmette.com','Yes','Final meter reading, water bills and any sums due must be paid, copy of state declaration required, has 3-part village form',1);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Winnebago','County',NULL,'Either party(seller customary)','$1.50/$1000.00','Winnebago County Recorder','404 Elm St.','Rockford, IL 61101','(815) 987-3100',NULL,NULL,'State Declaration Form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Woodridge','Township','DuPage','Seller','$2.50/$1000.00','Village Hall','5 Plaza Drive','Woodridge, IL 60517','(630) 852-7000','www.vi.woodridge.com','Yes','Final meter reading, final water bill must be paid or deposit towrds final bill required, original deed required, copy of state declaration or contract required, has village form',0);
INSERT INTO `taxing_district` (`taxing_district`,`type`,`county`,`liable_party`,`amount`,`where`,`address`,`csz`,`phone`,`website`,`stamp_exempt`,`notes`,`stamp_required`) VALUES 
  ('Worth','Township','Cook',NULL,'None','Village Hall','7112 W. 111th Street','Worth, IL 60482','(708) 448-1181','www.villageofworth.com','No','Certificate of payment for water and sewer required $20',0);
/*!40000 ALTER TABLE `taxing_district` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


 update system_setting set ss_data='009' where ss_code='VERSION';