using Microsoft.AspNetCore.WebUtilities;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run(async (HttpContext context) =>
{
	if (context.Request.Method == "POST")
	{
		StreamReader reader = new StreamReader(context.Request.Body);
		var body = await reader.ReadToEndAsync();

		if (string.IsNullOrWhiteSpace(body))
		{
			context.Response.StatusCode = 400;
			context.Response.ContentType = "text/plain";
			await context.Response.WriteAsync("You must enter E-mail and Password to proceed");
			return;
		}

		var queries = QueryHelpers.ParseQuery(body);

		var email = "";
		var password = "";


			if (queries.ContainsKey("email"))
			{
				email = queries["email"];
			}
			else
			{
				context.Response.StatusCode = 400;
				throw new Exception("E-mail required");
			}

			if (queries.ContainsKey("password"))
			{
				password = queries["password"];
			}
			else
			{
				context.Response.StatusCode = 400;
				throw new Exception("Password required");
			} 

		var dbEmail = "example@gmail.com";
		var dbPassword = "qwerty123";

		if (email == dbEmail && password == dbPassword)
		{
			context.Response.StatusCode = 200;
			await context.Response.WriteAsync("Successful login");
		}
		else
		{
			context.Response.StatusCode = 400;
			await context.Response.WriteAsync("Invalid Login");
		}

	}
	else if (context.Request.Method == "GET")
	{
		context.Response.StatusCode = 200;
	}
});

app.Run();
