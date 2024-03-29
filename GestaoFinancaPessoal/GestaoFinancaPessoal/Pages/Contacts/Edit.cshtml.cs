using GestaoFinancaPessoal.Authorization;
using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Pages.Contacts
{
    #region snippet
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Contact = await Context.Contact.FirstOrDefaultAsync(
                                                 m => m.ContactId == id);

            if (Contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Contact,
                                                      ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //// Fetch Contact from DB to get OwnerID.
            //var contact = await Context
            //    .FirstOrDefaultAsync(m => m.ContactId == id);

            //if (contact == null)
            //{
            //    return NotFound();
            //}

            //var isAuthorized = await AuthorizationService.AuthorizeAsync(
            //                                         User, contact,
            //                                         ContactOperations.Update);
            //if (!isAuthorized.Succeeded)
            //{
            //    return new ChallengeResult();
            //}

            //Contact.OwnerID = contact.OwnerID;

            //if (contact.Status == ContactStatus.Approved)
            //{
            //    // If the contact is updated after approval, 
            //    // and the user cannot approve,
            //    // set the status back to submitted so the update can be
            //    // checked and approved.
            //    var canApprove = await AuthorizationService.AuthorizeAsync(User,
            //                            contact,
            //                            ContactOperations.Approve);

            //    if (!canApprove.Succeeded)
            //    {
            //        contact.Status = ContactStatus.Submitted;
            //    }
            //}

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool ContactExists(int id)
        {
            return Context.Contact.Any(e => e.ContactId == id);
        }
    }
    #endregion
}
