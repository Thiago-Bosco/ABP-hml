using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbpSolution1.IbmJobs;

namespace AbpSolution1.Web.Pages.IbmJobs
{
    public class EditModalModel : PageModel
    {
        private readonly IIbmJobAppService _ibmJobAppService;

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateIbmJobDto IbmJob { get; set; } = new();

        public List<SelectListItem> CriticidadeList { get; set; } = new();

        public EditModalModel(IIbmJobAppService ibmJobAppService)
        {
            _ibmJobAppService = ibmJobAppService;
        }

        public async Task OnGetAsync()
        {
            var ibmJobDto = await _ibmJobAppService.GetAsync(Id);
            IbmJob = new CreateUpdateIbmJobDto
            {
                NomeJob = ibmJobDto.NomeJob,
                JobStream = ibmJobDto.JobStream,
                Workstation = ibmJobDto.Workstation,
                Aplicacao = ibmJobDto.Aplicacao,
                PrimeiroResponsavel = ibmJobDto.PrimeiroResponsavel,
                SegundoResponsavel = ibmJobDto.SegundoResponsavel,
                TerceiroResponsavel = ibmJobDto.TerceiroResponsavel,
                HorarioAcionamento = ibmJobDto.HorarioAcionamento,
                Criticidade = ibmJobDto.Criticidade,
                Descricao = ibmJobDto.Descricao
            };

            CriticidadeList = new List<SelectListItem>
            {
                new SelectListItem("Baixa", "Baixa"),
                new SelectListItem("Média", "Media"),
                new SelectListItem("Alta", "Alta"),
                new SelectListItem("Crítica", "Critica")
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _ibmJobAppService.UpdateAsync(Id, IbmJob);
            return new EmptyResult();
        }
    }
}