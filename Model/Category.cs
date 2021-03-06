﻿using System.Collections;
using System.Collections.Generic;

namespace BotWebApi.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}