

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.Run(async (HttpContext context) =>
{

	public enum MathOperation
	{
		sum,
		subtract,
		multiply,
		divide,
		mod
	}

if (context.Request.Method == "POST")
	{
		var reader = new StreamReader(context.Request.Body);
		var body = await reader.ReadToEndAsync();
		var queries = QueryHelpers.ParseQuery(body);

		string? operation = queries.TryGetValue("operation", out var op) ? op.ToString() : null;
		int firstNumber = queries.TryGetValue("firstNumber", out var first) ? Convert.ToInt32(first) : 0;
		int secondNumber = queries.TryGetValue("secondNumber", out var second) ? Convert.ToInt32(second) : 0;

		i (!string.IsNullOrWhiteSpace(operation))
		{
			if (Enum.TryParse<MathOperation>(operation, ignoreCase: true, out var mathOperation))
			{
				await context.Response.WriteAsync($"{Calc(mathOperation, firstNumber, secondNumber)}");
				return;
			}
		}

		context.Response.StatusCode = 400;
		await context.Response.WriteAsync("Please input valid entries.");
	}
	else if (context.Request.Method == "GET")
	{
		string? operation = context.Request.Query.TryGetValue("operation", out var op) ? op.ToString() : null;
		int firstNumber = context.Request.Query.TryGetValue("firstNumber", out var first) ? Convert.ToInt32(first) : 0;
		int secondNumber = context.Request.Query.TryGetValue("secondNumber", out var second) ? Convert.ToInt32(second) : 0;

		if (!string.IsNullOrWhiteSpace(operation))
		{
			if (Enum.TryParse<MathOperation>(operation, ignoreCase: true, out var mathOperation))
			{
				var result = Calc(mathOperation, firstNumber, secondNumber);
				await context.Response.WriteAsync(result.ToString());
				return;
			}
		}

		context.Response.StatusCode = 400;
		await context.Response.WriteAsync("Please input valid entries.");
	}
});

app.Run();

int Calc(MathOperation operation, int a, int b)
{
	switch (operation)
	{
		case MathOperation.sum:
			return a + b;
		case MathOperation.subtract:
			return a - b;
		case MathOperation.multiply:
			return a * b;
		case MathOperation.divide:
			return b != 0 ? a / b : 0;
		case MathOperation.mod:
			return b != 0 ? a % b : 0;
		default:
			throw new ArgumentOutOfRangeException(nameof(operation), "Unknown operation");
	}
}

