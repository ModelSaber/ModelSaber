using System;
using System.Collections.Generic;
using System.ComponentModel;
#if NETSTANDARD2_1_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace ModelSaber.Models
{
    public class User : BaseId
    {
        public string? Name { get; set; }
        public string? BSaber { get; set; }
        // this needs to be parsed to a string since javascript will freak out if its about '2^53 - 1' see https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Number/MAX_SAFE_INTEGER where this runs all the way up to 2^64
        public ulong? DiscordId { get; set; }
        public string? Avatar { get; set; }
        public UserLevel Level { get; set; }
        [JsonIgnore] 
        public virtual ICollection<ModelUser> Models { get; set; } = null!;
        [JsonIgnore] 
        public virtual ICollection<Vote> Votes { get; set; } = null!;
        [JsonIgnore]
        public virtual UserLogons Logon { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<UserTags> UserTags { get; set; } = null!;
    }

    public class UserTags : BaseId
    {
        public string Name { get; set; } = null!;
        public uint UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }

    [Description("UserLevels for what your user is")]
    public enum UserLevel : byte
    {
        [Description("Just your average Joe")]
        Normal,
        [Description("Oh you just got fancy")]
        Verified,
        [Description("Be carefull what you do")]
        Moderator,
        [Description("Praise them")]
        Admin
    }

    public class UserLogons
    {
        public Guid Id { get; set; }
        public uint UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
