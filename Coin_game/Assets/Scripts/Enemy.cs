using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private AddRoom room;
    
    private float health = 1;

    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health <= 0)
            {
                Defeated();
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        room = GetComponentInParent<AddRoom>();
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    private void Removwenemy()
    {
        Destroy(gameObject);
        room.enemies.Remove(gameObject);
    }
}