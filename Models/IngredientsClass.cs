using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesMVC.Models
{
    public class IngredientsClass
    {
        public IngredientsClass()
        {
            IngredientsIndices = new HashSet<IIClass>();
        }

        public int Iid { get; set; }
        public string Iname { get; set; }

        public virtual ICollection<IIClass> IngredientsIndices { get; set; }
    }
}
