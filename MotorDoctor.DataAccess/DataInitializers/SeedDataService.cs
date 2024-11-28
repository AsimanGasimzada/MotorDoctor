using Microsoft.EntityFrameworkCore;

namespace MotorDoctor.DataAccess.DataInitializers;

public static class SeedDataService
{
    public static void AddSeedData(this ModelBuilder builder)
    {
        builder.AddLanguages();
        builder.AddSettings();
        builder.AddStatuses();
    }


    private static void AddStatuses(this ModelBuilder builder)
    {

        Status status1 = new() { Id = 1 };
        Status status2 = new() { Id = 2 };
        Status status3 = new() { Id = 3 };
        Status status4 = new() { Id = 4 };

        List<Status> statuses = new List<Status> { status1, status2, status3, status4 };


        List<StatusDetail> statusDetails = new()
        {
            new() { Id = 1, Name = "Sifariş edilib", LanguageId = 1, StatusId = 1 },
            new() { Id = 2, Name = "Ordered", LanguageId = 2, StatusId = 1 },
            new() { Id = 3, Name = "Заказал", LanguageId = 3, StatusId = 1 },

            new() { Id = 4, Name = "Yolda", LanguageId = 1, StatusId = 2 },
            new() { Id = 5, Name = "On the Way", LanguageId = 2, StatusId = 2 },
            new() { Id = 6, Name = "В пути", LanguageId = 3, StatusId = 2 },

            new() { Id = 7, Name = "Sifariş tamamlandı", LanguageId = 1, StatusId = 3 },
            new() { Id = 8, Name = "Order Is Done", LanguageId = 2, StatusId = 3 },
            new() { Id = 9, Name = "Заказ выполнен", LanguageId = 3, StatusId = 3 },

            new() { Id = 10, Name = "Ləğv edildi", LanguageId = 1, StatusId = 4 },
            new() { Id = 11, Name = "Cancelled", LanguageId = 2, StatusId = 4 },
            new() { Id = 12, Name = "Отменено", LanguageId = 3, StatusId = 4 }
        };


        builder.Entity<Status>().HasData(statuses);
        builder.Entity<StatusDetail>().HasData(statusDetails);
    }
    private static void AddLanguages(this ModelBuilder builder)
    {
        Language language1 = new() { Id = 1, Name = "Azerbaijan", Code = "AZE", ImagePath = "https://res.cloudinary.com/dlilcwizx/image/upload/v1730241623/motordoctor.az/fajaznl6ilmlbmo05xbw.png" };
        Language language2 = new() { Id = 2, Name = "English", Code = "ENG", ImagePath = "https://res.cloudinary.com/dlilcwizx/image/upload/v1730241623/motordoctor.az/mygg6rnd9rkxwc6vlljx.png" };
        Language language3 = new() { Id = 3, Name = "Russian", Code = "RUS", ImagePath = "https://res.cloudinary.com/dlilcwizx/image/upload/v1730241623/motordoctor.az/upkqfbyfpy7rvmjdwfsm.png" };

        List<Language> languages = [language1, language2, language3];


        builder.Entity<Language>().HasData(languages);
    }

    private static void AddSettings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Setting>().HasData(
            new Setting { Id = 1, Key = "Logo" },
            new Setting { Id = 2, Key = "Telefon1" },
            new Setting { Id = 3, Key = "Telefon2" },
            new Setting { Id = 4, Key = "FacebookLink" },
            new Setting { Id = 5, Key = "InstagramLink" },
            new Setting { Id = 6, Key = "LinkedinLink" },
            new Setting { Id = 7, Key = "Unvan" },
            new Setting { Id = 8, Key = "IsSaatlari" },
            new Setting { Id = 9, Key = "Copyright" },
            new Setting { Id = 10, Key = "Email" },
            new Setting { Id = 11, Key = "WhatsAppLink" },
            new Setting { Id = 12, Key = "WebsiteTitle" },
            new Setting { Id = 13, Key = "WebsiteShortDescription" },
            new Setting { Id = 14, Key = "WebsiteLongDescription" },
            new Setting { Id = 15, Key = "WebsiteAuthor" },
            new Setting { Id = 16, Key = "WebsiteKeywords" },
            new Setting { Id = 17, Key = "WhyChooseUs" }
        );

