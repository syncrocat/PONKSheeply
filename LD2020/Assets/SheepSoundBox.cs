using UnityEngine;

public class SheepSoundBox : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        _musicPlayer.PlaySheepBounceSound(0);
        /*bounceSound.PlayOneShot(bounceSound.clip);*/
    }

    public void PlayDeathSound()
    {
        _musicPlayer.PlaySheepDeathSound(Random.Range(0, 3));
    }
}
