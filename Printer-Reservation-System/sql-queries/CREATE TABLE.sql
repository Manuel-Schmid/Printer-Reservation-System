USE [3D_Drucker];

DROP TABLE IF EXISTS tbl_Gottadmin

CREATE TABLE tbl_Gottadmin (
	GottadminID INT NOT NULL IDENTITY(1, 1),
	ID_Student INT NOT NULL,
	PRIMARY KEY (GottadminID),
	FOREIGN KEY (ID_Student) REFERENCES tbl_Student(ID)
);
