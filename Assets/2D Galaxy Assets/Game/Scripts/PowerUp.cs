using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update

    private float _speed = 3.0f;

    [SerializeField]
    private int powerUpId;

    [SerializeField]
    private AudioClip _audioClip;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
      
          if(transform.position.y < -7.37)
          {
            Destroy(this.gameObject);
          }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
        if(other.tag == "Player")
        {
            PlayerReposition player = other.GetComponent<PlayerReposition>();

            if(player != null)
            {
                
              if(powerUpId == 0)
              {

                player.TripleShotOn();

              }else if(powerUpId == 1)
              {
                  
                player.SpeedBoostOn();

              }else if(powerUpId == 2)
              {
                player.ShieldOn();
              }  
            }
            
            Destroy(this.gameObject);
        }
        
    }

    
}
