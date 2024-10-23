using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerBell : MonoBehaviour, IShootable
{
    [SerializeField] private FriedEggManager FriedEggManager;
    public SFXList sfxList;
    private AudioSource audioSource;
    private AudioClip selectedClip;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void TakeShot(Vector3 knockbackForce)
    {
        PlayClip();
        FriedEggManager.CompleteTask(2);
    }
    private void PlayClip(){
        if (sfxList != null  && sfxList.sfxClips.Length > 0 )
        {
            int randomIndex = Random.Range(0, sfxList.sfxClips.Length);
            selectedClip = sfxList.sfxClips[randomIndex];
        }
        if (audioSource != null)
        {
            audioSource.PlayOneShot(selectedClip);
        }
        else
        {
            Debug.LogWarning("AudioSource component is missing on " + gameObject.name);
        }
    }
}
