using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    using System.ComponentModel;

    public enum ContentType
    {
        [Description("Applicatin/PDF")]
        ApplicationPDF,

        [Description("Applicatin/ZIP")]
        ApplicationZIP
    }
}
