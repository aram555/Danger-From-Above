using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Random Money")]
    public int moneyOne;
    public int moneyTwo;
    [Header("Random Score")]
    public int scoreOne;
    public int scoreTwo;
    [Header("Particles")]
    public GameObject goal;
    public GameObject enemyHit;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Enemy")) {
            Instantiate(enemyHit, transform.position, Quaternion.Euler(90, 0, 0));
            Restart();
        }

        if(other.collider.CompareTag("Goal")) {
            Instantiate(goal, transform.position, Quaternion.Euler(90, 0, 0));
            Restart();

            int moneys = Random.Range(moneyOne, moneyTwo);
            int scores = Random.Range(scoreOne, scoreTwo);

            GameUI.game.moneys += moneys;
            PlayerPrefs.SetInt("Moneys", GameUI.game.moneys);

            GameUI.game.score += scores;
            PlayerPrefs.SetInt("Scores", GameUI.game.score);
        }
    }

    private void Restart() {
        transform.position = new Vector3(0, 10, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }
}
