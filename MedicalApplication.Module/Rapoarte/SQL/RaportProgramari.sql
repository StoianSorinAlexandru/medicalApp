
--BEGIN
CREATE OR ALTER PROCEDURE dbo.RaportProgramari
(
    @Data DATETIME2,
    @IdMedic UNIQUEIDENTIFIER = NULL,
    @IdSpecializare UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Day bounds derived from @Data; keeps the predicate SARGable (uses index seeks)
    DECLARE @DayStart DATETIME2(0) = DATETIMEFROMPARTS(YEAR(@Data), MONTH(@Data), DAY(@Data), 0, 0, 0, 0);
    DECLARE @DayEnd   DATETIME2(0) = DATEADD(DAY, 1, @DayStart);

    SELECT
        s.Denumire       AS DenumireSpecializare,
        m.Nume           AS NumeMedic,
        pa.Nume          AS NumePacient,
        p.Status,
        p.DataOra
    FROM dbo.Programari     AS p
    LEFT JOIN dbo.Medici        AS m  ON p.MedicId        = m.Id
    LEFT JOIN dbo.Pacienti      AS pa ON p.PacientId      = pa.Id
    LEFT JOIN dbo.Specializari  AS s  ON p.SpecializareId = s.Id
    WHERE
        -- date-only filter: [@DayStart, @DayEnd)
        p.DataOra >= @DayStart
        AND p.DataOra <  @DayEnd
        -- optional medic filter
        AND (@IdMedic IS NULL OR p.MedicId = @IdMedic)
        -- optional specialization filter
        AND (@IdSpecializare IS NULL OR p.SpecializareId = @IdSpecializare)
    ORDER BY p.DataOra;
END
GO
--END
