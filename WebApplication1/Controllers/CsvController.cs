using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Dtos;
using WebApplication1.Service;

namespace WebApplication1.Controllers;

public sealed class CsvController : ControllerBase
{
    private readonly IPostalCodeService postalCodeService;

    public CsvController(IPostalCodeService postalCodeService)
    {
        this.postalCodeService = postalCodeService;
    }
    [HttpGet]
    [Route("api/get-top-10-postal-codes")]
    public IActionResult GetCsv()
    {
        return Ok(postalCodeService.GetTop10PostalCodeAmounts("file.csv")
            .Select(x=> new  PostalCodeResponse(x)));
    }
    
}