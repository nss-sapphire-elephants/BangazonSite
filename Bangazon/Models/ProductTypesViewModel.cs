using System.Collections.Generic;
using Bangazon.Models;

namespace Bangazon.Controllers
{
    internal class ProductTypesViewModel
    {
        public ProductTypesViewModel()
        {
        
        }

        public List<GroupedProducts> GroupedProducts { get; internal set; }
    }
}