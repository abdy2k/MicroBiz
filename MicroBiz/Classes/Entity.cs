﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroBiz
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}