using Beseler.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLogging();
builder.AddServiceDefaults();
builder.Services.AddRequestTimeouts();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseRequestLogging();

app.MapDefaultEndpoints();
app.MapFallbackToFile("index.html");

app.Run();
