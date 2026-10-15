
/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `salonbeaute` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci */;

USE `salonbeaute`;
DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `client` (
  `idClient` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(100) NOT NULL,
  `prenom` varchar(100) NOT NULL,
  `telephone` varchar(20) DEFAULT NULL,
  `genre` varchar(20) DEFAULT NULL,
  `email` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`idClient`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES (36,'ABLODE','Kafui','90112233','Femme',NULL),(37,'KOFFI','Yawa','91223344','Femme',NULL),(38,'DOSSOU','Edem','92334455','Homme',NULL),(39,'AMÉGBOR','Lélé','93445566','Femme',NULL),(40,'DEMA','Afi','91452362','Femme',NULL);
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `employe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employe` (
  `idEmploye` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(100) NOT NULL,
  `prenom` varchar(100) NOT NULL,
  `specialite` varchar(100) DEFAULT NULL,
  `actif` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`idEmploye`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `employe` WRITE;
/*!40000 ALTER TABLE `employe` DISABLE KEYS */;
INSERT INTO `employe` VALUES (19,'AGBÉNOU','Akossiwa','Coiffure',1),(20,'KPOGO','Eyram','Manucure / Pédicure',1),(21,'ATTIVOR','Sénam','Soins du visage',1),(22,'TÉTÉ','Mawuli','Coiffure / Tressage',1);
/*!40000 ALTER TABLE `employe` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `facture`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `facture` (
  `idFacture` int(11) NOT NULL AUTO_INCREMENT,
  `idRdv` int(11) DEFAULT NULL,
  `dateFacture` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`idFacture`),
  KEY `idRdv` (`idRdv`),
  CONSTRAINT `facture_ibfk_1` FOREIGN KEY (`idRdv`) REFERENCES `rendezvous` (`idRdv`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `facture` WRITE;
/*!40000 ALTER TABLE `facture` DISABLE KEYS */;
INSERT INTO `facture` VALUES (15,15,'2026-06-04 23:10:11');
/*!40000 ALTER TABLE `facture` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `facturedetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `facturedetail` (
  `idDetail` int(11) NOT NULL AUTO_INCREMENT,
  `idFacture` int(11) NOT NULL,
  `idProduit` int(11) DEFAULT NULL,
  `idPrestation` int(11) DEFAULT NULL,
  `quantite` int(11) NOT NULL DEFAULT 1,
  `prix` decimal(10,2) NOT NULL,
  PRIMARY KEY (`idDetail`),
  KEY `idFacture` (`idFacture`),
  KEY `idProduit` (`idProduit`),
  KEY `idPrestation` (`idPrestation`),
  CONSTRAINT `facturedetail_ibfk_1` FOREIGN KEY (`idFacture`) REFERENCES `facture` (`idFacture`) ON DELETE CASCADE,
  CONSTRAINT `facturedetail_ibfk_2` FOREIGN KEY (`idProduit`) REFERENCES `produit` (`idProduit`),
  CONSTRAINT `facturedetail_ibfk_3` FOREIGN KEY (`idPrestation`) REFERENCES `prestation` (`idPrestation`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `facturedetail` WRITE;
/*!40000 ALTER TABLE `facturedetail` DISABLE KEYS */;
INSERT INTO `facturedetail` VALUES (8,15,NULL,65,1,5000.00);
/*!40000 ALTER TABLE `facturedetail` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `prestation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prestation` (
  `idPrestation` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(150) NOT NULL,
  `prix` decimal(10,2) NOT NULL,
  `dureeMinutes` int(11) NOT NULL,
  `categorie` varchar(100) DEFAULT NULL,
  `description` text DEFAULT NULL,
  `actif` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`idPrestation`)
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `prestation` WRITE;
/*!40000 ALTER TABLE `prestation` DISABLE KEYS */;
INSERT INTO `prestation` VALUES (64,'Coupe femme',3000.00,45,'Coiffure',NULL,1),(65,'Tressage simple',5000.00,90,'Coiffure',NULL,1),(66,'Manucure classique',2500.00,30,'Onglerie',NULL,1),(67,'Pédicure complète',3500.00,45,'Onglerie',NULL,1),(68,'Soin du visage hydratant',6000.00,60,'Soin visage',NULL,1),(69,'Coupe homme',1500.00,20,'Coiffure',NULL,1);
/*!40000 ALTER TABLE `prestation` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `produit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `produit` (
  `idProduit` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(150) NOT NULL,
  `categorie` varchar(50) DEFAULT NULL,
  `prix` decimal(10,2) NOT NULL,
  `stockActuel` int(11) NOT NULL DEFAULT 0,
  `seuilAlerte` int(11) NOT NULL DEFAULT 5,
  `description` text DEFAULT NULL,
  `actif` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`idProduit`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `produit` WRITE;
/*!40000 ALTER TABLE `produit` DISABLE KEYS */;
INSERT INTO `produit` VALUES (60,'Shampoing Pantene','Soin cheveux',2500.00,10,3,'',1),(61,'Crème hydratante Nivea','Hygiène & soin',3000.00,8,2,'',1),(62,'Vernis à ongles OPI','Onglerie',1500.00,15,5,'',1),(63,'Huile de coco','Soin cheveux',2000.00,2,3,'',1),(64,'Gel coiffant','Soin cheveux',1800.00,6,2,'',1);
/*!40000 ALTER TABLE `produit` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `rendezvous`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rendezvous` (
  `idRdv` int(11) NOT NULL AUTO_INCREMENT,
  `idClient` int(11) NOT NULL,
  `idEmploye` int(11) NOT NULL,
  `idPrestation` int(11) NOT NULL,
  `dateHeureDebut` datetime NOT NULL,
  `dateHeureFin` datetime NOT NULL,
  `statut` varchar(50) DEFAULT 'Confirmé',
  PRIMARY KEY (`idRdv`),
  KEY `idClient` (`idClient`),
  KEY `idEmploye` (`idEmploye`),
  KEY `idPrestation` (`idPrestation`),
  CONSTRAINT `rendezvous_ibfk_1` FOREIGN KEY (`idClient`) REFERENCES `client` (`idClient`),
  CONSTRAINT `rendezvous_ibfk_2` FOREIGN KEY (`idEmploye`) REFERENCES `employe` (`idEmploye`),
  CONSTRAINT `rendezvous_ibfk_3` FOREIGN KEY (`idPrestation`) REFERENCES `prestation` (`idPrestation`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `rendezvous` WRITE;
/*!40000 ALTER TABLE `rendezvous` DISABLE KEYS */;
INSERT INTO `rendezvous` VALUES (15,37,19,65,'2026-06-04 22:29:45','2026-06-04 23:59:45','Confirmé'),(16,37,20,66,'2026-06-04 22:30:41','2026-06-04 23:00:41','Confirmé'),(17,38,22,69,'2026-06-04 22:37:09','2026-06-04 22:57:09','En attente'),(18,39,21,68,'2026-06-04 23:05:01','2026-06-05 00:05:01','En attente'),(19,37,22,66,'2026-06-08 06:16:22','2026-06-08 06:46:22','En attente'),(20,40,19,64,'2026-06-08 12:40:31','2026-06-08 13:25:31','Terminé');
/*!40000 ALTER TABLE `rendezvous` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `utilisateur`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `utilisateur` (
  `idUser` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(100) NOT NULL,
  `prenom` varchar(100) NOT NULL,
  `nomUtilisateur` varchar(100) NOT NULL,
  `motDePasse` varchar(255) NOT NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE KEY `nomUtilisateur` (`nomUtilisateur`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `utilisateur` WRITE;
/*!40000 ALTER TABLE `utilisateur` DISABLE KEYS */;
INSERT INTO `utilisateur` VALUES (2,'Akakpo','Célestine','gerante','rosa2024');
/*!40000 ALTER TABLE `utilisateur` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

