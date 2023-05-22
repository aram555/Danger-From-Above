using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject Plane;
    public LayerMask ground;
    GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)) {
            if(hit.collider) {
                plane = Instantiate(Plane, hit.point, Quaternion.identity);
            }
        }

        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Ground")) Destroy(plane);
    }
}
