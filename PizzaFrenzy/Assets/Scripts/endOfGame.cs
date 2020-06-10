using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// controls game over screen
public class endOfGame : MonoBehaviour
{

    public GameObject scoreText;  // the final score
    string score;  // the string used to obtain the final score

    // Start is called before the first frame update
    private void Start()
    {
        // load and render the game score
        score = PlayerPrefs.GetString("score");
        scoreText.GetComponent<Text>().text = score;
        scoreText.GetComponent<Text>().enabled = true;
    }

    // function that restarts the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // function that returns to menu
    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

}
