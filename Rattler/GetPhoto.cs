using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Rattler
{
    static class GetPhoto
    {
        public static string PhotoById(long id)
        {
            WebRequest request = WebRequest.Create("https://vk.com/id" + id);
            WebResponse response = request.GetResponse();
            try
            {

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        HtmlDocument htmlSnippet = new HtmlDocument();
                        htmlSnippet.LoadHtml(reader.ReadToEnd());

                        List<string> hrefTags = new List<string>();

                        foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//img[@src]"))
                        {
                            HtmlAttribute att = link.Attributes["src"];
                            hrefTags.Add(att.Value);
                            if (att.Value.Contains("deactivated"))
                            {
                                return "https://vk.com/images/camera_100.png?ava=1";
                            }
                            return att.Value;
                        }

                    }
                }
                response.Close();
            }
            catch (Exception ex) { }
            finally
            {
                response.Close();
            }
            return "https://vk.com/images/camera_100.png?ava=1";
        }
        public static string GenRandomString(int Length)
        {
            string Alphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            //string Ret = "";
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            int Position = 0;

            for (int i = 0; i < Length; i++)
            {
                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
                //Ret = Ret + Alphabet[Position]; //- так делать не стоит, в данном случае StringBuilder дает явный выигрыш в скорости
            }

            //return Ret;

            return sb.ToString();
        }
    }
}
