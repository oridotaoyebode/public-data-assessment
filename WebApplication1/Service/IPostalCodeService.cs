using WebApplication1.Models.Dtos;

namespace WebApplication1.Service;

public interface IPostalCodeService
{
    IEnumerable<PostalCodeAmount> GetTop10PostalCodeAmounts(string filePath);
}