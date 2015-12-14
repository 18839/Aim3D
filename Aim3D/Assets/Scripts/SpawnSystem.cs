using UnityEngine;
using System.Collections;

public class SpawnSystem : MonoBehaviour {
    [SerializeField]
    private Wave[] _waves;//ammount of waves
    [SerializeField]
    private Enemy _enemy;//import enemy class

    private Wave _currentWave;//has the info from current wave
    private int _currentWaveNum;//which number the current wave is

    private int _enemiesToSpawn;//how much are yet to spawn, if 0 then no enemies wil be spawned for this wave
    private int _enemiesAlive;//the ammount of enemies that are still alive(includes enemies yet to be spawned)
    private float _nextSpawnTime;//for checking if if it is time for the next enemy to spawn

    void Start()
    {
        NextWave();
    }

    void Update()
    {
        spawnCheck();
    }

    void spawnCheck()
    {
        if (_enemiesToSpawn > 0 && Time.time > _nextSpawnTime)//checks if there are enemies left to spawn this wave and if it is time to spawn a enemy, if it executes it ajusts the amount of enemies that need to spawn, resets the timer and spawns a enemy
        {
            _enemiesToSpawn--;
            _nextSpawnTime = Time.time + _currentWave.spawnTime;

            Enemy spawnedEnemy = Instantiate(_enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;//when ondeath is called, onenemydeath wil be called to
        }
    }

    void OnEnemyDeath()//decreases the int enemies alive and checks if next wave can start
    {
        print("i dieded");
        _enemiesAlive--;
        if(_enemiesAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()//ads 1 to the wave number and sets the currentwave to a new wave and sets the ammount of enemies to spawn to the amouhnt given by the new wave  
    {
        _currentWaveNum++;
        if (_currentWaveNum - 1 < _waves.Length)
        {
            _currentWave = _waves[_currentWaveNum - 1];

            _enemiesToSpawn = _currentWave.enemyCount;
            _enemiesAlive = _enemiesToSpawn;
        }
    }
    [System.Serializable]
    public class Wave//class for edditing the waves
    {
        public int enemyCount;//how much enemies in the wave
        public float spawnTime;//how much time inbetween enemy spawns
    }
}
