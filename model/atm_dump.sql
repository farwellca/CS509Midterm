-- MySQL dump 10.13  Distrib 8.0.45, for Linux (aarch64)
--
-- Host: host.docker.internal    Database: atm
-- ------------------------------------------------------
-- Server version       9.6.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */
;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */
;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */
;
/*!50503 SET NAMES utf8mb4 */
;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */
;
/*!40103 SET TIME_ZONE='+00:00' */
;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */
;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */
;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */
;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */
;
SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;

SET @@SESSION.SQL_LOG_BIN = 0;

--
-- GTID state at the beginning of the backup
--

SET
    @@GLOBAL.GTID_PURGED = /*!80000 '+'*/ 'f23c16fb-017d-11f1-8840-ce88a17fb6c0:1-85';

--
-- Table structure for table `Accounts`
--

DROP TABLE IF EXISTS `Accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */
;
/*!50503 SET character_set_client = utf8mb4 */
;
CREATE TABLE `Accounts` (
    `AccountNum` int NOT NULL,
    `Holder` varchar(255) NOT NULL,
    `Balance` int NOT NULL,
    `Status` varchar(255) NOT NULL,
    PRIMARY KEY (`AccountNum`),
    CONSTRAINT `Accounts_ibfk_1` FOREIGN KEY (`AccountNum`) REFERENCES `Users` (`ID`) ON DELETE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */
;

--
-- Dumping data for table `Accounts`
--

/*!40000 ALTER TABLE `Accounts` DISABLE KEYS */
;
INSERT INTO
    `Accounts`
VALUES (
        2,
        'Bob Jones',
        10000,
        'Active'
    ),
    (
        3,
        'John Smith',
        20000,
        'Disabled'
    ),
    (
        4,
        'Billy Bob',
        123456,
        'Disabled'
    );
/*!40000 ALTER TABLE `Accounts` ENABLE KEYS */
;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */
;
/*!50503 SET character_set_client = utf8mb4 */
;
CREATE TABLE `Users` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `Username` varchar(255) NOT NULL,
    `Pin` varchar(255) NOT NULL,
    `Type` varchar(255) NOT NULL,
    PRIMARY KEY (`ID`),
    UNIQUE KEY `Username` (`Username`)
) ENGINE = InnoDB AUTO_INCREMENT = 6 DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */
;

--
-- Dumping data for table `Users`
--

/*!40000 ALTER TABLE `Users` DISABLE KEYS */
;
INSERT INTO
    `Users`
VALUES (1, 'admin1', '11111', 'Admin'),
    (
        2,
        'cust1',
        '11111',
        'Customer'
    ),
    (
        3,
        'cust2',
        '22222',
        'Customer'
    ),
    (
        4,
        'cust3',
        '12345',
        'Customer'
    );
/*!40000 ALTER TABLE `Users` ENABLE KEYS */
;

--
-- Dumping routines for database 'atm'
--
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */
;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */
;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */
;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */
;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */
;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */
;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */
;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */
;

-- Dump completed on 2026-03-10  2:47:42