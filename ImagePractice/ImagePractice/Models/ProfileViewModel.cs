using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImagePractice.Models
{
    public class ProfileViewModel
    {
        [Key]
        public int ID { get; set; } //primary key will be auto increament

        public byte[] UploadedFile { get; set; } //This is will saved as varcharbindary(max) into the database
    }
    public class Context : DbContext
    {
        public Context()
          : base("FileContext") //connection string name in the database
        {

        }

        public DbSet<ProfileViewModel> FileTable { get; set; }
    }
}