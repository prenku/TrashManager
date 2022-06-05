using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashManager.Data;
using TrashManager.Domain.Twilio;
using TrashManager.Models;
using Twilio.Rest;
using Twilio.Types;

namespace TrashManager.Workers
{
    public class SendNotificationsJob
    {
        private readonly IRepository _repository; 
        public string MessageTemplate;
        private readonly string TwillioAccointSID = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["TwilioAccountSID"];
        private readonly string TwilioAuth = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["TwilioAuth"];
        private readonly string TwilioMessageService = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["TwilioMessageService"];
        private readonly string TwilioNumber = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["TwilioNumber"];

        public SendNotificationsJob( IRepository repository)
        {
            _repository = repository;
        }
        public void Execute()
        {
            var currentTurn = _repository.GetCurrentTurn();

            var twilioRestClient = new RestClient();
            MessageTemplate = $"Hi {currentTurn.UserName} Just a reminder that it is your turn to take out the trash. Merci.";

             twilioRestClient.SendMessage(currentTurn.PhoneNumber, MessageTemplate);
            _repository.UpdateNextTurn(currentTurn); 


        }
    }
}