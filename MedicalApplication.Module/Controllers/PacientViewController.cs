using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using MedicalApplication.Module.BusinessObjects;
using MedicalApplication.Module.BusinessObjects.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MedicalApplication.Module.Controllers
{
    public partial class PacientViewController : ObjectViewController<DetailView, Pacient>
    {
        public PacientViewController()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            var modificationController = Frame.GetController<ModificationsController>();
            if (modificationController != null)
            {
                modificationController.SaveAction.Executing += SaveAction_Executing; ;
            }
        }

        private void SaveAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var pacient = (Pacient)View.CurrentObject;
            var (isValid, message) = Validate(pacient);
            if (isValid)
            {
                MessageHelper.ShowSuccessMessage(message, Application);
            }
            else
            {
                MessageHelper.ShowWarningMessage(message, Application);
                e.Cancel = true;
            }
        }


        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        #region Public - Validation
        /// <summary>
        /// Implements entity-level validation for CNP and Phone rules.
        /// </summary>
        private (bool, string) Validate(Pacient pacient)
        {

            // CNP: optional in model? Business requirement says it must be valid when present
            if (!string.IsNullOrWhiteSpace(pacient.CNP))
            {
                var cnpError = FieldsValidator.Validate(pacient.CNP); 
                if (cnpError is not null)
                {
                    return (false, cnpError);
                }
            }

            // Phone: optional; if present allow either E.164 or Romanian local format
            if (!string.IsNullOrWhiteSpace(pacient.NumarTelefon))
            {
                var (result, message) = FieldsValidator.IsValidPhone(pacient.NumarTelefon);
                if (!result)
                {
                    return (false, message);
                }
            }
            return (true, "");
        }
        #endregion


    }
}
