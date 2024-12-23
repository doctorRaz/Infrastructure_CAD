using System.Text.RegularExpressions;

namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Проверка почты по маске
        /// </summary>
        /// <param name="str">проверяемая почта</param>
        /// <returns>успех mail возможен</returns>
        public static bool IsCorrectMail(this string str)
        {
            //mail https://stackoverflow.com/questions/5342375/regex-email-validation
            //https://www.codeproject.com/Articles/5260174/Email-Address-Validation-Explained-in-Detail-Code

            // Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                    + "@"
                                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            Match match = regex.Match(str);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
