using CreatureCreator.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton(c =>
{
    return new CreatureCreatorService(
                builder.Configuration.GetValue<string>("MySqlServer"),
        builder.Configuration.GetValue<string>("MySqlUsername"),
        builder.Configuration.GetValue<string>("MySqlPassword"),
        builder.Configuration.GetValue<string>("MySqlWorldDb"),
        builder.Configuration.GetValue<string>("MySqlCharactersDb"),
        builder.Configuration.GetValue<string>("MySqlHotfixDb"),
        builder.Configuration.GetValue<int>("VerifiedBuild"),
        builder.Configuration.GetValue<int>("IdRangeFrom"),
        builder.Configuration.GetValue<int>("IdRangeTo")
            );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();