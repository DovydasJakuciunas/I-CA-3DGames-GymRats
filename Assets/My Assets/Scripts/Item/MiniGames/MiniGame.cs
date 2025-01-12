using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public abstract void StartGame(System.Action<bool> onComplete);
}
