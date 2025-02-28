using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float powerupStrenght = 15f;
    public bool hasPowerup = false;
    private Rigidbody rb;
    private GameObject focalPoint;
    public float speed;

    public GameObject powerupIndicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point"); // Look for GameObject called ""
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * speed * forwardInput); // according to focal.transform's Forward
        powerupIndicator.transform.position = transform.position - new Vector3(0, -0.38f) ;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountDownRoutine()); //for starting the timer 
        }
    }

    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>(); //gettin enemy rigidbody
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position); //sending away
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrenght, ForceMode.Impulse);
            
            Debug.Log("Collided with "+collision.gameObject.name + " with powerup set to "+hasPowerup);
        }
    }
}
