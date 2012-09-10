using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AsteriskIVRStep
    {
        #region Contstants

        public const string IVR_ACTION_REDIRECT_CODE = "REDIRECT";
        public const string IVR_ACTION_STREAM_CODE = "STREAM_FILE";
        public const string IVR_ACTION_SAY_TIME_CODE = "SAY_TIME";

        #endregion

        public int AsteriskIVRStepID { get; set; }
        public string KeyPress { get; set; }
        public string Action { get; set; }
        public string Data { get; set; }
        public int Submenu { get; set; }
        public int Step { get; set; }
        public int? GoToStep { get; set; }
        public int AsteriskIVRDefinitionID { get; set; }

        public virtual AsteriskIVRDefinition AsteriskIVRDefinition { get; set; }

    }
}
