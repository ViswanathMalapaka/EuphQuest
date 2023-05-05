using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gm.scenetoload == 3)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
