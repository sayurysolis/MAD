CREATE DATABASE DSB_topografia
USE DSB_topografia;

CREATE TABLE Empresa (
    ID_Empresa INT IDENTITY(1,1) PRIMARY KEY,
	nombre NVARCHAR(17),
    RazonSocial NVARCHAR(100),
    DomicilioFiscal NVARCHAR(MAX),
    Contacto NVARCHAR(100),
    RegistroPatronal NVARCHAR(100),
    RFC NVARCHAR(20),
    FechaInicio DATE
);

CREATE TABLE Departamento(
	ID_Departamento INT IDENTITY(10,10) PRIMARY KEY,
	Nombre NVARCHAR(30),
	estado BIT DEFAULT 1,--1 activo, 0 inactivo 
	EmpresaID INT,
	FOREIGN KEY (EmpresaID) REFERENCES Empresa(ID_Empresa)
	);
	
	SELECT *FROM Departamento

CREATE TABLE Puesto(
	ID_Puesto INT IDENTITY(100,1) PRIMARY KEY,
	Nombre VARCHAR(30),
	Descripcion VARCHAR(MAX),
	EmpresaID INT,
	DepartamentoID INT,
	FOREIGN KEY (EmpresaID) REFERENCES Empresa(ID_Empresa),
	FOREIGN KEY (DepartamentoID) REFERENCES Departamento(ID_Departamento)
);

CREATE TABLE Empleado(
	EmpresaID INT,
	DepID INT,
	PuestoID INT,
	Gerente  BIT,--1 Gerente, 0 empleado

	ID_Empleado INT  IDENTITY  (1000,1) PRIMARY KEY ,--Numero de empleado
	Contrasena NVARCHAR(20) NOT NULL, -- implementar HASH en un futuro para seguridad

	
	CURP VARCHAR(18) UNIQUE,
	NSS VARCHAR(11) UNIQUE,
	RFC VARCHAR (13) UNIQUE,
	
	Nombre NVARCHAR(MAX) NOT NULL,
	ApellidoPaterno NVARCHAR(50) NOT NULL,
	ApellidoMaterno NVARCHAR(50) ,--NOT NULL,
	FechaNacimiento DATE,
	
	Banco NVARCHAR(30),
	NumeroCuenta VARCHAR(20) UNIQUE,
	SalarioDiario DECIMAL (10,2),
	SalarioDiarioIntegrado DECIMAL (10,2),-- capturar en automatico en c#
	
	Email NVARCHAR(50),
	DireccionID INT,
	TelefonoID INT,
	
	estatus BIT, -- 1 para activo 0 para inactivo
	FechaIngreso DATE,

	FOREIGN KEY (EmpresaID) REFERENCES Empresa(ID_Empresa),
	FOREIGN KEY (DepID) REFERENCES Departamento(ID_Departamento),
	FOREIGN KEY (PuestoID) REFERENCES Puesto(ID_Puesto),
);

CREATE TABLE telefonos (
	ID_Telefono INT IDENTITY(1,1) PRIMARY KEY,
	NumeroTelefono VARCHAR(10),
	EmpleadoID INT,
	FOREIGN KEY (EmpleadoID) REFERENCES Empleado (ID_Empleado)
);
CREATE TABLE DireccionEmpleado (
	ID_Direccion INT IDENTITY PRIMARY KEY,
	calle VARCHAR (100),
	numeroExterior VARCHAR(10),
	colonia VARCHAR(100),
	municipio VARCHAR(100),
	estado VARCHAR(100),
	codigoPostal VARCHAR(5),
	EmpleadoID INT,
	FOREIGN KEY (EmpleadoID) REFERENCES Empleado(ID_Empleado)
);
CREATE TABLE Nomina(
	ID_Nomina INT IDENTITY PRIMARY KEY,
	MES INT,
	Anio INT,
	SueldoBruto DECIMAL (10,2),
	SueldoNeto DECIMAL (10,2),

	EmpleadoID INT,
	FOREIGN KEY(EmpleadoID) REFERENCES Empleado(ID_Empleado)
);
CREATE TABLE PercepcionesDeduccion(
	ID_PercDed INT IDENTITY (1,1) PRIMARY KEY,
	Tipo CHAR(1),  -- 'P' para percepcion, 'D' deduccion	 
	nombre NVARCHAR(100),
	EsPorcetanje BIT,
	Valor DECIMAL (10,2)
);