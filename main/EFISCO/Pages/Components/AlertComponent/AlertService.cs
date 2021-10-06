using EFISCO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFISCO.Pages.Components.AlertComponent
{
    public class AlertService
    {
        public event Action<string, TiposAviso> OnShow;

        public event Action OnHide;

        //public delegate void EventHandler(object sender, string text);
        //public event EventHandler ShowText;

        //public void GO(object obj , string text)
        //{
        //    ShowText?.Invoke(obj,text);
        //}

        public void Show(string text,TiposAviso tiposAviso)
        {
            OnShow?.Invoke(text, tiposAviso);
        }
       
        public void Hide()
        {
            OnHide?.Invoke();
        }
    }
}
