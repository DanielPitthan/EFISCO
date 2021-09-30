using System.Security;

namespace CrossCuting.Tools
{
    public class TransformToSecureString
    {
        public static SecureString Transform(string text)
        {
            SecureString scString = new SecureString();

            foreach (char ch in text.ToCharArray())
            {
                scString.AppendChar(ch);
            }
            return scString;
        }

    }
}
