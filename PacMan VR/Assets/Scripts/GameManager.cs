using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Static reference to the singleton instance
    private static GameManager _instance;

    // Public accessor for the singleton instance
    public static GameManager Instance
    {
        get
        {
            // Check if the instance has not been set yet
            if (_instance == null)
            {
                // Attempt to find an existing instance in the scene
                _instance = FindObjectOfType<GameManager>();

                // If no instance exists, create a new one
                if (_instance == null)
                {
                    // Create a new GameObject to hold the GameManager instance
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public Ghosts[] ghosts;
    public Player player;
    public Transform pellets;
    public Sword[] swords;
    public GameObject youDiedUI;
    public GameObject youWinUI;
    public GameObject youLoseUI;
    public bool SwordGrabbed
    {
        get
        {
            // Check if any sword is grabbed
            foreach (Sword sword in swords)
            {
                if (sword.Grabbed)
                    return true;
            }
            return false;
        }
    }

    public int score { get; private set; }
    public int highScore { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();

        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
        else
        {
            highScore = 0;
        }
    }

    private void Awake()
    {
        // Ensure that this GameManager persists across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void GameOver()
    {
        // Deactivate all ghosts
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        // Deactivate all pellets
        foreach (Transform singlePellet in pellets)
        {
            singlePellet.gameObject.SetActive(false);
        }

        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(4.0f); // Wait for 4 seconds

        youLoseUI.SetActive(false);
        SceneManager.LoadScene(2); // You may want to load the game over scene after showing the UI
    }


    private void NewRound()
    {
        foreach (Transform singlePellet in pellets)
        {
            pellets.gameObject.SetActive(true);
        }
        ResetState();
    }

    private IEnumerator DelayedResetState()
    {

        player.GetComponent<CharacterController>().enabled = false;

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].DeActivate();
        }

        // Wait for five second
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }


        player.GetComponent<CharacterController>().enabled = true;

        youDiedUI.SetActive(false);
        player.ResetState();
    }

    private void ResetState()
    {
        StartCoroutine(DelayedResetState());
    }

    private void SetScore(int _score)
    {
        score = _score;
    }

    private void SetLives(int _lives)
    {
        lives = _lives;
    }

    public void PlayerEaten()
    {
        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            youDiedUI.SetActive(true);
            StartCoroutine(DelayedResetState());
        }

        if (this.lives <= 0)
        {
            UpdateHighScore(); // Update high score before game over
            youLoseUI.SetActive(true);
            GameOver();


        }
    }

    public void PelletEaten(Pellet pellet)
    {
        AudioManager.Instance.PlayAudio("PelletEaten");
        pellet.gameObject.SetActive(false); // Deactivate the specific pellet that was eaten
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            youWinUI.SetActive(true);
            StartCoroutine(DelayedYouWin());
        }
    }

    private IEnumerator DelayedYouWin()
    {
        yield return new WaitForSeconds(5.0f);
        youWinUI.SetActive(false);
        SceneManager.LoadScene(2);

    }

    public void SwordHit(Ghosts ghost)
    {
        AudioManager.Instance.PlayAudio("GhostDeath");
        ghost.gameObject.SetActive(false);
        SetScore(this.score + ghost.points);
        ghost.ResetState();
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform singlePellet in pellets)
        {
            if (singlePellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", score);
            highScore = score; // Update the high score variable as well
        }
    }
}
