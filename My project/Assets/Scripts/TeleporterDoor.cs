using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterDoor : MonoBehaviour
{
    public TeleporterDoor otherDoor;

    [SerializeField]
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = otherDoor.transform.position + offset;
        }
    }
}
