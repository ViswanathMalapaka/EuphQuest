using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestTransitionCubeController : MonoBehaviour
{
    public GameManager gm;
    public EnvironmentManager em;
    public LevelLoader ll;

    //public int[] stagesList = {0,2,3};
    public int[] stagesList = {0,2,3};
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        em = GameObject.Find("EvironmentManager").GetComponent<EnvironmentManager>();
        ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && gm.enemiesKilled >= em.enemiesToKill)
        {
            int scene = Random.Range(0, stagesList.Length);
            Debug.Log(stagesList[scene]);
            StartCoroutine(ll.LoadLevel(stagesList[scene]));
            //SceneManager.LoadScene(stagesList[scene]);
            
        }
    }
}
