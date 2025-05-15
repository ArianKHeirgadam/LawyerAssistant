using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Enums
{
    public enum ActiveStatus
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "فعال")]
        Active = 1 ,
        //==============================
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "غیر فعال")]
        Deactive = 0
    }
}
