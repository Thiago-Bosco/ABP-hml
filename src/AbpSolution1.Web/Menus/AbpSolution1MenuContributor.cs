using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using AbpSolution1.Localization;
using Volo.Abp.UI.Navigation;

namespace AbpSolution1.Web.Menus;

public class AbpSolution1MenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<AbpSolution1Resource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                AbpSolution1Menus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        // Add IBM Jobs menu item
        context.Menu.AddItem(
            new ApplicationMenuItem(
                AbpSolution1Menus.IbmJobs,
                l["Menu:IbmJobs"],
                url: "/IbmJobs",
                icon: "fas fa-cogs"
            )
        );

        return Task.CompletedTask;
    }
}