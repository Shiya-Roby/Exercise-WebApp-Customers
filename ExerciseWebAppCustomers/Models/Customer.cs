using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseWebAppCustomers.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CId { get; set; }
        public string CName { get; set; }
        public string CPic { get; set; }
        public string CCity { get; set; }
        [NotMapped]/* Database wont store*/
        public IFormFile PicFile { get; set; }
    }
}
