using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerSpawner spawner;
    public NewHoleScript hole;

    void Start()
    {
        this.transform.rotation = new Quaternion(0, 0.383f, 0, 0.924f);
        spawner.Spawn();
        hole.Begin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
