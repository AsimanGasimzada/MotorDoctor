using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Business.UIServices.Abstractions;

namespace MotorDoctor.Business.UIServices.Implementations;

internal class ContactService : IContactService
{
    private readonly ISettingService _settingService;
    private readonly IEmailService _emailService;
    public ContactService(ISettingService settingService, IEmailService emailService)
    {
        _settingService = settingService;
        _emailService = emailService;

    }

    public async Task<ContactDto> GetContactDtoAsync()
    {
        ContactDto dto = new()
        {
            Settings = await _settingService.GetSettingsWithDictionaryAsync(Constants.SelectedLanguage)
        };

        return dto;
    }

    public async Task<bool> SendEmailAsync(ContactDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        string emailBody = _getTemplateContent(dto);

        await _emailService.SendEmailAsync(new() { Body = emailBody, ToEmail = "asimanjg@code.edu.az", Subject = "Contact Info" });

        return true;

    }



    private string _getTemplateContent(ContactDto dto)
    {

        string result = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>FAQ Email</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            color: #333;
        }

        .email-container {
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }

        .email-header {
            text-align: center;
            background-color: #00204a;
            color: white;
            padding: 10px;
            border-radius: 8px 8px 0 0;
        }

            .email-header h1 {
                margin: 0;
            }

        .faq-section {
            margin-top: 20px;
        }

        .faq-item {
            margin-bottom: 15px;
        }

            .faq-item h2 {
                font-size: 18px;
                margin-bottom: 5px;
                color: #00204a;
            }

            .faq-item p {
                font-size: 16px;
                margin: 0;
            }

        .footer {
            text-align: center;
            margin-top: 20px;
            padding-top: 10px;
            border-top: 1px solid #ddd;
            font-size: 14px;
            color: #666;
        }

            .footer a {
                color: #00204a;
                text-decoration: none;
            }

                .footer a:hover {
                    text-decoration: underline;
                }
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""email-header"">
            <h1>İstifadəçi sorğusu</h1>
        </div>
        <div class=""faq-section"">
            <div class=""faq-item"">
                <h2>Ad soyad</h2>
                <p>[REPLACE_FULLNAME]</p>
            </div>
            <div class=""faq-item"">
                <h2>Email</h2>
                <p>[REPLACE_EMAIL]</p>
            </div>
       

            <div class=""faq-item"">
                <h2>Mesaj</h2>
                <p>[REPLACE_MESSAGE]</p>
            </div>
        </div>

    </div>
</body>
</html>
";

        result = result.Replace("[REPLACE_FULLNAME]", dto.Fullname);
        result = result.Replace("[REPLACE_MESSAGE]", dto.Message);
        result = result.Replace("[REPLACE_EMAIL]", dto.Email);

        return result;
    }
}
