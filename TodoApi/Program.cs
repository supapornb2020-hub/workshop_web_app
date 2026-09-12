using TodoApi.Dtos;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var todos = new List<TodoGetDto>
{
    new(1, "Learn C#", false),
    new(2, "Learn API.NET core", false),
    new(3, "Build 5 web API", false),
    new(4, "Run  web API", false),
};

app.MapGet("/api/todos", () =>
    Results.Ok(todos));

// filter by id

app.MapGet("/api/todos/{id}", (int id) =>
{
    var todo = todos.FirstOrDefault(x => x.Id == id);

    return todo is null
        ? Results.NotFound()
        : Results.Ok(todo);
});
// Post new todo
app.MapPost("/api/todos", (TodoPostDto dto) =>
{
    var nextId = todos.Count == 0 ? 1 : todos.Max(x => x.Id) + 1;
    var todo = new TodoGetDto(nextId, dto.Title, false);
    todos.Add(todo);

    return Results.Created($"/api/todos/{todo.Id}", todo);
});


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
