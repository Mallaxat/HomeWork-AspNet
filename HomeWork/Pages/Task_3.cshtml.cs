using HomeWork.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages;

public class Task_3Model : PageModel
{
    public List<Country> Countries { get; set; }

    public void OnGet()
    {
        Countries = new List<Country>
        {
            new Country
            {
                CountryName = "Россия",
                CountryСurrency = '₽',
                CountryPeople = 146000000
            },
            new Country
            {
                CountryName = "США",
                CountryСurrency = '$',
                CountryPeople = 340000000
            },
            new Country
            {
                CountryName = "Германия",
                CountryСurrency = '€',
                CountryPeople = 84000000
            },
            new Country
            {
                CountryName = "Япония",
                CountryСurrency = '¥',
                CountryPeople = 123000000
            },
            new Country
            {
                CountryName = "Великобритания",
                CountryСurrency = '£',
                CountryPeople = 68000000
            }
        };

    }
}