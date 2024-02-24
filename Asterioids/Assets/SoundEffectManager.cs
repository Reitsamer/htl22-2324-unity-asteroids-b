using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    public InitSound sounds;
    private AudioSource src;

    public void Start()
    {
        src = GetComponent<AudioSource>();
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            src.clip = sounds.Shoot;
            src.Play();
        }
    }

}

