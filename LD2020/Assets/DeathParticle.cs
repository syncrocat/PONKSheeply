using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathParticle : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);

        timer = 0;
        Rigidbody _rb = GetComponent<Rigidbody>();


        float thing1 = Random.Range(-1f, 1f);
        float thing2 = Random.Range(2f, 3f);
        float thing3 = Random.Range(-1f, 1f);

        float magnitude = 750;

        _rb.AddForce(new Vector3(thing1, thing2, thing3) * magnitude);
        _rb.AddTorque(new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f)));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > 1.5)
        {
            Destroy(this.gameObject);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("deathParticle") || collision.gameObject.CompareTag("sheep"))
        {
            Debug.Log("i want death");
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

    }
}
