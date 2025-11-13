CREATE PROCEDURE sp_EditEmpresa
    @nombre NVARCHAR(50),
    @RazonSocial NVARCHAR(100),
    @DomicilioFiscal NVARCHAR(MAX),
    @Contacto NVARCHAR(100),
    @RegistroPatronal NVARCHAR(15),
    @RFC NVARCHAR(20)
AS
BEGIN
    UPDATE Empresa
    SET nombre=@nombre, RazonSocial=@RazonSocial, DomicilioFiscal=@DomicilioFiscal,
        Contacto=@Contacto, RegistroPatronal=@RegistroPatronal, RFC=@RFC
    WHERE ID_Empresa=1;
END;
GO

CREATE PROCEDURE sp_EditarDepartamento
    @ID_Departamento INT,
    @Nombre NVARCHAR(100)
    
AS
BEGIN
    UPDATE Departamento
    SET Nombre = @Nombre
    WHERE ID_Departamento = @ID_Departamento;
END;
GO
 

CREATE PROCEDURE sp_EditarPuesto
    @ID_Puesto INT,
	@Nombre NVARCHAR(30),
	@Descripcion VARCHAR(MAX),
	@estatus BIT
AS
BEGIN
    UPDATE Puesto
    SET Nombre=@Nombre, Descripcion=@Descripcion, estatus=@estatus
    WHERE ID_Puesto=@ID_Puesto;
END;
GO

CREATE PROCEDURE sp_EditarEmpleado
	@ID_Empleado INT,
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
	@RFC VARCHAR(13),

	@Banco NVARCHAR(30),
	@NumeroCuenta VARCHAR(20),
	@SalarioDiario DECIMAL(10,2),
	@SalarioDiarioIntegrado DECIMAL(10,2),
	--/////////////////////////////--
	@calle NVARCHAR(30),
	@numero INT,
	@colonia NVARCHAR(50),
	@municipio NVARCHAR(50),
	@estado NVARCHAR(30),
	@codigoPostal VARCHAR(6),
	--/////////////////////////////--
	@Email NVARCHAR(50),
	@Telefono VARCHAR(10),
	@FechaIngreso DATE
AS
BEGIN
	UPDATE Empleado
	SET 
		Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno,
		FechaNacimiento = @FechaNacimiento, DepID = @DepID,PuestoID = @PuestoID,
		CURP = @CURP, NSS = @NSS, RFC = @RFC,
		Banco = @Banco, NumeroCuenta = @NumeroCuenta, 
		SalarioDiario = @SalarioDiario, SalarioDiarioIntegrado = @SalarioDiarioIntegrado,

		calle = @calle, numero = @numero, colonia = @colonia, 
		municipio = @municipio,estado = @estado, codigoPostal = @codigoPostal,

		Email = @Email, Telefono = @Telefono, FechaIngreso = @FechaIngreso
	WHERE 
		ID_Empleado = @ID_Empleado;
END;
GO

CREATE PROCEDURE sp_EditarConcepto
    @ID_Conceptos INT,
    @Tipo BIT,             -- 1 = Percepción, 0 = Deducción
    @nombre NVARCHAR(100),
    @EsPorcentaje BIT,
    @Valor DECIMAL(10,2),
    @General BIT
AS
BEGIN
    UPDATE Conceptos
    SET 
        Tipo = @Tipo,
        nombre = @nombre,
        EsPorcetanje = @EsPorcentaje,
        Valor = @Valor,
        General = @General
    WHERE ID_Conceptos = @ID_Conceptos;
END;
GO
--------
CREATE PROCEDURE sp_CerrarPeriodoYCrearNuevo
AS
BEGIN
    DECLARE @MesActual INT, @AnioActual INT;

    SELECT TOP 1 @MesActual = Mes, @AnioActual = Anio
    FROM Periodo
    WHERE Cerrado = 0
    ORDER BY id_Periodo DESC;

    IF @MesActual IS NOT NULL
    BEGIN
        UPDATE Periodo
        SET Cerrado = 1
        WHERE Mes = @MesActual AND Anio = @AnioActual;

        -- Calcular el siguiente mes/año
        SET @MesActual = @MesActual + 1;
        IF @MesActual > 12
        BEGIN
            SET @MesActual = 1;
            SET @AnioActual = @AnioActual + 1;
        END
    END
    ELSE
    BEGIN
        -- Si no hay periodo anterior, empezar desde el mes actual del sistema
        SET @MesActual = MONTH(GETDATE());
        SET @AnioActual = YEAR(GETDATE());
    END

    -- Crear el nuevo periodo
    INSERT INTO Periodo (diaInicio, diaFin, Mes, Anio, Cerrado)
    VALUES (1, 30, @MesActual, @AnioActual, 0);
END;
GO