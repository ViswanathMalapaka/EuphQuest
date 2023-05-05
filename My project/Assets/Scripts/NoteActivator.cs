using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteActivator : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sr;
    public SongManager sm;
    public Color defaultColor;
    public Color activatedColor;
    public KeyCode key;
    private GameObject note;
    private bool active;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = defaultColor;
        note = null;
        active = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            sr.color = activatedColor;
            if(active && note != null)
            {
                Destroy(note);
                sm.notesHit++;
            }
        }
        if(Input.GetKeyUp(key))
        {
            sr.color = defaultColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Note")
        {
            //Debug.Log("Entered");
            active = true;
            note = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Exited");
        active = false;
        note = null;
    }

    // IEnumerator Pressed(){
    //     sr.color = Color.black;
    //     yield return new WaitForSeconds(0.05f);
    //     sr.color = Color.red;
    // }
}
