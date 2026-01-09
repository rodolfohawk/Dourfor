using Dourfor.Core.Handlers;
using Dourfor.Core.Requests.Profiles;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dourfor.Web.Pages.Profiles;

public partial class EditProfilePage : ComponentBase
{
    #region Properties

           private string imageDataUrl;
    public bool IsBusy { get; set; } = false;
    public UpdateProfileRequest InputModel { get; set; } = new();

    #endregion

    #region Parameters

    [Parameter]
    public string Id { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public IProfileHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        GetProfileByIdRequest? request = null;
        try
        {
            request = new GetProfileByIdRequest
            {
                Id = long.Parse(Id)
            };
        }
        catch
        {
            Snackbar.Add("Parâmetro inválido", Severity.Error);
        }

        if (request is null)
            return;

        IsBusy = true;
        try
        {
            var response = await Handler.GetByIdAsync(request);
            if (response is { IsSuccess: true, Data: not null })
                InputModel = new UpdateProfileRequest
                {
                    Id = response.Data.Id,
                    Title = response.Data.Title,
                    Description = response.Data.Description,
                    Age = response.Data.Age,
                    ImageUrl = response.Data.ImageUrl
                };
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

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.UpdateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add("Categoria atualizada", Severity.Success);
                NavigationManager.NavigateTo("/profiles");
            }
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
}