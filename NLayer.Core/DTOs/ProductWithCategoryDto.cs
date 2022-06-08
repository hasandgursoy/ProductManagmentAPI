using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ProductWithCategoryDto : ProductDto // Product Dto ' da Category Bilgisi olmadığı için miras alıyoruz.
    {
        public CategoryDto Category { get; set; }



    }
}
