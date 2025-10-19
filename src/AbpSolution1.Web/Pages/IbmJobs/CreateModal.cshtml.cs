using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbpSolution1.IbmJobs;

namespace AbpSolution1.Web.Pages.IbmJobs
{
    public class CreateModalModel : PageModel
    {
        private readonly IIbmJobAppService _ibmJobAppService;

        [BindProperty]
        public CreateUpdateIbmJobDto IbmJob { get; set; } = new();

        public List<SelectListItem> CriticidadeList { get; set; } = new();

        public CreateModalModel(IIbmJobAppService ibmJobAppService)
        {
            _ibmJobAppService = ibmJobAppService;
        }

        public async Task OnGetAsync()
        {
            IbmJob = new CreateUpdateIbmJobDto();
            
            CriticidadeList = new List<SelectListItem>
            {
                new SelectListItem("Baixa", "Baixa"),
                new SelectListItem("Média", "Media"),
                new SelectListItem("Alta", "Alta"),
                new SelectListItem("Crítica", "Critica")
            };
            
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _ibmJobAppService.CreateAsync(IbmJob);
            return new EmptyResult();
        }
    }
}