using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public AudioSource aus;
    public AudioClip aud;

    public SongPuzzleManager spm;
    [SerializeField]
    public int output;
    // Start is called before the first frame update
    void Start()
    {
        aus = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            aus.PlayOneShot(aud);
            spm.pressedOrder.Add(output);
        }
    }
}
