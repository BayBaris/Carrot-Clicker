using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Actions")]
    public static Action onCarrotClicked;
    public static Action<Vector2> onCarrotClickedPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount >0) 
        {
            ManageTouches();
        }

        /*
        if (Input.GetMouseButtonDown(0))
        {
            ThrowRayCast();
        }
        */
    }

    private void ManageTouches()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                ThrowRayCast(touch.position);
            }
        }
    }

    private void ThrowRayCast(Vector2 touchPosition)
    {
        RaycastHit2D hit =  Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(touchPosition));

        if(hit.collider == null)
        {
            return;
        }
        onCarrotClicked?.Invoke();
        // World Space
        onCarrotClickedPosition?.Invoke(hit.point);
    }
}
