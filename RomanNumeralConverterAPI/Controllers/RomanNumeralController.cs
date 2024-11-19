using LogDBContext;
using LoggingRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RomanNumeralConverter;
using RomanNumeralConverterAPI.Utilities;
using Swashbuckle.AspNetCore.Annotations;

namespace RomanNumeralConverterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RomanNumeralController : ControllerBase
    {
        private readonly IRepository<RMLoggingModel> _logRepo;

        public RomanNumeralController(IRepository<RMLoggingModel> logRepo)
        {
            _logRepo = logRepo;
        }

        [HttpGet("ConvertRomanToInt")]
        [SwaggerOperation(
        Summary = "Convert Roman Numeral to Integer",
        Description = "Provide a valid Roman numeral to convert it to an integer value."
            )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ConvertRomanToInt(string romanNumber)   
        {
           
            try
            {
                var result = RomanNumConverter.RomanToInt(romanNumber);
                await _logRepo.AddAsync(LogEntryGenerator.GetModelFromContext(HttpContext, result.ToString()));
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logRepo.AddAsync(LogEntryGenerator.GetModelFromContext(HttpContext, ex.Message));
                return BadRequest(ex.Message);
            }
        
            
        }
        
        [HttpGet("ConvertIntToRoman")]
        [SwaggerOperation(
        Summary = "Convert Integer to Roman Numeral",
        Description = "Provide an integer value (1 to 1000) to convert it to a Roman numeral."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ConvertIntToRoman(int value)
        {

            try
            {
                var result = RomanNumConverter.IntToRoman(value);
                await _logRepo.AddAsync(LogEntryGenerator.GetModelFromContext(HttpContext, result.ToString()));
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logRepo.AddAsync(LogEntryGenerator.GetModelFromContext(HttpContext, ex.Message));
                return BadRequest(ex.Message);
            }


        }

    }
}


