using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class ClientLoginViewModels
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        [DisplayName("名")]
        [StringLength(10, ErrorMessage = "{0} 最大不超過{1}個字元")]
        public string FirstName { get; set; }
        [StringLength(10, ErrorMessage = "{0} 最大不超過{1}個字元")]

        [DisplayName("中間名")]
        [Required]
        [DataType(DataType.Password)]
        public string MiddleName { get; set; }
        [StringLength(10, ErrorMessage = "{0} 最大不超過{1}個字元")]

        [DisplayName("姓名")]
        [Required]
        public string LastName { get; set; }
    }
}