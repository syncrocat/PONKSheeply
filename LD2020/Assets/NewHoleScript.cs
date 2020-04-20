using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewHoleScript : MonoBehaviour
{
    private bool _hasBegun = false;
    bool hasBall = false;
    float timer = -1;
    private MusicPlayer _musicPlayer;
    Renderer myRenderer;
    public Material hasBallMat;
    public Material noBallMat;
    public Material hasPlayerMat;
    float x = 0;
    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        _musicPlayer = GameObject.FindWithTag("musicPlayer").GetComponent<MusicPlayer>();
        myRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_hasBegun)
        {
            return;
        }

        //Hover
        x += Time.fixedDeltaTime;
        float sinx = 2 * Mathf.Sin(2 * x);
        if (x > 3000) x -= 3000;
        transform.position = new Vector3(startingPos.x, startingPos.y + sinx, startingPos.z);

        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
        if (timer > 0 && timer < 1)
        {
            _musicPlayer.PlayLevelCompleteSound();
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "level11111111") SceneManager.LoadScene("Menu");
            SceneManager.LoadScene(sceneName += "1");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sheep")
        {
            _musicPlayer.PlaySheepEnterHoleSound();
            Debug.Log("gay af");
            hasBall = true;
            myRenderer.material = hasBallMat;

        }

        if (other.gameObject.tag == "Player" && hasBall)
        {
            //other.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1000, 0));
            //other.enabled = false;
            timer = 2f;
            myRenderer.material = hasPlayerMat;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "sheep")
        {
            _musicPlayer.PlaySheepExitHoleSound();
            hasBall = false;
            myRenderer.material = noBallMat;
        }
    }

    public void Begin()
    {
        startingPos = transform.position;
        _hasBegun = true;
    }
}
