using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleManager : MonoBehaviour
{
    public Transform playerPos;
    public Transform enemyPos;
    public GameObject player;
    public GameObject enemy;
    public GameObject SongTrackPrefab;
    GameObject spawnedTrack;
    SongManager sm;
    Vector3 trackSpawnPosition = new Vector3(0.2870023f, 1.62f, 8f);
    PlayerBattler pb;
    CharacterBattler eb;
    BattleState state;
    GameObject gmo;
    GameManager gm;

    LevelLoader ll;
    public AudioSource songplayer;
    public AudioClip OverworldMusic;

    private bool canAttack = true;
    private bool isDefending = false;
    public bool RhythmGamePlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        gmo = GameObject.Find("GameManager");
        ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        sm = null;
        songplayer = GameObject.Find("SongPlayer").GetComponent<AudioSource>();
        
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        if(state == BattleState.PLAYERTURN && canAttack == true)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                canAttack = false;
                StartCoroutine(PlayerAttack());
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                canAttack = false;
                StartCoroutine(PlayerDefend());
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                canAttack = false;
                RhythmGamePlaying = true;
                Debug.Log("Shoudld Spawn");
                spawnedTrack = Instantiate(SongTrackPrefab, trackSpawnPosition, Quaternion.identity);
                sm = spawnedTrack.transform.GetChild(0).gameObject.GetComponent<SongManager>();
            }
        }

        if(RhythmGamePlaying && sm != null && sm.notesHit >= 5)
        {
            Destroy(spawnedTrack);
            RhythmGamePlaying = false;
            //Get all remaining notes in a list
            GameObject[] notesList = GameObject.FindGameObjectsWithTag("Note");

            //Destroy any remaining notes
            foreach (GameObject n in notesList)
            {
                Destroy(n);
            }


            //DO SOMETHING ELSE HERE, CALCULATE RHYTHM GAME DAMAGe
            StartCoroutine(PlayerAttack());
        }
    }

    IEnumerator SetupBattle()
	{
        gm = gmo.GetComponent<GameManager>();

		GameObject playerGO = Instantiate(player, playerPos);
		pb = playerGO.GetComponent<PlayerBattler>();
        pb.innit(gm.PlayerName, gm.PlayerMaxHp, gm.PlayerCurrentHP, gm.PlayerSpeed, gm.PlayerDefense, gm.PlayerAttackPower);

		GameObject enemyGO = Instantiate(enemy, enemyPos);
		eb = enemyGO.GetComponent<CharacterBattler>();
        eb.innit("Slime",10,10,2f,0,3);

        Debug.Log("Battle Against the " + eb.Name + " has begun!");

		yield return new WaitForSeconds(2f);
        Debug.Log("What will you do?");
		state = BattleState.PLAYERTURN;
	}

    IEnumerator PlayerAttack()
    {
        bool isDead = eb.TakeDamage(pb.AttackPower);

        Debug.Log(eb.Name + " took " + pb.AttackPower + " Damage");
        yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			StartCoroutine(EndBattle());
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}

        canAttack = true;
    }

    IEnumerator PlayerDefend() 
    {
        isDefending = true;
        Debug.Log("You Brace for an Attack!");
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
        canAttack = true;
    }

    IEnumerator EnemyTurn()
    {
        bool isDead;
        int totalDamage;
        Debug.Log(eb.Name + " Attacks!");
        yield return new WaitForSeconds(1f);

        if(isDefending)
        {
            totalDamage = eb.AttackPower - pb.Defense;
            Debug.Log(totalDamage);
            isDead = pb.TakeDamage(totalDamage);
        }
        else
        {
            totalDamage = eb.AttackPower;
            isDead = pb.TakeDamage(totalDamage);
        }
        
        Debug.Log("You took "+ totalDamage + " damage");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("You now have " + pb.CurrentHP + " health.");

        if(isDead)
		{
			state = BattleState.LOST;
			StartCoroutine(EndBattle());
		} else
		{
			state = BattleState.PLAYERTURN;
			Debug.Log("What will you do?");
		}
        isDefending = false;
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
		{
            gm.saveStats(pb);
            gm.enemiesKilled += 1;
            Debug.Log("Battle Won!");
			yield return new WaitForSeconds(1f);
            songplayer.clip = OverworldMusic;
            songplayer.Play();
            StartCoroutine(ll.LoadLevel(gm.scenetoload));
            //SceneManager.LoadScene(gm.scenetoload);

		} else if (state == BattleState.LOST)
		{
			Debug.Log("Battle Lost...");
            yield return new WaitForSeconds(1f);
            StartCoroutine(ll.LoadLevel(gm.scenetoload));
            //SceneManager.LoadScene(gm.scenetoload);
		}
    }
}
