using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWebAPI
{
    public class Utility
    {

        public static string Encrypt(string password)
        {
            var provider = MD5.Create();
            string salt = "S0m3R@nd0mSalt";            
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-","").ToLower();
        }

        public static string DefaultPassEnc()
        {
            string password = ConfigurationManager.AppSetting["AppSettings:DefaultPass"];
            return Encrypt(password);
        }

        public static string DefaultPass()
        {
            return ConfigurationManager.AppSetting["AppSettings:DefaultPass"];
        }

        public const double EarthRadius = 6371;
        public static double GetDistance(double Latitude1, double Longitude1, double Latitude2 , double Longitude2)
        {
            double distance = 0;
            double Lat = (Latitude2 - Latitude1) * (Math.PI / 180);
            double Lon = (Longitude2 - Longitude1) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(Latitude1 * (Math.PI / 180)) * Math.Cos(Latitude2 * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = EarthRadius * c;
            return distance;
        }
    }

    static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
