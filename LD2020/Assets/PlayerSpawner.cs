using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        GameObject.FindWithTag("Player").transform.position = this.transform.position + new Vector3(0,50,0);
        Destroy(this.gameObject);
    }
}
