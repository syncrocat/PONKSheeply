using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleScript : MonoBehaviour
{
    private bool _sheepHasFinished = false;
    private bool _playerHasFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_sheepHasFinished && _playerHasFinished)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName += "1");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("sheep"))
        {
            _sheepHasFinished = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHasFinished = true;
        }
    }
}
