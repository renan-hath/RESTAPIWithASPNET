CREATE TABLE IF NOT EXISTS `persons` (
  `id` bigint NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `first_name` varchar(80) NOT NULL,
  `last_name` varchar(80) NOT NULL,
  `address` varchar(100) NOT NULL,
  `gender` varchar(6) NOT NULL
);