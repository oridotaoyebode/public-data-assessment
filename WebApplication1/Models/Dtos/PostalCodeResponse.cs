using System.Globalization;

namespace WebApplication1.Models.Dtos;

public class PostalCodeResponse
{
    public string PostalCode { get; }
    public string Amount { get; }
    public PostalCodeResponse(PostalCodeAmount amount)
    {
        PostalCode = amount.PostalCode;
        Amount = amount.Amount.ToString("C", new CultureInfo("en-GB"));
    }
}