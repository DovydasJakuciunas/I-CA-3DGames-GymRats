using Cinemachine;
using UnityEngine;

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

    [SerializeField]
    float maxTilt = 45.0f; // The maximum tilt angle when zoomed in

    [SerializeField]
    float minTilt = 20.0f; // The minimum tilt angle when zoomed out

    private void Start()
    {
        cameraDistance = virtualCamera.m_Lens.OrthographicSize; // Initializes the camera distance to the orthographic size of the virtual camera
    }

    private void Update()
    {
        if (componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body); // Gets the CinemachineFramingTransposer component
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            // Adjust the zoom
            cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

            // Clamp the zoom level
            cameraDistance = Mathf.Clamp(cameraDistance, minZoom, maxZoom);

            if (componentBase is CinemachineFramingTransposer framingTransposer)
            {
                // Adjust camera distance (zoom)
                framingTransposer.m_CameraDistance = cameraDistance;

                // Adjust camera tilt angle based on the zoom level
                float normalizedZoom = (cameraDistance - minZoom) / (maxZoom - minZoom); // Normalize zoom to 0-1 range
                float targetTilt = Mathf.Lerp(minTilt, maxTilt, normalizedZoom); // Interpolate tilt based on zoom
                virtualCamera.transform.rotation = Quaternion.Euler(targetTilt, virtualCamera.transform.rotation.eulerAngles.y, 0);
            }
        }
    }
}
