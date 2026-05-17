-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 08, 2026 at 09:15 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pabeo`
--

-- --------------------------------------------------------

--
-- Table structure for table `employee`
--

CREATE TABLE `employee` (
  `employee_id` int(11) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `position` varchar(50) NOT NULL,
  `contact_number` varchar(15) NOT NULL,
  `offiice_assignment` varchar(100) NOT NULL,
  `email_address` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `employee`
--

INSERT INTO `employee` (`employee_id`, `full_name`, `position`, `contact_number`, `offiice_assignment`, `email_address`, `password`) VALUES
(1, 'CHARLES KENDRICK UBANA', 'OFFICER', '09876543211', 'MAIN OFFICE', 'cha@gmail.com', '15E2B0D3C33891EBB0F1EF609EC419420C20E320CE94C65FBC8C3312448EB225'),
(2, 'JUAN DELA CRUZ', 'PABEO FIELD TECHNICIAN', '09123456781', 'MAIN OFFICE', 'juan@gmail.com', '15E2B0D3C33891EBB0F1EF609EC419420C20E320CE94C65FBC8C3312448EB225'),
(3, 'MARIA SANTOS', 'PABEO AGRICULTURAL OFFICER', '09123456782', 'MAIN OFFICE', 'maria@gmail.com', '15E2B0D3C33891EBB0F1EF609EC419420C20E320CE94C65FBC8C3312448EB225'),
(4, 'JOSE REYES', 'PABEO FARM INSPECTOR', '09123456783', 'MAIN OFFICE', 'jose@gmail.com', '15E2B0D3C33891EBB0F1EF609EC419420C20E320CE94C65FBC8C3312448EB225'),
(5, 'ANNA LOPEZ', 'PABEO DATA ENCODER', '09123456784', 'MAIN OFFICE', 'anna@gmail.com', '15E2B0D3C33891EBB0F1EF609EC419420C20E320CE94C65FBC8C3312448EB225'),
(6, 'MARK CRUZ', 'PABEO PROGRAM COORDINATOR', '09123456785', 'MAIN OFFICE', 'mark@gmail.com', '15E2B0D3C33891EBB0F1EF609EC419420C20E320CE94C65FBC8C3312448EB225'),
(109, 'NINA THERESSA RAGOS', 'PABEO OFFICER', '009876543211', 'MAIN OFFICE', 'nina@gmail.com', '15E2B0D3C33891EBB0F1EF609EC419420C20E320CE94C65FBC8C3312448EB225');

-- --------------------------------------------------------

--
-- Table structure for table `farmer`
--

CREATE TABLE `farmer` (
  `farmer_id` int(50) NOT NULL,
  `full_name` varchar(255) NOT NULL,
  `birth_date` date NOT NULL,
  `email` varchar(100) DEFAULT NULL,
  `contact_number` varchar(11) NOT NULL,
  `residence_address` text NOT NULL,
  `farm_location` text NOT NULL,
  `classification` varchar(50) NOT NULL,
  `registration_status` varchar(50) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `machinery`
--

CREATE TABLE `machinery` (
  `machinery_id` int(11) NOT NULL,
  `machinery_name` varchar(100) NOT NULL,
  `machinery_type` varchar(50) NOT NULL,
  `station_id` int(11) NOT NULL,
  `condition` varchar(50) NOT NULL,
  `availability_status` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `machinery`
--

INSERT INTO `machinery` (`machinery_id`, `machinery_name`, `machinery_type`, `station_id`, `condition`, `availability_status`) VALUES
(14, 'WALK BEHIND TRANPLANTER', 'TRANSPLANTER', 1, 'GOOD', 'AVAILABLE'),
(15, 'DC35 TRACTOR', 'TRACTOR', 1, 'GOOD', 'AVAILABLE'),
(16, 'DC70 TRACTOR', 'TRACTOR', 1, 'GOOD', 'AVAILABLE'),
(17, 'DC60 TRACTOR', 'TRACTOR', 1, 'GOOD', 'AVAILABLE'),
(18, 'M9540 TRACTOR', 'TRACTOR', 1, 'GOOD', 'AVAILABLE'),
(21, 'RIDE-IN TYPE TRANSPLANTER', 'TRANSPLANTER', 2, 'GOOD', 'AVAILABLE'),
(22, 'FLATBED DRYER', 'DRYER', 2, 'GOOD', 'AVAILABLE'),
(23, 'M9540 TRACTOR', 'TRACTOR', 2, 'GOOD', 'AVAILABLE'),
(24, 'TYM TRACTOR WITH ATTACHMENTS', 'TRACTOR', 2, 'GOOD', 'AVAILABLE'),
(25, 'CORN SHELLER', 'SHELLER', 2, 'GOOD', 'AVAILABLE');

-- --------------------------------------------------------

--
-- Table structure for table `operator`
--

CREATE TABLE `operator` (
  `operator_id` int(11) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `position` varchar(50) NOT NULL,
  `contact_number` varchar(15) NOT NULL,
  `station_id` int(11) NOT NULL,
  `availability_status` varchar(50) DEFAULT 'AVAILABLE'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `operator`
--

INSERT INTO `operator` (`operator_id`, `full_name`, `position`, `contact_number`, `station_id`, `availability_status`) VALUES
(7, 'JUAN DELA CRUZ', 'MACHINERY OPERATOR', '09171234567', 3, 'AVAILABLE'),
(8, 'PEDRO SANTOS', 'MACHINERY OPERATOR', '09181234567', 3, 'AVAILABLE'),
(9, 'CARLO REYES', 'MACHINERY OPERATOR', '09191234567', 3, 'CURRENTLY OPERATING MACHINE'),
(10, 'MARIO GARCIA', 'MACHINERY OPERATOR', '09201234567', 4, 'AVAILABLE'),
(11, 'ROBERTO CRUZ', 'MACHINERY OPERATOR', '09211234567', 4, 'AVAILABLE'),
(12, 'ANTONIO RAMOS', 'MACHINERY OPERATOR', '09221234567', 4, 'AVAILABLE');

-- --------------------------------------------------------

--
-- Table structure for table `service`
--

CREATE TABLE `service` (
  `service_id` int(11) NOT NULL,
  `service_name` varchar(100) NOT NULL,
  `service_type` varchar(50) NOT NULL,
  `description` text NOT NULL,
  `machinery_id` int(100) NOT NULL,
  `policy_limit` varchar(100) NOT NULL,
  `employee_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `service`
