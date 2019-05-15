using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MNB_arfolyam.MNBInterface;

namespace MNB_arfolyam
{
    class Program
    {
        static void Main(string[] args)
        {
            MNBArfolyamServiceSoapClient client = new MNBArfolyamServiceSoapClient();
            try
            {
                var resp = client.GetCurrentExchangeRates(new GetCurrentExchangeRatesRequestBody());
                var respString = resp.GetCurrentExchangeRatesResult;
                Console.WriteLine(resp.GetCurrentExchangeRatesResult);

                XDocument xdoc = XDocument.Parse(respString);
                Console.WriteLine(xdoc);

                // megjeleníti a XML tag-ek nélkül
                var Rates = xdoc.Descendants("Rate");
                foreach (var item in Rates)
                {
                    string[] row = new string[3];
                    row[0] = item.Attribute("curr").Value;
                    row[1] = item.Attribute("unit").Value;
                    row[2] = item.Value;
                    Console.WriteLine(row[0] + row[1] + row[2]);
                }

                // Nap kinyérése az XML-ből
                var Day = xdoc.Descendants("Day");
                foreach (var item in Day)
                {
                    Console.WriteLine("Aktuális nap: {0}", item.Attribute("date").Value);
                }

                // EURÓ kinyerése az XML-ből
                var euro = xdoc.Descendants("Rate");
                foreach (var item in euro)
                {
                    if (item.Attribute("curr").Value == "EUR")
                    {
                        Console.WriteLine("Euró mai árfolyama {0}", item.Value);
                    }
                                    }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
            
        }
    }
}
