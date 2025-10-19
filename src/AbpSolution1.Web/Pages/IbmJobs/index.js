$(function () {
  var l = abp.localization.getResource("AbpSolution1");
  var createModal = new abp.ModalManager(abp.appPath + "ibm-jobs/CreateModal");
  var editModal = new abp.ModalManager(abp.appPath + "ibm-jobs/EditModal");

  var dataTable = $("#IbmJobsTable").DataTable(
    abp.libs.datatables.normalizeConfiguration({
      serverSide: true,
      paging: true,
      order: [[1, "asc"]],
      searching: false,
      scrollX: true,
      ajax: abp.libs.datatables.createAjax(abpSolution1.ibmJobs.ibmJob.getList),
      columnDefs: [
        {
          title: "Ações",
          rowAction: {
            items: [
              {
                text: "Editar",
                visible: abp.auth.isGranted("AbpSolution1.IbmJobs.Edit"),
                action: function (data) {
                  editModal.open({ id: data.record.id });
                },
              },
              {
                text: "Excluir",
                visible: abp.auth.isGranted("AbpSolution1.IbmJobs.Delete"),
                confirmMessage: function (data) {
                  return (
                    "Tem certeza que deseja excluir o job: " +
                    data.record.nomeJob +
                    "?"
                  );
                },
                action: function (data) {
                  abpSolution1.ibmJobs.ibmJob
                    .delete(data.record.id)
                    .then(function () {
                      abp.notify.info("Job excluído com sucesso");
                      dataTable.ajax.reload();
                    });
                },
              },
            ],
          },
        },
        {
          title: "Nome do Job",
          data: "nomeJob",
        },
        {
          title: "Job Stream",
          data: "jobStream",
        },
        {
          title: "Workstation",
          data: "workstation",
        },
        {
          title: "Aplicação",
          data: "aplicacao",
        },
        {
          title: "1º Responsável",
          data: "primeiroResponsavel",
        },
        {
          title: "Criticidade",
          data: "criticidade",
        },
        {
          title: "Horário",
          data: "horarioAcionamento",
        },
      ],
    })
  );

  createModal.onResult(function () {
    dataTable.ajax.reload();
  });

  editModal.onResult(function () {
    dataTable.ajax.reload();
  });

  $("#NewIbmJobButton").click(function (e) {
    e.preventDefault();
    createModal.open();
  });

  // Função de pesquisa
  $("#SearchButton").click(function (e) {
    e.preventDefault();
    var filter = $("#FilterText").val();
    dataTable.ajax.reload();
  });
});
