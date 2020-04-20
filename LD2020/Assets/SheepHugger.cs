using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHugger : MonoBehaviour
{
    public GameObject sheep;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = sheep.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = sheep.transform.position;
    }
}
