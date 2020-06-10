using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

// controls the UI
public class ManageUI : MonoBehaviour
{

    public GameObject[] allUI;  // all UI house objects
    public List<GameObject> invisibleUI;  // UI that isnt visible
    public List<GameObject> visibleUI;  // UI that is currently visible
    public Camera mainCamera;  // the camera
    public GameObject pizzaIcon;  // the pizza icon
    public OrderGeneration orderGeneration;  // class that controls pizza shop
    bool pizzaDisp = false;  // the pizza display boolean
    Vector3 startpos;  // start position of UI house queue
    Vector3 delta;  // distance between house UI
    private AudioSource source;  // order bell audio
    public GameObject multiplier;  // text that shows how many pizzas are ready

    // Start is called before the first frame update
    void Start()
    {
        // get all UI houses
        allUI = GameObject.FindGameObjectsWithTag("UI");
        // create list for invisible UI from array
        invisibleUI = new List<GameObject>(allUI);
        // set start position and delta for house UI;
        startpos = new Vector3(0, 0, 0.4f);
        delta = new Vector3(0, 0, -0.08f);
        // set order bell sound
        source = pizzaIcon.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //houses
        for (int i = 0; i < visibleUI.ToArray().Length; i++)
        {
            // set house position and render
            visibleUI[i].transform.localPosition = startpos + i * delta;
            visibleUI[i].GetComponent<Renderer>().enabled = true;
            visibleUI[i].GetComponentInChildren<Text>().enabled = true;
            // update the time left for each visible house
            for (int j = 0; j < orderGeneration.allHouses.Length; j++)
            {
                if (orderGeneration.allHouses[j].name == visibleUI[i].name)
                {
                    visibleUI[i].GetComponentInChildren<Text>().text = System.Convert.ToInt32(orderGeneration.deliveryLimits[j]).ToString();
                }
            }
        }

        //pizzaicon
        if (pizzaDisp)
        {
            // set pizza icon time to house with least amount of time left
            pizzaIcon.GetComponentInChildren<Text>().enabled = true;
            float minTime = 9999;
            for (int i = 0; i < orderGeneration.orders.ToArray().Length; i++)
            {
                for (int j = 0; j < orderGeneration.allHouses.Length; j++)
                {
                    if (orderGeneration.orders[i] == orderGeneration.allHouses[j])
                    {
                        if (orderGeneration.deliveryLimits[j] < minTime)
                        {
                            minTime = orderGeneration.deliveryLimits[j];
                        }
                    }
                }
            }
            pizzaIcon.GetComponentInChildren<Text>().text = System.Convert.ToInt32(minTime).ToString();
        }
    }

    // function that adds UI object to visible UI queue
    public void addUI(GameObject house)
    {
        for (int i = 0; i < invisibleUI.ToArray().Length; i++)
        {
            if (house.name == invisibleUI[i].name)
            {
                visibleUI.Add(invisibleUI[i]);
                invisibleUI.RemoveAt(i);
            }
        }
    }

    // function that removes UI object from visible UI queue
    public void removeUI(GameObject house)
    {
        for (int i = 0; i < visibleUI.ToArray().Length; i++)
        {
            if (house.name == visibleUI[i].name)
            {
                invisibleUI.Add(visibleUI[i]);
                visibleUI[i].GetComponentInChildren<Text>().enabled = false;
                visibleUI[i].GetComponent<Renderer>().enabled = false;
                visibleUI.RemoveAt(i);
            }
        }
    }

    // turns pizza icon on and sets multiplier
    public void pizzaOn() {
        if (pizzaIcon.GetComponent<Renderer>().enabled == false)
        {
            source.Play();
        }
        pizzaIcon.GetComponent<Renderer>().enabled = true;
        multiplier.GetComponent<Text>().text = "x " + orderGeneration.orders.ToArray().Length.ToString();
        multiplier.GetComponent<Text>().enabled = true;
        pizzaDisp = true;
    }

    // turns pizza icon off
    public void pizzaOff()
    {
        pizzaIcon.GetComponent<Renderer>().enabled = false;
        pizzaIcon.GetComponentInChildren<Text>().enabled = false;
        multiplier.GetComponent<Text>().enabled = false;
        pizzaDisp = false;
    }


}

  