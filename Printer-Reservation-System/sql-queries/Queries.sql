﻿/* ***************************************************************************** */
/* Alter Database
*/


ALTER TABLE tbl_Student
ALTER COLUMN Passwort VARCHAR(100);
ALTER TABLE tbl_Student
ADD UNIQUE (eMail);


/* ***************************************************************************** */
/* SELECT all printers
*/

DROP PROC IF EXISTS spSelectPrinters;
GO 
CREATE PROC spSelectPrinters 
AS
SELECT Marke, Modell, Typ, Beschreibung, Druckbereich_Breite AS 'Breite Druckbereich', Druckbereich_Laenge AS 'Länge Druckbereich', Druckbereich_Hoehe AS 'Höhe Druckbereich'  FROM tbl_Drucker 
ORDER BY ID;

/*
EXEC spSelectPrinters;
*/


/* ***************************************************************************** */
/*SELECT all students
*/

DROP PROC IF EXISTS spSelectStudents;
GO 
CREATE PROC spSelectStudents 
AS
SELECT Name, Vorname, eMail AS 'E-Mail', Handy, Bemerkung, IsAdmin AS 'Admin' FROM tbl_Student
ORDER BY tbl_Student.ID;

/*
EXEC spSelectStudents;
*/

/* ***************************************************************************** */
/*SELECT all students
*/

DROP PROC IF EXISTS spSelectStudentStatus;
GO 
CREATE PROC spSelectStudentStatus 
(
	@eMail VARCHAR(50)
)
AS
SELECT Status FROM tbl_Status
JOIN tbl_Student ON tbl_Status.ID = ID_Status
WHERE tbl_Student.eMail = @eMail;

/*
EXEC spSelectStudentStatus @eMail = 'manysch3@gmail.com';
*/

/* ***************************************************************************** */
/*SELECT all reservations
*/


DROP PROC IF EXISTS spSelectReservations;
GO 
CREATE PROC spSelectReservations 
AS
SELECT stu.Name, stu.Vorname, pri.Marke AS Druckermarke, pri.Modell AS Druckermodell, Von, Bis, res.Bemerkung FROM tbl_Reservation AS res
JOIN tbl_Student AS stu ON res.ID_Student = stu.ID
JOIN tbl_Drucker AS pri ON res.ID_Drucker = pri.ID
ORDER BY res.ID;

/*
EXEC spSelectReservations;
*/


/*
DROP PROC IF EXISTS spInsertStudent;
GO 
CREATE PROC spInsertStudent
(
	@Name VARCHAR(50),
	@Vorname VARCHAR(50),
	@eMail VARCHAR(50),
	@Handy VARCHAR(50),
	@Passwort VARCHAR(100)
)
AS
INSERT INTO tbl_Student (Name, Vorname, eMail, Handy, Passwort, IsAdmin, Bemerkung, ID_Status)
VALUES (@Name, @Vorname, @eMail, @Handy, @Passwort, 0, NULL, (SELECT ID FROM tbl_Status WHERE Status='Anfrage Registration'));
GO
*/
/*
EXEC spInsertStudent @Name='Lssse', @Vorname='Bao Minh', @eMail='bao.minh@gmail.com', @Handy='02949284972', @Passwort='96C34D848F3F576D6E43FB42D7B5DDBA12303D1280F6601448003B84C205B6B4';
*/

/*
DROP PROC IF EXISTS spValidateLogin;
GO 
CREATE PROC spValidateLogin
(
	@eMail VARCHAR(50),
	@Passwort VARCHAR(100)
)
AS
SELECT COUNT(ID) FROM tbl_Student WHERE eMail = @eMail and Passwort = @Passwort
GO
*/

/*
EXEC spValidateLogin @eMail='manysch3@gmail.com', @Passwort='CEBF2CB7F8D7C263837CF63E10CECB98A0560C181D34E6C4BDAB3F28E619CABC';
*/

/* ***************************************************************************** */
/*DELETE student
*/

DROP PROC IF EXISTS spDeleteStudent;
GO 
CREATE PROC spDeleteStudent 
(
	@eMail VARCHAR(50)
)
AS
DELETE FROM tbl_Student WHERE tbl_Student.eMail = @eMail;

EXEC spDeleteStudent @eMail='manuel.schmids@ksb-sg.ch';


/* ***************************************************************************** */
/* UPDATE student
*/

DROP PROC IF EXISTS spUpdateStudent;

GO 
CREATE PROC spUpdateStudent
(
	@Name VARCHAR(50),
	@Vorname VARCHAR(50),
	@currentEMail VARCHAR(50), /* how to change ? */
	@Handy VARCHAR(50),
	@Bemerkung TEXT,	
	@Status VARCHAR(50),
	@IsAdmin BIT
)
AS
UPDATE tbl_Student
SET    
Name = @Name,
Vorname = @Vorname,
/* eMail = @currentEMail, */
Handy = @Handy,
Bemerkung = @Bemerkung,
ID_Status = (SELECT tbl_Status.ID FROM tbl_Status WHERE tbl_Status.Status = @Status),
IsAdmin = @IsAdmin
WHERE tbl_Student.eMail = @currentEMail
GO

/*
EXEC spUpdateStudent @Name = 'se', @Vorname = 'be', @currentEMail = 'manysch3@gmail.com', @Handy = '019191919', @Bemerkung = 'weeew', @Status = 'Gesperrt', @IsAdmin = true;
*/


/* ***************************************************************************** */
/*SELECT all registrations (Students who have not yet been accepted)
*/

DROP PROC IF EXISTS spSelectRegistrations;
GO 
CREATE PROC spSelectRegistrations 
AS
SELECT Name, Vorname, eMail AS 'E-Mail', Handy FROM tbl_Student
WHERE ID_Status = 1 /* 1 means not yet accepted */
ORDER BY tbl_Student.ID;

/*
EXEC spSelectRegistrations;
*/


/* ***************************************************************************** */
/* UPDATE studentstatus
*/

DROP PROC IF EXISTS spUpdateStudentStatus;

GO 
CREATE PROC spUpdateStudentStatus
(
	@eMail VARCHAR(50),
	@NewStatusID INT
)
AS
UPDATE tbl_Student
SET    
ID_Status = @NewStatusID
WHERE tbl_Student.eMail = @eMail
GO