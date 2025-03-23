using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        // dir.Normalize();
        // Vector3 dirNormalized = dir.normalized;
        dir = dir.normalized;
        rb.AddForce(dir * speed);
    }
}
