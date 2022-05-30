using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AM.Effect;
using Assets.Scripts.AM.UI;
using Assets.Scripts.MsgFramework;
using Assets.Scripts.MsgFramework.Item;
using UnityEngine;

public class BulletController : ItemBase
{
    public float force;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        force = 40;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * force;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject.Find("MainCamera").GetComponent<CameraShake>().SetSmallShakeAmount();
        if (coll.gameObject.tag == "Ground")
        {
            Dispatch(AreaCode.EFFECT, 0, new EffectMsg(1, transform.position, transform.eulerAngles));
            Destroy(this.gameObject);
        }
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            coll.gameObject.GetComponent<EnemyController>().hitCount++;
        }
        if (coll.gameObject.tag == "Box")
        {
            Destroy(this.gameObject);
            coll.attachedRigidbody.AddForce(transform.right * 600);
            coll.gameObject.GetComponent<BoxController>().hitCount++;
        }
        if (coll.gameObject.tag == "Rope")
        {
            coll.attachedRigidbody.AddForce(transform.right * 600);
            Destroy(coll.transform.parent.gameObject, 5);
            Destroy(coll.gameObject);
        }
        if(coll.gameObject.tag == "Player"){
            //TODO 玩家被击中 减百分之10
            Dispatch(AreaCode.UI, UIEventCode.ADD_PLAYER_HP, -.1f);
        }
    }
}