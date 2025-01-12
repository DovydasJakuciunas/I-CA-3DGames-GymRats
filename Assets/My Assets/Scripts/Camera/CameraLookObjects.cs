using UnityEngine;

public class CmaeraLookObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject Camera;

    private void Update()
    {
        transform.LookAt(Camera.transform);
    }
}

