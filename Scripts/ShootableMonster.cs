using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monster {

    [SerializeField]
    private float rate = 2.0F; //частота стрельбы

    [SerializeField]
    private Color bulletColor = Color.white;
    private Bullet bullet;

    protected override void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet"); // загружаем в экземпляр наш префаб из папки Bullet (получаем ссылку)
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate); // повторяем Shoot , с задержкой rate и через каждые rate секунд
    }

    private void Shoot() //логика стрельбы 
    {
        Vector3 position = transform.position; // создаём позицию, придаём ей начальную позицию монстра
        position.y += 0.5F; // т.к. точка отсчёта монстра находится снизу спрайта, то добавим половину юнита по y
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet; //создаём клона

        newBullet.Parent = gameObject; // пулю пускает монстр
        newBullet.Direction = - newBullet.transform.right; //цветок влево смотрит , поэтому - right
        newBullet.Color = bulletColor; 
    }

    protected override void OnTriggerEnter2D(Collider2D collider) //переопределяем, чтоб не умирал, когда выстрелит
    {
        //как у двигающегося монстра реализация (умирает, если сверху прыгнуть)
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F)
                ReceiveDamage();
            else unit.ReceiveDamage();
        }
    }
}
