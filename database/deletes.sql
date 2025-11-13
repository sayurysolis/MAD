CREATE PROCEDURE sp_BajaDepartamento
    @ID_Departamento INT
AS
BEGIN
    UPDATE Departamento
    SET estatus = 0
    WHERE ID_Departamento = @ID_Departamento;
END;
GO

CREATE PROCEDURE sp_BajaPuesto
    @ID_Puesto INT
AS
BEGIN
    UPDATE Puesto
    SET estatus = 0
    WHERE ID_Puesto = @ID_Puesto;
END;
GO

CREATE PROCEDURE sp_BajaEmpleado
    @ID_Empleado INT
AS
BEGIN
    UPDATE Empleado
    SET estatus = 0
    WHERE ID_Empleado = @ID_Empleado;
END;
GO

CREATE PROCEDURE sp_BajaConcepto
    @ID_Conceptos INT
AS
BEGIN
    UPDATE Conceptos
    SET estatus = 0
    WHERE ID_Conceptos = @ID_Conceptos;
END;
GO