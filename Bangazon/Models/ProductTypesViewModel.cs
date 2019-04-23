using System.Collections.Generic;
using Bangazon.Models;

namespace Bangazon.Controllers
{
    public class ProductTypesViewModel
    {
        public ProductTypesViewModel()
        {
        }

        public List<GroupedProducts> GroupedProducts { get; set; }
    }
}