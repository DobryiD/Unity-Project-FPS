using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public int MaxWaves = 5;
    public float spawnDelay;
    public Text task;
    public EnemySpawn[] enemySpawn;
    public bool firstWaveStarted = false;

    [SerializeField]
    private PickingConsumables[] consumables;
    public GameObject triggerZombie;
    [SerializeField]
    private int currentNumberZombies;
    [SerializeField]
    private int maxNumberZombiesForWave;
    [SerializeField]
    private Transform[] spawnPlaceZombie;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private Transform[] spawnPlaceBoss;

    private float timerRest;
    
    private bool waveActive = false;
    private EnemySpawn chosenEnemy;
    private float random;
    private int currentWave;
    private float cumulative;
    private int timeBetweenWaves=25;
    private PlayerHealth playerHealth;
    private SceneChanger changer;

    void Awake()
    {
        Cursor.visible = false;
        currentWave = 0;
        timerRest = timeBetweenWaves;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        changer = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneChanger>();
    }

    void Update()
    {
        if ((currentWave == MaxWaves && currentNumberZombies <= 0)||(playerHealth.GetCurrentHealth()<=0)) {
            if (changer != null)
            {
                StartCoroutine(FinishingTheGame());
            }
            return;
        }

        if (timerRest <= 0 && playerHealth.GetCurrentHealth() > 0)
        {
            StartCoroutine(Spawning());

        }
        else if (firstWaveStarted&&!waveActive)
        {
            timerRest -= Time.deltaTime;
        }
        else if (waveActive && currentNumberZombies <= 0)
        {
            waveActive = false;
            maxNumberZombiesForWave +=  maxNumberZombiesForWave / 4;
            for (int i = 0; i < consumables.Length; i++)
            {
                if (!consumables[i].isActive()) {
                    consumables[i].Spawned();
                }
            }
        }
        
    }
    public void StartFirstWave() {

        task.text = "Survive !";
        triggerZombie.GetComponent<Animator>().SetTrigger("WakeUp");
        firstWaveStarted = true;
        StartCoroutine(Spawning());
        gameObject.GetComponent<BoxCollider>().enabled = false;

    }
    IEnumerator FinishingTheGame() {
        yield return new WaitForSeconds(7f);
        changer.Fade("MainMenu");
        Cursor.visible = true;
        yield return null;
    }
    IEnumerator Spawning()
    {
        int numberOfSpawnedEnemies=0;
                waveActive = true;
                timerRest = timeBetweenWaves;
                currentNumberZombies = maxNumberZombiesForWave;
                ++currentWave;
                if (currentWave == MaxWaves)
                {
                    SpawnBoss(ref numberOfSpawnedEnemies);
                }
                else
                {
                    int temp = maxNumberZombiesForWave / 4;
                    while (numberOfSpawnedEnemies < maxNumberZombiesForWave)
                    {
                        for (int i = 0; i <  temp; i++)
                        {
                            SpawnRandomEnemy(ref numberOfSpawnedEnemies);
                        }
                        yield return new WaitForSeconds(spawnDelay);
                    }
                    
                }
        yield return null;
            
    }
    public bool isWaveActive() {
        return waveActive;
    }
    public float GetRestingTime() {
        return timerRest;
    }
    public void EnemyDies() {
        --currentNumberZombies;
    }

    public int GetNumberZombie() {
        return currentNumberZombies;
    }
    public int GetWave() {
        return currentWave;
    }

    void SpawnBoss(ref int numberOfZombies) {
        Instantiate(boss, spawnPlaceBoss[Random.Range(0, spawnPlaceBoss.Length)].position,
            spawnPlaceBoss[Random.Range(0, spawnPlaceBoss.Length)].rotation);
        ++numberOfZombies;
        currentNumberZombies = 1;
    }

    void SpawnRandomEnemy(ref int numberOfZombies)
    {
        random = Random.value;
        cumulative = 0f;
        for (int i = 0; i < enemySpawn.Length; i++)
        {
            cumulative += enemySpawn[i].chance;
            if (random < cumulative && currentWave >= enemySpawn[i].wave)
            {
                chosenEnemy = enemySpawn[i];
                break;
            }
        }
        Transform tempPos = spawnPlaceZombie[Random.Range(0, spawnPlaceZombie.Length)];
        Instantiate(chosenEnemy.prefab[Random.Range(0, chosenEnemy.prefab.Length)], tempPos.position,
            tempPos.rotation);
        ++numberOfZombies;

    }

}
