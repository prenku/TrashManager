using Microsoft.Extensions.Configuration;
using System.Linq;
using TrashManager.Data;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TrashManager.Domain.Twilio
{
    public class RestClient
    {
        private readonly ITwilioRestClient _client;
        public IConfiguration Configuration { get; set; }


        private readonly string TwillioAccointSID = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["TwilioAccountSID"];
        private readonly string TwilioAuth = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["TwilioAuth"];
        private readonly string TwilioMessageService = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["TwilioMessageService"];
        private readonly string TwilioNumber = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["TwilioNumber"];

        public RestClient()
        {
            _client = new TwilioRestClient(TwillioAccointSID, TwilioAuth);
        }

        public RestClient(ITwilioRestClient client)
        {
            _client = client;
        }

        public void SendMessage(string phoneNumber, string message)
        {



            var to = new PhoneNumber(phoneNumber);
            MessageResource.Create(
                to,
                from: new PhoneNumber(TwilioNumber),
                body: message,
                client: _client);

        }
    }
}
