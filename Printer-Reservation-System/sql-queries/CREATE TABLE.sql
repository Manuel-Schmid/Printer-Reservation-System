USE [3D_Drucker];

DROP TABLE IF EXISTS tbl_SupremeAdmin

CREATE TABLE tbl_SupremeAdmin (
	SupremeAdminID INT NOT NULL IDENTITY(1, 1),
	ID_Student INT NOT NULL,
	PRIMARY KEY (SupremeAdminID),
	FOREIGN KEY (ID_Student) REFERENCES tbl_Student(ID)
);

INSERT INTO tbl_SupremeAdmin (ID_Student)
VALUES (14);