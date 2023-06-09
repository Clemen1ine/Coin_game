using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AddRoom _room;
    
    private float _health = 1;

    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                Defeated();
            }
        }
    }

    private void Start()
    {
        _room = GetComponentInParent<AddRoom>();
    }

    public void Defeated()
    {

    }

    private void Removwenemy()
    {
        Destroy(gameObject);
        _room.enemies.Remove(gameObject);
    }

}