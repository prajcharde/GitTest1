using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Repository.Entity
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
       [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }
        public bool MaritalStatus { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [DataType(DataType.ImageUrl)]
        public string image { get; set; }
    }
}

