using System;
using Asterisk.NET.FastAGI;
using Domain.Entities;
using Domain.Concrete;
using System.Linq;
using System.Collections.Generic;

namespace Asterisk.IVR
{
    public class IVRDefinition : AGIScript
    {
        //TODO wczytywac to z bazy danych, dla konktetnego IVR
        private string escapeKeys = "0123456789*#";

        public override void Service(AGIRequest request, AGIChannel channel)
        {
            // First of all i need user definition of IVR (if exist) from DB
            var steps = LoadUserDefinitionForIVR(request.Dnid);

            //Next we pick up the phone
            Answer();

            if (steps != null)
            {
                // Executing menu algorytm defined in VRG definition
                doSteps(steps);
            }

            Hangup();
        }

        #region Methods

        private void doSteps(List<AsteriskIVRStep> steps)
        {
            int submenu = 0;
            string key = "\0";
            int nextStep = 0;
            bool goTo = true;
            AsteriskIVRStep currentStep = null;

            var startMenu = steps.Where(s => s.Step == 0).FirstOrDefault();
            string startMenuKey = startMenu.KeyPress;

            while (true)
            {
                // Try to find current step
                if (goTo)
                {
                    currentStep = steps.Where(s => s.Step == nextStep && s.Submenu == submenu).FirstOrDefault();
                    goTo = false;
                }
                else
                {
                    if (key != "\0")
                    {
                        if (key == startMenuKey)
                            currentStep = startMenu;
                        else
                            currentStep = steps.Where(s => s.KeyPress == key && s.Submenu == submenu).FirstOrDefault();
                        key = "\0";
                    }
                    else
                    {
                        currentStep = null;
                    }
                }

                if (currentStep != null)
                {

                    submenu = currentStep.Submenu;

                    if (!string.IsNullOrEmpty(currentStep.Action))
                    {
                        key = doStepAction(currentStep.Action, currentStep.Data);
                    }

                    if (currentStep.GoToStep.HasValue)
                    {
                        nextStep = currentStep.GoToStep.Value;
                        goTo = true;
                    }
                    else
                    {
                        if (key == "\0")
                        {
                            key = WaitForDigit(5000).ToString();
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private List<AsteriskIVRStep> LoadUserDefinitionForIVR(string extention)
        {
            var repository = new EFAsteriskIVRDefinitionRepository("Data Source=.\\SQLEXPRESS; Initial Catalog=ccrm_db; Integrated Security=True;");
            var IVRDefinition = repository.SearchFor(a => a.Extention == extention).FirstOrDefault();
            if (IVRDefinition != null)
            {
                return IVRDefinition.AsteriskIVRSteps.ToList();
            }
            else
                return null;
        }

        private string doStepAction(string action, string data)
        {
            char escChar = '\0';

            switch (action)
            {
                case AsteriskIVRStep.IVR_ACTION_REDIRECT_CODE:
                    Exec("Dial", data);
                    break;
                case AsteriskIVRStep.IVR_ACTION_SAY_TIME_CODE:
                    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    SayTime(Convert.ToInt64((DateTime.Now - epoch).TotalSeconds), escapeKeys);
                    break;
                case AsteriskIVRStep.IVR_ACTION_STREAM_CODE:
                    escChar = StreamFile("press-1", escapeKeys);
                    break;
            }

            return escChar.ToString();
        }

        #endregion
    }
}
