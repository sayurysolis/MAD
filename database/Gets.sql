CREATE PROCEDURE sp_GetEmpresa
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1* FROM Empresa;
END;
GO

CREATE VIEW vw_DepartamentoEmpresa AS
SELECT 
    d.ID_Departamento,
    d.Nombre AS NombreDepartamento,
    d.estado AS Activo,
    e.nombre AS NombreEmpresa
    
FROM Departamento d
JOIN Empresa e ON d.EmpresaID = e.ID_Empresa
GO





    CREATE VIEW Vista_Empleado AS
SELECT 
    e.ID_Empleado,
    e.Nombre,
    e.ApellidoPaterno,
    e.ApellidoMaterno,
    e.CURP,
    e.NSS,
    e.RFC,
    e.FechaNacimiento,
    e.Banco,
    e.NumeroCuenta,
    e.SalarioDiario,
    e.SalarioDiarioIntegrado,
    e.Email,
    e.Direccion,
    e.Telefono,
    e.estatus,
    e.FechaIngreso,
    e.Gerente,
    emp.Nombre AS NombreEmpresa,
    d.Nombre AS NombreDepartamento,
    p.Nombre AS NombrePuesto
FROM Empleado e
INNER JOIN Empresa emp ON e.EmpresaID = emp.ID_Empresa
INNER JOIN Departamento d ON e.DepID = d.ID_Departamento
INNER JOIN Puesto p ON e.PuestoID = p.ID_Puesto;

GO


CREATE TABLE Matriz (
    id_Matriz INT IDENTITY(1,1) PRIMARY KEY,
    id_Empleado INT,
    NombreEmpleado NVARCHAR(MAX),
    Faltas INT,
    Asistencia DECIMAL(10,2),
    Puntualidad DECIMAL(10,2),
    Despensa DECIMAL(10,2),
    Vacaciones DECIMAL(10,2),
    PrimaVacacional DECIMAL(10,2),
    PrestamoEmpresa DECIMAL(10,2),
    PrestamoInfo DECIMAL(10,2),
    FondoAhorro DECIMAL(10,2),
    Productividad DECIMAL(10,2),
    HorasExtras INT,
    PensionAlimenticia DECIMAL(10,2),
    FOREIGN KEY (id_Empleado) REFERENCES Empleado(ID_Empleado)
);
