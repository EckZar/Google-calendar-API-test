using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MeetingSchedulerService.Helpers
{
    class GoogleHelper
    {

        public static void Main()
        {

            String serviceAccountEmail = "";

            var certificate = new X509Certificate2(@"key.p12", "notasecret", X509KeyStorageFlags.Exportable);

            ServiceAccountCredential saCreds = new ServiceAccountCredential(
               new ServiceAccountCredential.Initializer(serviceAccountEmail)
               {
                   Scopes = new[] { CalendarService.Scope.Calendar }
               }.FromCertificate(certificate));

            GoogleCredential credential;

            credential = GoogleCredential.FromServiceAccountCredential(saCreds);

            bool isManila = true;

            if (isManila)
                credential = credential.CreateWithUser("tretiakovdv@pik.ru");
            else
                credential = credential.CreateWithUser("tretiakovdv@pik.ru");

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "PIK virtual meeting",
            });

            var ev = new Event();
            EventDateTime start = new EventDateTime();
            start.DateTime = new DateTime(2021, 7, 20);

            EventDateTime end = new EventDateTime();
            end.DateTime = new DateTime(2021, 7, 20);

            //ev.

            ev.Attendees = new List<EventAttendee>
            {
                new EventAttendee()
                {
                    Email = "student2@pik.ru"
                }
            };

            ev.Start = start;
            ev.End = end;
            ev.Summary = "New Event";
            ev.ConferenceData = new ConferenceData();
            ev.ConferenceData.CreateRequest = new CreateConferenceRequest();
            ev.ConferenceData.CreateRequest.ConferenceSolutionKey = new ConferenceSolutionKey();
            ev.ConferenceData.CreateRequest.ConferenceSolutionKey.Type = "hangoutsMeet";
            ev.ConferenceData.CreateRequest.RequestId = Guid.NewGuid().ToString();

            var eventRequestGoogle = service.Events.Insert(ev, "tretiakovdv@pik.ru");
            eventRequestGoogle.ConferenceDataVersion = 1;
            var eventResponse = eventRequestGoogle.Execute();

        }
    }
}
