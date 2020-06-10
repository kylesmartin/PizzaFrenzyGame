using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// assigns houses to random locations
public class HouseAssigner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        3 lists: houses, rotations, positions
        randomly select a house
        assign first position and rotation to that house
        remove everything from lists
        */
        GameObject[] allHouses = GameObject.FindGameObjectsWithTag("House");
        List<GameObject> houses = new List<GameObject>(allHouses);
        List<Vector3> positions = new List<Vector3>();
        List<Quaternion> rotations = new List<Quaternion>();
        for (int i = 0; i < allHouses.Length; i++)
        {
            positions.Add(allHouses[i].transform.position);
            rotations.Add(allHouses[i].transform.rotation);
        }
        // assign positions and rotations to houses
        while (houses.ToArray().Length > 0)
        {
            int ind = Random.Range(0, houses.ToArray().Length);
            houses[ind].transform.position = positions[0];
            houses[ind].transform.rotation = rotations[0];
            houses.RemoveAt(ind);
            positions.RemoveAt(0);
            rotations.RemoveAt(0);
        }
    }

}
