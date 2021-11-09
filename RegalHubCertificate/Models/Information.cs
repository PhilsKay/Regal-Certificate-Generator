using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegalHubCertificate.Models
{
    public class Information
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password,ErrorMessage ="Invalid")]
        public string Pass { get; set; }
    }
}
