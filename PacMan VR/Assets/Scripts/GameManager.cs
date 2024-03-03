using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghosts[] ghosts;
    public Player player;
    public Transform pellets;
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
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
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

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        player.ResetState();
    }



    private void SetScore(int _score)
    {
        score = _score;
    }

    private void SetLives(int _lives)
    {
        lives = _lives;
    }

    public void GhostEaten(Ghosts ghost)
    {
        int points = ghost.points;
        SetScore(this.score + points);
    }

    public void PlayerEaten()
    {
        //Need to put UI so u can do nothing

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }

        if (this.lives <= 0)
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            //You won UI
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform singlePellet in pellets)
        {
            if (pellets.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}
