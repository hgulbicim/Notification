# Notification
Multiple Platform Notification Prompt Service
* .Net Core 2.2 
* MongoDB
* Nustache
* Generic Repository Pattern
* AutoMapper

# Example Code

```javascript
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
