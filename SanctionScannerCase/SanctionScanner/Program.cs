using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;


namespace SanctionScanner
{
    public class Program
    {

        static void Main(string[] args)
        {           
            List<string> name = new List<string>();
            decimal totalPrice = 0;
            string link = "https://www.arabam.com/";

            Uri uri = new Uri(link);

            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;

            string html = webClient.DownloadString(uri);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            for (int i = 1; i < 31; i++)
            {

                string connection = document.DocumentNode.SelectSingleNode("//*[@id='wrapper']/div[2]/div[2]/div/div[2]/div/div[1]/div[" + i + "]/div/div/div[1]/a").Attributes["href"].Value;

                connection = "https://www.arabam.com/" + connection;

                Uri uri1 = new Uri(connection);

                WebClient webClient1 = new WebClient();
                webClient1.Encoding = Encoding.UTF8;

                string html1 = webClient1.DownloadString(uri1);
                HtmlAgilityPack.HtmlDocument document1 = new HtmlAgilityPack.HtmlDocument();
                document1.LoadHtml(html1);


                string title = document1.DocumentNode.SelectSingleNode("//*[@id='wrapper']/div[2]/div[3]/div/div[1]/p").InnerText;
                string price = document1.DocumentNode.SelectSingleNode("//*[@id='js-hook-for-observer-detail']/div[2]/div[1]/div/span/text()").InnerText;
                string price1 = document1.DocumentNode.SelectSingleNode("//*[@id='js-hook-for-observer-detail']/div[2]/div[1]/div/span/text()").InnerText;
                price = price.Trim();
                price1 = price1.Trim();
                price1 = price1.Substring(0, price.Length - 3);

                decimal amount = Convert.ToDecimal(price1);

                totalPrice = totalPrice + amount;              

                name.Add(title + "-----" + price);
            }
            foreach (var item in name)
            {
                Console.WriteLine(item);

            }
            Console.WriteLine("TotalPrice =  " + totalPrice);


            string fileName = @"D:\result.txt";
                                
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();
            foreach (var item in name)
            {
                string writeText = item;
                File.AppendAllText(fileName, Environment.NewLine + writeText);
            }
            




            Console.Read();

        }
    }
}
