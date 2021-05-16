using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    static Camera mainCamera;
    static float startTime;
    public static Vector3 GetMouseWorldPos()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }

    public static Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
    }

    public static float GetRotationAngle(Vector3 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // rotation in degrees

    }

    public static bool Tap()
    {
        if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);
            
            if (touchZero.phase == UnityEngine.TouchPhase.Began)
                startTime = Time.time;

            if (touchZero.phase == UnityEngine.TouchPhase.Canceled || touchZero.phase == UnityEngine.TouchPhase.Ended)
            {
                float dt = Time.time - startTime;
                Debug.Log("dt: " + dt + " Time: " + Time.time + " start: " + startTime);
                if (dt <= 0.25f)
                    return true;
            }
        }

        return false;
    }
}
