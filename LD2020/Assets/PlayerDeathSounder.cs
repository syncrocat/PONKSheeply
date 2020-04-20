using UnityEngine;

public class PlayerDeathSounder : MonoBehaviour
{
    private MusicPlayer _musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _musicPlayer = GameObject.FindWithTag("musicPlayer").GetComponent<MusicPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        _musicPlayer.PlayPlayerDeathSound(Random.Range(0, 5));
    }
}
