using UnityEngine;
using System.Collections;
using UnityEngine.AI;

//Script to manage the visibility of the sphere when player reaches near it
public class SphereVisibilityManager
{
    private GameObject sphere;
    private Transform playerTransform;
    private NavMeshAgent playerAgent;
    private float delay;

    private bool sphereActive = true;
    private Coroutine sphereCoroutine;  // Handle the coroutine for sphere visibility
    private MonoBehaviour monoBehaviourContext; // Context to run coroutines

    public SphereVisibilityManager(GameObject sphere, Transform playerTransform, NavMeshAgent playerAgent, MonoBehaviour monoBehaviourContext, float delay = 1f)
    {
        this.sphere = sphere;
        this.playerTransform = playerTransform;
        this.playerAgent = playerAgent;
        this.monoBehaviourContext = monoBehaviourContext;
        this.delay = delay;
    }

    public void HandleSphereVisibility(bool isWalking)
    {
        if (!isWalking && IsNearSphere())
        {
            if (sphereActive)
            {
                if (sphereCoroutine == null) // Avoid starting multiple coroutines
                {
                    sphereCoroutine = monoBehaviourContext.StartCoroutine(TurnOffSphereAfterDelay());
                }
            }
        }
        else
        {
            // If the player moves, ensure the sphere is visible
            if (!sphereActive)
            {
                sphere.SetActive(true);
                sphereActive = true;

                // Stop any ongoing coroutine
                if (sphereCoroutine != null)
                {
                    monoBehaviourContext.StopCoroutine(sphereCoroutine);
                    sphereCoroutine = null;
                }
            }
        }
    }

    private bool IsNearSphere()
    {
        // Check if the player is near the sphere
        return Vector3.Distance(playerTransform.position, sphere.transform.position) <= playerAgent.stoppingDistance;
    }

    private IEnumerator TurnOffSphereAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        sphere.SetActive(false);
        sphereActive = false;
        sphereCoroutine = null;
    }
}
