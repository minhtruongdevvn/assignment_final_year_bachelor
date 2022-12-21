namespace AtmAPI.Constants;

public static class ModelConstants
{
	public const string Student = "student";
	public const string Lecturer = "lecturer";
	public const string Operator = "operator";

	// Used to generate a unique code for each user.
	public const string IdentityCode =
		@"CONCAT(
			'AT{0}',
			CAST(
				SUBSTRING(
					CONVERT(VARCHAR, [CreatedAt], 111),
					0,
					CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111))
				) AS INTEGER
			) % 1000,
			'.',
			RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)
		)";

	public static class Commons
	{
		public const int MaxLength = 500;
		public const int DescriptionLength = 1500;
		public const int CategoryNameLength = 100;
	}
}
