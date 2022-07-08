using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Process")]
    [SerializeField] float _playerMoveForwardSpeed = 1.3f;
    [SerializeField] float _mouseSpeed = 1f;
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

    //Game Manager
    GameManager gameManager;

    //Animation 
    Animator _playerAnimator;
    bool _playerStopRunning;

    public bool FinishLineArrived { get { return _finishLineArrived; } set { _finishLineArrived = value; } }
    public float PlayerMoveForwardSpeed { get { return _playerMoveForwardSpeed; } set { _playerMoveForwardSpeed = value; } }
    public float MouseSpeed { get { return _mouseSpeed; } set { _mouseSpeed = value; } }
    public float MobilScreenSpeed { get { return _mobilScreenSpeed; } set { _mobilScreenSpeed = value; } }
    public bool PlayerDeadChecker { get { return _playerDeadChecker; } set { _playerDeadChecker = value; } }
    public Rigidbody PlayerRb { get { return _playerRb; } set { _playerRb = value; } }
    public bool PlayerStopRunning { get { return _playerStopRunning; } set { _playerStopRunning = value; } }

    void Awake()
    {
        opponents = FindObjectsOfType<Opponent>();
        _playerRb = GetComponent<Rigidbody>();
        PlayerFirsRank();
        gameManager = FindObjectOfType<GameManager>();
        _playerAnimator = GetComponent<Animator>();
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
        if (gameManager.isGameStarted)
        {
            if (FinishLineArrived)
            {

            }
            else
            {
                _playerAnimator.SetBool("RunPlayer", true);

                MoveForward();
                SwerveMechanicMouse(_mouseSpeed);
                SwerveMechanicMobil(_mobilScreenSpeed);
                PlayerRankManager();
                PlayerHorizontalBorder();
            }
            PLayerDead();
        }
        if (_playerStopRunning)
        {
            _playerAnimator.SetBool("RunPlayer", false);
        }
    }
    void PlayerHorizontalBorder()
    {
        //to make player stay in horizontal border
        Vector3 playerBorder = new Vector3(Mathf.Clamp(transform.position.x, -1.470f, 1.470f), transform.position.y, transform.position.z);
        transform.position = playerBorder;
    }
    void MoveForward()
    {
        Vector3 playerForward = Vector3.forward * _playerMoveForwardSpeed * Time.deltaTime;
        transform.position = transform.position + playerForward;
        transform.rotation = Quaternion.Euler(Vector3.forward);
    }
    void SwerveMechanicMouse(float mouseSpeed)
    {
        //swerve mechanic for mouse 
        if (Input.GetMouseButtonDown(0))
        {
            swerveFirstPosition.x = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            swerveLastPosition.x = Input.mousePosition.x;
            distanceFirstLastPosition = swerveLastPosition - swerveFirstPosition;
            transform.Translate(distanceFirstLastPosition.x * Time.deltaTime * mouseSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swerveFirstPosition = Vector3.zero;
            swerveLastPosition = Vector3.zero;
        }
    }
    void SwerveMechanicMobil(float mobilScreenSpeed)
    {
        //swerve mechanic for mobil devices
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
        // player can change scale of the wall with swerve mechanic.
        // I increase opacity during swerve mechanic
        // when scale of the wall is %100, player can not scale wall
        // in this case, it look like painting the wall
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

                wallScaleSize.transform.parent.localScale = new Vector3(1, Mathf.Lerp(0, 1f, transparencyRation), 1);
                Debug.Log("You've just painted %" + paintedWallRatio + " of the wall!");

                paintedWallRatioText.text = "%" + paintedWallRatio;
                if (paintedWallRatio == 100)
                {
                    gameManager.RestartGame();
                }
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
        // painting for mobile devices
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
                    paintedWallRatioText.text = "%" + paintedWallRatio;
                    if (paintedWallRatio == 100)
                    {
                        gameManager.RestartGame();
                    }
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
        //to get starting rank of the player at the beginning of the game
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
        // to check is player dead or alive?
        if (_playerDeadChecker)
        {
            transform.position = playerFirstPosition;
            _playerDeadChecker = false;
        }
    }
    void PlayerRankManager()
    {
        // getting current rank of the player
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
        // I can't increase "count" 1 unit because of update method 
        // update increase "count" 2 units
        // that's why I handle "count" units with this method
        if (count == 21)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 11\n" + Ranks.LOOSING.ToString();

        }
        if (count == 19)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 10 ";

        }
        if (count == 17)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 9 ";

        }
        if (count == 15)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 8 ";
        }
        if (count == 13)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 7 ";

        }
        if (count == 11)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 6 ";
        }
        if (count == 9)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 5 \n" + Ranks.FASTER.ToString() + Ranks.DUDE.ToString();

        }
        if (count == 7)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 4 ";

        }
        if (count == 5)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 3 ";

        }
        if (count == 3)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": 2";
        }
        if (count == 1)
        {
            playerRankText.text = Ranks.RANK.ToString() + ": " + count + "\n" + Ranks.AWSOME.ToString();
        }
    }
    enum Ranks
    {
        RANK, AWSOME, LOOSING, FASTER, DUDE
    }
}