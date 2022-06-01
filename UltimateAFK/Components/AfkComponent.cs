// -----------------------------------------------------------------------
// <copyright file="AfkComponent.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace UltimateAFK.Components
{
    using System.Linq;
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using Exiled.Permissions.Extensions;
    using UltimateAFK.Models;
    using UnityEngine;

    /// <summary>
    /// Controls an individual's afk status.
    /// </summary>
    public class AfkComponent : MonoBehaviour
    {
        private const float Interval = 1f;
        private float timer;

        private Player player;
        private PositionInfo lastPosition;
        private int afkTime;
        private int afkCounter;

        private static Plugin Plugin => Plugin.Instance;

        private void Awake()
        {
            player = Player.Get(gameObject);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= Interval)
            {
                timer = 0f;
                CheckAfk();
            }
        }

        private void CheckAfk()
        {
            if (player.CheckPermission("uafk.ignore") ||
                player.IsDead ||
                Plugin.Config.MinimumPlayers > Player.Dictionary.Count ||
                (Plugin.Config.IgnoreTutorials && player.Role.Type == RoleType.Tutorial) ||
                player.Role is Scp096Role { TryingNotToCry: true })
                return;

            PositionInfo currentPosition = new PositionInfo(player);
            if (currentPosition != lastPosition)
            {
                afkTime = 0;
                lastPosition = currentPosition;
                return;
            }

            afkTime++;
            if (afkTime < Plugin.Config.AfkTime)
                return;

            int gracePeriodRemaining = Plugin.Config.GraceTime + Plugin.Config.AfkTime - afkTime;
            if (gracePeriodRemaining > 0)
            {
                player.Broadcast(1, string.Format(Plugin.Translation.GracePeriodWarning, gracePeriodRemaining).Replace("$seconds", gracePeriodRemaining == 1 ? "second" : "seconds"), shouldClearPrevious: true);
                return;
            }

            Log.Debug($"{player} has been detected as AFK.", Plugin.Config.Debug);
            afkTime = 0;

            if (Plugin.Config.TryReplace)
                TryReplace();

            if (Plugin.Instance.Config.SpectateLimit > 0 && ++afkCounter > Plugin.Instance.Config.SpectateLimit)
            {
                player.Disconnect(Plugin.Translation.KickReason);
                return;
            }

            ForceSpectator();
        }

        private void TryReplace()
        {
            Player toSwap = Player.List.FirstOrDefault(ply => ply.IsDead && !ply.IsOverwatchEnabled && ply != player);
            if (toSwap is not null)
                new PlayerInfo(player).AddTo(toSwap);
        }

        private void ForceSpectator()
        {
            player.Role.Type = RoleType.Spectator;
            player.Broadcast(Plugin.Translation.SpectatorForced);
        }
    }
}