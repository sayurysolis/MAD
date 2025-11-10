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

SELECT * FROM Departamento
DROP VIEW vw_DepartamentoEmpresa


UPDATE Departamento SET estado= 0
WHERE ID_Departamento LIKe 10