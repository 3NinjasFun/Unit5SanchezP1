using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRate = 1.0f;

    int score;
    public int lives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;

    public bool isGameActive;

    public Button restartButton;
    public Button resumeButton;
    public GameObject titleScreen;

    

    public bool isPaused = false;
    public bool isPlaying = true;

    private Button start;

 


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);
        spawnRate /= difficulty;

        titleScreen.gameObject.SetActive(false);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLives(int liveToSub)
    {
        lives -= liveToSub;
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()  
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            GameObject[] clones = GameObject.FindGameObjectsWithTag("Good");

            foreach (GameObject clone in clones)
            {
                BoxCollider boxCollider = clone.GetComponent<BoxCollider>();

                if(boxCollider != null)
                {
                    boxCollider.enabled = false;
                }
            }


            Time.timeScale = 0.0f;

            resumeButton.gameObject.SetActive(true);
            
        }
    }
    public void Resume()

    {
        Time.timeScale = 1.0f;
        resumeButton.gameObject.SetActive(false);

        GameObject[] clones = GameObject.FindGameObjectsWithTag("Good");

        foreach (GameObject clone in clones)
        {
            BoxCollider boxCollider = clone.GetComponent<BoxCollider>();

            if (boxCollider != null)
            {
                boxCollider.enabled = true;
            }
        }
    }
}
