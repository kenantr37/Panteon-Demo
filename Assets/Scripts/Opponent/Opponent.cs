using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Opponent : MonoBehaviour
{
    //Opponent movement Process
    [SerializeField] float opponentMoveForwardSpeed = .5f;
    [SerializeField] List<Transform> objectsToEscape;

    //wayPoint Process
    //[SerializeField] List<Transform> wayPoints;
    [SerializeField] int wayPointIndex = 0;

    public float calculateHypotenuse;

    //Spaning to starting position of the opponent Process
    [SerializeField] bool _opponentMoveToStartChecker;
    Vector3 _opponentStartingPosition;
    public bool OppoonentStartingPosition { get { return _opponentMoveToStartChecker; } set { _opponentMoveToStartChecker = value; } }

    //Opponent FinishLine Rb
    Rigidbody _opponentRb;

    //NAVMESH OF OPPONENT
    NavMeshAgent _opponentNavMesh;
    [SerializeField] bool _opponentFollowNavMeshEnable;
    [SerializeField] bool _opponentFollowWayPointActive;

    [SerializeField] Transform opponentNavMeshFollowposition;
    [SerializeField] Transform opponentBridgeWayPoint;

    public Rigidbody OpponentRb { get { return _opponentRb; } set { _opponentRb = value; } }
    public NavMeshAgent OpponentNavMesh { get { return _opponentNavMesh; } set { _opponentNavMesh = value; } }
    public bool OpponentFollowNavMeshEnable { get { return _opponentFollowNavMeshEnable; } set { _opponentFollowNavMeshEnable = value; } }
    public bool OpponentWayPointActive { get { return _opponentFollowWayPointActive; } set { _opponentFollowWayPointActive = value; } }

    void Awake()
    {
        _opponentRb = GetComponent<Rigidbody>();
        _opponentNavMesh = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        _opponentStartingPosition = transform.position;
        _opponentFollowNavMeshEnable = true;
    }
    void Update()
    {
        MoveToStart();
        OpponentHorizontalBorder();
        OpponentMoveLeftRight();
        OpponentMoveForward();

    }
    void OpponentMoveForward()
    {
        if (_opponentFollowNavMeshEnable)
        {
            _opponentNavMesh.destination = opponentNavMeshFollowposition.position;
        }
        if (_opponentFollowWayPointActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, opponentBridgeWayPoint.position, opponentMoveForwardSpeed * Time.deltaTime);
        }
    }
    void OpponentMoveLeftRight()
    {
        foreach (Transform everyObject in objectsToEscape)
        {
            float verticalDistanceBetweenNextObject = Mathf.Abs(transform.position.z - everyObject.position.z);
            float horizontalDistanceBetweenNextObject = Mathf.Abs(transform.position.x - everyObject.position.x);

            if (verticalDistanceBetweenNextObject <= 2f)
            {
                Debug.DrawLine(transform.position, everyObject.position, Color.green);
                calculateHypotenuse = Mathf.Sqrt(Mathf.Pow(horizontalDistanceBetweenNextObject, 2) + Mathf.Pow(verticalDistanceBetweenNextObject, 2));

                if (calculateHypotenuse <= 1f && everyObject.position.x < transform.position.x)
                {
                    Debug.DrawLine(transform.position, everyObject.position, Color.red);
                    transform.Translate(Vector3.right * Time.deltaTime * .9f);
                }
                else if (calculateHypotenuse <= 1f && everyObject.position.x > transform.position.x)
                {
                    Debug.DrawLine(transform.position, everyObject.position, Color.red);
                    transform.Translate(Vector3.left * Time.deltaTime * .9f);
                }
            }
        }
    }
    void MoveToStart()
    {
        if (_opponentMoveToStartChecker)
        {
            transform.position = _opponentStartingPosition;
            _opponentMoveToStartChecker = false;

            _opponentFollowNavMeshEnable = true;
            _opponentNavMesh.enabled = true;
        }
    }
    void OpponentHorizontalBorder()
    {
        Vector3 opponentBorder = new Vector3(Mathf.Clamp(transform.position.x, -1.470f, 1.470f), transform.position.y, transform.position.z);
        transform.position = opponentBorder;
    }
}