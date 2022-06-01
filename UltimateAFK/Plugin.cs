// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace UltimateAFK
{
    using System;
    using Exiled.API.Features;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config, Translation>
    {
        private EventHandlers eventHandlers;

        /// <summary>
        /// Gets an instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <inheritdoc/>
        public override string Author => "Build";

        /// <inheritdoc/>
        public override string Name => "UltimateAFK";

        /// <inheritdoc/>
        public override string Prefix => "UltimateAFK";

        /// <inheritdoc/>
        public override Version RequiredExiledVersion { get; } = new(5, 2, 1);

        /// <inheritdoc/>
        public override Version Version { get; } = new(4, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Instance = this;

            eventHandlers = new EventHandlers();
            Exiled.Events.Handlers.Player.Verified += eventHandlers.OnVerified;
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= eventHandlers.OnVerified;
            eventHandlers = null;

            Instance = null;
            base.OnDisabled();
        }
    }
}