using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MedicalApplication.Module.BusinessObjects.Utilities
{
    public class MessageHelper
    {
        /// <summary>
        /// Afișează un mesaj de avertizare în partea de sus a aplicației
        /// </summary>
        /// <param name="message"></param>
        public static void ShowWarningMessage(string message, XafApplication application)
        {

            MessageOptions mo = new MessageOptions();
            mo.Duration = 10000;
            mo.Message = message;
            mo.Type = InformationType.Warning;
            mo.Web.Position = InformationPosition.Top;
            mo.OkDelegate = null;
            application.ShowViewStrategy.ShowMessage(mo);
        }

        /// <summary>
        /// Afișează un mesaj de succes în partea de sus a aplicației
        /// </summary>
        /// <param name="message"></param>
        public static void ShowSuccessMessage(string message, XafApplication application)
        {

            MessageOptions mo = new MessageOptions();
            mo.Duration = 10000;
            mo.Message = message;
            mo.Type = InformationType.Success;
            mo.Web.Position = InformationPosition.Top;
            mo.OkDelegate = null;
            application.ShowViewStrategy.ShowMessage(mo);
        }
    }
}
