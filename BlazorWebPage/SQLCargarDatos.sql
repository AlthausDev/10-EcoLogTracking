/****** Script for SelectTopNRows command from SSMS  ******/
DELETE FROM Tarea;
DBCC CHECKIDENT ('Tarea', RESEED, 0);
GO

INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 1', 'Descripción de la tarea 1' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 2', 'Descripción de la tarea 2' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 3', 'Descripción de la tarea 3' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 4', 'Descripción de la tarea 4' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 5', 'Descripción de la tarea 5' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 6', 'Descripción de la tarea 6' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 7', 'Descripción de la tarea 7' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 8', 'Descripción de la tarea 8' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 9', 'Descripción de la tarea 9' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 10', 'Descripción de la tarea 10' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 11', 'Descripción de la tarea 11' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 12', 'Descripción de la tarea 12' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 13', 'Descripción de la tarea 13' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 14', 'Descripción de la tarea 14' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 15', 'Descripción de la tarea 15' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 16', 'Descripción de la tarea 16' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 17', 'Descripción de la tarea 17' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 18', 'Descripción de la tarea 18' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 19', 'Descripción de la tarea 19' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 20', 'Descripción de la tarea 20' , 1);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 21', 'Descripción de la tarea 21' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 22', 'Descripción de la tarea 22' , 0);
INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('Tarea 23', 'Descripción de la tarea 23' , 1);