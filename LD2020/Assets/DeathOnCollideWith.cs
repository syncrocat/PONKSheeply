using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeathOnCollideWith : MonoBehaviour
{
   // public string toCollideTag;
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
        // I have this hardcoded nonsense but im getting railed because tags cannot be string variables
        if ((collision.gameObject.CompareTag("wolf") && collision.gameObject.GetComponent<WolfBehaviour>().GetState() == WolfState.Chase) || (collision.gameObject.CompareTag("spikes") && gameObject.CompareTag("Player")))
        {
            //Debug.Log("WTF");
            GetComponent<DeathScript>().Die();
        }
    }
}
