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
	@DepartamentoID INT
AS
BEGIN
    INSERT INTO Puesto(Nombre, Descripcion,EmpresaID,DepartamentoID)
    VALUES ( @Nombre, @Descripcion,1,@DepartamentoID);
END;
GO

CREATE PROCEDURE sp_AddEmpleado
	@Nombre NVARCHAR(MAX),
	@ApellidoPaterno NVARCHAR(50),
	@ApellidoMaterno NVARCHAR(50),
	@FechaNacimiento DATE,
	---------------
	@DepID INT,
	@PuestoID INT,
	---------------
	@CURP VARCHAR(18),
	@NSS VARCHAR(11),
	@RFC VARCHAR (13),

	@Banco NVARCHAR(30),
	@NumeroCuenta VARCHAR(20),
	@SalarioDiario DECIMAL (10,2),
	@SalarioDiarioIntegrado DECIMAL (10,2),
	--/////////////////////////////--
	@calle NVARCHAR (30),
	@numero INT,
	@colonia NVARCHAR(50),
	@municipio NVARCHAR(50),
	@estado NVARCHAR (30),
	@codigoPostal VARCHAR(6),
	--/////////////////////////////--
	@Email NVARCHAR(50),
	@Telefono VARCHAR(10),
	@FechaIngreso DATE
AS
BEGIN
	INSERT INTO Empleado(Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,
						 EmpresaID,DepID,PuestoID,CURP,NSS,RFC,
						 Banco,NumeroCuenta,SalarioDiario,SalarioDiarioIntegrado,
						 calle,numero,colonia,municipio,estado,codigoPostal,
						 Email,Telefono,FechaIngreso)

	VALUES (@Nombre,@ApellidoPaterno,@ApellidoMaterno,@FechaNacimiento,
			1,@DepID,@PuestoID,@CURP,@NSS,@RFC,
			@Banco,@NumeroCuenta,@SalarioDiario,@SalarioDiarioIntegrado,
			@calle,@numero,@colonia,@municipio,@estado,@codigoPostal,
			@Email,@Telefono,@FechaIngreso);
END;
GO

CREATE PROCEDURE sp_AddConceptos
	@Tipo BIT,
	@nombre NVARCHAR(100),
	@EsPorcetanje BIT,
	@Valor DECIMAL (10,2),
	@General BIT 
AS
BEGIN
	INSERT INTO  Conceptos(Tipo,nombre,EsPorcetanje,Valor,General)
	VALUES(@Tipo,@nombre,@EsPorcetanje,@Valor,@General);
END;
GO





CREATE PROCEDURE sp_CrearPrimerPeriodo
    @Mes INT,
    @Anio INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Periodo)
    BEGIN
        INSERT INTO Periodo (Mes,Anio)
        VALUES (@Mes, @Anio);
    END
END;
GO



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
