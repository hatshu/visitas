using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.EnlaceVisitasPersonas
{
    public class CreateModel : PageModel
    {
        public string QueryStringEnlace;
       

        private readonly CatecVisitas.Models.PersonContext _context;

        public CreateModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
           
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EnlaceVisitaPersona EnlaceVisitaPersona { get; set; }
        public IList<EnlaceVisitaPersona> EnlaceVisitaPersonaList { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
           
            if (!ModelState.IsValid)
            {
                return Page();
            }

            QueryStringEnlace = Request.HttpContext.Request.QueryString.ToString();
            string[] arrayQuery = QueryStringEnlace.Split('=','&');

            //TODO: TRATAMIENTO PARA LA PRIMERA PARTE DEL QUERYSTRING


            EnlaceVisitaPersona.PersonaID = Convert.ToInt32(arrayQuery[1]);
            EnlaceVisitaPersona.VisitaID = Convert.ToInt32(arrayQuery[3]);

            _context.EnlaceVisitaPersona.Add(EnlaceVisitaPersona);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}