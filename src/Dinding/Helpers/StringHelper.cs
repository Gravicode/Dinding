namespace Dinding.Helpers
{
    public class StringHelper
    {
        public static string Shorten(string Content, int MaxLength)
        {
            return Content.Length > MaxLength ? Content.Substring(0, MaxLength) + "..." : Content;
        }
    }
}
