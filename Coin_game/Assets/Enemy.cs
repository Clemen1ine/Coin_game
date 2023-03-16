using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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

    private void Defeated()
    {
        Destroy(gameObject);
    }
}