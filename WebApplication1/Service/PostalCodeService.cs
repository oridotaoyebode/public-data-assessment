using WebApplication1.Helpers;
using WebApplication1.Models.Dtos;

namespace WebApplication1.Service;

public sealed class PostalCodeService : IPostalCodeService
{
    private readonly ILogger<PostalCodeService> logger;
    public PostalCodeService(ILogger<PostalCodeService> logger)
    {
        this.logger = logger;
    }
    
    public IEnumerable<PostalCodeAmount> GetTop10PostalCodeAmounts(string fileName)
    {
        try
        {
            using TextReader reader = new StreamReader($"Files/{fileName}");
            var parser = new CsvParser(reader);
            var dataset = parser.Parse();
            var list = new List<PostalCodeAmount>();
            foreach (var data in dataset)
            {
                if (data.Length > 10) //If the data is valid
                {
                    var fullPostalCode = data[3];
                    if (!string.IsNullOrEmpty(fullPostalCode))
                    {
                        ReadOnlySpan<char> fullPostalCodeSpan = fullPostalCode.AsSpan().Trim('"'); //No memory allocation
                        int spaceIndex = fullPostalCodeSpan.IndexOf(' ');
                        var shortenedPostalCode = spaceIndex != -1 ? fullPostalCodeSpan[..spaceIndex].ToString() : fullPostalCodeSpan.ToString();
                        decimal amount = decimal.TryParse(data[1].AsSpan().Trim('"'), out var parsedAmount) ? parsedAmount : 0;
                        list.Add(new PostalCodeAmount(shortenedPostalCode, amount));
                    }
                }
               

            }
        
            return list.GroupBy(x => x.PostalCode)
                .Select(x => new PostalCodeAmount(x.Key, x.Average(x => x.Amount)))
                .OrderByDescending(x => x.Amount)
                .Take(10);
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "Error while reading file");
            return Array.Empty<PostalCodeAmount>();
        }
       
    }
}