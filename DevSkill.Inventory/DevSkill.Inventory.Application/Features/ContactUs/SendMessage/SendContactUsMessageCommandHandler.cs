using AutoMapper;
using Cortex.Mediator.Commands;
using DevSkill.Core.Application;
using DevSkill.Core.Application.UtilitiesContracts;
using DevSkill.Core.Domain;
using DevSkill.Core.Domain.EmailServiceContracts;
using DevSkill.Inventory.Domain;

namespace DevSkill.Inventory.Application.Features.ContactUs.SendMessage
{
    public class SendContactUsMessageCommandHandler : ICommandHandler<SendContactUsMessageCommand, ResultResponse>
    {
        #region Fields

        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICaptchaService _captchaService;
        private readonly IServerTime _serverTime;

        #endregion

        #region Ctor
        public SendContactUsMessageCommandHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper,
            ICaptchaService captchaService,
            IEmailService emailService,
            IServerTime serverTime)
        {
            _mapper = mapper;
            _serverTime = serverTime;
            _captchaService = captchaService;
            _emailService = emailService;
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<ResultResponse> Handle(SendContactUsMessageCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Name) || string.IsNullOrEmpty(command.Email) || string.IsNullOrEmpty(command.Message))
                return ResultResponse.Fail(400, ["Name, Email and Message are required."]);

            if (string.IsNullOrWhiteSpace(command.RecaptchaToken))
                return ResultResponse.Fail(400, ["The reCAPTCHA challenge was not completed. Please try again."]);

            var captcha = await _captchaService.VerifyAsync(command.RecaptchaToken);
            if (!captcha.IsValid)
                return ResultResponse.Fail(400, [captcha.ErrorMessage ?? "ReCAPTCHA validation failed."]);

            return ResultResponse.Success(200, "Your message has been sent successfully.");
        }
        #endregion

    }
}
