﻿using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace hello_world_receive
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("hello", false, false, false, null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ModuleHandle, eventArgs) => {
                    var body = eventArgs.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Message received: {0}", message);
                };
                channel.BasicConsume("hello", true, consumer);
                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }

        }
    }
}
