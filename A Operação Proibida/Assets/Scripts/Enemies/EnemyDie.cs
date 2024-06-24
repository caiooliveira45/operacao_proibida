using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyBody;
    private GameObject _player;

    public bool isDead = false;
    public float jumpForce;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyKiller")
        {
            Rigidbody2D playerRB = _player.GetComponent<Rigidbody2D>();
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
            playerRB.AddForce(new Vector2(0f, jumpForce));
            isDead = true;
        }
    }
}
