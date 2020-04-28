using KofCWebSite.Core.Enums;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models.Popup
{
    delegate void SetButton();
    public class PopupButtonModel
    {
        public PopupButtonModel()
        {

        }

        public PopupButtonModel(ButtonType buttonType)
        {
            SetButton buttonSetter = buttonType switch
            {
                ButtonType.Close => SetAsClose,
                ButtonType.Cancel => SetAsCancel,
                ButtonType.Save => SetAsSave,
                ButtonType.SaveAndContinue => SetAsSaveAndContinue,
                ButtonType.Ok => SetAsOk,
                ButtonType.Back => SetAsBack,
                _ => SetAsClose
            };

            buttonSetter?.Invoke();
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public string ButtonCss { get; set; } 
        public string IconCss { get; set; }
        public bool Visible { get; set; } = true;
        public bool DataDismiss { get; set; } = true;
        public string ContainerSelector { get; set; } = ".modal-footer";

        public string ButtonHtml
        {
            get
            {
                //<button id="btnClose" type="button" class="btn btn-success" data-dismiss="modal"> Close</button>
                var buttonStr = new StringBuilder();
                buttonStr.Append("<button type='button' ");
                buttonStr.Append($"id='{Id}' ");
                buttonStr.Append($"class='{ButtonCss}' ");
                if (DataDismiss)
                    buttonStr.Append($"data-dismiss='modal' ");

                buttonStr.Append("> ");

                if (!string.IsNullOrWhiteSpace(IconCss))
                {
                    buttonStr.Append($"<span class='{IconCss}'></span> ");
                }

                buttonStr.Append(Text);
                buttonStr.Append("</button>");

                return buttonStr.ToString();
            }
        }

        private void SetAsClose()
        {
            Id = "btnClose";
            Text = "Close";
            ButtonCss = "btn btn-primary";
            IconCss = "fa fa-stop-circle";
        }

        private void SetAsOk()
        {
            SetAsClose();
            Text = "Ok";
            ButtonCss = "btn btn-outline-primary";
            IconCss = "";
        }

        private void SetAsBack()
        {
            SetAsClose();
            Text = "Back";
            IconCss = "fa fa-chevron-left";
        }

        private void SetAsCancel()
        {
            Id = "btnCancel";
            Text = "Cancel";
            ButtonCss = "btn btn-outline-danger";
            IconCss = "fa fa-ban";
        }

        private void SetAsSave()
        {
            Id = "btnSave";
            Text = "Save";
            ButtonCss = "btn btn-success";
            IconCss = "fa fa-check-circle";
        }

        private void SetAsSaveAndContinue()
        {
            SetAsSave();
            Text = "Save And Continue";
            IconCss = "fa fa-play-circle";
        }
    }
}
