using System;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public abstract void StartGame(Action<bool> onComplete);

    public virtual int StaminaCost => 30; 
}
