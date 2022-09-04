using Application.Booking;
using Application.Booking.Ports;
using Application.Guest;
using Application.Guest.Ports;
using Application.Payment.Ports;
using Application.Room;
using Application.Room.Ports;
using Payments.Application;
//extern alias BookApp;
//extern alias PaymentApp;


using Data;
using Data.Booking;
using Data.Guest;
using Data.Rooms;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using MediatR;

//using BookingApplication = Application;


//using BookingApplication = BookApp::Application;
//using PaymentApplication = PaymentApp::Application;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(BookingManager));

#region IoC
builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();

builder.Services.AddScoped<IRoomManager, RoomManager>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<IBookingManager, BookingManager>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();



#endregion


#region DB Wiring Up
builder.Services.AddDbContext<HotelDbContext>(
    options => options.UseInMemoryDatabase("HotelManagementDb"));
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
