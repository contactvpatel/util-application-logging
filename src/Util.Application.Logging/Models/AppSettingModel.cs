namespace Util.Application.Logging.Models
{
    internal class AppSettingModel
    {
        public string? ApplicationName { get; set; }
        public string? ApplicationVersion { get; set; }
        public string? Secret { get; set; }
        public bool EnablePerformanceFilterLogging { get; set; }
    }
}
