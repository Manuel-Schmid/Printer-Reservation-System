/* ***************************************************************************** */
/* Alter Database
*/


ALTER TABLE tbl_Student
ALTER COLUMN Passwort VARCHAR(100);
ALTER TABLE tbl_Student
ADD UNIQUE (eMail);


/* ***************************************************************************** */
/*SELECT all students which are not in registration process
*/

DROP PROC IF EXISTS spSelectStudents;
GO 
CREATE PROC spSelectStudents 
AS
SELECT Name, Vorname, eMail AS 'E-Mail', Handy, Bemerkung, ID_Status as 'Stat_ID', Status, IsAdmin AS 'Admin' FROM tbl_Student
JOIN tbl_Status ON ID_Status = tbl_Status.ID
WHERE ID_Status != 1
ORDER BY tbl_Student.ID;
GO

/*
EXEC spSelectStudents;
*/


/* ***************************************************************************** */
/*SELECT all students names
*/

DROP PROC IF EXISTS spSelectStudentNames;
GO 
CREATE PROC spSelectStudentNames 
AS
SELECT CAST(Vorname AS VARCHAR(16)) + ' ' + CAST(Name AS VARCHAR(16)) as 'Schueler' FROM tbl_Student
ORDER BY ID;
GO

/*
EXEC spSelectStudentNames;
*/


/* ***************************************************************************** */
/*SELECT if student is admin
*/

DROP PROC IF EXISTS spSelectIsStudentAdmin;
GO 
CREATE PROC spSelectIsStudentAdmin 
(
	@eMail VARCHAR(50)
)
AS
SELECT COUNT(IsAdmin) FROM tbl_Student
WHERE eMail = @eMail AND IsAdmin = 1;
GO

/*
EXEC spSelectIsStudentAdmin @eMail = 'test@test.com';
*/

/* ***************************************************************************** */
/*SELECT student status as text
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
GO

/*
EXEC spSelectStudentStatus @eMail = 'manysch3@gmail.com';
*/

/* ***************************************************************************** */
/* INSERT new student
*/


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

/*
EXEC spInsertStudent @Name='Lssse', @Vorname='Bao Minh', @eMail='bao.minh@gmail.com', @Handy='02949284972', @Passwort='96C34D848F3F576D6E43FB42D7B5DDBA12303D1280F6601448003B84C205B6B4';
*/


/* ***************************************************************************** */
/* Validate student login
*/

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
GO

/*
EXEC spDeleteStudent @eMail='fck.dich@steffen.ch';
*/

/* ***************************************************************************** */
/* UPDATE student
*/

