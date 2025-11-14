---EMPRESA
CREATE PROCEDURE sp_GetEmpresa
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1* FROM Empresa;
END;
GO

--DEPARTAMENTO
CREATE VIEW vw_DepartamentoEmpresa AS
SELECT 
    d.ID_Departamento,
    d.Nombre AS NombreDepartamento,
    CASE 
        WHEN d.estatus = 1 THEN 'Activo'
        ELSE 'Inactivo'
    END AS Activo,
    e.nombre AS NombreEmpresa
FROM Departamento d
JOIN Empresa e ON d.EmpresaID = e.ID_Empresa;
GO

CREATE PROCEDURE sp_GetDepartamento
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM vw_DepartamentoEmpresa
    WHERE Activo='Activo';
END
GO



--PUESTO

CREATE PROCEDURE sp_GetPuestos
    @DepartamentoID INT = 0  -- 0 = todos los departamentos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.ID_Puesto,
        p.Nombre,
        p.Descripcion,
        d.Nombre AS Departamento,     -- ?? Departamento ahora va antes
        CASE 
            WHEN p.estatus = 1 THEN 'Activo'
            ELSE 'Inactivo'
        END AS Estatus,
        e.Nombre AS Empresa
    FROM Puesto p
    INNER JOIN Empresa e ON p.EmpresaID = e.ID_Empresa
    INNER JOIN Departamento d ON p.DepartamentoID = d.ID_Departamento
    WHERE 
        (@DepartamentoID = 0 OR d.ID_Departamento = @DepartamentoID);
END
GO

--DROP PROCEDURE sp_GetPuestos

--EMPLEADO

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

    e.calle,
    e.numero,
    e.colonia,
    e.municipio,
    e.estado,
    e.codigoPostal,

    e.Telefono,
    e.estatus,
    e.FechaIngreso,
    
    emp.Nombre AS NombreEmpresa,
    d.Nombre AS NombreDepartamento,
    p.Nombre AS NombrePuesto
FROM Empleado e
INNER JOIN Empresa emp ON e.EmpresaID = emp.ID_Empresa
INNER JOIN Departamento d ON e.DepID = d.ID_Departamento
INNER JOIN Puesto p ON e.PuestoID = p.ID_Puesto;
GO
CREATE PROCEDURE sp_GetEmpleados
AS
BEGIN
    SELECT *
    FROM Vista_Empleado
    WHERE estatus = 1; -- activos (opcional)
END;
GO

CREATE PROCEDURE sp_GetEmpleadoByID
    @ID_Empleado INT
AS
BEGIN
    SELECT 
        e.*,
        emp.Nombre AS NombreEmpresa,
        d.Nombre AS NombreDepartamento,
        p.Nombre AS NombrePuesto
    FROM Empleado e
    INNER JOIN Empresa emp ON e.EmpresaID = emp.ID_Empresa
    INNER JOIN Departamento d ON e.DepID = d.ID_Departamento
    INNER JOIN Puesto p ON e.PuestoID = p.ID_Puesto
    WHERE e.ID_Empleado = @ID_Empleado;
END;
GO

--CONCEPTOS 
CREATE PROCEDURE sp_GetConceptos
AS
BEGIN
    SELECT * 
    FROM Conceptos
    WHERE estatus = 1;  -- solo activos
END;
GO

CREATE VIEW vw_ConceptosTipoTexto AS
SELECT 
    ID_Conceptos,
    CASE 
        WHEN Tipo = 1 THEN 'Percepción'
        ELSE 'Deducción'
    END AS TipoTexto,
    Nombre,
    EsPorcetanje,
    Valor,
    General,
    Estatus
FROM dbo.Conceptos
WHERE Estatus = 1;

--PERIODOS
--Nomina
--NominaDetalle



/*
CREATE TABLE Matriz (
    id_Matriz INT IDENTITY(1,1) PRIMARY KEY,
    id_Periodo INT,
    id_Empleado INT,
    
    Faltas INT DEFAULT 0,
    Asistencia DECIMAL(10,2) DEFAULT 0,
    Puntualidad DECIMAL(10,2) DEFAULT 0,
    Despensa DECIMAL(10,2) DEFAULT 0,
    Vacaciones DECIMAL(10,2) DEFAULT 0,
    PrimaVacacional DECIMAL(10,2) DEFAULT 0,
    PrestamoEmpresa DECIMAL(10,2) DEFAULT 0,
    PrestamoInfo DECIMAL(10,2) DEFAULT 0,
    FondoAhorro DECIMAL(10,2) DEFAULT 0,
    Productividad DECIMAL(10,2) DEFAULT 0,
    HorasExtras INT DEFAULT 0,
    PensionAlimenticia DECIMAL(10,2) DEFAULT 0,
    
    SueldoBruto DECIMAL(10,2),
    ISR DECIMAL(10,2),
    IMSS DECIMAL(10,2),
    SueldoNeto DECIMAL(10,2),

    FOREIGN KEY (id_Periodo) REFERENCES Periodo(id_Periodo),
    FOREIGN KEY (id_Empleado) REFERENCES Empleado(ID_Empleado)
);*/

/*CREATE TABLE DEDPERNOMINA (
    id_DPN INT IDENTITY(1,1) PRIMARY KEY,
    id_Empleado INT NOT NULL,
    id_PD INT NOT NULL, -- ID de la percepci?n o deducci?n
    Mes INT NOT NULL,
    Ano INT NOT NULL,

    FOREIGN KEY (id_Empleado) REFERENCES Empleado(ID_Empleado),
    FOREIGN KEY (id_PD) REFERENCES PercepcionesDeduccion(ID_PercDed)
);*/






/*
DELETE FROM Departamento;
DBCC CHECKIDENT ('Departamento', RESEED, 0);
Select* From Empresa;
*/