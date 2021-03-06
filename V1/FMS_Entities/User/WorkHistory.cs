﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Entities
{
  public  class WorkHistory
    {
       
            [Key]
            public int UserId { get; set; }
           
      [ForeignKey("UserId")]
            public UserInfo UserInfo;
           
      [Required]
            public string CompanyName { get; set; }
          
      [Required]
            public string Position { get; set; }
            [Required]
            public string  Experience{ get; set; }



    }
}
