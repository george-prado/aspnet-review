var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) => {
	Endpoint? endpoint = context.GetEndpoint();

	if(endpoint != null)
		await context.Response.WriteAsync($"The route is: {endpoint.DisplayName}\n");
	
	await next(context);
});

app.UseRouting();

app.Use(async (context, next) => {
	Endpoint? endpoint = context.GetEndpoint();

	if (endpoint != null)
		await context.Response.WriteAsync($"The route is: {endpoint.DisplayName}\n");
	
	await next(context);
});

app.UseEndpoints(endpoints =>
{
	app.MapGet("/", async (context) =>
	{
		await context.Response.WriteAsync("Using get");
	});

	app.MapPost("/", async (context) =>
	{
		await context.Response.WriteAsync("Using post");
	});
});

app.Run();
