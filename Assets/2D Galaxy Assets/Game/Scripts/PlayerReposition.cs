using UnityEngine;
using System.Collections;

public class PlayerReposition : MonoBehaviour
{   

    public bool canTripleShot = false;

    public bool canSpeedUp = false;

    public bool canShield = false;

    public int lifes = 3;

    public int damageShield = 3;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private GameObject _laserPrefab; 

    [SerializeField]
    private GameObject _tripleShot;

    [SerializeField]
    private float _speed = 10.0f;

    [SerializeField]
    private GameObject[] _engines;

    
    private UIManager _uiManager;

    private GameManager _gameManager;

    private SpawnManager _spawnManager;

    private AudioSource _audioSource;
    
    private float _fireRate = 0.5f;

    private float _canFire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0 , 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uiManager != null)
        {
            _uiManager.UpdateLives(lifes);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }

        _audioSource = GetComponent<AudioSource>();
    }    

    // Update is called once per frame
    void Update()
    {
        Movement();


        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        if(Time.time  > _canFire)
            {
                _audioSource.Play();
                if(canTripleShot)
                {
                     Instantiate(_tripleShot, transform.position, Quaternion.identity);
                }else{
                     Instantiate( _laserPrefab, transform.position + new Vector3(0, 0.98f, 0), Quaternion.identity);
                     _canFire = Time.time + _fireRate;
                }
                
            }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        if(canSpeedUp)
        {
            transform.Translate(Vector3.right  * (2 * _speed) * horizontalInput * Time.deltaTime );  
            transform.Translate(Vector3.up * (2 * _speed) * verticalInput * Time.deltaTime);

        }else
        {

            transform.Translate(Vector3.right  * _speed * horizontalInput * Time.deltaTime );  
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);  

        }
        

        if(transform.position.y > 5.659275f)
        {

            transform.position = new Vector3(transform.position.x, 5.659275f, 0);

        }else if(transform.position.y < -5.774934)
        {

            transform.position = new Vector3(transform.position.x, -5.774934f, 0);

        }

        if(transform.position.x > 15.49685)
        {
             transform.position = new Vector3(15.49685f, transform.position.y, 0);
        }else if(transform.position.x < -15.37499)
        {
             transform.position = new Vector3(-15.37499f, transform.position.y, 0);
        }
    }


    public void SpeedBoostOn()
    {
        canSpeedUp = true;
        StartCoroutine(SpeedBoostCooldown());
    }

    public IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        canSpeedUp = false;
    }
    public void TripleShotOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotCooldown());
    }

    public IEnumerator TripleShotCooldown()
    {
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));

        canTripleShot = false;
    }

    public void ShieldOn()
    {
        damageShield = 3;
        canShield = true;
        _shieldGameObject.SetActive(true);
    }

    public void Damage()
    {
        if(canShield == true)
        {
            damageShield--;

            if(damageShield < 1)
            {
                canShield = false;
                _shieldGameObject.SetActive(false);
            }
        }else
        {
             lifes--;
            
             if(lifes == 2)
             {
                 int dmg = Random.Range(0, 1);
                 _engines[dmg].SetActive(true);
             }

             
             if(lifes == 1)
             {
                 if(_engines[0].activeSelf)
                 {
                     _engines[1].SetActive(true);
                 }else
                 {
                     _engines[0].SetActive(true);
                 }
             }


             _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

            if(_uiManager != null)
            {
                _uiManager.UpdateLives(lifes);
            }

            if(lifes < 1)
            {
                _gameManager.gameOver = true;
                _uiManager.ShowTitle(_gameManager.gameOver);
                 Instantiate(_explosion, transform.position, Quaternion.identity);
                 Destroy(this.gameObject);
            }
        }
       
    }
}
