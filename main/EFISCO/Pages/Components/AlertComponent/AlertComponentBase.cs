using EFISCO.Enums;
using Microsoft.AspNetCore.Components;

namespace EFISCO.Pages.Components.AlertComponent
{
    public class AlertComponentBase:ComponentBase
    {
        protected string Error { get; set; }
        protected string Aviso { get; set; }
        protected string Sucesso { get; set; }

        [Inject] AlertService AlertService { get; set; }

        protected override void OnInitialized()
        {
            AlertService.OnShow += ShowAlert;
            AlertService.OnHide += HideAlert;
        }

        protected void HideAlert()
        {
            Error = Aviso = Sucesso = "";
           
            InvokeAsync(StateHasChanged);

        }

        public void ShowAlert(string text,TiposAviso tiposAviso)
        {
            switch (tiposAviso)
            {
                case TiposAviso.Erro:
                    Error = text;
                    break;
                case TiposAviso.Sucesso:
                    Sucesso = text;
                    break;
                case TiposAviso.Aviso:
                    Aviso = text;
                    break;
                default:
                    break;
            }
            InvokeAsync(StateHasChanged);
        }

    }
}
