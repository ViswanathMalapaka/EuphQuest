using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;        //Public variable to store a reference to the player game object
    public GameManager gm;
    public Vector3 offset;            //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start () 
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = new Vector3(0, 0.4f, -0.69f);

        transform.position = new Vector3(gm.playerPosition.x + offset.x, gm.playerPosition.y + offset.y, gm.playerPosition.z + offset.z);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
    }
}