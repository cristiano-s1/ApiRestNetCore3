using System;

namespace SmartSchool.Helpers
{
    public static class DateTimeExtensions
    {
        // Método extensivo (this)
        public static int GetCurrentAge(this DateTime dateTime)
        {
            // Data atual
            var currentDate = DateTime.UtcNow;

            // data tual - data passada pelo parametro
            int age = currentDate.Year - dateTime.Year;

            if (currentDate < dateTime.AddYears(age))
            {
                age--;
            }

            return age;

        }
    }
}
