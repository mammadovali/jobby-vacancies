using Jobby.Infrastructure.Operations;
using Jobby.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace Jobby.WebApi.SeedDatas
{
    public class ApplicationSeedDbContext
    {
        public async Task SeedAsync(ApplicationDbContext context, ILogger<ApplicationSeedDbContext> logger)
        {
            var policy = CreatePolicy(logger, nameof(ApplicationSeedDbContext));
            using (context)
            {
                await policy.ExecuteAsync(async () =>
                {
                    await UserSeedAsync(context);
                    if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
                });
            }
        }

        private async Task UserSeedAsync(ApplicationDbContext context)
        {
            // Admin #1
            if (!await context.Users.AnyAsync(u => u.Email == "alimammadsoy@gmail.com"))
            {
                var user1 = new Domain.Entities.Identity.User(
                    "alimammadsoy@gmail.com",
                    PasswordHasher.HashPassword("jobby$admin!")
                );

                user1.SetProfile("Jobby Admin");
                await context.Users.AddAsync(user1);
            }
            await context.SaveChangesAsync();

        }

        private AsyncRetryPolicy CreatePolicy(ILogger<ApplicationSeedDbContext> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().WaitAndRetryAsync(

                retries,
                retry => TimeSpan.FromSeconds(5),
                (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogTrace($"{prefix} Exception {exception.GetType().Name} with message {exception.Message} detected on attemt {retry} of {retries}");
                });
        }
    }
}
