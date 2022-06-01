// -----------------------------------------------------------------------
// <copyright file="PositionInfo.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace UltimateAFK.Models
{
    using System;
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using UnityEngine;

    /// <summary>
    /// Represents a position and rotation.
    /// </summary>
    public readonly struct PositionInfo : IEquatable<PositionInfo>
    {
        private readonly Vector3 position;
        private readonly Vector2 rotation;
        private readonly float cameraRotation;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionInfo"/> struct.
        /// </summary>
        /// <param name="player">The player to take the info from.</param>
        public PositionInfo(Player player)
        {
            position = player.Position;
            rotation = player.Rotation;
            cameraRotation = player.Role is Scp079Role scp079 ? scp079.Camera.Rotation : 0f;
        }

        /// <summary>
        /// Checks if two <see cref="PositionInfo"/> are equal.
        /// </summary>
        /// <param name="x">The first instance.</param>
        /// <param name="y">The second instance.</param>
        /// <returns>Whether the two <see cref="PositionInfo"/> instances are equal.</returns>
        public static bool operator ==(PositionInfo x, PositionInfo y) => x.Equals(y);

        /// <summary>
        /// Checks if two <see cref="PositionInfo"/> are not equal.
        /// </summary>
        /// <param name="x">The first instance.</param>
        /// <param name="y">The second instance.</param>
        /// <returns>Whether the two <see cref="PositionInfo"/> instances are not equal.</returns>
        public static bool operator !=(PositionInfo x, PositionInfo y) => !(x == y);

        /// <inheritdoc />
        public bool Equals(PositionInfo other)
        {
            return position.Equals(other.position) &&
                   rotation.Equals(other.rotation) &&
                   cameraRotation.Equals(other.cameraRotation);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is PositionInfo other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = position.GetHashCode();
                hashCode = (hashCode * 397) ^ rotation.GetHashCode();
                hashCode = (hashCode * 397) ^ cameraRotation.GetHashCode();
                return hashCode;
            }
        }
    }
}