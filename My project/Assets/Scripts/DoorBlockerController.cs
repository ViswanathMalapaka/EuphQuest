using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlockerController : MonoBehaviour
{
    public GameManager gm;
    public EnvironmentManager em;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        em = GameObject.Find("EvironmentManager").GetComponent<EnvironmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(gm.enemiesKilled >= em.enemiesToKill)
        {
            Debug.Log("The door has opened");
            Destroy(gameObject);
        }
    }
}
