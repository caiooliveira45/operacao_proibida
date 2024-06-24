using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _pointA;
    [SerializeField]
    private GameObject _pointB;
    [SerializeField]
    private EnemySight _enemySight;
    [SerializeField]
    private EnemyDie _enemyDie;
    private Rigidbody2D _rb;
    private Transform _currentPoint;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentPoint = _pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyDie.isDead)
            gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_enemySight.playerEnter)
            Move();
        else
            _rb.velocity = Vector2.zero;
    }

    private void Move()
    {
        Vector2 point = _currentPoint.position - transform.position;
        if (_currentPoint == _pointB.transform)
        {
            _rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            _rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f && _currentPoint == _pointB.transform)
        {
            _currentPoint = _pointA.transform;
            Flip();
        }
        else if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f && _currentPoint == _pointA.transform)
        {
            _currentPoint = _pointB.transform;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(_pointB.transform.position, 0.5f);
        Gizmos.DrawLine(_pointA.transform.position, _pointB.transform.position);
    }
}
