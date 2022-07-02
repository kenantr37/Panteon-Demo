using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    [SerializeField] float opponentMoveForwardSpeed = .5f;
    [SerializeField] List<Transform> objectsToEscape;

    [SerializeField] List<Transform> wayPoints;
    [SerializeField] int wayPointIndex = 0;

    [SerializeField] Opponent opponent;
    public float calculateHypotenuse;

    void Update()
    {
        OpponentMoveForward();
        OpponentMoveLeftRight();
        OpponentFarFromOthers();
    }
    void OpponentMoveForward()
    {
        int lastWayPointIndex = wayPointIndex;
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[wayPointIndex].position, opponentMoveForwardSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward);

        if (Vector3.Distance(transform.position, wayPoints[wayPointIndex].position) <= 3f)
        {
            lastWayPointIndex = wayPointIndex;
            wayPointIndex++;

            if (wayPointIndex == wayPoints.Count)
            {
                wayPointIndex = lastWayPointIndex;
                //BURAYA KARAKTERÝ DURDURACAK BÝR ÞEYLER YAZ
            }
        }
    }
    void OpponentMoveLeftRight()
    {
        foreach (Transform everyObject in objectsToEscape)
        {
            float verticalDistanceBetweenNextObject = Mathf.Abs(transform.position.z - everyObject.position.z);
            float horizontalDistanceBetweenNextObject = Mathf.Abs(transform.position.x - everyObject.position.x);

            if (verticalDistanceBetweenNextObject <= 1)
            {
                Debug.DrawLine(transform.position, everyObject.position, Color.green);
                calculateHypotenuse = Mathf.Sqrt(Mathf.Pow(horizontalDistanceBetweenNextObject, 2) + Mathf.Pow(verticalDistanceBetweenNextObject, 2));

                if (calculateHypotenuse <= .5f && everyObject.position.x <= transform.position.x)
                {
                    Debug.DrawLine(transform.position, everyObject.position, Color.red);
                    transform.Translate(Vector3.right * Time.deltaTime * 10);
                }
                else if (calculateHypotenuse <= .5f && everyObject.position.x > transform.position.x)
                {
                    Debug.DrawLine(transform.position, everyObject.position, Color.red);
                    transform.Translate(Vector3.left * Time.deltaTime * 10);
                }
            }
        }
    }
    void OpponentFarFromOthers()
    {
        //if (Vector3.Distance(transform.position, opponent.transform.position) <= .5f)
        //{
        //    Debug.DrawLine(transform.position, opponent.transform.position, Color.blue);
        //}
    }
}
