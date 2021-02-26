// <copyright file="ProductManagementController.cs" company="Pankaj">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>PANKAJTHAKUR\User</author>
// <date>2/21/2021 1:46:30 PM</date>
// <summary>The class is used to perform CRUD Operation in product</summary>

namespace ProductManagement.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProductManagement.DataAccess.Interface;
    using ProductManagement.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ProductManagementController" />.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]

    public class ProductManagementController : ControllerBase
    {
        /// <summary>
        /// Defines the _unitOfWork.
        /// </summary>
        public readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductManagementController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unitOfWork<see cref="IUnitOfWork"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public ProductManagementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// The GetProducts.
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{Product}}}"/>.</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            IEnumerable<Product> productList = await _unitOfWork.Product.GetAllAsync(includeProperties: "Category");
            var productDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(productList);
            return Ok(productDto);
        }

        /// <summary>
        /// The CreateProducts.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{Product}}}"/>.</returns>
        [HttpPost]
        [Route("AddProduct")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> CreateProducts([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Product.AddAsync(product);
                bool success = await _unitOfWork.Save();
                if (success)
                    return Ok("Product Added Successfully");
                else
                    return NotFound("Error Saving data");
            }
            return BadRequest();
        }

        /// <summary>
        /// The DeleteProducts.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult}"/>.</returns>
        [HttpDelete]
        [Route("RemoveProduct/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteProducts(int id)
        {
            await _unitOfWork.Product.RemoveAsync(id);
            bool success = await _unitOfWork.Save();
            if (!success)
            {
                return NotFound("Error Deleting Product");
            }
            return Ok("Product Removed Successfully");
        }

        /// <summary>
        /// The UpdateProduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPut]
        [Route("UpdateProduct")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {

                _unitOfWork.Product.Update(product);

                bool success = await _unitOfWork.Save();
                if (!success)
                {
                    return NotFound("Product Not Found");
                }
                return Ok("Updated Successfully");
            }
            return BadRequest();
        }

        /// <summary>
        /// The GetProducts.
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{Product}}}"/>.</returns>
        [HttpGet]
        [Route("CategoryList")]
        public async Task<ActionResult<IEnumerable<Product>>> GetCategoryList()
        {
            IEnumerable<Category> categoryList = await _unitOfWork.Category.GetAllAsync();
            if (categoryList == null)
                return BadRequest("Category not found");
            var categoryDto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categoryList);
            return Ok(categoryDto);
        }
    }
}
