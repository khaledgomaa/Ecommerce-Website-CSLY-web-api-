using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSLY.Models;
using CSLY.Dtos;

namespace CSLY.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Category, CategoryDto>();
            Mapper.CreateMap<CategoryDto, Category>();

            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<ProductDto, Product>();
        }
    }
}