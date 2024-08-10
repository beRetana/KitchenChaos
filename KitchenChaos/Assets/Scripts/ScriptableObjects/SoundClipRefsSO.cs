using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundClipRefsSO : ScriptableObject{

    public AudioClip[] chop;
    public AudioClip[] deliveryFailed;
    public AudioClip[] deliverySucess;
    public AudioClip[] footStep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
