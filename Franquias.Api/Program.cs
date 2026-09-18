
using System.Text;
using Franquias.Api.Data;
using Franquias.Api.Repositories;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBFranquias>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Conexao")));

builder.Services.AddScoped<PerfilRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<FranqueadoraRepository>();
builder.Services.AddScoped<UnidadeFranqueadaRepository>();
builder.Services.AddScoped<FranqueadoRepository>();
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<ProdutoServicoRepository>();
builder.Services.AddScoped<FornecedorRepository>();
builder.Services.AddScoped<EstoqueRepository>();
builder.Services.AddScoped<VendaRepository>();
builder.Services.AddScoped<CobrancaRepository>();
builder.Services.AddScoped<ChamadoSuporteRepository>();

builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<FranqueadoraService>();
builder.Services.AddScoped<UnidadeFranqueadaService>();
builder.Services.AddScoped<FranqueadoService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoServicoService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<IEstoqueService,EstoqueService>();
builder.Services.AddScoped<VendaService>();
builder.Services.AddScoped<CobrancaService>();
builder.Services.AddScoped<ChamadoSuporteService>();
builder.Services.AddScoped<RelatorioService>();

builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Emissor"],
        ValidAudience = builder.Configuration["Jwt:Audiencia"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Chave"]!)
        )
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Franquias API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Description = "Digite apenas o token JWT (sem a palavra 'Bearer' na frente)."
    });

    options.AddSecurityRequirement(document =>
        new Microsoft.OpenApi.OpenApiSecurityRequirement
        {
            [new Microsoft.OpenApi.OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
        });
});
var app = builder.Build();


using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider.GetRequiredService<DBFranquias>();
    contexto.Database.Migrate();
    SeedData.Popular(contexto);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/",() => Results.Redirect("/swagger")).ExcludeFromDescription();

app.Run();