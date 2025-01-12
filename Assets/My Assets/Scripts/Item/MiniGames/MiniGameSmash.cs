using UnityEngine;

public class MiniGameSmash : MiniGame
{
    private int smashCount = 0;
    private int targetCount = 10;
    private System.Action<bool> onComplete;

    public override void StartGame(System.Action<bool> onComplete)
    {
        this.onComplete = onComplete;
        Debug.Log("Smash A+D started");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            smashCount++;
            Debug.Log("Smash count: " + smashCount);

            if (smashCount >= targetCount)
            {
                Debug.Log("Smash A+D completed");
                onComplete?.Invoke(true);
                smashCount = 0;
            }
        }
    }
}
