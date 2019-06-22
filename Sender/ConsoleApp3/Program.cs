using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello this is the sender application!");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                
                channel.QueueDeclare(queue: "msgKey",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Console.WriteLine("Enter message to send");

                Customer customer = new Customer();
                customer.CustomerName = Console.ReadLine();

                String jsonified = JsonConvert.SerializeObject(customer);
              
                var body = Encoding.UTF8.GetBytes(jsonified);
                channel.BasicPublish(exchange: "",
                                     routingKey: "msgKey",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", jsonified);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
