# Notification
Multi-Platform Notification Service (iOS, Android, SMS, Web, Email, N+ Type)

Self Hosted Http Rest Service

* .Net Core 2.2 
* MongoDB
* Nustache
* Generic Repository Pattern
* AutoMapper

# Example Code

Service Request Url: http://localhost:5000/api/Notification/Add
\
Request Json Body:
```json
{
  "RequestInfo": {
    "RequestId": "Test"
  },
  "Language": "en",
  "TemplateId": "ConfirmMail",
  "ChannelCode": "Online",
  "ScheduleDate": "2019.08.02 17:50",
  "Recipients": [
    {
      "Recipient": "huseyin.gulbicim@gmail.com",
      "Platform": "Email"
    },
    {
      "Recipient": "5541112233",
      "Platform": "SMS"
    }
  ],
  "TemplateItem": {
    "Fullname": "Huseyin Gulbicim",
    "CallbackUrl": "http://localhost"
  }
}
```
* TemplateItem can be changed dynamically according to the names of the objects used in the theme.

```json
"TemplateItem": {
    "Fullname": "Ahmet",
    "OtpCode": "5848",
    "CustomProperty": "Custom",
}
```
