using SDG.Unturned;
using System;
using UnityEngine;

namespace TeleportUtil
{
    public static class Extensions
    {
        internal static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        internal static string xyz_Location(this Vector3 Location)
        {
            return "x:" + Math.Round(Location.x, 2).ToString() + ", y:" + Math.Round(Location.y, 2).ToString() + ", z:" + Math.Round(Location.z, 2).ToString();
        }

        internal static void TeleportCar(this InteractableVehicle vehicle, Vector3 Location)
        {
            vehicle.transform.position = Location;
            VehicleManager.sendVehiclePosition(vehicle, Location);
        }
    }
}
