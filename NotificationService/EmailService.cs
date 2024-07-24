using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NotificationService
{
    public class EmailService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly SmtpClient _smtpClient;

        public EmailService(string hostname, string queueName = "emailQueue")
        {
            _hostname = hostname;
            _queueName = queueName;

            // Configurar SmtpClient
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("carlosochoa06.01.12@gmail.com", "pggfdrvpahnksgzs"),
                EnableSsl = true,
            };
        }

        public void Start()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = _hostname };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare(queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var lines = message.Split('\n');
                        var to = lines[0].Substring(4); // "To: ...".Length = 4
                        var subject = lines[1].Substring(9); // "Subject: ...".Length = 9
                        var bodyMessage = string.Join('\n', lines.Skip(3));

                        var mailMessage = new MailMessage("carlosochoa06.01.12@gmail.com", to, subject, bodyMessage);
                        _smtpClient.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        // Manejar errores de envío de correo
                        Console.WriteLine($"Error sending email: {ex.Message}");
                    }
                };

                channel.BasicConsume(queue: _queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Email service started. Waiting for messages...");
            }
            catch (Exception ex)
            {
                // Manejar errores de conexión a RabbitMQ
                Console.WriteLine($"Error connecting to RabbitMQ: {ex.Message}");
            }
        }
    }
}
