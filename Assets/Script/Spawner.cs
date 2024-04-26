using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] house_prefabs;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnHouse(int player_index, Vector3 pos, Quaternion rot)
    {
        Instantiate(house_prefabs[player_index], pos, rot);
    }
}
