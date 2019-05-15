using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TouristPlaceApi.Models;

namespace TouristPlaceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => { 
                  cfg.CreateMap<Place, Places>().ForMember(d => d.Country, p => p.MapFrom(s => s.Country.Name));
                  //cfg.CreateMap<Places, Place>().ForMember(d => d.Country, opt => opt.Ignore());
                 }
            );

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
