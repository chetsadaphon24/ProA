using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameController controller;

    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controller.UpdateCheckPoint(transform.position);
        }
    }
}
