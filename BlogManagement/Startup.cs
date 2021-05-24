using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogManagement.Utility;
using BlogManagement.Utility.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using BlogManagement.Core;
using BlogManagement.Dal;
using BlogManagement.Interface;
using FreeSql;
using Swashbuckle.AspNetCore.Filters;

namespace BlogManagement
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
            services.AddControllers(option =>
            {
                option.Filters.Add<BlogExceptionFilter>();
            });
            
            services.AddSingleton<BlogActionFilter>();
            services.AddScoped(typeof(IUser), typeof(UserDal));

            #region 跨域

            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.WithOrigins("*");
                });
            });

            #endregion

            #region JWTToken
            JWTTokenOptions jwtTokenOptions = new JWTTokenOptions();
            services.Configure<JWTTokenOptions>(this.Configuration.GetSection("JWTToken"));
            jwtTokenOptions = this.Configuration.GetSection("JWTToken").Get<JWTTokenOptions>();
            //Configuration.Bind("JWTToken", jwtTokenOptions);
            services.AddSingleton<JWTTokenOptions>(jwtTokenOptions);
            
            services.AddAuthentication(option =>
                {
                    //认证middleware配置
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        //获取或设置要使用的Microsoft.IdentityModel.Tokens.SecurityKey用于签名验证
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenOptions.Secret)),
                        //获取或设置一个string，它表示将使用的有效发行者检查代币的发行者
                        ValidIssuer = jwtTokenOptions.Issuer,
                        //获取或设置一个字符串，该字符串表示将用于检查的有效受众反对令牌的观众
                        ValidAudience = jwtTokenOptions.Audience,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        //允许的服务器时间偏移量
                        ClockSkew = TimeSpan.Zero,
                        //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                        ValidateLifetime = true
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            #endregion

            #region Swagger

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "BlogManagement API Doc-V1",
                    Description = "BlogManagement API接口文档-V1版",
                    Contact = new OpenApiContact { Name = "BlogSystem", Email = "liutao19980127@gmial.com" },
                });
                options.OrderActionsBy(x => x.RelativePath);

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "BlogManagement.xml"));

                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "请在输入时添加Bearer和一个空格",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/V1/swagger.json", "ApiJelperDoc-V1");
                x.RoutePrefix = "";//路径配置设置为空，表示直接在根域名下访问该文件
            });

            app.UseRouting();

            app.UseCors("any");

            app.UseAuthentication();//鉴权
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
