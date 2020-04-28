-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 28 Kwi 2020, 14:35
-- Wersja serwera: 10.4.11-MariaDB
-- Wersja PHP: 7.4.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `unity`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `account`
--

CREATE TABLE `account` (
  `Id` int(11) NOT NULL,
  `Login` varchar(50) COLLATE utf8_polish_ci NOT NULL,
  `Password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_polish_ci NOT NULL,
  `Active` tinyint(1) NOT NULL,
  `HP` int(11) NOT NULL,
  `Attack` int(11) NOT NULL,
  `Attack Speed` int(11) NOT NULL,
  `Defense` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `armor`
--

CREATE TABLE `armor` (
  `Id` int(11) NOT NULL,
  `Type` varchar(15) COLLATE utf8_polish_ci NOT NULL,
  `HP` int(11) NOT NULL,
  `Defense` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `armor`
--

INSERT INTO `armor` (`Id`, `Type`, `HP`, `Defense`) VALUES
(6, 'Chest', 5, 5),
(7, 'Chest', 6, 5),
(8, 'Chest', 7, 12),
(9, 'Chest', 15, 5),
(10, 'Chest', 20, 20),
(11, 'Chest', 20, 30),
(12, 'Head', 2, 3),
(13, 'Head', 6, 4),
(14, 'Head', 7, 10),
(15, 'Head', 12, 3),
(16, 'Head', 10, 10),
(17, 'Head', 10, 15),
(18, 'Legs', 3, 4),
(19, 'Legs', 1, 6),
(20, 'Legs', 5, 4),
(21, 'Legs', 6, 8),
(22, 'Legs', 3, 11),
(23, 'Legs', 6, 10),
(24, 'Shield', 1, 12),
(25, 'Shield', 3, 14),
(26, 'Shield', 3, 16),
(27, 'Shield', 7, 20),
(28, 'Shield', 15, 40);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `food`
--

CREATE TABLE `food` (
  `Id` int(11) NOT NULL,
  `Type` varchar(20) COLLATE utf8_polish_ci NOT NULL,
  `HP Recover` int(11) NOT NULL,
  `Poison` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `food`
--

INSERT INTO `food` (`Id`, `Type`, `HP Recover`, `Poison`) VALUES
(1, 'Apple', 10, 0),
(2, 'Meat', 15, 0),
(3, 'Mushroom', 0, 5),
(4, 'Bread', 5, 0),
(5, 'Frog', 0, 20),
(6, 'Onion', 5, 5);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `materials`
--

CREATE TABLE `materials` (
  `id` int(11) NOT NULL,
  `Name` varchar(15) COLLATE utf8_polish_ci NOT NULL,
  `Resistance` int(11) NOT NULL,
  `Stacks` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `materials`
--

INSERT INTO `materials` (`id`, `Name`, `Resistance`, `Stacks`) VALUES
(1, 'Tree', 5, 3),
(4, 'Tree', 4, 2),
(5, 'Tree', 6, 5),
(6, 'Stone', 6, 3),
(7, 'Stone', 10, 5),
(8, 'Stone', 6, 3),
(9, 'Stone', 10, 5),
(10, 'Stone', 6, 3),
(11, 'Gold', 5, 1),
(12, 'Stone', 8, 2),
(13, 'Gold', 11, 3),
(14, 'Iron', 7, 2),
(15, 'Iron', 10, 4),
(16, 'Iron', 11, 5),
(17, 'Iron', 4, 1),
(18, 'Leather', 5, 1),
(19, 'Leather', 6, 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `player`
--

CREATE TABLE `player` (
  `Id` int(11) NOT NULL,
  `Nickname` varchar(20) COLLATE utf8_polish_ci NOT NULL DEFAULT 'Project',
  `Password` varchar(20) COLLATE utf8_polish_ci NOT NULL DEFAULT '12345',
  `HP` int(11) NOT NULL DEFAULT 10,
  `Attack` int(11) NOT NULL DEFAULT 5,
  `SpeedAtack` int(11) NOT NULL DEFAULT 5,
  `Speed` int(11) NOT NULL DEFAULT 3,
  `Defense` int(11) NOT NULL DEFAULT 7
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `player`
--

INSERT INTO `player` (`Id`, `Nickname`, `Password`, `HP`, `Attack`, `SpeedAtack`, `Speed`, `Defense`) VALUES
(1, 'Gracz', '12345', 10, 5, 5, 3, 7);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `weapon`
--

CREATE TABLE `weapon` (
  `Id` int(11) NOT NULL,
  `Type` varchar(15) COLLATE utf8_polish_ci NOT NULL,
  `Attack` int(11) NOT NULL,
  `Speed Attack` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `weapon`
--

INSERT INTO `weapon` (`Id`, `Type`, `Attack`, `Speed Attack`) VALUES
(2, 'Sword', 7, 5),
(3, 'Sword', 10, 5),
(4, 'Sword ', 12, 10),
(7, 'Gun', 4, 3),
(8, 'Gun', 6, 3),
(11, 'Gun', 9, 5),
(12, 'Axe', 13, 4);

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `armor`
--
ALTER TABLE `armor`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `food`
--
ALTER TABLE `food`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `materials`
--
ALTER TABLE `materials`
  ADD PRIMARY KEY (`id`);

--
-- Indeksy dla tabeli `player`
--
ALTER TABLE `player`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `weapon`
--
ALTER TABLE `weapon`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT dla tabeli `account`
--
ALTER TABLE `account`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `armor`
--
ALTER TABLE `armor`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT dla tabeli `food`
--
ALTER TABLE `food`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT dla tabeli `materials`
--
ALTER TABLE `materials`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT dla tabeli `player`
--
ALTER TABLE `player`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT dla tabeli `weapon`
--
ALTER TABLE `weapon`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
