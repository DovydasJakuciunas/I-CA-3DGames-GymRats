using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    public int maxStamina = 100;
    public int currentStamina;
    public StaminaBar staminaBar;

    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    public void UseStamina(int stamina)
    {
        if (currentStamina - stamina >= 0)
        {
            currentStamina -= stamina;
            staminaBar.SetStamina(currentStamina);
        }
        else
        {
            Debug.Log("Not enough stamina!");
        }
    }

    public void RestoreStamina(int stamina)
    {
        currentStamina += stamina;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }

        staminaBar.SetStamina(currentStamina);
        Debug.Log("Stamina restored: " + stamina);
    }
}