DROP PROC IF EXISTS spUpdateStudent;
GO 
CREATE PROC spUpdateStudent
(
	@Name VARCHAR(50),
	@Vorname VARCHAR(50),
	@currentEMail VARCHAR(50), 
	@Handy VARCHAR(50),
	@Bemerkung TEXT,	
	@Status INT,
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
ID_Status = @Status,
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
GO

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
SET tbl_Student.ID_Status = @NewStatusID
WHERE tbl_Student.eMail = @eMail;
GO

/*
EXEC spUpdateStudentStatus @eMail = 'aösdf', @NewStatusID = 2;
*/


/* ***************************************************************************** */
/*SELECT all statuses
*/

DROP PROC IF EXISTS spSelectStatuses;
GO 
CREATE PROC spSelectStatuses 
AS
SELECT Status, ID as 'Status_ID' FROM tbl_Status
ORDER BY ID;
GO

/*
EXEC spSelectStatuses;
*/


/* ***************************************************************************** */
/* SELECT all printers
*/


DROP PROC IF EXISTS spSelectPrinters;
GO 
CREATE PROC spSelectPrinters 
AS
SELECT ID, Marke, Modell, Typ, Beschreibung, CAST(Druckbereich_Laenge AS VARCHAR(16)) + ' x ' + CAST(Druckbereich_Breite AS VARCHAR(16)) + ' x ' + CAST(Druckbereich_Hoehe AS VARCHAR(16)) as 'Druckbereich'  FROM tbl_Drucker 
ORDER BY ID;
GO

/*
EXEC spSelectPrinters;
*/

/* ***************************************************************************** */
/*DELETE printer
*/

DROP PROC IF EXISTS spDeletePrinter;
GO 
CREATE PROC spDeletePrinter 
(
	@ID INT
)
AS
DELETE FROM tbl_Reservation WHERE tbl_Reservation.ID_Drucker = @ID;
DELETE FROM tbl_Drucker WHERE tbl_Drucker.ID = @ID;
GO

/*
EXEC spDeletePrinter @eMail='manuel.schmids@ksb-sg.ch';
*/

/* ***************************************************************************** */
/* UPDATE printer
*/

DROP PROC IF EXISTS spUpdatePrinter;
GO 
CREATE PROC spUpdatePrinter
(
	@ID INT,
	@Marke VARCHAR(50),
	@Modell VARCHAR(50),
	@Typ VARCHAR(50), 
	@Beschreibung VARCHAR(50),
	@Druckbereich_Laenge FLOAT(53),
	@Druckbereich_Breite FLOAT(53),
	@Druckbereich_Hoehe FLOAT(53)
)
AS
UPDATE tbl_Drucker
SET    
Marke = @Marke,
Modell = @Modell,
Typ = @Typ,
Beschreibung = @Beschreibung,
Druckbereich_Laenge = @Druckbereich_Laenge,
Druckbereich_Breite = @Druckbereich_Breite,
Druckbereich_Hoehe = @Druckbereich_Hoehe
WHERE tbl_Drucker.ID = @ID
GO


/* ***************************************************************************** */
/* INSERT new printer
*/

DROP PROC IF EXISTS spInsertPrinter;
GO 
CREATE PROC spInsertPrinter
(
	@Marke VARCHAR(50),
	@Modell VARCHAR(50),
	@Typ VARCHAR(50), 
	@Beschreibung VARCHAR(50),
	@Druckbereich_Laenge FLOAT(53),
	@Druckbereich_Breite FLOAT(53),
	@Druckbereich_Hoehe FLOAT(53)
)
AS
INSERT INTO tbl_Drucker (Marke, Modell, Typ, Beschreibung, Druckbereich_Laenge, Druckbereich_Breite, Druckbereich_Hoehe, Bemerkung)
VALUES (@Marke, @Modell, @Typ, @Beschreibung, @Druckbereich_Laenge, @Druckbereich_Breite, @Druckbereich_Hoehe, NULL)
GO


/* ***************************************************************************** */
/* SELECT all reservations
*/

DROP PROC IF EXISTS spSelectReservations;
GO
CREATE PROC spSelectReservations 
AS
SELECT res.ID, stu.Name, stu.Vorname, res.ID_Drucker, CAST(dru.Marke AS VARCHAR(16)) + ' ' + CAST(dru.Modell AS VARCHAR(16)) as 'Drucker', (FORMAT (res.Von, 'dd.MM.yy hh:mm')) as 'Von', (FORMAT (res.Bis, 'dd.MM.yy hh:mm')) as 'Bis', res.Bemerkung  FROM tbl_Reservation as res
JOIN tbl_Student as stu on stu.ID = res.ID_Student
JOIN tbl_Drucker as dru on dru.ID = res.ID_Drucker
ORDER BY res.ID;
GO

/*
EXEC spSelectReservations;
*/

/* ***************************************************************************** */
/*DELETE reservation
*/

DROP PROC IF EXISTS spDeleteReservation;
GO 
CREATE PROC spDeleteReservation 
(
	@ID INT
)
AS
DELETE FROM tbl_Reservation WHERE tbl_Reservation.ID = @ID;
GO

/*
EXEC spDeleteReservation @eMail='manuel.schmids@ksb-sg.ch';
*/

/* ***************************************************************************** */
/* UPDATE reservation
*/

DROP PROC IF EXISTS spUpdateReservation;
GO 
CREATE PROC spUpdateReservation
(
	@ID INT,
	@ID_Drucker INT,
	@Von DATETIME, 
	@Bis DATETIME,
	@Bemerkung TEXT
)
AS
UPDATE tbl_Reservation
SET
ID_Drucker = @ID_Drucker,
Von = @Von,
Bis = @Bis,
Bemerkung = @Bemerkung
WHERE tbl_Reservation.ID = @ID;
GO


/* ***************************************************************************** */
/* INSERT new reservation
*/


DROP PROC IF EXISTS spInsertReservation;
GO
CREATE PROC spInsertReservation
(
	@ID_Drucker INT,
	@Student_eMail VARCHAR(50),
	@Von DATETIME, 
	@Bis DATETIME,
	@Bemerkung TEXT
)
AS
INSERT INTO tbl_Reservation (ID_Drucker, ID_Student, Von, Bis, Bemerkung)
VALUES (@ID_Drucker, (SELECT tbl_Student.ID FROM tbl_Student WHERE eMail = @Student_eMail), @Von, @Bis, @Bemerkung)
GO

/*
EXEC spInsertReservation @ID_Drucker=4, @Student_eMail='test@test.com', @Von='10.02.2021 10:30', @Bis='10.02.2021 10:45', @Bemerkung='test';
*/


/* ***************************************************************************** */
/*SELECT all printers for ddl
*/

DROP PROC IF EXISTS spSelectDDLPrinters;
GO 
CREATE PROC spSelectDDLPrinters 
AS
SELECT ID as 'DruckerID', CAST(dru.Marke AS VARCHAR(16)) + ' ' + CAST(dru.Modell AS VARCHAR(16)) as 'DruckerName' FROM tbl_Drucker as dru
ORDER BY ID;
GO

/*
EXEC spSelectDDLPrinters;
*/


/* ***************************************************************************** */
/* SELECT all Blocking Times
*/

DROP PROC IF EXISTS spSelectBlockingTimes;
GO
CREATE PROC spSelectBlockingTimes 
AS
SELECT sperr.ID, sperr_dru.ID_Drucker, CAST(dru.Marke AS VARCHAR(16)) + ' ' + CAST(dru.Modell AS VARCHAR(16)) as 'Drucker', sperr.Grund, (FORMAT (sperr.Von, 'dd.MM.yy hh:mm')) as 'Von', (FORMAT (sperr.Bis, 'dd.MM.yy hh:mm')) as 'Bis', sperr.Bemerkung FROM tbl_Sperrfenster as sperr
JOIN [tbl_Sperrfenster-Drucker] as sperr_dru on sperr.ID = sperr_dru.ID_Sperrfenster
JOIN tbl_Drucker as dru on dru.ID = sperr_dru.ID_Drucker
ORDER BY sperr.Von;
GO

/*
EXEC spSelectBlockingTimes;
*/


/* ***************************************************************************** */
/* SELECT all Exceptions from Blocking time
*/

DROP PROC IF EXISTS spSelectExceptions;
GO
CREATE PROC spSelectExceptions 
(@SperrfensterID INT)
AS
SELECT CAST(Vorname AS VARCHAR(16)) + ' ' + CAST(Name AS VARCHAR(16)) as 'Schueler' FROM tbl_Student as stu
JOIN tbl_SperrfensterAusnahmen as spa ON stu.ID = spa.ID_Student
JOIN tbl_Sperrfenster as sperr ON sperr.ID = spa.ID_Sperrfenster
WHERE spa.ID_Sperrfenster = @SperrfensterID
ORDER BY sperr.ID;
GO

/*
EXEC spSelectExceptions @SperrfensterID = 5;
*/


/* ***************************************************************************** */
/* INSERT new blocking time
*/
GO
DROP PROC IF EXISTS spInsertBlockingTime;
DROP TYPE IF EXISTS dbo.StudentList;

CREATE TYPE dbo.StudentList
AS TABLE
(
  StudentID INT ,
  Name VARCHAR(50),
  Vorname VARCHAR(50)
);

GO
CREATE PROC spInsertBlockingTime
(
	@Grund VARCHAR (50),
	@ID_Drucker INT,
	@Von DATETIME,
	@Bis DATETIME,
	@Schueler AS dbo.StudentList READONLY,
	@Bemerkung TEXT
)
AS

INSERT INTO tbl_Sperrfenster(Grund, Von, Bis, Bemerkung)
VALUES (@Grund, @Von, @Bis, @Bemerkung);

DECLARE @BlockingTimeID INT = (SELECT MAX(ID) FROM tbl_Sperrfenster);

INSERT INTO [tbl_Sperrfenster-Drucker](ID_Sperrfenster, ID_Drucker)
VALUES (@BlockingTimeID, @ID_Drucker);

/* --- */

DECLARE @RowCnt INT;
DECLARE @StudentID INT = 1;
DECLARE @Name VARCHAR(50);
DECLARE @Vorname VARCHAR(50);

SELECT @RowCnt = COUNT(*) FROM @Schueler;
 
WHILE @StudentID <= @RowCnt
BEGIN
	SELECT 
	  @Name = Name, 
	  @Vorname = Vorname 
	  FROM @Schueler WHERE StudentID = @StudentID
 
	  INSERT INTO [tbl_SperrfensterAusnahmen](ID_Sperrfenster, ID_Student)
	  VALUES (@BlockingTimeID, (SELECT ID FROM tbl_Student WHERE Name=@Name and Vorname=@Vorname));
 
   SET @StudentID += 1
END
GO

/*
DECLARE @List StudentList
INSERT INTO @List VALUES(1, 'Muster', 'Max'),(2, 'amon', 'gus'),(3, 'Korolev', 'Sergei');
EXEC spInsertBlockingTime @Grund='testetstetstetstestest', @ID_Drucker=4, @Von='10.02.2021 10:30', @Bis='10.02.2021 10:45', @Schueler=@List, @Bemerkung='test bemerkung test';
*/



