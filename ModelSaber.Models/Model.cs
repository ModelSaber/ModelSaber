﻿using System;
using System.Collections.Generic;
#if NETSTANDARD2_1_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace ModelSaber.Models
{
    public class Model : BaseId
    {
        public uint UserId { get; set; }
        public Guid Uuid { get; set; }
        public TypeEnum Type { get; set; }
        public Status Status { get; set; }
        public Platform Platform { get; set; }
        public ThumbnailEnum? ThumbnailExt { get; set; }
        public string Name { get; set; } = "";
        public string? Hash { get; set; }
        public string? Description { get; set; }
        public string Thumbnail => !ThumbnailExt.HasValue ? "null" : $"images/{Uuid}{ThumbnailExt?.GetThumbExt()}";
        public string DownloadPath => $"download?id={Uuid}";
        public string MinVersion { get; set; } = "";
        public string BuildVersion { get; set; } = "";
        public string UnitySystem { get; set; } = "";
        public int UnitySystemVersion { get; set; }
        public bool Nsfw { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public virtual ICollection<ModelTag> Tags { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<ModelVariation> ModelVariations { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<ModelUser> Users { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Vote> Votes { get; set; } = null!;
        [JsonIgnore]
        public virtual ModelVariation ModelVariation { get; set; } = null!;
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }

    public class ModelVariation : BaseId
    {
        public uint ModelId { get; set; }
        public uint ParentModelId { get; set; }
        [JsonIgnore]
        public virtual Model Model { get; set; } = null!;
        [JsonIgnore]
        public virtual Model ParentModel { get; set; } = null!;
    }

    public class ModelUser : BaseId
    {
        public uint ModelId { get; set; }
        public uint UserId { get; set; }
        [JsonIgnore]
        public virtual Model Model { get; set; } = null!;
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }

    public class Vote : BaseId
    {
        public uint ModelId { get; set; }
        public uint? UserId { get; set; }
        public string? GameId { get; set; }
        public bool DownVote { get; set; }
        [JsonIgnore]
        public virtual Model Model { get; set; } = null!;
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
