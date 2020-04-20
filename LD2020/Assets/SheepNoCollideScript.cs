using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepNoCollideScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("sheep") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player_Piece"))
        {
            
            Collider[] colliders = collision.gameObject.GetComponents<Collider>();
            foreach (Collider c in colliders)
            {
                Physics.IgnoreCollision(c, GetComponent<Collider>());
            }
        }
        else
        {
            Debug.Log("player");
        }
    }
}
