using System;

namespace LactafarmaAPI.Data.Entities
{
    public class Alias
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long DrugId { get; set; }
        public Drug Drug { get; set; }
    }
}