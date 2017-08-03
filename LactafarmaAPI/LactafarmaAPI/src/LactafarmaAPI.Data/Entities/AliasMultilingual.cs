using System;

namespace LactafarmaAPI.Data.Entities
{
    public class AliasMultilingual
    {
        public long AliasId { get; set; }
        public string Name { get; set; }
        public Guid LanguageId { get; set; }
        public Alias Alias { get; set; }
        public Language Language { get; set; }
    }
}