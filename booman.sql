CREATE DATABASE booman;

USE booman;

CREATE TABLE account(
	phone VARCHAR(255) NOT NULL PRIMARY KEY,
  	email VARCHAR(255) NULL,
  	password VARCHAR(255) NULL,
  	full_name VARCHAR(255) NULL,
  	role VARCHAR(255) NULL
);
INSERT INTO account (phone, email, password, full_name, role) VALUES
('0987654321', 'john.doe@example.com', 'pass123', 'John Doe', 'admin'),
('0123456789', 'jane.doe@example.com', 'pass456', 'Jane Doe', 'manager'),
('0998877665', 'alice.smith@example.com', 'pass789', 'Alice Smith', 'accountant'),
('0888777666', 'bob.johnson@example.com', 'passabc', 'Bob Johnson', 'receptionist'),
('0777666555', 'emma.davis@example.com', 'passdef', 'Emma Davis', 'receptionist');

CREATE TABLE room(
	room_num VARCHAR(255) NOT NULL PRIMARY KEY,
  	room_type VARCHAR(255) NULL,
  	price DECIMAL NULL,
  	status VARCHAR(255) NULL
);
INSERT INTO room(room_num, room_type, price, status) VALUES
('P001', 'standard', 800000, 'empty'),
('P002', 'standard', 800000, 'empty'),
('P003', 'standard', 800000, 'empty'),
('P004', 'standard', 800000, 'empty'),
('P005', 'standard', 800000, 'empty');

CREATE TABLE service(
	id VARCHAR(255) NOT NULL PRIMARY KEY,
  	service_name VARCHAR(255) NULL,
  	description VARCHAR(255) NULL,
  	price DECIMAL NULL
);
INSERT INTO service(id, service_name, description, price) VALUES
('DV001', 'dich vu 1', 'test dich vu 1', 30000),
('DV002', 'dich vu 2', 'test dich vu 2', 40000),
('DV003', 'dich vu 3', 'test dich vu 3', 50000),
('DV004', 'dich vu 4', 'test dich vu 4', 60000),
('DV005', 'dich vu 5', 'test dich vu 5', 90000);

CREATE TABLE room_services(
	room_id	VARCHAR(255) NOT NULL,
  	service_id VARCHAR(255) NOT NULL,
  	quantity INT NULL,
  	FOREIGN KEY (room_id) REFERENCES room(room_num),
  	FOREIGN KEY (service_id) REFERENCES service(id)
);
INSERT INTO room_services(room_id, service_id, quantity) VALUES
('P003', 'DV001', 2),
('P003', 'DV003', 2),
('P003', 'DV004', 1),
('P005', 'DV002', 3),
('P005', 'DV003', 3);

CREATE TABLE customer(
	id VARCHAR(255) NOT NULL PRIMARY KEY,
  	customer_name VARCHAR(255) NULL,
  	phone VARCHAR(255) NULL,
  	email VARCHAR(255) NULL,
  	address VARCHAR(255) NULL
);
INSERT INTO customer(id, customer_name, phone, email, address) VALUES
('KH000001', 'John Doe', '0987654321', 'john.doe@example.com', '123 Main Street'),
('KH000002', 'Jane Doe', '0123456789', 'jane.doe@example.com', '456 Elm Street'),
('KH000003', 'Alice Smith', '0998877665', 'alice.smith@example.com', '789 Oak Street'),
('KH000004', 'Bob Johnson', '0888777666', 'bob.johnson@example.com', '1011 Pine Street'),
('KH000005', 'Emma Davis', '0777666555', 'emma.davis@example.com', '1213 Maple Street');

CREATE TABLE booking(
	id VARCHAR(255) NOT NULL PRIMARY KEY,
  	customer_id VARCHAR(255) NOT NULL,
  	booking_date DATETIME NULL,
  	checkin_date DATETIME NULL,
  	stay_duration INT NULL,
  	checkout_date DATETIME NULL,
  	act_checkin_time DATETIME NULL,
  	act_checkout_time DATETIME NULL,
  	special_request VARCHAR(255) NULL,
  	status VARCHAR(255) NULL
);
INSERT INTO booking(id, customer_id, booking_date, checkin_date, stay_duration, checkout_date, act_checkin_time, act_checkout_time, special_request, status) VALUES
('BKI000001', 'KH000001', '2024-03-10 10:24:37', '2024-03-20', 3, '2024-03-23', NULL, NULL, 'test', 'depositing'),
('BKI000002', 'KH000001', '2024-03-10 11:15:48', '2024-03-22', 5, '2024-03-27', NULL, NULL, 'test', 'depositing'),
('BKI000003', 'KH000001', '2024-03-10 12:36:19', '2024-03-23', 4, '2024-03-27', NULL, NULL, 'test', 'checkin'),
('BKI000004', 'KH000001', '2024-03-10 13:51:22', '2024-03-24', 3, '2024-03-27', NULL, NULL, 'test', 'depositing'),
('BKI000005', 'KH000001', '2024-03-10 14:29:44', '2024-03-25', 4, '2024-03-29', NULL, NULL, 'test', 'checkin');

CREATE TABLE booked_rooms(
	booking_id VARCHAR(255) NOT NULL,
  	room_num VARCHAR(255) NOT NULL,
  	FOREIGN KEY (booking_id) REFERENCES booking(id),
  	FOREIGN KEY (room_num) REFERENCES room(room_num)
);
INSERT INTO booked_rooms(booking_id, room_num) VALUES
('BKI000001', 'P001'),
('BKI000002', 'P005'),
('BKI000003', 'P002'),
('BKI000004', 'P004'),
('BKI000005', 'P003');