using UnityEngine;
using UnityEngine.AI;

//Reference for Mouse movement : https://www.youtube.com/watch?v=7eAwVUsiqZU

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private Camera cam;
    [SerializeField]private NavMeshAgent player;
    [SerializeField]private GameObject tagetDest;
    [SerializeField]private LineRenderer line;

    private float lastClickTime;
    private const float DOUBLE_CLICK_TIME = 0.2f;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;   //Gets the time since last click
            lastClickTime = Time.time;                              //Gets last time mouse was clicked
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);    //Get camera and get the mouse position
            RaycastHit hitPoint;

            if(Physics.Raycast(ray, out hitPoint))                  //If the raycast hit something move the agent to that position
            {
                tagetDest.transform.position = hitPoint.point;
                player.SetDestination(hitPoint.point);
            }
            if(timeSinceLastClick <= DOUBLE_CLICK_TIME)            //If the time since last click is less than the double click time
            {
                player.speed = player.speed * 2;      //Double the acceleration of agent
                player.SetDestination(tagetDest.transform.position); //Move the agent to the target destination
            }
        }
        DrawPath();
    }

    private void DrawPath()
    {
        if(player.path.corners.Length <2) return;

        line.positionCount = player.path.corners.Length;
        line.SetPositions(player.path.corners);
    }

}
