using Amazon.SQS;
using Amazon.SQS.Model;
using DevSkill.Inventory.Web.Models;
using Microsoft.Extensions.Options;

namespace DevSkill.Inventory.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly AwsOptions _awsOptions;

        public Worker(ILogger<Worker> logger,
            IOptions<AwsOptions> awsOptions)
        {
            _logger = logger;
            _awsOptions = awsOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    var attributeNames = new List<string>() { "All" };
                    int maxNumberOfMessages = 5;
                    var visibilityTimeout = (int)TimeSpan.FromMinutes(10).TotalSeconds;
                    var waitTimeSeconds = (int)TimeSpan.FromSeconds(5).TotalSeconds;

                    var client = new AmazonSQSClient();

                    var request = new ReceiveMessageRequest
                    {
                        QueueUrl = _awsOptions.SQSUrl,
                        AttributeNames = attributeNames,
                        MaxNumberOfMessages = maxNumberOfMessages,
                        VisibilityTimeout = visibilityTimeout,
                        WaitTimeSeconds = waitTimeSeconds,
                    };

                    var response = await client.ReceiveMessageAsync(request);

                    if (response.Messages.Count > 0)
                    {
                        response.Messages.ForEach(async m =>
                        {
                            Console.Write($"Message ID: '{m.MessageId}'");

                            var delRequest = new DeleteMessageRequest
                            {
                                QueueUrl = "https://sqs.us-east-1.amazonaws.com/0123456789ab/MyTestQueue",
                                ReceiptHandle = m.ReceiptHandle,
                            };

                            var delResponse = await client.DeleteMessageAsync(delRequest);
                        });
                    }
                    else
                    {
                        _logger.LogInformation("No messages to delete.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Falied to upload the image into the s3 bucket", ex.Message);
                }

                await Task.Delay(1000 * 60 * 5, stoppingToken);
            }
        }
    }
}
