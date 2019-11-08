using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinancialChat.Bot
{
    public class BotService
    {
        public async Task<string> ProcessCSVFromExternalService(string stockCode)
        {
            try
            {
                var csvValues = new List<string>();
                using var client = new HttpClient();
                using var result = await client.GetAsync($"https://stooq.com/q/l/?s={stockCode}&f=sd2r2ohlcv&h&e=csv");
                if (result.IsSuccessStatusCode)
                {
                    var bytes = await result.Content.ReadAsByteArrayAsync();

                    using var reader = new StreamReader(new MemoryStream(bytes));
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        csvValues.Add(values[3]);
                    }
                }

                var high = decimal.Parse(csvValues.Skip(1).First(), CultureInfo.InvariantCulture);
                return $"{stockCode.ToUpper()} quote is ${high.ToString(CultureInfo.InvariantCulture)}";
            }
            catch
            {
                return "There was an error processing the request.";
            }
        }
    }
}
