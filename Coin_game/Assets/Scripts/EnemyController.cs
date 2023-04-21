using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private Transform target;
    
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    public Transform homePos;

    private void Start()
    {
        homePos.parent = null;
        animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange )
        {
            FollowPlayer();
        }
        else if(Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            GoHome();
        }
    }

    public void FollowPlayer()
    {
        animator.SetBool("isMoving", true);
        animator.SetFloat("moveX",(target.position.x - transform.position.x));
        animator.SetFloat("moveY",(target.position.y - transform.position.y)); 
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void GoHome()
    {
        animator.SetFloat("moveX",(target.position.x - transform.position.x));
        animator.SetFloat("moveY",(target.position.y - transform.position.y));

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            animator.SetBool("isMoving", false);
        }
    }
}