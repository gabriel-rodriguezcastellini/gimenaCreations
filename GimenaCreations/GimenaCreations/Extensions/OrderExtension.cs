using GimenaCreations.Models;

namespace GimenaCreations.Extensions;

internal static class OrderExtension
{
    /// <summary>
    /// Creates a unique identifier for the given <paramref name="order"/> instance.
    /// </summary>
    internal static string ToOrderTrackingGroupId(this Order order) =>$"{order.Id}:{order.ApplicationUserId}:{order.Date.Ticks}";
}
