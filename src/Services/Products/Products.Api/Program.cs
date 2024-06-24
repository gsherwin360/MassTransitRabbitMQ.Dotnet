using Infrastructure.MongoDb;
using MassTransit;
using Products.Domain;
using Products.Infrastructure.Consumers.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDbServices<Product>(builder.Configuration, "products");
builder.Services.AddProductServices();

builder.Services.AddMassTransit(x =>
{
	// Add a consumer for handling validation of product requests
	x.AddConsumer<ValidateProductConsumer>();

	// Configure MassTransit to use RabbitMQ
	x.UsingRabbitMq((context, cfg) =>
	{
		// Configure RabbitMQ host details from app settings
		cfg.Host(new Uri(builder.Configuration["RabbitMQ:Host"]), h =>
		{
			h.Username(builder.Configuration["RabbitMQ:Username"]);
			h.Password(builder.Configuration["RabbitMQ:Password"]);
		});

		// Configure the receive endpoint for the specified queue name
		cfg.ReceiveEndpoint(builder.Configuration["RabbitMQ:QueueName"], ep =>
		{
			ep.ConfigureConsumer<ValidateProductConsumer>(context);
		});

		// Configure message retry strategy:
		// - Incremental retry up to 3 attempts
		// - Initial retry delay of 1 second, increasing by 2 seconds for each subsequent attempt
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