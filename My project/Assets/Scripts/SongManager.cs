using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    //the current position of the song (in seconds)
    public float songPosition;

    //the current position of the song (in beats)
    public float songPosInBeats;

    //the duration of a beat
    public float secPerBeat;

    //how much time (in seconds) has passed since the song started
    public float dsptimesong;

    public float timesinceplayed;

    //beats per minute of a song
    public float bpm;

    //keep all the position-in-beats of notes in the song
    float[] notes = {5f,5.5f,6f,6.5f,7f,7.5f,8f,8.5f,9f,9.5f,10f,10.5f,11f,11.5f,12,12.5f};
    int[] lanes = {1,2,3,2,1,2,3,2,1,2,3,2,1,2,3,2};

    //the index of the next note to be spawned
    int nextIndex = 0;
    public float beatsShownInAdvance;
    public AudioSource audiosource;
    public AudioClip clip;
    GameObject nao1;
    NoteActivator na1;
    GameObject nao2;
    NoteActivator na2;
    GameObject nao3;
    NoteActivator na3;
    public GameObject notePrefab;
    public int notesHit;

    
    
    // Start is called before the first frame update
    void Start()
    {
        notesHit = 0;

        secPerBeat = 60f/bpm;

        dsptimesong = (float) AudioSettings.dspTime;

        audiosource = GetComponent<AudioSource>();

        nao1 = GameObject.Find("Activator1");

        na1 = nao1.GetComponent<NoteActivator>();

        nao2 = GameObject.Find("Activator2");

        na2 = nao2.GetComponent<NoteActivator>();

        nao3 = GameObject.Find("Activator3");

        na3 = nao3.GetComponent<NoteActivator>();

        timesinceplayed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);

        songPosInBeats = songPosition / secPerBeat;

        if (nextIndex < notes.Length && notes[nextIndex] < songPosInBeats + beatsShownInAdvance)
        {
            Vector3 lanePosition;
        
            if(lanes[nextIndex] == 1)
            {
                lanePosition = nao1.transform.position;
            }
            else if(lanes[nextIndex] == 2)
            {
                lanePosition = nao2.transform.position;
            }
            else
            {
                lanePosition = nao3.transform.position;
            }
            GameObject newNote = Instantiate(notePrefab, lanePosition + new Vector3(0,5,0), Quaternion.identity);
            //Set up note info
            NoteController newNoteController = newNote.GetComponent<NoteController>();
            
            newNoteController.SpawnPos = lanePosition + new Vector3(0,5,0);
            newNoteController.RemovePos = lanePosition;
            newNoteController.BeatsShownInAdvance = beatsShownInAdvance;
            newNoteController.beatOfThisNote = notes[nextIndex];
            newNoteController.songPosInBeats = songPosInBeats;
            newNoteController.bpm = bpm;

            nextIndex++;
        }

        if(Time.time - timesinceplayed >= secPerBeat)
        {
            audiosource.PlayOneShot(clip);
            //Debug.Log(songPosInBeats);
            timesinceplayed = Time.time;
        }
    }
}
