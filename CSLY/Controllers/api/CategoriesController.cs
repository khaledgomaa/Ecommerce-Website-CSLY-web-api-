using CSLY.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSLY.Models;
using AutoMapper;
using CSLY.Dtos;
using CSLY.MyAuthorizations;

namespace CSLY.Controllers.api
{
    [JwtAuthorize]
    public class CategoriesController : ApiController
    {
        private GenericUnitofWork dbContext = new GenericUnitofWork();

        public IHttpActionResult GetCategories(string query = null)
        {
            var categoryInDb = dbContext.GetRepositoryInstance<Category>()
                .GetAll()
                .ToList();
            if (!string.IsNullOrWhiteSpace(query))
                categoryInDb = categoryInDb.Where(c => c.CategoryName.Contains(query)).ToList();

            return Ok(categoryInDb);
        }


        public IHttpActionResult GetCategory(int id)
        {
            var categoryInDb = dbContext.GetRepositoryInstance<Category>()
                .GetFirstOrDefault(id);
            if(categoryInDb != null && categoryInDb.IsActive == true)
            {
                return Ok(categoryInDb);
            }
            return NotFound();
        }

        
        [Route("api/Categories/GetCategoriesPagination/{status}/{pageNumber}")]
        public IHttpActionResult GetCategoriesPagination(int status , int pageNumber)
        {
            var categoryInDb = dbContext.GetRepositoryInstance<Category>()
                .GetAll()
                .Skip(status * (pageNumber - 1))
                .Take(status);

            if (categoryInDb == null)
                return NotFound();
            return Ok(categoryInDb);
        }

        [HttpPost]
        public IHttpActionResult AddCategory(Category newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            newCategory.IsActive = true;
            dbContext.GetRepositoryInstance<Category>().Add(newCategory);
            dbContext.Complete();
            return Created(new Uri(Request.RequestUri + "/" + newCategory.CategoryId), newCategory);
        }

        [HttpPut]
        public IHttpActionResult UpdateCategory(Category newcategory,int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categoryInDb = dbContext.GetRepositoryInstance<Category>().GetFirstOrDefault(id);
            if(categoryInDb != null)
            {
                if (categoryInDb.IsActive == true)
                {
                    categoryInDb.CategoryName = newcategory.CategoryName;
                    dbContext.Complete();
                    return Created(new Uri(Request.RequestUri + "/" + categoryInDb.CategoryId), categoryInDb);
                }
            }
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var categoryId = new Category { CategoryId = id };
            dbContext.GetRepositoryInstance<Category>().Remove(categoryId);
            dbContext.Complete();
            return Ok();
        }

    }
}

