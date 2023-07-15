// -----------------------------------------------------------------------
// <copyright file="AfkComponent.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using PlayerRoles;

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
                Plugin.Instance.Config.MinimumPlayers > Player.Dictionary.Count ||
                (Plugin.Instance.Config.IgnoreTutorials && player.Role.Type == RoleTypeId.Tutorial) ||
                player.Role is Scp096Role { TryNotToCryActive: true })
            {
                afkTime = 0;
                return;
            }

            PositionInfo currentPosition = new PositionInfo(player);
            if (currentPosition != lastPosition)
            {
                afkTime = 0;
                lastPosition = currentPosition;
                return;
            }

            afkTime++;
            if (afkTime < Plugin.Instance.Config.AfkTime)
                return;

            int gracePeriodRemaining = Plugin.Instance.Config.GraceTime + Plugin.Instance.Config.AfkTime - afkTime;
            if (gracePeriodRemaining > 0)
            {
                player.Broadcast(1, string.Format(Plugin.Instance.Translation.GracePeriodWarning, gracePeriodRemaining).Replace("$seconds", gracePeriodRemaining == 1 ? "second" : "seconds"), shouldClearPrevious: true);
                return;
            }

            Log.Debug($"{player} has been detected as AFK.");
            afkTime = 0;

            if (Plugin.Instance.Config.TryReplace)
                TryReplace();

            player.ClearInventory();
            if (Plugin.Instance.Config.SpectateLimit > 0 && ++afkCounter > Plugin.Instance.Config.SpectateLimit)
            {
                player.Disconnect(Plugin.Instance.Translation.KickReason);
                return;
            }

            ForceSpectator();
        }

        private void TryReplace()
        {
            Player toSwap = Player.List.FirstOrDefault(toSwap => toSwap.IsDead && !toSwap.IsOverwatchEnabled && toSwap != player);
            if (toSwap is not null)
                new PlayerInfo(player).AddTo(toSwap);
        }

        private void ForceSpectator()
        {
            player.Role.Set(RoleTypeId.Spectator);
            player.Broadcast(Plugin.Instance.Translation.SpectatorForced);
        }
    }
}