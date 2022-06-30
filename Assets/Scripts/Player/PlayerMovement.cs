using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerMoveForwardSpeed = 1f;
    [SerializeField] float swerveSpeed = 2.5f;
    [SerializeField] Vector3 swerveFirstPosition, swerveLastPosition, distanceFirstLastPosition;

    void Start()
    {
        float startPos = Input.mousePosition.x;
    }
    void Update()
    {
        MoveForward();
        SwerveMechanicMouse();
        SwerveMechanicMobil();
    }
    void MoveForward()
    {
        Vector3 playerForward = Vector3.forward * playerMoveForwardSpeed * Time.deltaTime;
        transform.position = transform.position + playerForward;
        transform.rotation = Quaternion.Euler(Vector3.forward);
    }
    void SwerveMechanicMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swerveFirstPosition.x = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            swerveLastPosition.x = Input.mousePosition.x;
            distanceFirstLastPosition = swerveLastPosition - swerveFirstPosition;
            transform.Translate(distanceFirstLastPosition.x * Time.deltaTime * swerveSpeed * Time.deltaTime
                , 0, 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swerveFirstPosition = Vector3.zero;
            swerveLastPosition = Vector3.zero;
        }
    }
    void SwerveMechanicMobil()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swerveFirstPosition.x = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                swerveLastPosition.x = touch.position.x;
                distanceFirstLastPosition = swerveLastPosition - swerveFirstPosition;
                transform.Translate(distanceFirstLastPosition.x * swerveSpeed * Time.deltaTime
                    , 0, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swerveLastPosition = Vector3.zero;
                swerveLastPosition = Vector3.zero;
            }
        }
    }
}