        modelBuilder.Entity<SettingDetail>().HasData(
            new SettingDetail { Id = 1, Value = "logo.png", LanguageId = 1, SettingId = 1 },
            new SettingDetail { Id = 2, Value = "logo.png", LanguageId = 2, SettingId = 1 },
            new SettingDetail { Id = 3, Value = "logo.png", LanguageId = 3, SettingId = 1 },

            new SettingDetail { Id = 4, Value = "+994 51 434 15 23", LanguageId = 1, SettingId = 2 },
            new SettingDetail { Id = 5, Value = "+994 51 434 15 23", LanguageId = 2, SettingId = 2 },
            new SettingDetail { Id = 6, Value = "+994 51 434 15 23", LanguageId = 3, SettingId = 2 },

            new SettingDetail { Id = 7, Value = "+994 51 434 15 23", LanguageId = 1, SettingId = 3 },
            new SettingDetail { Id = 8, Value = "+994 51 434 15 23", LanguageId = 2, SettingId = 3 },
            new SettingDetail { Id = 9, Value = "+994 51 434 15 23", LanguageId = 3, SettingId = 3 },

            new SettingDetail { Id = 10, Value = "facebook.png", LanguageId = 1, SettingId = 4 },
            new SettingDetail { Id = 11, Value = "facebook.com", LanguageId = 2, SettingId = 4 },
            new SettingDetail { Id = 12, Value = "facebook.com", LanguageId = 3, SettingId = 4 },

            new SettingDetail { Id = 13, Value = "instagramcomg", LanguageId = 1, SettingId = 5 },
            new SettingDetail { Id = 14, Value = "instagramcomg", LanguageId = 2, SettingId = 5 },
            new SettingDetail { Id = 15, Value = "instagramcomg", LanguageId = 3, SettingId = 5 },

            new SettingDetail { Id = 16, Value = "linkedin.com", LanguageId = 1, SettingId = 6 },
            new SettingDetail { Id = 17, Value = "linkedin.com", LanguageId = 2, SettingId = 6 },
            new SettingDetail { Id = 18, Value = "linkedin.com", LanguageId = 3, SettingId = 6 },

            new SettingDetail { Id = 19, Value = "address.com", LanguageId = 1, SettingId = 7 },
            new SettingDetail { Id = 20, Value = "address.com", LanguageId = 2, SettingId = 7 },
            new SettingDetail { Id = 21, Value = "address.com", LanguageId = 3, SettingId = 7 },

            new SettingDetail { Id = 22, Value = "MON-SAT: 09:00-19:00", LanguageId = 1, SettingId = 8 },
            new SettingDetail { Id = 23, Value = "MON-SAT: 09:00-19:00", LanguageId = 2, SettingId = 8 },
            new SettingDetail { Id = 24, Value = "MON-SAT: 09:00-19:00", LanguageId = 3, SettingId = 8 },

            new SettingDetail { Id = 25, Value = "© 2023 Qode Interactive , All Rights Reserved", LanguageId = 1, SettingId = 9 },
            new SettingDetail { Id = 26, Value = "© 2023 Qode Interactive , All Rights Reserved", LanguageId = 2, SettingId = 9 },
            new SettingDetail { Id = 27, Value = "© 2023 Qode Interactive , All Rights Reserved", LanguageId = 3, SettingId = 9 },

            new SettingDetail { Id = 28, Value = "info@motordoctor.az", LanguageId = 1, SettingId = 10 },
            new SettingDetail { Id = 29, Value = "info@motordoctor.az", LanguageId = 2, SettingId = 10 },
            new SettingDetail { Id = 30, Value = "info@motordoctor.az", LanguageId = 3, SettingId = 10 },

            new SettingDetail { Id = 31, Value = "https://wa.me/994501234567", LanguageId = 1, SettingId = 11 },
            new SettingDetail { Id = 32, Value = "https://wa.me/994501234567", LanguageId = 2, SettingId = 11 },
            new SettingDetail { Id = 33, Value = "https://wa.me/994501234567", LanguageId = 3, SettingId = 11 },

            new SettingDetail { Id = 34, Value = "MotorDoctor", LanguageId = 1, SettingId = 12 },
            new SettingDetail { Id = 35, Value = "MotorDoctor", LanguageId = 2, SettingId = 12 },
            new SettingDetail { Id = 36, Value = "MotorDoctor", LanguageId = 3, SettingId = 12 },

            new SettingDetail { Id = 37, Value = "Online Shop", LanguageId = 1, SettingId = 13 },
            new SettingDetail { Id = 38, Value = "Online Shop", LanguageId = 2, SettingId = 13 },
            new SettingDetail { Id = 39, Value = "Online Shop", LanguageId = 3, SettingId = 13 },

            new SettingDetail { Id = 40, Value = "Online Shop", LanguageId = 1, SettingId = 14 },
            new SettingDetail { Id = 41, Value = "Online Shop", LanguageId = 2, SettingId = 14 },
            new SettingDetail { Id = 42, Value = "Online Shop", LanguageId = 3, SettingId = 14 },

            new SettingDetail { Id = 43, Value = "MotorDoctor", LanguageId = 1, SettingId = 15 },
            new SettingDetail { Id = 44, Value = "MotorDoctor", LanguageId = 2, SettingId = 15 },
            new SettingDetail { Id = 45, Value = "MotorDoctor", LanguageId = 3, SettingId = 15 },

            new SettingDetail { Id = 46, Value = "MotorDoctor", LanguageId = 1, SettingId = 16 },
            new SettingDetail { Id = 47, Value = "MotorDoctor", LanguageId = 2, SettingId = 16 },
            new SettingDetail { Id = 48, Value = "MotorDoctor", LanguageId = 3, SettingId = 16 },

            new SettingDetail { Id = 49, Value = "KEYFİYYƏTİN ADI MOTORDOCTOR", LanguageId = 1, SettingId = 17 },
            new SettingDetail { Id = 50, Value = "KEYFİYYƏTİN ADI MOTORDOCTOR", LanguageId = 2, SettingId = 17 },
            new SettingDetail { Id = 51, Value = "KEYFİYYƏTİN ADI MOTORDOCTOR", LanguageId = 3, SettingId = 17 }
        );
    }

}

