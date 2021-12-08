using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Definitions

    Vector3 initialTouch;
    Vector3 offset;

    Vector2 initialPoint = Vector2.negativeInfinity;
    Vector2 finalPoint = Vector2.negativeInfinity;

    [Header("General Settings", order = 0)]
    [Space(order = 1)]

    [SerializeField]Vector2 panLimitZoomOut;
    [SerializeField]Vector2 panCenterZoomOut;
    [SerializeField]Vector2 panLimitZoomIn;
    [SerializeField]Vector2 panCenterZoomIn;

    [SerializeField]LayerMask groundLayer;
    [Space(order = 2)]
    [SerializeField]float zoomIn = 6;
    [SerializeField] float zoomOut = 15;

    [Header("Joystick Settings", order = 0) ]
    [Space(order = 1)]

    [Tooltip("The bounds of the Joystick in pixels")]
    [SerializeField] float joystickBound = 400;

    [SerializeField] float joystickSpeed = 10;
    [Tooltip("It is multiplied by the speed and provides the maximum panning speed when at the highest zoom level")]
    [SerializeField][Range(0,1)] float SpeedZoomInMultiplier = .5f;
    [Tooltip("Dead zone of the Joystick")]
    [SerializeField][Range(0,1)] float deadZone = .1f;
    [Space(20, order = 2)]
    [SerializeField] CityBuilderResources.Status status;
    [Space(10, order = 3)]
    [SerializeField] bool debug = true;

    public CityBuilderResources.Status Status { get => status; set => status = value; }
    public bool moveCamera = false;
    public bool zoom = false;

    private int layerID = 1 << 9;

    #endregion


    // Update is called once per frame
    void LateUpdate()
    {
        //if (moveCamera)
        //{
        //    if (Input.touchCount > 0)
        //    {
        //        CalculateZoom();
        //    }
        //    else
        //    {
        //        switch (Status)
        //        {
        //            case (CityBuilderResources.Status.Game):
        //                InGameModeMovement();
        //                break;

        //            case (CityBuilderResources.Status.Build):
        //                InBuildModeMovement();
        //                break;

        //            default: break;
        //        }
        //    }
        //    KeepCameraInBounds();
        //}
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 2)
            {
                CalculateZoom();
            }
            else
            {

                switch (Status)
                {
                    case (CityBuilderResources.Status.Game):
                        InGameModeMovement();
                        break;

                    case (CityBuilderResources.Status.Build):
                        InBuildModeMovement();
                        break;

                    default: break;
                }

            }
        }

        KeepCameraInBounds();

    }

    private void OnDrawGizmos()
    {
        if (debug) 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(panCenterZoomOut.x, 1, panCenterZoomOut.y), new Vector3(panLimitZoomOut.x * 2, 0, panLimitZoomOut.y * 2));
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(new Vector3(panCenterZoomIn.x, 1, panCenterZoomIn.y), new Vector3(panLimitZoomIn.x * 2, 0, panLimitZoomIn.y * 2));

            var T = Mathf.InverseLerp(zoomOut, zoomIn, Camera.main.orthographicSize);

            Vector2 interpolatedPanLimit = Vector2.zero;
            interpolatedPanLimit.x = Mathf.Lerp(panLimitZoomOut.x, panLimitZoomIn.x, T);
            interpolatedPanLimit.y = Mathf.Lerp(panLimitZoomOut.y, panLimitZoomIn.y, T);

            Vector2 interpolatedPanCenter = Vector2.zero;
            interpolatedPanCenter.x = Mathf.Lerp(panCenterZoomOut.x, panCenterZoomIn.x, T);
            interpolatedPanCenter.y = Mathf.Lerp(panCenterZoomOut.y, panCenterZoomIn.y, T);

            Gizmos.color = Color.gray;
            Gizmos.DrawWireCube(new Vector3(interpolatedPanCenter.x, 1, interpolatedPanCenter.y), new Vector3(interpolatedPanLimit.x * 2, 0, interpolatedPanLimit.y * 2));

            RaycastHit hitInfo;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, Mathf.Infinity, layerID))
            {
                var point = hitInfo.point;
                point.y = 1;

                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hitInfo.distance, Color.yellow);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(point, .1f);
            }

        }
    }

    private void KeepCameraInBounds()
    {
        var posInicial = Vector3.zero;
        Vector3 posFinal = posInicial;
        RaycastHit hitInfo;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, Mathf.Infinity, layerID))
        {
            var T = Mathf.InverseLerp(zoomOut, zoomIn, Camera.main.orthographicSize);

            Vector2 interpolatedPanLimit = Vector2.zero;
            interpolatedPanLimit.x = Mathf.Lerp(panLimitZoomOut.x, panLimitZoomIn.x, T);
            interpolatedPanLimit.y = Mathf.Lerp(panLimitZoomOut.y, panLimitZoomIn.y, T);
            
            Vector2 interpolatedPanCenter = Vector2.zero;
            interpolatedPanCenter.x = Mathf.Lerp(panCenterZoomOut.x, panCenterZoomIn.x, T);
            interpolatedPanCenter.y = Mathf.Lerp(panCenterZoomOut.y, panCenterZoomIn.y, T);

            posInicial = hitInfo.point;
            posFinal.x = Mathf.Clamp(hitInfo.point.x, -interpolatedPanLimit.x + interpolatedPanCenter.x, interpolatedPanLimit.x + interpolatedPanCenter.x);
            posFinal.z = Mathf.Clamp(hitInfo.point.z, -interpolatedPanLimit.y + interpolatedPanCenter.y, interpolatedPanLimit.y + interpolatedPanCenter.y);
        }
        else 
        {
            Debug.Log("Not ground Detected");
        }
        var offset = posFinal - posInicial;
        offset = new Vector3(offset.x, 0, offset.z);

        transform.position += offset;
    }

    private void InGameModeMovement()
    {
        switch (Input.GetTouch(0).phase)
        {
            case TouchPhase.Began:
                initialTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                offset = initialTouch - transform.position;
                break;
            case TouchPhase.Moved:
                transform.position = new Vector3(initialTouch.x - offset.x, transform.position.y, initialTouch.z - offset.z);
                offset = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position;

                break;
            default:
                break;
        }
    }
    private void InBuildModeMovement()
    {
        switch (Input.GetTouch(0).phase)
        {
            case TouchPhase.Began:

                initialPoint = Input.GetTouch(0).position;
                break;
            
            case TouchPhase.Moved:
                finalPoint = Input.GetTouch(0).position;
                JoystickMovement(initialPoint, finalPoint);
                break;
           
            case TouchPhase.Stationary:
                if (finalPoint != Vector2.negativeInfinity) 
                {
                    JoystickMovement(initialPoint, finalPoint);
                }
                break;

            case TouchPhase.Ended:
                initialPoint = finalPoint = Vector2.negativeInfinity;
                break;

            default:
                break;
        }
    }

    private void JoystickMovement(Vector2 pointA, Vector2 pointB)
    {
        var vectorAB = pointB - pointA;
        var directionClamped = Vector2.ClampMagnitude(vectorAB, joystickBound);

        if (directionClamped.magnitude > joystickBound * deadZone) 
        {
            var unitVector = new Vector3(directionClamped.x, 0, directionClamped.y) / joystickBound;

            var T = Mathf.InverseLerp(zoomOut, zoomIn, Camera.main.orthographicSize);
            var speed = Mathf.Lerp(joystickSpeed, joystickSpeed * SpeedZoomInMultiplier, T);

            var distance = speed * Time.deltaTime;

            transform.Translate(distance * unitVector);
        }
    }
    private void CalculateZoom()
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

        float difference = currentMagnitude - prevMagnitude;

        Zoom(difference * 0.01f);
    }
    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomIn, zoomOut);
    }
}
