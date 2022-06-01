// -----------------------------------------------------------------------
// <copyright file="Translation.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace UltimateAFK
{
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Translation : ITranslation
    {
        /// <summary>
        /// Gets or sets the broadcast to send when a player is in the grace period for being afk.
        /// </summary>
        [Description("The broadcast to send when a player is in the grace period for being afk.")]
        public string GracePeriodWarning { get; set; } = "<color=red>You will be classified as AFK in</color> {0} <color=red>$seconds!</color>";

        /// <summary>
        /// Gets or sets the reason to display when a player is kicked for being afk.
        /// </summary>
        [Description("The reason to display when a player is kicked for being afk.")]
        public string KickReason { get; set; } = "[Kicked by uAFK]\nYou were AFK for too long!";

        /// <summary>
        /// Gets or sets the broadcast to send when a player is forced to spectator for being afk.
        /// </summary>
        [Description("The broadcast to send when a player is forced to spectator for being afk.")]
        public Broadcast SpectatorForced { get; set; } = new("You were detected as AFK and moved to spectator!", 30);
    }
}