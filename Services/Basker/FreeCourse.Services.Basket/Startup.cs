using FreeCourse.Services.Basket.Services;
using FreeCourse.Services.Basket.Settings;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //id "sub" key'i olarak gelsin nameidentifier olarak gelmesin diye
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");


            //IdentityToken ile userId okumak için
            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            //IdentityToken için
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = Configuration["IdentityServerURL"];
                options.Audience = "resource_basket";
                options.RequireHttpsMetadata = false;
            });

            //Redis settingsi sürekli option ile çağırmamak için
            services.AddSingleton<RedisService>(sp =>
            {
                var redisSetting = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
                var redis = new RedisService(redisSetting.Host, redisSetting.Port);
                redis.Connect();
                return redis;
            });

            //Appsettingde tanımladığımız değerleri otomatik olarak RedisSetting sınıfına kaydediyor
            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));

            //bu kısımda sisteme giriş yapmış bir kullanıcı gerekli
            var requreAuthorize = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requreAuthorize));
            });

            services.AddScoped<IBasketService, BasketService>();


            


            



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCourse.Services.Basket", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCourse.Services.Basket v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
