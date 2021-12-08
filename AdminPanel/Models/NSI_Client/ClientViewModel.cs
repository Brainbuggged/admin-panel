using AdminPanel.Models.Models.NSI_Client;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models.Models.NSI_Client
{
    public class ClientViewModel :ClientModel
    {
        [Display(Name = "Изображение")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
