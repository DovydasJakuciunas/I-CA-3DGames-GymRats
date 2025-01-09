using Unity.VisualScripting;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    public int maxStamina = 100;
    public int currentStamina;

    public StaminaBar staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            UseStamina(10);
        }
    }

    public void UseStamina(int stamina)
    {
        if (currentStamina - stamina >= 0)
        {
            currentStamina -= stamina;
            staminaBar.SetStamina(currentStamina);
            Debug.Log("Stamina used: " + stamina);
        }
    }
}
