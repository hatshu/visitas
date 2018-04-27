using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatecVisitas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace CatecVisitas.Pages
{
    public class IndexModel : PageModel
    {
        public   List<string> AuthorizedUsers { get; set; }

        public void OnGet()
        {
            
        }
    }
}
