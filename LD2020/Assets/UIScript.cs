using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public GameObject player;
    public MusicPlayer musicPlayer;
    public GameObject muteButton;
    public GameObject unmuteButton;
    public GameObject muteSfxButton;
    public GameObject unmuteSfxButton;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GameObject.FindWithTag("musicPlayer").GetComponent<MusicPlayer>();
        if (musicPlayer._musicMuted)
        {
            muteButton.SetActive(false);
            unmuteButton.SetActive(true);
        } else
        {
            unmuteButton.SetActive(false);
            muteButton.SetActive(true);
        }

        if (musicPlayer._sfxMuted)
        {
            muteSfxButton.SetActive(false);
            unmuteSfxButton.SetActive(true);
        } else
        {
            unmuteSfxButton.SetActive(false);
            muteSfxButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLevel()
    {
        player.GetComponent<DeathScript>().Die();
    }

    public void MuteMusic()
    {
        musicPlayer.MuteMusic();
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
    }

    public void MuteSfx()
    {
        musicPlayer.MuteSfx();
        muteSfxButton.SetActive(false);
        unmuteSfxButton.SetActive(true);
    }

    public void UnMuteMusic()
    {
        musicPlayer.UnMuteMusic();
        unmuteButton.SetActive(false);
        muteButton.SetActive(true);
    }

    public void UnMuteSfx()
    {
        musicPlayer.UnMuteSfx();
        unmuteSfxButton.SetActive(false);
        muteSfxButton.SetActive(true);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }
}
