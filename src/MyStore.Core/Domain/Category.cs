using System;
using System.Collections.Generic;

namespace MyStore.Core.Domain
{
    public class Category
    {
        public AggregateId Id { get; set; }
        public string Name { get; set; }
    }
}