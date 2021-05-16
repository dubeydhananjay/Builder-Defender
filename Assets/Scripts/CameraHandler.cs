using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float orthographicSize, targetOrthographicSize;
    private Touch touchZeroPrevious, touchOnePrevious;
    private Camera mainCamera;
    void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
        mainCamera = Camera.main;
    }


    void Update()
    {
#if UNITY_EDITOR
        HandleMovement();
        ZoomInAndOut();
#else
        MobileZoomInAndOut();
        MobileCameraMovement();
#endif

    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void ZoomInAndOut()
    {
        SetOrthographicSize(Input.mouseScrollDelta.y);
    }

    private void MobileZoomInAndOut()
    {
        if (Input.touchCount == 2)
        {

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;


            if (touchOne.phase == TouchPhase.Began && touchZero.phase == TouchPhase.Began)
            {
                touchZeroPrevious = touchZero;
                touchOnePrevious = touchOne;
                prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;

            }

            if (touchOne.phase == TouchPhase.Moved && touchZero.phase == TouchPhase.Moved)
            {
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                SetOrthographicSize(deltaMagnitudeDiff);
            }


        }
    }

    private void SetOrthographicSize(float zoomInAndOutVal)
    {
        float zoomAmount = 2f;
        float minOrthographicSize = 10f;
        float maxOrthographicSize = 30f;
        float zoomSpeed = 5f;

        targetOrthographicSize += zoomInAndOutVal * zoomAmount;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    private void MobileCameraMovement()
    {
        if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);
            if (touchZero.phase == TouchPhase.Moved)
            {
                float speed = 2f * Time.deltaTime;
                transform.Translate(-touchZero.deltaPosition.x * speed, -touchZero.deltaPosition.y * speed, transform.position.z);
            }
        }
    }
}
