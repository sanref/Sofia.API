using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sofia.API.Models;

namespace Sofia.API.Controllers
{
    public static class ASSETEndpoints
    {
        public static void MapASSETEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/ASSET").WithTags(nameof(ASSET));

            group.MapGet("/", async (SofiaContext db) =>
            {
                return await db.ASSETs.ToListAsync();
            })
            .WithName("GetAllASSETs")
            .WithOpenApi();

            group.MapGet("/{id}", async Task<Results<Ok<ASSET>, NotFound>> (int asset_id, SofiaContext db) =>
            {
                return await db.ASSETs.AsNoTracking()
                    .FirstOrDefaultAsync(model => model.asset_id == asset_id)
                    is ASSET model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
            .WithName("GetASSETById")
            .WithOpenApi();

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int asset_id, ASSET aSSET, SofiaContext db) =>
            {
                var affected = await db.ASSETs
                    .Where(model => model.asset_id == asset_id)
                    .ExecuteUpdateAsync(setters => setters
                      .SetProperty(m => m.asset_id, aSSET.asset_id)
                      .SetProperty(m => m.asset_name, aSSET.asset_name)
                      .SetProperty(m => m.symbol, aSSET.symbol)
                      .SetProperty(m => m.company, aSSET.company)
                      );
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateASSET")
            .WithOpenApi();

            group.MapPost("/", async (ASSET aSSET, SofiaContext db) =>
            {
                db.ASSETs.Add(aSSET);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/ASSET/{aSSET.asset_id}", aSSET);
            })
            .WithName("CreateASSET")
            .WithOpenApi();

            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int asset_id, SofiaContext db) =>
            {
                var affected = await db.ASSETs
                    .Where(model => model.asset_id == asset_id)
                    .ExecuteDeleteAsync();
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeleteASSET")
            .WithOpenApi();

            group.MapGet("/getByName/{id}", async Task<Results<Ok<ASSET>, NotFound>> (int asset_id, SofiaContext db) =>
            {
                return await db.ASSETs.AsNoTracking()
                    .FirstOrDefaultAsync(model => model.asset_id == asset_id)
                    is ASSET model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
            .WithName("GetByName")
            .WithOpenApi();
        }
    }
}
