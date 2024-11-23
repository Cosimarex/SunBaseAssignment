using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartBTN : MonoBehaviour
{
    public Spawner circleSpawner; 

    public void RestartGame()
    {
        circleSpawner.SpawnCircles();
    }
}
