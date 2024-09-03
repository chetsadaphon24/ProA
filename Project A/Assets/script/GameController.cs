using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    Vector2 CheckPointPos;
    Rigidbody2D playerRb;

    public ParticleSystem particleSystem;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        CheckPointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void UpdateCheckPoint(Vector2 pos)
    {
        CheckPointPos = pos;
    }

    void Die()
    {
        particleSystem.Play();
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        playerRb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = CheckPointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }
}
