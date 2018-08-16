using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Monster {

    [SerializeField]
    private float rate = 2.0F; //частота стрельбы

    [SerializeField]
    private Color bulletColor = Color.red;

    private int lives = 3;
    public int Lives
    {
        get { return lives; }
        set
        {
            if (value < 6)
            {
                lives = value;
            }

            


        }
    }

    new private Rigidbody2D rigidbody;

    private Bullet bullet;

    protected override void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        bullet = Resources.Load<Bullet>("Bullet"); // загружаем в экземпляр наш префаб из папки Bullet (получаем ссылку)
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate ); // повторяем Shoot , с задержкой rate и через каждые rate секунд
    }

    private void Shoot() //логика стрельбы 
    {
        Vector3 position = transform.position; // создаём позицию, придаём ей начальную позицию монстра
        position.y += 0.5F; // т.к. точка отсчёта монстра находится снизу спрайта, то добавим половину юнита по y

        for (int i = 0; i <= 2; i++)
        {
            position.y += 0.6F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet; //создаём клона

            newBullet.Parent = gameObject; // пулю пускает монстр
            newBullet.Direction = -newBullet.transform.right; // влево смотрит , поэтому - right
            newBullet.Color = bulletColor;
        }
        
    }
    public override void ReceiveDamage()
    {
        Lives--;





        if (Lives == 0)// если нет жизней 
        {
            Die();
        }

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 5.0F, ForceMode2D.Impulse);

    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        
      
        Bullet bullet = collider.GetComponent<Bullet>();
        Character character = collider.GetComponent<Character>();
   
       
        if (character)
        {
            character.ReceiveDamage();
        }
        if (bullet && bullet.Parent != gameObject ) 
        {
           
                ReceiveDamage();
           
        }
    }
}
