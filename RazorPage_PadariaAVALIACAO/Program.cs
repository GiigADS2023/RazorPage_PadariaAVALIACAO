using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPage_PadariaAVALIACAO.Data;
using System.Globalization;
using RazorPage_PadariaAVALIACAO.Services;
namespace RazorPage_PadariaAVALIACAO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RazorPage_PadariaAVALIACAOContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RazorPage_PadariaAVALIACAOContext") ?? throw new InvalidOperationException("Connection string 'RazorPage_PadariaAVALIACAOContext' not found.")));

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<VendaService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var defaultCulture = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };

            app.UseRequestLocalization(localizationOptions);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
