using UnityEngine;
using UnityEngine.AI;

//Reference for Mouse movement : https://www.youtube.com/watch?v=7eAwVUsiqZU

//Error with the draw method, Not drawing the path accurately
public class PlayerMovement : MonoBehaviour
{
    #region Fields

    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent player;
    [SerializeField] private GameObject targetDestination;
    [SerializeField] private LineRenderer line;

    private float lastClickTime;                        
    private const float DOUBLE_CLICK_TIME = 0.2f;       //Checks the thresehold for double click
    private float savedSpeed;                           //Reset the speed to original speed after double click
    private bool isDoubleClicked = false;                       //So the player can't double click multiple times in turn speeding up the agent by each double click

    #endregion

    #region MouseClick Movement

    private void Start()
    {
        savedSpeed = player.speed;
        ResetPlayerSpeed();
    }

    private void Update()
    {
        //Alows me to prevent much sliding on the navMeshSurface
        player.acceleration = 60;
        player.angularSpeed = 120;

        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }

        DrawPath();
        ResetSpeedWhenDestinationReached();    
    }

    //Allows to draw a path for where the agent is going
    private void DrawPath()
    {
        if (!line.enabled || player.path.corners.Length < 1) return;

        // Create a new array to hold the player's current position and path corners
        Vector3[] pathCorners = new Vector3[player.path.corners.Length + 1];

        // Add the player's current position as the first point
        pathCorners[0] = transform.position;

        // Add the rest of the path corners
        for (int i = 0; i < player.path.corners.Length; i++)
        {
            pathCorners[i + 1] = player.path.corners[i];
        }

        // Set the LineRenderer positions to follow the updated path
        line.positionCount = pathCorners.Length;
        line.SetPositions(pathCorners);
    }

    //All code to handle mouse click
    private void HandleMouseClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);    //Get camera and get the mouse position
        

        if (Physics.Raycast(ray, out RaycastHit hitPoint))                  //If the raycast hit something move the agent to that position
        {
            targetDestination.transform.position = hitPoint.point;
            player.SetDestination(hitPoint.point);

            if (HasDoubleClicked())        //If the time since last click is less than the double click time
            {
                player.speed = player.speed * 2;                            //Double the acceleration of agent
                isDoubleClicked = true;
            }
            else
            {
                ResetPlayerSpeed();
            }
        }
    }

    //Method to check if player has double clicked
    private bool HasDoubleClicked()
    {
        float timeSinceLastClick = Time.time - lastClickTime;   //Gets the time since last click
        lastClickTime = Time.time;                              //Gets last time mouse was clicked
        return timeSinceLastClick <= DOUBLE_CLICK_TIME && !isDoubleClicked;    //If the time since last click is less than the double click time
    }

    //Reset speed for agent
    private void ResetPlayerSpeed()
    {
        player.speed = savedSpeed;
        isDoubleClicked = false;
    }

    //Method to see if player made it to destination
    private void ResetSpeedWhenDestinationReached()
    {
        if(!player.pathPending  && player.remainingDistance <= player.stoppingDistance && 
            (!player.hasPath || player.velocity.sqrMagnitude ==0f))
        {
           ResetPlayerSpeed();
        }
    }

    #endregion
}
