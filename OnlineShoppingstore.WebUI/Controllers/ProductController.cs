﻿using OnlineShoppingstore.WebUI.Models;
using OnlineShoppingStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingstore.WebUI.Controllers {
    public class ProductController : Controller {
        private readonly IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repo) {
            repository = repo;
        }
        public ViewResult List(string category, int page = 1) {
            ProductsListViewModel model = new ProductsListViewModel {
                Products = repository.Products
                        .Where(p => category == null || p.Category == category)
                        .OrderBy(p => p.Id)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}