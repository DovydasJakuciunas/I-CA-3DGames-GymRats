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

    void Update()
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
