using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Opponent : MonoBehaviour
{
    [Header("Opponent movement Process")]
    [SerializeField] float _opponentMoveForwardSpeed;
    [SerializeField] List<Transform> objectsToEscape;

    public float calculateHypotenuse;

    [Header("Spaning to starting position of the opponent Process")]
    [SerializeField] bool _opponentMoveToStartChecker;
    Vector3 _opponentStartingPosition;
    public bool OppoonentStartingPosition { get { return _opponentMoveToStartChecker; } set { _opponentMoveToStartChecker = value; } }

    //Opponent FinishLine Rb
    Rigidbody _opponentRb;

    [Header("Navmesh process of opponent")]
    NavMeshAgent _opponentNavMesh;
    [SerializeField] bool _opponentFollowNavMeshEnable;
    [SerializeField] bool _opponentFollowWayPointActive;
    [SerializeField] Transform opponentNavMeshFollowposition;
    [SerializeField] Transform opponentBridgeWayPoint;

    //gamemanager of opponent
    GameManager gameManager;
    //opponent animator
    Animator opponentAnimator;
    bool _opponentStopAnim;

    public Rigidbody OpponentRb { get { return _opponentRb; } set { _opponentRb = value; } }
    public NavMeshAgent OpponentNavMesh { get { return _opponentNavMesh; } set { _opponentNavMesh = value; } }
    public bool OpponentFollowNavMeshEnable { get { return _opponentFollowNavMeshEnable; } set { _opponentFollowNavMeshEnable = value; } }
    public bool OpponentWayPointActive { get { return _opponentFollowWayPointActive; } set { _opponentFollowWayPointActive = value; } }
    public float OppponentMoveForwardSpeed { get { return _opponentMoveForwardSpeed; } set { _opponentMoveForwardSpeed = value; } }
    public bool OpponentStopAnim { get { return _opponentStopAnim; } set { _opponentStopAnim = value; } }

    void Awake()
    {
        _opponentRb = GetComponent<Rigidbody>();
        _opponentNavMesh = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();
        opponentAnimator = GetComponent<Animator>();

    }
    void Start()
    {
        _opponentStartingPosition = transform.position;
        _opponentFollowNavMeshEnable = true;
    }
    void Update()
    {
        if (gameManager.isGameStarted)
        {
            opponentAnimator.SetBool("RunOpp", true);

            MoveToStart();
            OpponentHorizontalBorder();
            OpponentMoveLeftRight();
            OpponentMoveForward();
        }
        if (_opponentStopAnim)
        {
            opponentAnimator.SetBool("RunOpp", false);
        }
    }
    void OpponentMoveForward()
    {
        //opponent can move forward by referencing navmesh and my own waypoint system
        if (_opponentFollowNavMeshEnable)
        {
            _opponentNavMesh.destination = opponentNavMeshFollowposition.position;
        }
        if (_opponentFollowWayPointActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, opponentBridgeWayPoint.position, _opponentMoveForwardSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward);
        }
    }
    void OpponentMoveLeftRight()
    {
        // I used hypotenuse to calculate distance between opponent and every single obstacle
        foreach (Transform everyObject in objectsToEscape)
        {
            float verticalDistanceBetweenNextObject = Mathf.Abs(transform.position.z - everyObject.position.z);
            float horizontalDistanceBetweenNextObject = Mathf.Abs(transform.position.x - everyObject.position.x);

            if (verticalDistanceBetweenNextObject <= 1f)
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
        //if opponent dead, spawn initial location
        if (_opponentMoveToStartChecker)
        {
            transform.position = _opponentStartingPosition;
            _opponentMoveToStartChecker = false;

            _opponentFollowNavMeshEnable = true;
            _opponentNavMesh.enabled = true;
            _opponentMoveForwardSpeed = 0;
        }
    }
    void OpponentHorizontalBorder()
    {
        Vector3 opponentBorder = new Vector3(Mathf.Clamp(transform.position.x, -1.470f, 1.470f), transform.position.y, transform.position.z);
        transform.position = opponentBorder;
    }
}