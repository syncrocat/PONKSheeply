using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public List<AudioSource> deathAudioSources = new List<AudioSource>();
    public GameObject deathParticle;
    public List<GameObject> playerBodyRender;
    public SheepSoundBox sheepSoundBox;
    public PlayerDeathSounder playerDeathSounder;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -100)
        {
            this.Die();
        }
    }

    public void Die()
    {
        if (dead)
        {

        }
        else
        {
            dead = true;
            
            for (int i = 0; i < 5; i++)
            {
                //Vector3 newVector = new Vector3(-2.5f + i * 0.5f, 0, -2.5f + i * 0.5f);
                GameObject newDeathParticle = Instantiate(deathParticle, transform.position, Quaternion.identity);
            }

            if (gameObject.CompareTag("Player"))
            {
                playerDeathSounder.Play();
                GetComponent<Rigidbody>().velocity = Vector3.zero; 
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<ShepherdMovementManager>().enabled = false;
                GetComponent<GravityComponent>().enabled = false;
                GetComponent<Collider>().enabled = false;
                foreach (GameObject GO in playerBodyRender)
                {
                    Destroy(GO);
                }
                
            } else if (gameObject.CompareTag("sheep"))
            {
                sheepSoundBox.PlayDeathSound();
                foreach (GameObject GO in playerBodyRender)
                {
                    Destroy(GO);
                }
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
       

    }
}
