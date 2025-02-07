using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("api/classify-number")]
public class NumberClassificationController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public NumberClassificationController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("get-number-fact")]
    public async Task<IActionResult> GetNumberFact([FromQuery] string number)
    {
        // Validate input
        if (!int.TryParse(number, out int num))
        {
            return BadRequest(new { number, error = true, message = "Invalid input. Please provide a valid integer." });
        }

        // Classify number properties
        var properties = GetNumberProperties(num);
        var digitSum = GetDigitSum(num);
        var isPrime = IsPrime(num);
        var isPerfect = IsPerfect(num);
        var funFact = await FetchFunFact(num);

        // Return JSON response
        return Ok(new
        {
            number = num,
            is_prime = isPrime,
            is_perfect = isPerfect,
            properties,
            digit_sum = digitSum,
            fun_fact = funFact
        });
    }

    private static string[] GetNumberProperties(int number)
    {
        bool isArmstrong = IsArmstrong(number);
        bool isOdd = number % 2 != 0;
        bool isEven = number % 2 == 0;

        if (isArmstrong && isOdd) return new[] { "armstrong", "odd" };
        if (isArmstrong && isEven) return new[] { "armstrong", "even" };
        if (isOdd) return new[] { "odd" };
        return new[] { "even" };
    }

    private static bool IsArmstrong(int number)
    {
        int sum = number.ToString().Select(c => (int)Math.Pow(c - '0', number.ToString().Length)).Sum();
        return sum == number;
    }

    private static int GetDigitSum(int number)
    {
        return Math.Abs(number).ToString().Select(c => c - '0').Sum();
    }

    private static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    private static bool IsPerfect(int number)
    {
        if (number < 1) return false;
        int sum = Enumerable.Range(1, number / 2).Where(i => number % i == 0).Sum();
        return sum == number;
    }

    private async Task<string> FetchFunFact(int number)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"http://numbersapi.com/{number}/math");
            return response;
        }
        catch
        {
            return "No fun fact available at the moment.";
        }
    }
}
