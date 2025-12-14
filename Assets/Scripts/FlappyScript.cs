using UnityEngine;
using System.Collections;

public class FlappyScript : MonoBehaviour
{
    public AudioClip FlyAudioClip, DeathAudioClip, ScoredAudioClip;
    public Sprite GetReadySprite;
    public float RotateUpSpeed = 1, RotateDownSpeed = 1;
    public GameObject IntroGUI, DeathGUI;
    public Collider2D restartButtonGameCollider;
    public float VelocityPerJump = 3;

    // ★ 設為 0 → 鳥不會往右跑（不會離開地板/管子）
    public float XSpeed = 0;

    void Start()
    {
    }

    FlappyYAxisTravelState flappyYAxisTravelState;

    enum FlappyYAxisTravelState
    {
        GoingUp, GoingDown
    }

    Vector3 birdRotation = Vector3.zero;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (GameStateManager.GameState == GameState.Intro)
        {
            MoveBirdOnXAxis();  // 雖然 XSpeed=0 → 不會動

            if (WasTouchedOrClicked())
            {
                BoostOnYAxis();
                GameStateManager.GameState = GameState.Playing;
                IntroGUI.SetActive(false);
                ScoreManager.ResetScore();
            }
        }
        else if (GameStateManager.GameState == GameState.Playing)
        {
            MoveBirdOnXAxis(); // 同樣不會動

            if (WasTouchedOrClicked())
            {
                BoostOnYAxis();
            }
        }
        else if (GameStateManager.GameState == GameState.Dead)
        {
            Vector2 contactPoint = Vector2.zero;

            if (Input.touchCount > 0)
                contactPoint = Input.touches[0].position;
            if (Input.GetMouseButtonDown(0))
                contactPoint = Input.mousePosition;

            if (restartButtonGameCollider ==
                Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(contactPoint)))
            {
                GameStateManager.GameState = GameState.Intro;
                Application.LoadLevel(Application.loadedLevelName);
            }
        }
    }

    void FixedUpdate()
    {
        if (GameStateManager.GameState == GameState.Intro)
        {
            if (GetComponent<Rigidbody2D>().linearVelocity.y < -1)
                GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(0, GetComponent<Rigidbody2D>().mass * 5500 * Time.deltaTime));
        }
        else if (GameStateManager.GameState == GameState.Playing ||
                 GameStateManager.GameState == GameState.Dead)
        {
            FixFlappyRotation();
        }
    }

    bool WasTouchedOrClicked()
    {
        return (Input.GetButtonUp("Jump") ||
                Input.GetMouseButtonDown(0) ||
                (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended));
    }

    // ⭐⭐⭐ 復原 BoostOnYAxis（不能刪）
    void BoostOnYAxis()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, VelocityPerJump);
        GetComponent<AudioSource>().PlayOneShot(FlyAudioClip);
    }

    // ⭐⭐⭐ 復原 MoveBirdOnXAxis（不能刪）
    void MoveBirdOnXAxis()
    {
        transform.position += new Vector3(Time.deltaTime * XSpeed, 0, 0);
    }

    private void FixFlappyRotation()
    {
        if (GetComponent<Rigidbody2D>().linearVelocity.y > 0)
            flappyYAxisTravelState = FlappyYAxisTravelState.GoingUp;
        else
            flappyYAxisTravelState = FlappyYAxisTravelState.GoingDown;

        float degreesToAdd =
            (flappyYAxisTravelState == FlappyYAxisTravelState.GoingUp) ?
            (6 * RotateUpSpeed) :
            (-3 * RotateDownSpeed);

        birdRotation = new Vector3(
            0, 0, Mathf.Clamp(birdRotation.z + degreesToAdd, -90, 45));
        transform.eulerAngles = birdRotation;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag == "Pipeblank")
            {
                GetComponent<AudioSource>().PlayOneShot(ScoredAudioClip);
                ScoreManager.AddScore();
            }
            else if (col.gameObject.tag == "Pipe")
            {
                FlappyDies();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag == "Floor")
            {
                FlappyDies();
            }
        }
    }

    void FlappyDies()
    {
        GameStateManager.GameState = GameState.Dead;
        DeathGUI.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(DeathAudioClip);

        ScoreManager.SaveHighScore();

        FindObjectOfType<HighScoreSpriteDisplay>().UpdateHighScoreDisplay();  // ★ 必須刷新
    }
}
