using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float upMovementIntervalTime = 2f;
    [SerializeField] float upMovementHeight = 1f;
    [SerializeField] AnimationCurve upMovementCurve;

    bool disabledPhysics = false;
    float originalY = 0;

    AudioSource audioSource;
    bool canBePicked = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (canBePicked && other.gameObject.GetComponent<Player>() != null)
        {
            canBePicked = false;
            audioSource?.Play();
            GameManager.instance.HandleCollect();

            Destroy(GetComponentInChildren<Renderer>());
            Destroy(GetComponent<Rigidbody>());

            Destroy(gameObject, 1f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (disabledPhysics) return;

        if (other.gameObject.CompareTag("Floor"))
        {
            DisablePhysics();
        }
    }

    float t = 0;
    int direction = 1;

    private void Update()
    {
        if (disabledPhysics)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            t += direction * Time.deltaTime / upMovementIntervalTime;

            if (t >= 1f) direction = -1;
            else if (t <= 0f) direction = 1;

            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(originalY, originalY + upMovementHeight, upMovementCurve.Evaluate(t));

            transform.position = newPos;
        }
    }

    void DisablePhysics()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            disabledPhysics = true;
            rigidbody.isKinematic = true;
            originalY = transform.position.y;
        }
    }
}
