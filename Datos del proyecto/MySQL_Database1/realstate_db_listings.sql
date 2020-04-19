CREATE DATABASE  IF NOT EXISTS `realstate_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `realstate_db`;
-- MySQL dump 10.13  Distrib 8.0.16, for Win64 (x86_64)
--
-- Host: localhost    Database: realstate_db
-- ------------------------------------------------------
-- Server version	8.0.16

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `listings`
--

DROP TABLE IF EXISTS `listings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `listings` (
  `listings_id` int(11) NOT NULL AUTO_INCREMENT,
  `listing_number` varchar(50) NOT NULL,
  `smls_number` varchar(45) DEFAULT NULL,
  `tu_nuevo_espacio_number` varchar(45) DEFAULT NULL,
  `is_smls_promoted` bit(1) DEFAULT NULL,
  `is_tu_nuevo_espacio_promoted` bit(1) DEFAULT NULL,
  `listing_type` varchar(45) NOT NULL,
  `address` varchar(255) DEFAULT NULL,
  `city` varchar(45) DEFAULT NULL,
  `department` varchar(45) DEFAULT NULL,
  `bloque_number` varchar(10) DEFAULT NULL,
  `unit_number` varchar(10) DEFAULT NULL,
  `urbanization_name` varchar(45) DEFAULT NULL,
  `neighborhood` varchar(45) DEFAULT NULL,
  `estrato` varchar(11) DEFAULT NULL,
  `sales_price` decimal(19,2) DEFAULT NULL,
  `administration_fee` decimal(19,2) DEFAULT NULL,
  `parking_spaces` int(11) DEFAULT NULL,
  `has_open_kitchen` bit(1) DEFAULT NULL,
  `has_elevator` bit(1) DEFAULT NULL,
  `is_closed_urbanization` bit(1) DEFAULT NULL,
  `fk_companies_id` int(11) NOT NULL,
  `has_balcony` bit(1) DEFAULT NULL,
  `has_maid_room` bit(1) DEFAULT NULL,
  `is_studio` bit(1) DEFAULT NULL,
  `has_pool` bit(1) DEFAULT NULL,
  `year_built` int(11) DEFAULT NULL,
  `fk_contacts_id` int(11) NOT NULL,
  `area_size` int(11) DEFAULT NULL,
  `number_of_rooms` int(11) DEFAULT NULL,
  `number_of_bathrooms` int(11) DEFAULT NULL,
  PRIMARY KEY (`listings_id`),
  KEY `fk_listings_companies_idx` (`fk_companies_id`),
  KEY `fk_listings_contacts_idx` (`fk_contacts_id`),
  CONSTRAINT `fk_listings_companies` FOREIGN KEY (`fk_companies_id`) REFERENCES `companies` (`companies_id`),
  CONSTRAINT `fk_listings_contacts` FOREIGN KEY (`fk_contacts_id`) REFERENCES `contacts` (`contacts_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `listings`
--

LOCK TABLES `listings` WRITE;
/*!40000 ALTER TABLE `listings` DISABLE KEYS */;
/*!40000 ALTER TABLE `listings` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-06-09 10:39:27
