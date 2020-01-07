using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyExplosion;

    [SerializeField]
    private UIManager _uiManager;
    
    [SerializeField]
    private AudioClip _audioClip;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down  * _speed  * Time.deltaTime );  

        if(transform.position.y < -7.65)
        {
            float randomX = Random.Range(-15.2f, 15.2f);
            transform.position = new Vector3(randomX, 8.2f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
        }else if(other.tag == "Player")
        {
            PlayerReposition player = other.GetComponent<PlayerReposition>();

            if(player != null)
            {
                player.Damage();
            }
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uiManager != null)
        {
            _uiManager.UpdateScore();
        }

        Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
        Destroy(this.gameObject);
    }
}
