INSERT INTO Empresa(nombre,RazonSocial,DomicilioFiscal,Contacto,RegistroPatronal,RFC,FechaInicio)
values('DSB Topografía',
'Construccion de obras de urbanizacion',
'Villa Roma 123,Privada de las villas,Garcia,Nuevo Leon ',
'8121539530',
'D41-13615-10-5',
'HEEV-800425-914',
'10/03/ 2014');

-- en caso de hacer delete
--DELETE FROM Empresa;
--DBCC CHECKIDENT ('Empresa', RESEED, 0);
GO

CREATE PROCEDURE sp_AddDepartamento
@nombre NVARCHAR(100)
AS
BEGIN
	INSERT INTO Departamento(Nombre,EmpresaID)
	VALUES (@nombre,1);
END;
GO

CREATE PROCEDURE sp_AddPuesto
    @Nombre VARCHAR(30),
    @Descripcion VARCHAR(MAX),
	@DepartamentoID int
	
AS
BEGIN
    INSERT INTO Puesto(Nombre, Descripcion,EmpresaID,DepartamentoID)
    VALUES ( @Nombre, @Descripcion,1,@DepartamentoID);
END;
GO








/*CREATE PROCEDURE SP_AddEmpleado 
	@EmpresaID INT,
	@DepID INT,
	@PuestoID INT,
	@Gerente  BIT,--1 Gerente, 0 empleado

	@ID_Empleado INT ,--Numero de empleado
	@Contrasena NVARCHAR(20),

	
	@CURP VARCHAR(18),
	@NSS VARCHAR(11),
	@RFC VARCHAR (13),
	
	@Nombre NVARCHAR(MAX),
	@ApellidoPaterno NVARCHAR(50),
	@ApellidoMaterno NVARCHAR(50),
	@FechaNacimiento DATE,
	
	@Banco NVARCHAR(30),
	@NumeroCuenta VARCHAR(20),
	@SalarioDiario DECIMAL (10,2),
	@SalarioDiarioIntegrado DECIMAL (10,2),
	
	@Email NVARCHAR(50),
	@DireccionID INT,
	@TelefonoID INT,
	
	@estatus BIT, -- 1 para activo 0 para inactivo
	@FechaIngreso DATE,
AS
BEGIN 
	SET
	 


GO*/




--FUTURO

/*
CREATE PROCEDURE sp_AddEmpresa
@RazonSocial NVARCHAR(100),
@domicilioFiscal NVARCHAR(MAX),
@contacto NVARCHAR(100),
@registroPatronal NVARCHAR(100),
@RFC NVARCHAR(20),
@Fechainicio DATE
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS (SELECT 1 FROM Empresa WHERE RFC=@RFC)
		BEGIN 
		RAISERROR('Ya existe una empresa con ese RFC',16,1);
		RETURN;
	END
INSERT INTO Empresa (RazonSocial, DomicilioFiscal, contacto,RegistroPatronal,RFC,FechaInicio)
VALUES (@RazonSocial,@domicilioFiscal,@contacto,@registroPatronal,@RFC,@Fechainicio);
END ;
GO*/
