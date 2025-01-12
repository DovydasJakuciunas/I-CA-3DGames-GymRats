using UnityEngine;

public class MiniGameUIManager : MonoBehaviour
{
    public static MiniGameUIManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowMiniGameUI(GameObject uiPrefab)
    {
        Instantiate(uiPrefab, transform);
    }

    public void HideMiniGameUI()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
