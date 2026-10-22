-- Run this script on the database used by MiniSupermarket.API.
-- It is safe to execute more than once and preserves existing order data.
USE [TiemtaphoaNgocMai];
GO

IF COL_LENGTH(N'dbo.Orders', N'RewardPointsAwarded') IS NULL
BEGIN
    ALTER TABLE dbo.Orders
        ADD RewardPointsAwarded int NOT NULL
            CONSTRAINT DF_Orders_RewardPointsAwarded DEFAULT (0) WITH VALUES;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_GetRevenueByCashier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.Username AS CashierUsername,
        COALESCE(r.TotalOrders, 0) AS TotalOrders,
        COALESCE(r.TotalRevenue, CONVERT(decimal(18, 2), 0)) AS TotalRevenue,
        COALESCE(r.TotalPointsAwarded, 0) AS TotalPointsAwarded
    FROM dbo.Users AS u
    LEFT JOIN
    (
        SELECT
            CashierUsername,
            COUNT(*) AS TotalOrders,
            SUM(TotalAmount) AS TotalRevenue,
            SUM(RewardPointsAwarded) AS TotalPointsAwarded
        FROM dbo.Orders
        GROUP BY CashierUsername
    ) AS r ON r.CashierUsername = u.Username
    WHERE u.Role = N'Cashier' AND COALESCE(u.IsActive, 1) = 1
    ORDER BY COALESCE(r.TotalRevenue, 0) DESC, u.Username;
END;
GO

EXEC dbo.sp_GetRevenueByCashier;
GO
