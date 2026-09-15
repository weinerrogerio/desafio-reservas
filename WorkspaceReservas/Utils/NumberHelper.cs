namespace WorkspaceReservas.Utils
{
    public class NumberHelper
    {
        public static decimal ConvertToDecimal(string number)
        {
            string normalizedNumber = number.Replace(',', '.');
            var isSuccess = decimal.TryParse(
                normalizedNumber,
                System.Globalization.NumberStyles.Number,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out decimal decimalValue);
            if ( isSuccess ) return decimalValue;
            return 0;
        }

        public static bool IsNumeric(string number)
        {
            var validNumber = decimal.TryParse(
                number,
                System.Globalization.NumberStyles.Number,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out _);
            if ( validNumber ) return validNumber;
            return false;
        }
    }
}