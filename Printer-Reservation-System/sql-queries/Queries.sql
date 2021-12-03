/* ***************************************************************************** */
/*SELECT all printers
*/
/*
DROP PROC IF EXISTS spSelectPrinters;
GO 
CREATE PROC spSelectPrinters 
AS
SELECT Marke, Modell, Typ, Beschreibung, Druckbereich_Breite AS 'Breite Druckbereich', Druckbereich_Laenge AS 'Länge Druckbereich', Druckbereich_Hoehe AS 'Höhe Druckbereich'  FROM tbl_Drucker 
ORDER BY ID;*/
/*
EXEC spSelectPrinters;*/


/* ***************************************************************************** */
/*SELECT all students
*/
/*
DROP PROC IF EXISTS spSelectStudents;
GO 
CREATE PROC spSelectStudents 
AS
SELECT Name, Vorname, eMail AS 'E-Mail', Handy, Bemerkung, Status, Beschreibung, IsAdmin AS 'Admin' FROM tbl_Student
JOIN tbl_Status ON ID_Status = tbl_Status.ID
ORDER BY tbl_Student.ID;*/

/*EXEC spSelectStudents;*/

/* ***************************************************************************** */
/*SELECT all reservations
*/
/*
DROP PROC IF EXISTS spSelectReservations;
GO 
CREATE PROC spSelectReservations 
AS*/
SELECT  FROM tbl_Reservation
ORDER BY ID;

/*EXEC spSelectReservations;*/