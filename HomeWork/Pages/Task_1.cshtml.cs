using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages;

public class Task_1Model : PageModel
{
    public DateTime DateNow { get; private set; }  = DateTime.Now;
    public int DateOfYear { get; private set; }
    
    private char[] Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private int rand =0;
    public char Letter {get; private set;}
   
    public void OnGet()
    {
        DateOfYear=DateNow.DayOfYear;
        
        rand=Random.Shared.Next(0,Letters.Length+1);
        Letter = Letters[rand];
    }
}