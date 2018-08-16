using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private GameObject parent;
    public GameObject Parent { set { parent = value; }  get { return parent; } } // поле для записи 
    private float speed = 10.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }
    
    public Color Color
    {
        set { sprite.color = value; }
      
    }



    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 1.4F); //после создания уничтожится через 1.4 сек
       
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        
        if(unit && unit.gameObject != parent) //уничтожимся, если впечатаемся в кого-нибудь
        {
           if (!(unit is Monster )) unit.ReceiveDamage();// 
            Destroy(gameObject);
        }
        
    }
}
