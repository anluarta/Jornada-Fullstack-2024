﻿using Fina.Api.Data;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Categories;
using Fina.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handler
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };
            try
            {

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria criada");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao criar categoria");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);
                if (category == null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Categoria deletada");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao deletar categoria");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query = context.Categories
                .AsNoTracking()
                .Where(c => c.UserId == request.UserId)
                .OrderBy(c => c.Title);

                var categories = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(data: categories,
                                                          currentPage: request.PageNumber,
                                                          pageSize: request.PageSize,
                                                          totalCount: count);
            }
            catch
            {
                return new PagedResponse<List<Category>>(null, 500, "Erro ao buscar categorias");
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                return category is null
                    ? new Response<Category?>(null, 404, "Categoria não encontrada")
                    : new Response<Category?>(category);
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao buscar categoria");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);
                if (category == null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }
                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Categoria atualizada");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao atualizar categoria");
            }
        }
    }
}
