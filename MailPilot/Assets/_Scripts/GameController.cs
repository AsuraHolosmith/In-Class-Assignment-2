using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;


public class GameController : MonoBehaviour
{
    [Header("Scene Game Objects")]
    public GameObject cloud;
    public GameObject island;
    public int numberOfClouds;
    public List<GameObject> clouds;

    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;


    //public HighScoreSO highScoreSO;

    [Header("UI Control")]
    public GameObject startLabel;
    public GameObject startButton;
    public GameObject endLabel;
    public GameObject restartButton;

    public ScoreBoard scoreBoard;

    public List<SceneSettings> sceneSettings;
    private SceneSettings activeSceneSettings;

    // public properties
    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            scoreBoard.lives = _lives;

            if(_lives < 1)
            {
                
                SceneManager.LoadScene("End");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives.ToString();
            }
           
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            scoreBoard.score = _score;

            
            if (scoreBoard.highScore < _score)
            {
                scoreBoard.highScore = _score;
            }
            scoreLabel.text = "Score: " + _score.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObjectInitialization();
        SceneConfiguration();
    }

    private void GameObjectInitialization()
    {
        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");
    }


    private void SceneConfiguration()
    {
        // converts the scene name to an enum
        var sceneToCompare = (Scene) System.Enum.Parse(typeof(Scene),
            SceneManager.GetActiveScene().name.ToUpper());

        // uses LINQ to return a setting that matches the current scene name
        var query = from setting in sceneSettings
                    where setting.scene == sceneToCompare
                    select setting;

        // returns the first item that matches the criteria from the list
        activeSceneSettings = query.ToList().First();

        {
            if (activeSceneSettings.scene == Scene.MAIN)
            {
                Lives = 5;
                Score = 0;
            }

            activeSoundClip = activeSceneSettings.activeSoundClip;
            scoreLabel.enabled = activeSceneSettings.scoreLabelEnabled;
            livesLabel.enabled = activeSceneSettings.livesLabelEnabled;
            highScoreLabel.enabled = activeSceneSettings.highScoreLabelEnabled;
            startLabel.SetActive(activeSceneSettings.startLabelSetActive);
            endLabel.SetActive(activeSceneSettings.endLabelSetActive);
            startButton.SetActive(activeSceneSettings.startButtonActive);
            restartButton.SetActive(activeSceneSettings.restartButtonActive);

            scoreLabel.text = "Score: " + scoreBoard.score;
            livesLabel.text = "Lives: " + scoreBoard.lives;
            highScoreLabel.text = "High Score: " + scoreBoard.highScore;
        }

        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        {
            AudioSource activeAudioSource = audioSources[(int)activeSoundClip];
            activeAudioSource.playOnAwake = true;
            activeAudioSource.loop = true;
            activeAudioSource.volume = 0.5f;
            activeAudioSource.Play();
        }



        // creates an empty container (list) of type GameObject
        clouds = new List<GameObject>();

        for (int cloudNum = 0; cloudNum < numberOfClouds; cloudNum++)
        {
            clouds.Add(Instantiate(cloud));
        }

        Instantiate(island);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Event Handlers
    public void OnStartButtonClick()
    {
        
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
