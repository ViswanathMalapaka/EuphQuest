using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyInformation
    {
        public float movementSpeed;
        public float playerDetectionRadius;
        public float restrictedRadius;
        public int ShouldSpawn;
        public float spawnpointX;
        public float spawnpointY;
        public float spawnpointZ;
        public float restrictedPointX;
        public float restrictedPointY;
        public float restrictedPointZ;
        public int posInList;
    }

    [System.Serializable]
    public class EnemyList
    {
        public EnemyInformation[] enemies;
    }

    public TextHolder enemiesJson;
    public EnemyList enemyList = new EnemyList();
    public EnemyController e;
    public bool hasSpawned = false;
    public static EnvironmentManager instance;
    public GameManager gm;
    int spawnNum;
    public int curScene;
    public int enemiesToKill;

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
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        InitialSpawnEnemies();
        curScene = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(curScene != 0 && curScene != 1)
            {
                InitialSpawnEnemies();
                curScene = 0;
            }
            else if (curScene != 0)
            {
                SpawnAliveEnemeis();
                curScene = 0;
            }
        }

        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            curScene = SceneManager.GetActiveScene().buildIndex;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int thisScene = scene.buildIndex;

        if(thisScene != 1)
        {
            if(curScene != 1)
            {
                InitialSpawnEnemies();
                curScene = thisScene;
            }
            else
            {
                SpawnAliveEnemeis();
                curScene = thisScene;
            }
        }

        else
        {
            curScene = thisScene;
        }
    }

    void InitialSpawnEnemies()
    {
        gm.enemiesKilled = 0;
        enemiesJson = GameObject.Find("EnemyList").GetComponent<TextHolder>();
        enemyList = JsonUtility.FromJson<EnemyList>(enemiesJson.textFile.text);

        spawnNum = Random.Range(1, enemyList.enemies.Length + 1);
        enemiesToKill = spawnNum;

        for(int i = 0; i < spawnNum; i++)
        {
            Debug.Log("Spawned");
            e.spawnpoint = new Vector3(enemyList.enemies[i].spawnpointX,enemyList.enemies[i].spawnpointY,enemyList.enemies[i].spawnpointZ);
            e.restrictedPoint = new Vector3(enemyList.enemies[i].restrictedPointX,enemyList.enemies[i].restrictedPointY,enemyList.enemies[i].restrictedPointZ);
            e.movementSpeed = enemyList.enemies[i].movementSpeed;
            e.playerDetectionRadius = enemyList.enemies[i].playerDetectionRadius;
            e.restrictedRadius = enemyList.enemies[i].restrictedRadius;
            e.posInList = enemyList.enemies[i].posInList;

            Instantiate(e, e.spawnpoint, Quaternion.identity);
        }

    }

    void SpawnAliveEnemeis()
    {
        for(int i = 0; i < spawnNum; i++)
        {
            if(enemyList.enemies[i].ShouldSpawn == 1)
            {
                Debug.Log("Spawned");
                e.spawnpoint = new Vector3(enemyList.enemies[i].spawnpointX,enemyList.enemies[i].spawnpointY,enemyList.enemies[i].spawnpointZ);
                e.restrictedPoint = new Vector3(enemyList.enemies[i].restrictedPointX,enemyList.enemies[i].restrictedPointY,enemyList.enemies[i].restrictedPointZ);
                e.movementSpeed = enemyList.enemies[i].movementSpeed;
                e.playerDetectionRadius = enemyList.enemies[i].playerDetectionRadius;
                e.restrictedRadius = enemyList.enemies[i].restrictedRadius;
                e.posInList = enemyList.enemies[i].posInList;

                Instantiate(e, e.spawnpoint, Quaternion.identity);
            }
            
        }
    }
}
