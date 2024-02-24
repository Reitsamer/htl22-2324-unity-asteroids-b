
using UnityEngine;

[CreateAssetMenu(fileName = "SoundManager", menuName = "Sounds/editedSounds")]
public class InitSound : ScriptableObject
{
    [SerializeField] AudioClip ingameMusic_1 = null;
    public AudioClip IngameMusic_1 { get { return ingameMusic_1; } }


    [SerializeField] AudioClip shoot = null;
    public AudioClip Shoot { get { return shoot; } }

}