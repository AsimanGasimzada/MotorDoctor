using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Extensions;

public static class LanguageHelper
{
    public static void CheckLanguageId(ref Languages language)
    {
        foreach (var l in Enum.GetNames(typeof(Languages)))
        {
            if (language.ToString() == l)
                return;
        }

        language = Languages.Azerbaijan;
    }
    public static bool CheckLanguageId(int id)
    {
        foreach (var l in Enum.GetValues(typeof(Languages)))
        {

            if (id == (int)l)
                return true;
        }

        return false;
    }



}
