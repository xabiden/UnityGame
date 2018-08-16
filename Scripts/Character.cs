using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : Unit {

    [SerializeField] // атрибут для отображение последующего поля в инспекторе
    private int lives = 5; //количество жизней

    public int Lives
    {
        get { return lives; }
        set {
                if (value < 6)
            {
                lives = value;
            }
                    
            livesBar.Refresh();

               
            }
    }



    private AudioClip shoot;

    private AudioClip jump;

    private LivesBar livesBar;

    [SerializeField]
    private float speed = 3.0F; //скорость

    [SerializeField]
    private float jumpForce = 4.0F; // сила прыжка

    private bool isGrounded = false;

    private Bullet bullet;

    public GameObject menu;
    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite; //ссылки на компоненты
   

    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>(); //находит livesBar на сцене
        rigidbody = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>(); // получение ссылок

        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot();
        if (isGrounded) State = CharState.Idle;
        if (Input.GetButton("Horizontal")) Run(); //если нажата клавиша типа горизонтальной выываем метод Run
        if (Input.GetButton("Jump") && isGrounded == true) Jump();  //если нажата клавиша типа прыжка выываем метод Jump
        if (transform.position.y < -10)
        {
            menu.SetActive(true);
            Die(); //если координата меньше -10, то умираем
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal"); //в заивисмости от нажатой клавиши Input.GetAxis получает -1 или 1
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        //MoveTowards(откуда, куда (тек.положение+ направление),какое расстояние нужно пройти за один кадр(скорость на время между кадрами))

        sprite.flipX = direction.x < 0.0F;

        if (isGrounded) State = CharState.Run;
    }
    private void Jump()
    {
        GetComponent<AudioSource>().Play();
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        // приложить силу(вектор силы(напрвление * длину), тип силы(импульс))
        State = CharState.Jump;

    }

    private void Shoot()
    {
        GetComponent<AudioSource>().Play();
        Vector3 position = transform.position;
        position.y += 0.8F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);

        newBullet.Parent = gameObject; 
    }

    public override void ReceiveDamage()
    {
        Lives--;
        

      
        
       
        if (Lives == 0)// если нет жизней 
        {

           // State = CharState.Die;
            Die(); 
            menu.SetActive(true);
            
        }

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 5.0F, ForceMode2D.Impulse);
        
    }
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Unit unit = collision.gameObject.GetComponent<Unit>();
        if (unit)
        {
           
            ReceiveDamage();
        }
      
        if (collision.gameObject.tag == "EndLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //загружаем следующий уровень
        }

    }
    
}

public enum CharState
{
    Idle, //0
    Run, //1
    Jump ,//2
    //Die //3
}