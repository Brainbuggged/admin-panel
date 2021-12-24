using System;
using System.Collections.Generic;
using AdminPanel.Models;

namespace AdminPanel.Extensions
{
    public class SettingsExtension
    {
        private const string signalrConnectionString = "http://77.73.69.181:91/api";
        private DateTime DateNow = DateTime.UtcNow.AddHours(3);
        private string availableProductStatuses = $"{(int)ProductStatus.Vistavlen}, {(int)ProductStatus.zakonchilsya}";
        private List<ProductStatus> list_AvailableProductStatuses = new List<ProductStatus> { ProductStatus.Vistavlen, ProductStatus.zakonchilsya };


        public string GetSignalrConnectionString() => signalrConnectionString;
        public DateTime GetDateTimeNow() => this.DateNow;
        public string AvailableProductStatuses() => this.availableProductStatuses;
        public List<ProductStatus> ListAvailableProductStatuses() => this.list_AvailableProductStatuses;
    }
    public static class Time
    {
        public static DateTime Now { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}