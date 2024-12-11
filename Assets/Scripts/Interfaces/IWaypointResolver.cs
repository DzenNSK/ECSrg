using UnityEngine;

namespace ECSReaction
{
    public interface IWaypointResolver
    {
        Transform GetNextWaypoint();
    }
}
