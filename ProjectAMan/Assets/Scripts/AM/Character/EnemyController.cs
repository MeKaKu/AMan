using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AM.Item;
using Assets.Scripts.AM.UI;
using Assets.Scripts.MsgFramework;
using Assets.Scripts.MsgFramework.Character;
using UnityEngine;
public class EnemyController : CharacterBase
{

    public int hitCount;
    Rigidbody2D rig;

    public int HP;
    public int behaveMode;
    public int dropMode;
    public GameObject moveStart;
    public GameObject moveEnd;
    bool direction;
    public float speed;
    float time;
    public float waitTime;
    public GameObject[] dropItems;
    private int nowHitCount;
    private Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player");
        transform.position = moveStart.transform.position;
        rig = GetComponent<Rigidbody2D>();
        if (moveStart.transform.position.x - moveEnd.transform.position.x > 0)
        {
            GameObject tmp = moveStart;
            moveStart = moveEnd;
            moveEnd = tmp;
        }
        anim = this.GetComponent<Animator>();
        direction = true;
        hitCount = 0;
        nowHitCount = hitCount;
    }

    // Update is called once per frame
    void Update()
    {
        down();
        if (nowHitCount != hitCount)
        {
            nowHitCount = hitCount;
            anim.SetTrigger("IsHit");
            //显示命中敌人的光标
            Dispatch(AreaCode.UI, UIEventCode.SHOW_HIT_ENEMY_CURSOR, null);
        }
        checkPlayer();
        behave();
        switchBehaveMode();
    }
    void down(){
        if (hitCount >= HP)
        {
            anim.SetBool("isDown",true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Collider2D>().enabled = false;
            Invoke("die",0);
        }
    }
    void die()
    {
        if (hitCount >= HP)
        {
            //显示击杀敌人的光标
            Dispatch(AreaCode.UI, UIEventCode.SHOW_KILL_ENEMY_CURSOR, null);
            switch (dropMode)
            {
                case 1:
                    {
                        GameObject newItem = Instantiate(dropItems[dropMode]);
                        newItem.transform.position = transform.position + new Vector3(0, 1, 0);
                        //敌人掉落可卡因 加20%
                        newItem.GetComponent<Drag>().addHp = .2f;
                        break;
                    }
            }
            Destroy(this);
        }
    }
    void behave()
    {
        switch (behaveMode)
        {
            case 1:
                {
                    Move();
                    break;
                }
            case 2:
                {
                    time += Time.deltaTime;
                    break;
                }
            case 3:
                {
                    ChooseFireMode();
                    break;
                }
        }
    }
    void switchBehaveMode()
    {
        switch (behaveMode)
        {
            case 1:
                {
                    Switch1();
                    break;
                }
            case 2:
                {
                    Switch2();
                    break;
                }
        }
    }
    void Switch1()
    {
        if (direction && transform.position.x >= moveEnd.transform.position.x)
        {
            anim.SetBool("IsWalk",false);
            behaveMode = 2;
            direction = !direction;
            time = 0;
        }
        if (!direction && transform.position.x <= moveStart.transform.position.x)
        {
            anim.SetBool("IsWalk",false);
            behaveMode = 2;
            direction = !direction;
            time = 0;
        }
    }
    void Switch2()
    {
        if (time >= waitTime)
        {
            behaveMode = 1;
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, 1);
            anim.SetBool("IsWalk",true) ;
        }
    }
    void Move()
    {

        if (direction)
        {
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
    public void ExploreHit()
    {
        hitCount += 10;
    }
    public GameObject bullet;
    public Transform firePoint;
    public float fireTime = 0;
    public float coolTime;
    public GameObject player;
    public float radius;
    public int FireMode;
    public LayerMask layerMask;
    private void Fire()
    {
        Vector2 direction = player.transform.position - firePoint.transform.position + new Vector3(0, 4, 0);
        firePoint.LookAt(direction);
        firePoint.Rotate(0, -90, 0);
        if (this.transform.localScale.x > 0 && direction.x > 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        else if (this.transform.localScale.x <= 0 && direction.x < 0)
        {

            firePoint.Rotate(0, 0, 180);
            Instantiate(bullet, firePoint.position, firePoint.rotation);

        }
    }
    private void ShotGunFire(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Vector2 direction = player.transform.position - firePoint.transform.position + new Vector3(0, 4, 0) + Vector3.down * (-number / 2 + i) / number * (player.transform.position - firePoint.transform.position + new Vector3(0, 4, 0)).x;
            firePoint.LookAt(direction);
            firePoint.Rotate(0, -90, 0);
            if (this.transform.localScale.x > 0 && direction.x > 0)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
            else if (this.transform.localScale.x <= 0 && direction.x < 0)
            {

                firePoint.Rotate(0, 0, 180);
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
        }
    }
    void ChooseFireMode()
    {
        switch (FireMode)
        {
            case 1:
                {
                    fireTime += Time.deltaTime;
                    if (fireTime >= coolTime)
                    {
                        Fire();
                        fireTime = 0;
                    }
                    break;
                }
            case 2:
                {
                    fireTime += Time.deltaTime;
                    if (fireTime >= coolTime)
                    {
                        Fire();
                        Invoke("Fire", 0.1f);
                        Invoke("Fire", 0.2f);
                        fireTime = 0;
                    }
                    break;
                }
            case 3:
                {
                    fireTime += Time.deltaTime;
                    if (fireTime >= coolTime)
                    {
                        ShotGunFire(5);
                        fireTime = 0;
                    }
                    break;
                }
        }
    }
    void checkPlayer()
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Player")
            {
                for (int i = 0; i < 9; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, Vector2.left + Vector2.down * (-4 + i) / 4);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "Player")
                        {
                            anim.SetBool("IsWalk",false);
                            behaveMode = 3;
                            break;
                        }
                    }
                }
                if (transform.localScale.x * (player.transform.position.x - transform.position.x) > 0)
                {
                    break;
                }
                else
                {
                    if (behaveMode == 3)
                    {
                        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, 1);
                    }

                }
            }
        }
    }
}
