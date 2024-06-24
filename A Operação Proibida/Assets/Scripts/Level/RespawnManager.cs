using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    [SerializeField]
    private Transform _currentRespawnPoint;
    private GameObject _player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        _player.transform.position = _currentRespawnPoint.position;
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        _currentRespawnPoint = newRespawnPoint;
    }
}
