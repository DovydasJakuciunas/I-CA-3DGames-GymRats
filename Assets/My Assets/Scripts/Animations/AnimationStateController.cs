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
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component not found on the GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement != null)
        {
            // Update walking and sprinting animation states
            animator.SetBool("isWalking", playerMovement.IsWalking);
            animator.SetBool("isSprinting", playerMovement.IsSprinting);
        }
    }
}
