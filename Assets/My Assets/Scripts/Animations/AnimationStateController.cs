using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // Find and assign the PlayerMovement component
        playerMovement = GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement != null)
        {
            // Calculate x (left/right movement) and y (forward/backward movement)
            float x = Input.GetAxis("Horizontal"); // Get input for left/right movement (-1 to 1)
            float y = playerMovement.IsSprinting ? 1.0f : (playerMovement.IsWalking ? 0.5f : 0.0f); // Forward/backward based on walking/sprinting

            // Smoothly update parameters in the Animator
            animator.SetFloat("movementX", Mathf.Lerp(animator.GetFloat("movementX"), x, Time.deltaTime * 10)); // Smooth left/right
            animator.SetFloat("movementY", Mathf.Lerp(animator.GetFloat("movementY"), y, Time.deltaTime * 10)); // Smooth forward/backward
        }
    }
}
