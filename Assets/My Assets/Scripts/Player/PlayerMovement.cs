using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region Fields

    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent player;
    [SerializeField] private GameObject targetDestination;
    [SerializeField] private GameObject sphere; // Reference to the sphere
    private SphereVisibilityManager sphereManager;

    private float lastClickTime;
    private const float DOUBLE_CLICK_TIME = 0.2f; // Checks the threshold for double click
    private float savedSpeed; // Reset the speed to original speed after double click
    private bool isDoubleClicked = false; // Prevents multiple double clicks in turn speeding up the agent by each double click

    private Coroutine sphereCoroutine; // Handle the coroutine for sphere visibility

    #endregion

    #region MouseClick Movement

    private void Start()
    {
        savedSpeed = player.speed;
        ResetPlayerSpeed();
        sphereManager = new SphereVisibilityManager(sphere, transform, player, this);
    }

    private void Update()
    {
        // Prevent much sliding on the NavMeshSurface
        player.acceleration = 10000;
        player.angularSpeed = 10000;

        //To showcase sphere when player reaches near it or turn it off
        sphereManager.HandleSphereVisibility(IsWalking);

        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }

        ResetSpeedWhenDestinationReached();

    }

    // All code to handle mouse click
    private void HandleMouseClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Get camera and get the mouse position

        if (Physics.Raycast(ray, out RaycastHit hitPoint)) // If the raycast hit something move the agent to that position
        {
            targetDestination.transform.position = hitPoint.point;
            player.SetDestination(hitPoint.point);

            if (HasDoubleClicked()) // If the time since last click is less than the double click time
            {
                player.speed = player.speed * 2; // Double the acceleration of agent
                isDoubleClicked = true;
            }
            else
            {
                ResetPlayerSpeed();
            }
        }
    }

    // Method to check if player has double clicked
    private bool HasDoubleClicked()
    {
        float timeSinceLastClick = Time.time - lastClickTime; // Gets the time since last click
        lastClickTime = Time.time; // Gets last time mouse was clicked
        return timeSinceLastClick <= DOUBLE_CLICK_TIME && !isDoubleClicked; // If the time since last click is less than the double click time
    }

    // Reset speed for agent
    private void ResetPlayerSpeed()
    {
        player.speed = savedSpeed;
        isDoubleClicked = false;
    }

    // Method to see if player made it to destination
    private void ResetSpeedWhenDestinationReached()
    {
        if (!player.pathPending && player.remainingDistance <= player.stoppingDistance &&
            (!player.hasPath || player.velocity.sqrMagnitude == 0f))
        {
            ResetPlayerSpeed();
        }
    }

    // Checking if agent is moving for the Animator
    public bool IsWalking
    {
        get
        {
            // Check if the agent is actively moving
            return !player.pathPending &&
                   player.remainingDistance > player.stoppingDistance &&
                   player.velocity.sqrMagnitude > 0f;
        }
    }

    // Checking if agent is sprinting for the Animator
    public bool IsSprinting
    {
        get
        {
            // The player is sprinting if the speed has been doubled
            return isDoubleClicked && IsWalking;
        }
    }

    #endregion
}
