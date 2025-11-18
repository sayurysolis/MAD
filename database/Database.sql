CREATE DATABASE DSB_topografia
USE DSB_topografia;

CREATE TABLE Empresa (
    ID_Empresa INT IDENTITY(1,1) PRIMARY KEY,
	nombre NVARCHAR(50),
    RazonSocial NVARCHAR(100),
    DomicilioFiscal NVARCHAR(MAX),
    Contacto NVARCHAR(100),
    RegistroPatronal NVARCHAR(15),
    RFC NVARCHAR(20),
    FechaInicio DATE,
	estatus BIT DEFAULT 1
);

CREATE TABLE Departamento(
	ID_Departamento INT IDENTITY(10,5) PRIMARY KEY,
	Nombre NVARCHAR(30),
	estatus BIT DEFAULT 1,

	EmpresaID INT ,
	FOREIGN KEY (EmpresaID) REFERENCES Empresa(ID_Empresa)
);

CREATE TABLE Puesto(
	ID_Puesto INT IDENTITY(100,2) PRIMARY KEY,
	Nombre NVARCHAR(30),
	Descripcion VARCHAR(MAX),
	estatus BIT DEFAULT 1,

	EmpresaID INT,
	DepartamentoID INT,
	FOREIGN KEY (EmpresaID) REFERENCES Empresa(ID_Empresa),
	FOREIGN KEY (DepartamentoID) REFERENCES Departamento(ID_Departamento)
);

CREATE TABLE Empleado(
	ID_Empleado INT  IDENTITY  (1000,1) PRIMARY KEY ,--Numero de 	
	Nombre NVARCHAR(MAX) NOT NULL,
	ApellidoPaterno NVARCHAR(50) NOT NULL,
	ApellidoMaterno NVARCHAR(50) ,--NOT NULL,
	FechaNacimiento DATE,
	---------------
	EmpresaID INT,
	DepID INT,
	PuestoID INT,
	---------------
	CURP VARCHAR(18) UNIQUE,
	NSS VARCHAR(11) UNIQUE,
	RFC VARCHAR (13) UNIQUE,
	
	Banco NVARCHAR(30),
	NumeroCuenta VARCHAR(20) UNIQUE,
	SalarioDiario DECIMAL (10,2),
	SalarioDiarioIntegrado DECIMAL (10,2),-- capturar en automatico en c#
	--/////////////////////////////--
	calle NVARCHAR (30),
	numero INT,
	colonia NVARCHAR(50),
	municipio NVARCHAR(50),
	estado NVARCHAR (30),
	codigoPostal VARCHAR(6),
	--/////////////////////////////--
	Email NVARCHAR(50),
	Telefono VARCHAR(10),
	
	estatus BIT DEFAULT 1, -- 1 para activo 0 para inactivo
	FechaIngreso DATE,

	FOREIGN KEY (EmpresaID) REFERENCES Empresa(ID_Empresa),
	FOREIGN KEY (DepID) REFERENCES Departamento(ID_Departamento),
	FOREIGN KEY (PuestoID) REFERENCES Puesto(ID_Puesto)
);

CREATE TABLE Conceptos(
	ID_Conceptos INT IDENTITY (1,1) PRIMARY KEY,
	Tipo BIT,  --1 Percepcion 0 para Deduccion
	nombre NVARCHAR(100),
	EsPorcetanje BIT,
	Valor DECIMAL (10,2),
	General BIT, --siempre se aplica?
	estatus BIT DEFAULT 1
);

CREATE TABLE Nomina(
	ID_Nomina INT IDENTITY (10000,1)PRIMARY KEY,
	Periodo DATE NOT NULL,

	Cerrado Bit Default 0, --0  es abierto
	DiasTrabajados INT,
	EmpleadoID INT,
	
	FOREIGN KEY(EmpleadoID) REFERENCES Empleado(ID_Empleado),
	
);
CREATE UNIQUE INDEX UX_Nomina_EmpleadoPeriodo
ON Nomina(EmpleadoID, Periodo);


CREATE TABLE NominaDetalle(
	ID_NominaDetalle INT IDENTITY(1,1) PRIMARY KEY,

	NominaID INT NOT NULL,
	ConceptosID INT NOT NULL,
	Importe DECIMAL (10,2),
	SueldoBruto DECIMAL(10,2) NULL,
    SueldoNeto DECIMAL(10,2) NULL,

	FOREIGN KEY(NominaID) REFERENCES Nomina(ID_Nomina),
	FOREIGN KEY(ConceptosID) REFERENCES Conceptos(ID_Conceptos)
	
);

SELECT * FROM Conceptos


UPDATE Empleado 
set Estatus =0
WHERE ID_Empleado = 1002





INSERT INTO Conceptos (Tipo, nombre, EsPorcetanje, Valor, General, estatus)
VALUES 
-- PERCEPCIONES
(1, 'Sueldo', 0, 0, 1, 1),           -- ID 1
(1, 'Puntualidad', 0, 0, 0, 1),      -- ID 2
(1, 'Asistencia', 0, 0, 0, 1),       -- ID 3
(1, 'Productividad', 0, 0, 0, 1),    -- ID 4
(1, 'Despensa', 0, 0, 0, 1),         -- ID 5

-- DEDUCCIONES
(0, 'IMSS', 0, 0, 1, 1),             -- ID 6
(0, 'ISR', 0, 0, 1, 1);              -- ID 7

SELECT ID_Conceptos, nombre FROM Conceptos;
SELECT NominaID, ConceptosID, Importe FROM NominaDetalle;



SELECT * FROM  Empleado


