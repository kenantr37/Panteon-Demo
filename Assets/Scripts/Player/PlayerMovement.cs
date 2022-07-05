using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Process")]
    [SerializeField] float _playerMoveForwardSpeed = 1f;
    [SerializeField] float _mouseSpeed = 2.5f;
    [SerializeField] float _mobilScreenSpeed = 0.005f;
    [SerializeField] Vector3 swerveFirstPosition, swerveLastPosition, distanceFirstLastPosition;

    [Header("Wall Process")]
    [SerializeField] Renderer wall;
    [SerializeField] GameObject wallScaleSize;
    [SerializeField] float paintedWallRatio;
    public TextMeshProUGUI paintedWallRatioText;
    float transparencyRation;

    [Header("Finish Line Checker")]
    [Tooltip("Is player on the finish way before the paint-wall ?")]
    [SerializeField] bool _finishLineArrived;

    [Header("Player Ranking")]
    [SerializeField] int _playerFirstRank;
    [SerializeField] TextMeshProUGUI playerRankText;
    Opponent[] opponents;

    //Did player fall to holl?
    bool _playerDeadChecker;
    Vector3 playerFirstPosition;

    //Camera rb setting
    Rigidbody _playerRb;

    public bool FinishLineArrived { get { return _finishLineArrived; } set { _finishLineArrived = value; } }
    public float PlayerMoveForwardSpeed { get { return _playerMoveForwardSpeed; } set { _playerMoveForwardSpeed = value; } }
    public float MouseSpeed { get { return _mouseSpeed; } set { _mouseSpeed = value; } }
    public float MobilScreenSpeed { get { return _mobilScreenSpeed; } set { _mobilScreenSpeed = value; } }
    public bool PlayerDeadChecker { get { return _playerDeadChecker; } set { _playerDeadChecker = value; } }
    public Rigidbody PlayerRb { get { return _playerRb; } set { _playerRb = value; } }

    void Awake()
    {
        opponents = FindObjectsOfType<Opponent>();
        _playerRb = GetComponent<Rigidbody>();
        PlayerFirsRank();
    }
    void Start()
    {
        float startPos = Input.mousePosition.x;
        _finishLineArrived = false;
        transparencyRation = 0f;

        Debug.Log("starting rank : " + _playerFirstRank);

        playerFirstPosition = transform.position;
        paintedWallRatioText.gameObject.SetActive(false);

    }

    void Update()
    {
        PLayerDead();

        if (FinishLineArrived)
        {
            PaintTheWallMouse();
            PaintTheWallMobil();
        }
        else
        {
            MoveForward();
            SwerveMechanicMouse(_mouseSpeed);
            SwerveMechanicMobil(_mobilScreenSpeed);
            PlayerRankManager();
        }
    }
    void MoveForward()
    {
        Vector3 playerForward = Vector3.forward * _playerMoveForwardSpeed * Time.deltaTime;
        transform.position = transform.position + playerForward;
        transform.rotation = Quaternion.Euler(Vector3.forward);
    }
    void SwerveMechanicMouse(float mouseSpeed)
    {
        if (Input.GetMouseButtonDown(0))
        {
            swerveFirstPosition.x = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            swerveLastPosition.x = Input.mousePosition.x;
            distanceFirstLastPosition = swerveLastPosition - swerveFirstPosition;
            transform.Translate(distanceFirstLastPosition.x * Time.deltaTime * mouseSpeed * Time.deltaTime
                , 0, 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swerveFirstPosition = Vector3.zero;
            swerveLastPosition = Vector3.zero;
        }
    }
    void SwerveMechanicMobil(float mobilScreenSpeed)
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
                transform.Translate(distanceFirstLastPosition.x * mobilScreenSpeed * Time.deltaTime
                    , 0, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swerveLastPosition = Vector3.zero;
                swerveLastPosition = Vector3.zero;
            }
        }
    }
    void PaintTheWallMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swerveFirstPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            swerveLastPosition = Input.mousePosition;
            if (swerveLastPosition != swerveFirstPosition)
            {
                wall.material.color = new Color(1, 0, 0, Mathf.Lerp(0f, 1f, transparencyRation));
                transparencyRation += .1f * Time.deltaTime;
                paintedWallRatio = Mathf.RoundToInt(wall.material.color.a * 100);

                wallScaleSize.transform.parent.localScale = new Vector3(1, Mathf.Lerp(0, 1, transparencyRation), 1);
                Debug.Log("You've just painted %" + paintedWallRatio + " of the wall!");

                paintedWallRatioText.text = "YOU'VE JUST PAINTED %" + paintedWallRatio;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            swerveFirstPosition = Vector3.zero;
            swerveLastPosition = Vector3.zero;
        }
    }
    void PaintTheWallMobil()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swerveFirstPosition.x = touch.position.x;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (swerveLastPosition != swerveFirstPosition)
                {
                    wall.material.color = new Color(1, 0, 0, Mathf.Lerp(0f, 1f, transparencyRation));
                    transparencyRation += 0.0005f;
                    paintedWallRatio = Mathf.RoundToInt(wall.material.color.a * 100);

                    wallScaleSize.transform.parent.localScale = new Vector3(1, Mathf.Lerp(0, 1, transparencyRation), 1);
                    Debug.Log("You've just painted %" + paintedWallRatio + " of the wall!");
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                swerveLastPosition = Vector3.zero;
                swerveLastPosition = Vector3.zero;
            }
        }
    }
    void PlayerFirsRank()
    {
        foreach (Opponent opponent in opponents)
        {
            if (transform.position.z <= opponent.transform.position.z)
            {
                _playerFirstRank++;
            }
        }
    }
    void PLayerDead()
    {
        if (_playerDeadChecker)
        {
            transform.position = playerFirstPosition;
            _playerDeadChecker = false;
        }
    }
    void PlayerRankManager()
    {
        float count = opponents.Length + 1;

        for (int i = 0; i < opponents.Length; i++)
        {
            if (transform.position.z > opponents[i].transform.position.z)
            {
                count--;
            }
            else
                count++;
        }
        ManuelCountDisplayer(count);
        Debug.Log(count);
    }
    void ManuelCountDisplayer(float count)
    {
        if (count == 21)
        {
            playerRankText.text = "Player Rank : 11";

        }
        if (count == 19)
        {
            playerRankText.text = "Player Rank : 10";

        }
        if (count == 17)
        {
            playerRankText.text = "Player Rank : 9";

        }
        if (count == 15)
        {
            playerRankText.text = "Player Rank : 8";
        }
        if (count == 13)
        {
            playerRankText.text = "Player Rank : 7";

        }
        if (count == 11)
        {
            playerRankText.text = "Player Rank : 6";
        }
        if (count == 9)
        {
            playerRankText.text = "Player Rank : 5";

        }
        if (count == 7)
        {
            playerRankText.text = "Player Rank : 4";

        }
        if (count == 5)
        {
            playerRankText.text = "Player Rank : 3";

        }
        if (count == 3)
        {
            playerRankText.text = "Player Rank : 2";
        }
        if (count == 1)
        {
            playerRankText.text = "Player Rank : " + count;
        }
    }
}