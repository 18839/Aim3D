using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnSystem : MonoBehaviour {
    [SerializeField]
    private Wave[] _waves;//ammount of waves
    [SerializeField]
    private Enemy _enemy;//import enemy class
    [SerializeField]
    private float _maxTimeBetweenWaves;//the max ammount of time you get between waves
    private float _maxTimeCounter;//actual vallue that counts down to 0


    private Wave _currentWave;//has the info from current wave

    private float _nextSpawnTime;//for checking if if it is time for the next enemy to spawn

    private bool _waveDone;//checks if wave is done

    private int _currentWaveNum;//which number the current wave is
    private int _enemiesToSpawn;//how much are yet to spawn, if 0 then no enemies wil be spawned for this wave
    private int _enemiesAlive;//the ammount of enemies that are still alive(includes enemies yet to be spawned)
    public int _percentHP;//percentage hp wat er bij komt
    private int _percentSpeed;//percentage movementspeed wat er bij komt

    private GameObject _timerText;//find timer object
    private Vector3 _textPos;

    void Awake()
    {
        NextWave();
        _timerText = GameObject.Find("TimeText");
    }

    void Update()
    {
        spawnCheck();
        _timerText.GetComponent<Text>().text = "Time until next wave: " + (Mathf.Round(_maxTimeCounter)-1);
        
        if (Mathf.Round(_maxTimeCounter) <= 0)
        {
            _timerText.SetActive(false);
        }
        else
        {
            _timerText.SetActive(true);
        }
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
        if (_waveDone == true)
        {
            _maxTimeCounter -= Time.deltaTime;
            Debug.Log(Mathf.Round(_maxTimeCounter));
            if (_maxTimeCounter <= 0)
            {
                NextWave();
                _waveDone = false;
            }
        }
    }

    void OnEnemyDeath()//decreases the int enemies alive and checks if next wave can start
    {
        print("i dieded");
        _enemiesAlive--;
        if(_enemiesAlive == 0)
        {
            _waveDone = true;
            _maxTimeCounter = _maxTimeBetweenWaves;
        }
    }

    void NextWave()//ads 1 to the wave number and sets the currentwave to a new wave and sets the value's given by the new wave  
    {
        _currentWaveNum++;
        if (_currentWaveNum - 1 < _waves.Length)
        {
            _currentWave = _waves[_currentWaveNum - 1];

            _enemiesToSpawn = _currentWave.enemyCount;
            _enemiesAlive = _enemiesToSpawn;
            _percentHP = _currentWave.addedPercentageHealth;
            _percentSpeed = _currentWave.addedPercentageMovespeed; 
            
        }
    }
    [System.Serializable]
    public class Wave//class for edditing the waves
    {
        public int enemyCount;//how much enemies in the wave
        public float spawnTime;//how much time inbetween enemy spawns
        public int addedPercentageHealth;//each ennemy has a static(not yet static this version) base health that wont be changed, to make each wave harder the health that the enemy starts with is baseHealth + (basehealth/100 * addedPercentageHealth). 
        public int addedPercentageMovespeed;//each ennemy has a static(not yet static this version) base movespeed that wont be changed, to make each wave harder the speed that the enemy starts with is baseHealth + (basespeed/100 * addedPercentageMovespeed).
        //why percentage? if i add +2 hp each round it makes later on the difrense between tanks and other units insignificant, round 1 tank 10hp speedster 3hp, round 15 tank 40hp speedster 33hp now we have a tanky speedster wich is op or a weak tank. percentage fixes that and same goes for th speed.
    }
}
