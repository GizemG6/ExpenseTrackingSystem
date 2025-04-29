using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Infrastructure.Services
{
	public class RabbitMqService : IDisposable
	{
		private readonly RabbitMqSettings _rabbitMqSettings;
		private IConnection _connection;
		public RabbitMQ.Client.IModel _channel;

		public RabbitMqService(IConfiguration configuration)
		{
			_rabbitMqSettings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();

			var factory = new ConnectionFactory()
			{
				HostName = _rabbitMqSettings.Host,
				Port = _rabbitMqSettings.Port,
				UserName = _rabbitMqSettings.Username,
				Password = _rabbitMqSettings.Password
			};

			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
		}

		public void PublishMessage(string queueName, string message)
		{
			_channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

			var body = Encoding.UTF8.GetBytes(message);

			try
			{
				_channel.BasicPublish(
					exchange: "",
					routingKey: queueName,
					basicProperties: null,
					body: body
				);
			}
			catch (Exception ex)
			{
				throw new Exception($"RabbitMQ publish error: {ex.Message}", ex);
			}
		}

		public void ListenToQueue(string queueName, Func<string, Task> onMessageReceived)
		{
			_channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += async (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				await onMessageReceived(message);
			};

			_channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
		}

		public void Dispose()
		{
			_channel?.Close();
			_connection?.Close();
		}
	}

	public class RabbitMqSettings
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
