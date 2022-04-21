using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT : MonoBehaviour
    
{

        [SerializeField] GameObject Player;
        [SerializeField] GameObject Bullet;
        [SerializeField] GameObject point;
        [SerializeField] float firerate;
        Animator MyAnim;
        float NF = 0.0f;


        void Start()
        {
            MyAnim = GetComponent<Animator>();
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 7f);
        }

    void Update()
    {
        Shootting();
    }

    void Shootting()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 7f, LayerMask.GetMask("Player"));
        if (col != null)
        {
            if (Time.time > NF)
            {
                NF = Time.time + firerate;
                Instantiate(Bullet, point.transform.position, transform.rotation);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "")
            {
                GameObject.Destroy(gameObject, 0.0f);
            }
        }


}