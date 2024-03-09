using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Sword sword;
    public bool SwordGrabbed { get { return sword.Grabbed; } }
    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();
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
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].DeActivate();
        }

        // Wait for one second
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

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
        //Need to put UI so u can do nothing

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            StartCoroutine(DelayedResetState());
        }

        if (this.lives <= 0)
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false); // Deactivate the specific pellet that was eaten
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            //You won UI
            StartCoroutine(DelayedNewRound());
        }
    }

    private IEnumerator DelayedNewRound()
    {
        yield return new WaitForSeconds(3.0f);
        NewRound();
    }

    public void SwordHit(Ghosts ghost)
    {
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
}
