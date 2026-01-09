using Dourfor.Api.Data;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Profiles;
using Dourfor.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dourfor.Api.Handlers;

public class ProfileHandler(AppDbContext context) : IProfileHandler
{

    public async Task<Response<Profile?>> UpdateAsync(UpdateProfileRequest request)
    {
        try
        {
            var model = await context
                .Profiles
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (model is null)
            {
                var profile = new Profile
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description,
                    Age = request.Age,
                    ImageUrl = request.ImageUrl
                };

                context.Profiles.AddAsync(profile);
                await context.SaveChangesAsync();

                return new Response<Profile?>(profile, 201, "Categoria criada com sucesso!");
            }
            else
            {
                model.Title = request.Title;
                model.Description = request.Description;
                model.ImageUrl = request.ImageUrl;

                context.Profiles.Update(model);
                await context.SaveChangesAsync();

                return new Response<Profile?>(model, message: "Categoria atualizada com sucesso");
            }
        }
        catch
        {
            return new Response<Profile?>(null, 500, "Não foi possível alterar a categoria");
        }
    }

    public async Task<Response<Profile?>> DeleteAsync(DeleteProfileRequest request)
    {
        try
        {
            var model = await context
                .Profiles
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (model is null)
                return new Response<Profile?>(null, 404, "Categoria não encontrada");

            context.Profiles.Remove(model);
            await context.SaveChangesAsync();

            return new Response<Profile?>(model, message: "Categoria excluída com sucesso!");
        }
        catch
        {
            return new Response<Profile?>(null, 500, "Não foi possível excluir a categoria");
        }
    }

    public async Task<Response<Profile?>> GetByIdAsync(GetProfileByIdRequest request)
    {
        try
        {
            var model = await context
                .Profiles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return model is null
                ? new Response<Profile?>(null, 404, "Categoria não encontrada")
                : new Response<Profile?>(model);
        }
        catch
        {
            return new Response<Profile?>(null, 500, "Não foi possível recuperar a categoria");
        }
    }

    public async Task<PagedResponse<List<Profile>>> GetAllAsync(GetAllProfilesRequest request)
    {
        try
        {
            var query = context
                .Profiles
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);

            var model = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Profile>>(
                model,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Profile>>(null, 500, "Não foi possível consultar as categorias");
        }
    }
}