using System;
using System.Security.Cryptography.X509Certificates;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.Collections.Generic;

namespace Google.Apis.Samples.PlusServiceAccount
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Calendar API - Service Account");
            Console.WriteLine("==========================");

            String serviceAccountEmail = "";

            var certificate = new X509Certificate2(@"key.p12", "notasecret", X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new ServiceAccountCredential(
               new ServiceAccountCredential.Initializer(serviceAccountEmail)
               {
                   Scopes = new[] { CalendarService.Scope.Calendar }
               }.FromCertificate(certificate));

            // Create the service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });

            var newEvent = new Event();
            EventDateTime start = new EventDateTime
            {
                DateTime = new DateTime(2021, 7, 20)
            };

            EventDateTime end = new EventDateTime
            {
                DateTime = new DateTime(2021, 7, 20)
            };

            newEvent.Start = start;
            newEvent.End = end;
            newEvent.Summary = "TEST";

            

            newEvent.ConferenceData = new ConferenceData
            {
                CreateRequest = new CreateConferenceRequest
                {
                    ConferenceSolutionKey = new ConferenceSolutionKey()
                    { 
                        Type = "hangoutsMeet"
                    },
                    RequestId = Guid.NewGuid().ToString()
                }                
            };

            var request = service.Events.Insert(newEvent, "tretiakovdv@pik.ru");
            request.ConferenceDataVersion = 1;
            var createEvent = request.Execute();

            Console.WriteLine(createEvent.Id);
            Console.WriteLine(createEvent.HangoutLink);
        }
    }
}
