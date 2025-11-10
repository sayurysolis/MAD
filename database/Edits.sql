CREATE PROCEDURE sp_EditarDepartamento
    @ID_Departamento INT,
    @Nombre NVARCHAR(100),
 
AS
BEGIN
    UPDATE Departamento
    SET Nombre = @Nombre,
    WHERE ID_Departamento = @ID_Departamento;
END
