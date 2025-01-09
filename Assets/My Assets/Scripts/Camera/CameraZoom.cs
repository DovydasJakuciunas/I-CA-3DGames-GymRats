using Cinemachine;
using UnityEngine;

//player would be a ble to zoom in and out of the camera
public class CameraZoom : MonoBehaviour
{
    float cameraDistance;
    CinemachineComponentBase componentBase;

    [SerializeField]
    CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    float zoomSpeed = 4.0f;

    [SerializeField]
    float minZoom = 5.0f;

    [SerializeField]
    float maxZoom = 20.0f;

    private void Start()
    {
        cameraDistance = virtualCamera.m_Lens.OrthographicSize;         //Initializes the camera distance to the orthographic size of the virtual camera
    }

    private void Update()
    {
        if (componentBase == null)  
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);  //Gets the CinemachineFramingTransposer component
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;   //Zooms in and out of the camera

            //Restricts the zoom to the min and max zoom values
            cameraDistance = Mathf.Clamp(cameraDistance, minZoom, maxZoom); //Clamps the camera distance between the min and max zoom values

            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = cameraDistance; //Sets the camera distance to the new camera distance
            }
        }
    }
}