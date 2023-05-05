using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public string PlayerName = "Hero";
    public int PlayerMaxHp = 500;
    public int PlayerCurrentHP = 500;
    public float PlayerSpeed = 3f;
    public int PlayerDefense = 1;
    public int PlayerAttackPower = 10;
    public int enemiesKilled;
    public Vector3 playerPosition;
    public int prevScene = 0;
    public int scenetoload;
    public static GameManager instance;

    void Awake()
    {
         if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void saveStats(PlayerBattler pb){
        PlayerName = pb.Name;
        PlayerMaxHp = pb.MaxHp;
        PlayerCurrentHP = pb.CurrentHP;
        PlayerSpeed = pb.Speed;
        PlayerDefense = pb.Defense;
        PlayerAttackPower = pb.AttackPower;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex != 1 && prevScene != 1)
        {
            GameObject playerObject = GameObject.Find("player");
            if (playerObject != null)
            {
                playerPosition = playerObject.transform.position;
            }
        }
        prevScene = scene.buildIndex;
    }
}
