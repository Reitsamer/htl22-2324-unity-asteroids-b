using UnityEngine;



[RequireComponent (typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public InitSound sounds;
    private AudioSource src;

    public void Start()
    {
        src = GetComponent<AudioSource>();  
        src.clip = sounds.IngameMusic_1;
        src.Play();
    }


    

}