--

INSERT INTO `service` (`service_id`, `service_name`, `service_type`, `description`, `machinery_id`, `policy_limit`, `employee_id`) VALUES
(9, 'Land Preparation Service', 'Land Preparation', 'Soil preparation including plowing, harrowing, and field conditioning.', 15, 'Max 5 hectares per request', 1),
(10, 'Rice Planting Service', 'Planting', 'Transplanting of rice seedlings with proper spacing and alignment.', 14, 'Max 5 hectares per request', 2),
(11, 'Rice Harvesting Service', 'Harvesting', 'Harvesting of rice crops including cutting, threshing, and cleaning.', 16, 'Max 5 hectares per day', 3),
(12, 'Corn Harvesting Service', 'Harvesting', 'Harvesting of corn crops using efficient field operations.', 17, 'Max 4 hectares per day', 4),
(13, 'Corn Shelling Service', 'Post-Harvest', 'Separation of corn kernels from cobs for faster processing.', 25, 'Max 1000 kg per batch', 5),
(14, 'Grain Drying Service', 'Post-Harvest', 'Drying of harvested grains to reduce moisture and maintain quality.', 22, 'Max 2000 kg per cycle', 6),
(15, 'Soil Tilling Service', 'Land Preparation', 'Loosening and turning of soil to prepare for planting.', 18, 'Max 4 hectares per request', 1),
(16, 'Field Excavation Service', 'Land Preparation', 'Digging and trenching for farm development and irrigation setup.', 24, 'Max 3 hectares per request', 2);

