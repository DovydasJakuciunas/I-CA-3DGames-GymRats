using UnityEngine;
using UnityEngine.AI;

//Reference for Mouse movement : https://www.youtube.com/watch?v=7eAwVUsiqZU

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject tagetDest;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);    //Get camera and get the mouse position
            RaycastHit hitPoint;

            if(Physics.Raycast(ray, out hitPoint))                  //If the raycast hit something move the agent to that position
            {
                tagetDest.transform.position = hitPoint.point;
                agent.SetDestination(hitPoint.point);
            }

        }
    }



}
