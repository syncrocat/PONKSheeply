using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLeaveScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = GameObject.FindWithTag("Level").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0, 15.77f * 3, -12.43f * 3);
        transform.rotation = new Quaternion(0.383f, 0, 0, 0.924f);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
