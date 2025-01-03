using UnityEngine;

//Code By Niall McGuinness

namespace GD.Items
{
    /// <summary>
    /// Items that implement this interface can be consumed by the player.
    /// </summary>
    public interface IConsumable
    {
        void Consume(GameObject consumer);
    }
}