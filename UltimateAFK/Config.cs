// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace UltimateAFK
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debug logs are enabled.
        /// </summary>
        [Description("Whether debug logs are enabled.")]
        public bool Debug { get; set; } = false;

        /// <summary>
        /// Gets or sets the minimum amount of players that should be on the server to run the afk check.
        /// </summary>
        [Description("The minimum amount of players that should be on the server to run the afk check.")]
        public int MinimumPlayers { get; set; } = 2;

        /// <summary>
        /// Gets or sets a value indicating whether tutorials will be ignored from afk checks.
        /// </summary>
        [Description("Whether tutorials will be ignored from afk checks.")]
        public bool IgnoreTutorials { get; set; } = true;

        /// <summary>
        /// Gets or sets the amount of time, in seconds, that a player will receive a warning that they have hit the maximum afk time.
        /// </summary>
        [Description("The amount of time, in seconds, that a player will receive a warning that they have hit the maximum afk time.")]
        public int GraceTime { get; set; } = 15;

        /// <summary>
        /// Gets or sets the amount of time, in seconds, before a player will be detected as afk.
        /// </summary>
        [Description("The amount of time, in seconds, before a player will be detected as afk.")]
        public int AfkTime { get; set; } = 45;

        /// <summary>
        /// Gets or sets a value indicating whether the player should be replaced by another spectator.
        /// </summary>
        [Description("Whether the player should be replaced by another spectator.")]
        public bool TryReplace { get; set; } = true;

        /// <summary>
        /// Gets or sets the amount of times a player will be forced to spectate before they are kicked.
        /// </summary>
        [Description("The amount of times a player will be forced to spectate before they are kicked.")]
        public int SpectateLimit { get; set; } = 2;
    }
}