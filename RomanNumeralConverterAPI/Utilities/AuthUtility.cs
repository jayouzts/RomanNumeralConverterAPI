namespace RomanNumeralConverterAPI.Utilities
{
    public class AuthUtility
    {

        public static bool IsAuthenticated(HttpRequest request, string authKeyValue)
        {
            const string AUTH_KEY_HEADER = "AuthKey";
            if (!request.Headers.ContainsKey(AUTH_KEY_HEADER)) return false;

            return (request.Headers[AUTH_KEY_HEADER].ToString() == authKeyValue);
        }
    }
}

