using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arvind_Project_Ado.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public string DateFormat { get; set; }
    }
}