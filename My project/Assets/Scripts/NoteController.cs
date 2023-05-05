using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public int lane;
    public Vector3 SpawnPos, RemovePos;

    public Vector3 offset = new Vector3(0,-3,0);
    public float BeatsShownInAdvance;
    public float beatOfThisNote;
    public float songPosInBeats;
    public float bpm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position = Vector3.MoveTowards(transform.position, RemovePos + offset, (Vector3.Distance(SpawnPos,RemovePos)/(BeatsShownInAdvance*450)));

        if(transform.position == RemovePos + offset)
        {
            Debug.Log("Missed");
            Destroy(gameObject);
        }
    }
}
