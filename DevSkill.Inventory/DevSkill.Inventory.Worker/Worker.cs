using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Web.Models;
using Microsoft.Extensions.Options;

namespace DevSkill.Inventory.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly AwsOptions _awsOptions;

        public Worker(ILogger<Worker> logger,
            IOptions<AwsOptions> awsOptions,
            IApplicationUnitOfWork applicationUnitOfWork)
        {
            _logger = logger;
            _applicationUnitOfWork = applicationUnitOfWork;
            _awsOptions = awsOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Call in worker service");

                    var attributeNames = new List<string>() { "All" };
                    int maxNumberOfMessages = 5;
                    var visibilityTimeout = (int)TimeSpan.FromMinutes(10).TotalSeconds;
                    var waitTimeSeconds = (int)TimeSpan.FromSeconds(5).TotalSeconds;

                    var client = new AmazonSQSClient();

                    var request = new ReceiveMessageRequest
                    {
                        QueueUrl = "https://sqs.us-east-1.amazonaws.com/424557340333/aspnet-b11-queue",
                        AttributeNames = attributeNames,
                        MaxNumberOfMessages = maxNumberOfMessages,
                        VisibilityTimeout = visibilityTimeout,
                        WaitTimeSeconds = waitTimeSeconds,
                        MessageAttributeNames = new List<string> { "All" },
                    };

                    var response = await client.ReceiveMessageAsync(request);

                    if (response != null)
                    {

                        if (response.Messages.Count > 0)
                        {

                            foreach (var message in response.Messages)
                            {

                                Console.Write($"Message ID: '{message.MessageId}'");


                                var imageUrl = message.MessageAttributes["ImageUrl"]?.StringValue;

                                if (imageUrl != null)
                                {
                                    var filePath = message.MessageAttributes["ImagePath"]?.StringValue;
                                    if (File.Exists(filePath))
                                    {
                                        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                        {
                                            var key = Path.GetFileName(filePath);
                                            var uploadRequest = new PutObjectRequest
                                            {
                                                BucketName = "aspnetb11",
                                                Key = $"products/{key}",
                                                InputStream = fileStream,
                                                ContentType = "image/jpeg",
                                                AutoCloseStream = true,
                                            };

                                            var s3Client = new AmazonS3Client();
                                            var uploadResponse = await s3Client.PutObjectAsync(uploadRequest);

                                            if (uploadResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                                            {
                                                //File.Delete(filePath);
                                                _logger.LogInformation($"Uploaded and deleted image: {filePath}");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _logger.LogWarning($"File not found: {filePath}");
                                    }

                                }

                                var delRequest = new DeleteMessageRequest
                                {
                                    QueueUrl = "https://sqs.us-east-1.amazonaws.com/424557340333/aspnet-b11-queue",
                                    ReceiptHandle = message.ReceiptHandle,
                                };

                                var delResponse = await client.DeleteMessageAsync(delRequest);
                            }
                        }

                        else
                        {
                            _logger.LogInformation("No messages to delete.");
                        }
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
