using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    [SerializeField] float opponentMoveForwardSpeed = .5f;
    [SerializeField] List<Transform> objectsToEscape;
    [SerializeField] float calculateHypotenuse;
    [SerializeField] GameObject wayPoint;

    float opponentXBorder = 0.403f;
    void Update()
    {
        OpponentMoveForward();
        OpponentMoveLeftRight();
        //OpponentKeepStayingOnTheWay();

    }
    void OpponentMoveForward()
    {
        //Vector3 opponentForward = new Vector3(0, transform.localPosition.y, 1) * Time.deltaTime * opponentMoveForwardSpeed;
        //transform.Translate(opponentForward);
        transform.position = Vector3.MoveTowards(transform.localPosition, wayPoint.transform.position, Time.deltaTime * opponentMoveForwardSpeed);
        transform.rotation = Quaternion.Euler(Vector3.forward);
    }
    void OpponentMoveLeftRight()
    {
        foreach (Transform everyObject in objectsToEscape)
        {
            float verticalDistanceBetweenNextObject = Mathf.Abs(transform.position.z - everyObject.position.z);
            float horizontalDistanceBetweenNextObject = Mathf.Abs(transform.localPosition.x - everyObject.position.x);

            if (verticalDistanceBetweenNextObject <= 1)
            {
                Debug.DrawLine(transform.position, everyObject.position, Color.green);
                calculateHypotenuse = Mathf.Sqrt(Mathf.Pow(horizontalDistanceBetweenNextObject, 2) + Mathf.Pow(verticalDistanceBetweenNextObject, 2));

                if (calculateHypotenuse <= .5f && everyObject.position.x <= transform.localPosition.x)
                {
                    Debug.DrawLine(transform.position, everyObject.position, Color.red);
                    transform.Translate(Vector3.right * Time.deltaTime * 3);
                }
                else if (calculateHypotenuse <= .5f && everyObject.position.x > transform.localPosition.x)
                {
                    Debug.DrawLine(transform.position, everyObject.position, Color.red);
                    transform.Translate(Vector3.left * Time.deltaTime * 3);
                }
            }
        }
    }
    void OpponentKeepStayingOnTheWay()
    {
        //float opponentWayBorderx = Mathf.Clamp(transform.localPosition.x, -0.482f, 0.482f);
        //transform.localPosition = new Vector3(opponentWayBorderx, transform.localPosition.y, transform.localPosition.z);

        if (transform.localPosition.x <= -opponentXBorder)
        {
            transform.Translate(0, 0, transform.localPosition.z);
        }
    }
}
