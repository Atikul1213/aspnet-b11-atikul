using Cortex.Mediator.Commands;
using DevSkill.Core.Application;

namespace DevSkill.Inventory.Application.Features.ContactUs.SendMessage
{
    public class SendContactUsMessageCommand : ICommand<ResultResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Message { get; set; }
        public string RecaptchaToken { get; set; }
    }
}
