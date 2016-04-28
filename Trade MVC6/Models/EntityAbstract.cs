using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trade_MVC6.Models
    {
    public abstract class EntityAbstract
        {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationAuthor { get; set; }
        public DateTime LastEditDate { get; set; }
        public string LastEditor { get; set; }

        }
    }
