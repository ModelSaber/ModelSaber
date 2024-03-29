﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ModelSaber.Models
{
    [Description("Platform type")]
    public enum Platform : byte
    {
        [Description("Platform PC")]
        Pc,
        [Description("Platform Quest")]
        Quest
    }

    [Flags, Description("Model status")]
    public enum Status : byte
    {
        [Description("Unpublished")]
        Unpublished = 1,
        [Description("Published")]
        Published = 2,
        [Description("ApprovalRequired")]
        ApprovalRequired = 4,
        [Description("Approved")]
        Approved = 8,
        [Description("Featured")]
        Featured = 16 // not in use
    }

    public enum ThumbnailEnum : byte
    {
        Image,
        Video
    }

    public enum TypeEnum : byte
    {
        Avatar,
        Note,
        Platform,
        Saber,
        Effect,
        Wall,
        HealthBar
    }

    public static class EnumExtensions
    {
        public static string GetThumbExt(this ThumbnailEnum en) =>
            en switch
            {
                ThumbnailEnum.Image => ".webp",
                ThumbnailEnum.Video => ".webm",
                _ => ".webp"
            };

        public static string GetTypeExt(this TypeEnum en) =>
            en switch
            {
                TypeEnum.Avatar => "avatar",
                TypeEnum.Note => "bloq",
                TypeEnum.Platform => "plat",
                TypeEnum.Saber => "saber",
                TypeEnum.Wall => "wall",
                TypeEnum.Effect => "effect",
                TypeEnum.HealthBar => "energy",
                _ => throw new ArgumentException("Could not get type extension from param", nameof(en))
            };

        public static IEnumerable<T> GetListFromFlag<T>(this T en) where T : Enum
            => Enum.GetValues(typeof(T)).Cast<T>().Where(v => en.HasFlag(v));

        public static T GetFlagFromList<T>(this IEnumerable<T> en) where T : Enum
            => (T)(object)(byte)en.Aggregate(0, (current, v) => current | (byte)(object)v);
    }
}
