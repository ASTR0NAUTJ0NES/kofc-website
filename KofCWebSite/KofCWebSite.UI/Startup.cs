using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Core.Services;
using KofCWebSite.Data.KofC;
using KofCWebSite.Data.Repositories;
using KofCWebSite.ReCaptcha;
using KofCWebSite.UI.Areas.Identity.Library;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SendGrid;
using System;

namespace KofCWebSite.UI
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
            string cs = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<KofCDbContext>(opts => opts.UseMySql(cs));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<KofCDbContext>();

            services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(3));

            services.AddScoped(typeof(IKofCRepository<Contact>), typeof(KofCRepository<Contact>));
            services.AddScoped(typeof(IKofCRepository<ContactType>), typeof(KofCRepository<ContactType>));
            services.AddScoped(typeof(IKofCRepository<ContactAddress>), typeof(KofCRepository<ContactAddress>));
            services.AddScoped(typeof(IKofCRepository<Album>), typeof(KofCRepository<Album>));
            services.AddScoped(typeof(IKofCRepository<AlbumImage>), typeof(KofCRepository<AlbumImage>));
            services.AddScoped(typeof(IKofCRepository<Request>), typeof(KofCRepository<Request>));
            services.AddScoped(typeof(IKofCRepository<Event>), typeof(KofCRepository<Event>));
            services.AddScoped(typeof(IKofCRepository<UploadedFile>), typeof(KofCRepository<UploadedFile>));
            services.AddScoped(typeof(IKofCRepository<NewsItem>), typeof(KofCRepository<NewsItem>));

            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IContactsService, ContactsService>();
            services.AddScoped<IAlbumsService, AlbumsService>();
            services.AddScoped<IRequestsService, RequestsService>();
            services.AddScoped<IImagesService, ImagesService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<INewslettersService, NewslettersService>();
            services.AddScoped<INewsItemsService, NewsItemsService>();

            services.AddTransient<IEmailSender, EmailSender>();           
            services.Configure<EmailSenderOptions>(Configuration.GetSection("EmailSenderOptions"));

            services.AddScoped<IGoogleReCaptchaClient, GoogleReCaptchaClient>();
            services.AddScoped<IReCaptchaService, ReCaptchaService>();
            services.AddScoped<ReCaptchaClientSettings>(opt =>
            {
                ReCaptchaClientSettings cfg = new ReCaptchaClientSettings();
                Configuration.GetSection("ReCaptchaSettings").Bind(cfg);
                return cfg;
            });

            services.AddControllersWithViews();
            services.AddMvc();
            //services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
