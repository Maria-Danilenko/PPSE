using WebApplication4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IPizzaService, PizzaService>(); //The service can change in the process of work and does not require many resources
builder.Services.AddSingleton<IUserService, UserService>(); //The service should be available throughout the life cycle of the running program
builder.Services.AddScoped<IOrderService, OrderService>(); //For each service request, a new instance of the service will be created, since the service can be created and destroyed depending on user requests, and it must be available for use during the request life cycle

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
