using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Joystick joystick;
    [Header("Player Stats")]
    public float speed;
    public float jummpForce;
    public float range;
    public float HP;
    [Header("Particle")]
    public GameObject enemyParticle;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = GameObject.FindObjectOfType<Joystick>();
    }
    // Update is called once per frame
    void Update()
    {
        GameUI.game.playerHP = HP;
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        rb.AddForce(new Vector3(x * speed, rb.velocity.y, z * speed));

        if(Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    void Jump() {
        rb.AddForce(Vector3.up * jummpForce, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Enemy")) {
            Instantiate(enemyParticle, transform.position, Quaternion.Euler(90, 0, 0));
            Restart();
        }

        if(other.collider.CompareTag("Ball")) Goal(other.collider);
    }

    private void Goal(Collider other) {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        Vector3 forward = new Vector3(x, rb.velocity.y, z);

        other.GetComponent<Rigidbody>().AddForce(forward * range, ForceMode.VelocityChange);
    }

    private void Restart() {
        HP--;
        
        if(HP <= 0) GameOver();
        transform.position = new Vector3(0, 10, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void GameOver() {
        GameUI.game.GameOver();
    }
}
