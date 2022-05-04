using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            string texto = "";
            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "mensagem",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                while (!texto.Equals("exit")){
                    Console.WriteLine("Escreva sua Mensagem: ");
                    texto = Console.ReadLine();
                    
                    var body = Encoding.UTF8.GetBytes(texto);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "mensagem",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Enviada {0}", texto);
                }             
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
