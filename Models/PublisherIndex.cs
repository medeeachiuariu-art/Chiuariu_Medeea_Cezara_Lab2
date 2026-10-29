using Chiuariu_Medeea_Cezara_Lab2.Models;

namespace Chiuariu_Medeea_Cezara_Lab2.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; } = default!;
        public IEnumerable<Book>? Books { get; set; }
    }
}