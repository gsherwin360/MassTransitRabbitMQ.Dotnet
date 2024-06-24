using Inventory.Domain;
using Inventory.Infrastructure.Consumers.Events;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDbServices<InventoryItem>(builder.Configuration, "inventory");
builder.Services.AddInventoryServices();

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<ProductCreatedEventConsumer>();

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host(new Uri(builder.Configuration["RabbitMQ:Host"]), h =>
		{
			h.Username(builder.Configuration["RabbitMQ:Username"]);
			h.Password(builder.Configuration["RabbitMQ:Password"]);
		});

		cfg.ReceiveEndpoint(builder.Configuration["RabbitMQ:QueueName"], ep =>
		{
			ep.ConfigureConsumer<ProductCreatedEventConsumer>(context);
		});

		cfg.UseMessageRetry(a => a.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)));
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();