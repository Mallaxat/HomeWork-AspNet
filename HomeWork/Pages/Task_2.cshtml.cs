using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages;

public class Task_2Model : PageModel
{
    public List<Cafe> Cafes { get; set; }

    public void OnGet()
    {
        Cafes = new()
        {
            new Cafe()
            {
                Name = "Cafe",
                Street = "Russia, 123 Main Street",
            },
            new Cafe()
            {
                Name = "Cafe2",
                Street = "China, 456 Main Street",
            }
        };
    }
}