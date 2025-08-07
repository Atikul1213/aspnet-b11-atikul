using Amazon.SQS;
using Amazon.SQS.Model;

namespace DevSkill.Inventory.Web.Extensions
{
    public static class AWSManager
    {
        public static async Task<SendMessageResponse> SendMessage(
          IAmazonSQS client,
          string queueUrl,
          string messageBody,
          Dictionary<string, MessageAttributeValue> messageAttributes)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                DelaySeconds = 10,
                MessageAttributes = messageAttributes,
                MessageBody = messageBody,
                QueueUrl = queueUrl,
            };

            var response = await client.SendMessageAsync(sendMessageRequest);
            Console.WriteLine($"Sent a message with id : {response.MessageId}");

            return response;
        }
    }
}
