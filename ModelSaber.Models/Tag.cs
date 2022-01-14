using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModelSaber.Models
{
    public class Tag : BaseId
    {
        public Guid CursorId { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<ModelTag> ModelTags { get; set; } = null!;
    }

    public class ModelTag : BaseId
    {
        public int ModelId { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; } = null!;
        [JsonIgnore]
        public virtual Model Model { get; set; } = null!;
    }
}
