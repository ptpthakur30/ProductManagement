using AutoMapper;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Utility
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name));
            CreateMap<Category, CategoryDTO>();
        }
        
    }
}
