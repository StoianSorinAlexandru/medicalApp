using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace MedicalApplication.Module.BusinessObjects.Utilities
{
    /// <summary>
    /// Validates Romanian CNP values for structure, control digit, and plausible date/county codes.
    /// Returns null when valid, or a localized error message when invalid. Null/empty values are considered valid.
    /// </summary>
    public static class FieldsValidator
    {

        private const string INVALID_PHONE_NUMBER = "Număr de telefon invalid.";
        private const string VALID_PHONE_NUMBER = "Număr de telefon valid.";

        #region Public
        /// <summary>
        /// Performs validation of the supplied CNP string. Returns null when valid or when value is null/empty.
        /// </summary>
        public static string? Validate(string? cnp)
        {
            if (string.IsNullOrWhiteSpace(cnp))
                return null; // optional by default

            var digits = new string(cnp.Where(char.IsDigit).ToArray());
            if (digits.Length != 13)
                return "CNP trebuie să conțină exact 13 cifre.";

            // Parse components
            int s = digits[0] - '0';
            int yy = int.Parse(digits.Substring(1, 2));
            int mm = int.Parse(digits.Substring(3, 2));
            int dd = int.Parse(digits.Substring(5, 2));
            int county = int.Parse(digits.Substring(7, 2));
            int nnn = int.Parse(digits.Substring(9, 3));
            int control = digits[12] - '0';

            // Determine century
            int century = s switch
            {
                1 or 2 => 1900,
                3 or 4 => 1800,
                5 or 6 => 2000,
                7 or 8 or 9 => 2000, // temporary residents: assume 2000-based
                _ => -1
            };
            if (century < 0)
                return "Prima cifră a CNP-ului este invalidă.";

            // Validate date
            int year = century + yy;
            if (!IsValidDate(year, mm, dd))
                return "Data din CNP este invalidă.";

            // County code 01..52
            if (county < 1 || county > 52)
                return "Codul de județ din CNP este invalid.";

            // Sequence 001..999
            if (nnn < 1 || nnn > 999)
                return "Secvența numerică din CNP este invalidă.";

            // Control digit per algorithm
            var controlKey = "279146358279"; // 12 digits
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += (digits[i] - '0') * (controlKey[i] - '0');
            }
            int check = sum % 11;
            if (check == 10) check = 1;
            if (check != control)
                return "Cifra de control a CNP-ului este invalidă.";

            return null;
        }

        /// <summary>
        /// Accepts E.164 (max 15 digits) or Romanian formats beginning with +40 or 0 and 9 digits following.
        /// </summary>
        public static (bool, string) IsValidPhone(string value)
        {
            value = value.Trim();
            // E.164
            var e164 = Regex.IsMatch(value, "^\\+[1-9]\\d{7,14}$");
            // Romanian specific
            var ro = Regex.IsMatch(value, "^(?:\\+40|0)\\d{9}$");
            if (e164 || ro)
                return (true, VALID_PHONE_NUMBER);
            return (false, INVALID_PHONE_NUMBER);
            
        }
        #endregion

        #region Private
        /// <summary>
        /// Checks whether the given year, month, day form a valid Gregorian date.
        /// </summary>
        private static bool IsValidDate(int year, int month, int day)
        {
            if (month < 1 || month > 12) return false;
            if (day < 1 || day > 31) return false;
            try
            {
                var _ = new DateTime(year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
