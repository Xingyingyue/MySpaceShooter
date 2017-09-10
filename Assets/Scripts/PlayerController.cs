using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float speed = 5.0f;
    private float tilt = 4.0f;
    private float fireGap=0.5f;
    private float nextFireTime = 0.0f;
    private Rigidbody rigidbody;

    public Boundary boundary;
    public Transform firePort;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Fire();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (rigidbody != null)
        {
            rigidbody.velocity = movement * speed;
            float positionX = Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax);
            float positionZ = Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax);
            rigidbody.position = new Vector3(positionX, 0.0f, positionZ);
            rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * (-tilt));
        }
    }

    void Fire()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireGap;
            GameObject bullet = BulletPoolScript.bulletPoolInstance.GetBullet();
            if (bullet != null)
            {
                GetComponent<AudioSource>().Play();
                bullet.SetActive(true);
                bullet.transform.position = firePort.transform.position;
                bullet.transform.rotation = firePort.transform.rotation;
            }
        }
    }
}

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}
