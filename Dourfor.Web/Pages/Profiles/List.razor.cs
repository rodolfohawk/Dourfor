using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Profiles;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Dourfor.Web.Pages.Profiles;

public partial class ListProfilesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Profile> Profiles { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;

    [Inject]
    public IProfileHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllProfilesRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Profiles = result.Data ?? [];
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Methods

    public async void OnDeleteButtonClickedAsync(long id, string title)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Ao prosseguir a categoria {title} será excluída. Esta é uma ação irreversível! Deseja continuar?",
            yesText: "EXCLUIR",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, title);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteProfileRequest { Id = id };
            await Handler.DeleteAsync(request);
            Profiles.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Profile {title} excluída", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public Func<Profile, bool> Filter => model =>
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        if (model.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (model.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (model.Description is not null &&
            model.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };


    #endregion
}