using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestaoFinancaPessoal.Pages
{
    #region snippet
    // requires using Microsoft.AspNetCore.Mvc.RazorPages;
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
    #endregion
}
