using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public Texture2D Texture;
        public SpriteRenderer FildRenderer;
        public Image[] liveImages;
        public Sprite liveSprite;
        public Sprite deadSprite;

        private GameXonix _gameXonix;

        private float _levelTime;
        public bool TimeIsUp;

        #region UI_ELEMENTS
        public Text Level;
        public Text Fill;
        public Text Score;

        public RectTransform GameOver;
        #endregion

        #region SWIPE_DETECTION
        private Vector3 _touchPosition;
        private float _swipeResistanceX = 50.0f;
        private float _swipeResistanceY = 100.0f;
        #endregion
        [SerializeField] private AudioClip _clickSound;

        public void PlaySound()
        {
            AudioManager.instance.PlayAudio(_clickSound);
        }
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            GameStart();
        }

        void Update()
        {
            CountDownLevelTime();
            Move();
        }

        private void GameStart()
        {
            SetUI(true);
            SetTimer();
            _gameXonix = new GameXonix();
            StartCoroutine(_gameXonix.Go());
        }

        public void SetTimer()
        {
            TimeIsUp = false;
            _levelTime = 60;
        }

        public void SetUI(bool isVisible)
        {
            GameOver.gameObject.SetActive(!isVisible);
        }


        private void Move()
        {
#if UNITY_EDITOR 
            if (!GameXonix.gameOverOrPause.IsPaused() && !GameXonix.gameOverOrPause.IsGameOver())
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    GameXonix.xonix.SetDirection(GameXonix.RIGHT);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GameXonix.xonix.SetDirection(GameXonix.UP);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    GameXonix.xonix.SetDirection(GameXonix.LEFT);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameXonix.xonix.SetDirection(GameXonix.DOWN);
                }
            }
#endif

#if UNITY_ANDROID
            if (!GameXonix.gameOverOrPause.IsPaused() && !GameXonix.gameOverOrPause.IsGameOver())
            {
                if(Input.GetMouseButtonDown(0))
                {
                    _touchPosition = Input.mousePosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    var deltaSwipe = _touchPosition - Input.mousePosition;
                    if(Mathf.Abs(deltaSwipe.x) > _swipeResistanceX)
                    {
                        //Swipe on the X axis
                        GameXonix.xonix.SetDirection((deltaSwipe.x < 0) ? GameXonix.RIGHT : GameXonix.LEFT);
                    }
                    else if (Mathf.Abs(deltaSwipe.y) > _swipeResistanceY)
                    {
                        //Swipe on the Y axis
                        GameXonix.xonix.SetDirection((deltaSwipe.y < 0) ? GameXonix.DOWN : GameXonix.UP);
                    }
                }
            }
#endif
        }

        private void CountDownLevelTime()
        {
            if (GameXonix.gameOverOrPause.IsPaused() || GameXonix.gameOverOrPause.IsGameOver()) return;
            if ((_levelTime > 0))
            {
                _levelTime -= Time.deltaTime;
            }
            else
                TimeIsUp = true;
        }

        public void OnPlayButton()
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        public void SetLivesCount(int live)
        {
            int deadCount = 3 - live;
            for (int i = 0; i < deadCount; i++)
                liveImages[i].sprite = deadSprite;
        }

        public void SetFillAmount(int fillArea)
        {
            Fill.text = Mathf.Round(fillArea).ToString() + " %";
        }

        public void SetLevelNum(int level)
        {
            Level.text = $"Level: {level}";
        }

        public void SetScoreNum(int score)
        {
            Score.text = $"Score: {score}";
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