-- --------------------------------------------------------

--
-- Table structure for table `service_request`
--

CREATE TABLE `service_request` (
  `farmer_id` int(50) NOT NULL,
  `service_id` int(11) NOT NULL,
  `request_date` date NOT NULL,
  `farm_location` varchar(150) NOT NULL,
  `hectares_served` decimal(5,2) NOT NULL,
  `validation_date` date DEFAULT NULL,
  `service_status` varchar(50) NOT NULL,
  `request_id` int(11) NOT NULL,
  `station_id` int(11) DEFAULT NULL,
  `operator_id` int(11) DEFAULT NULL,
  `assigned_machinery_id` int(11) DEFAULT NULL,
  `assignment_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `station`
--

CREATE TABLE `station` (
  `station_id` int(11) NOT NULL,
  `station_name` varchar(100) NOT NULL,
  `location` varchar(150) NOT NULL,
  `description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `station`
--

INSERT INTO `station` (`station_id`, `station_name`, `location`, `description`) VALUES
(3, 'PROVINCIAL FARM', 'CALASGASAN, DAET, CAMARINES NORTE', 'STATION 1'),
(4, 'MULTI-CORP PROCESSING', 'STO. DOMINGO, VINZONS, CAMARINES NORTE', 'STATION 2');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`employee_id`);

--
-- Indexes for table `farmer`
--
ALTER TABLE `farmer`
  ADD PRIMARY KEY (`farmer_id`);

--
-- Indexes for table `machinery`
--
ALTER TABLE `machinery`
  ADD PRIMARY KEY (`machinery_id`);

--
-- Indexes for table `operator`
--
ALTER TABLE `operator`
  ADD PRIMARY KEY (`operator_id`),
  ADD KEY `fk_operator_station` (`station_id`);

--
-- Indexes for table `service`
--
ALTER TABLE `service`
  ADD PRIMARY KEY (`service_id`),
  ADD KEY `fk_service_employee` (`employee_id`);

--
-- Indexes for table `service_request`
--
ALTER TABLE `service_request`
  ADD PRIMARY KEY (`request_id`),
  ADD KEY `fk_request_service` (`service_id`),
  ADD KEY `fk_request_farmer` (`farmer_id`);

--
-- Indexes for table `station`
--
ALTER TABLE `station`
  ADD PRIMARY KEY (`station_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `employee`
--
ALTER TABLE `employee`
  MODIFY `employee_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=111;

--
-- AUTO_INCREMENT for table `farmer`
--
ALTER TABLE `farmer`
  MODIFY `farmer_id` int(50) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `machinery`
--
ALTER TABLE `machinery`
  MODIFY `machinery_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=209;

--
-- AUTO_INCREMENT for table `operator`
--
ALTER TABLE `operator`
  MODIFY `operator_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `service`
--
ALTER TABLE `service`
  MODIFY `service_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=68;

--
-- AUTO_INCREMENT for table `service_request`
--
ALTER TABLE `service_request`
  MODIFY `request_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `station`
--
ALTER TABLE `station`
  MODIFY `station_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `operator`
--
ALTER TABLE `operator`
  ADD CONSTRAINT `fk_operator_station` FOREIGN KEY (`station_id`) REFERENCES `station` (`station_id`);

--
-- Constraints for table `service`
--
ALTER TABLE `service`
  ADD CONSTRAINT `fk_service_employee` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`);

--
-- Constraints for table `service_request`
--
ALTER TABLE `service_request`
  ADD CONSTRAINT `fk_request_farmer` FOREIGN KEY (`farmer_id`) REFERENCES `farmer` (`farmer_id`),
  ADD CONSTRAINT `fk_request_service` FOREIGN KEY (`service_id`) REFERENCES `service` (`service_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
