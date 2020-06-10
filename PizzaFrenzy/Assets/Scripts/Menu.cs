using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// controls menu
public class Menu : MonoBehaviour
{
    public GameObject difficulty;

    // function that loads game on button click
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // sets delivery time based on the selected difficulty
    private void OnDisable()
    {
        if (difficulty.GetComponent<Text>().text == "Easy")
        {
            PlayerPrefs.SetFloat("deliveryTime", 30f);
        } else if (difficulty.GetComponent<Text>().text == "Medium") {
            PlayerPrefs.SetFloat("deliveryTime", 25f);
        } else {
            PlayerPrefs.SetFloat("deliveryTime", 20f);
        }
        
    }
}
