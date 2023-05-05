using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongPuzzleManager : MonoBehaviour
{
    public GameObject barrels;
    public SoundButton s1;
    public SoundButton s2;
    public SoundButton s3;
    public ArrayList pressedOrder = new ArrayList();

    // Update is called once per frame
    void Update()
    {
        if (pressedOrder.Count == 4)
        {
            if ((int)pressedOrder[0] == 1 && (int)pressedOrder[1] == 2 && (int)pressedOrder[2] == 3 && (int)pressedOrder[3] == 2)
            {
                Debug.Log("Some barells have broke!");
                Destroy(barrels);
            }
            else
            {
                Debug.Log("Incorrect Pattern");
                pressedOrder.Clear();
            }
        }
    } 

}

