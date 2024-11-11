﻿using Microsoft.EntityFrameworkCore;

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

        List<Status> statuses = new List<Status> { status1, status2, status3 };


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
            new Setting { Id = 7, Key = "Unvan" }
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
            new SettingDetail { Id = 21, Value = "addressp.com", LanguageId = 3, SettingId = 7 }
        );
    }

}

