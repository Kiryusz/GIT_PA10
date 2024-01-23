using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animation thisAnimation;
    public float fallspeed = -9.8f;
    public float Jumpheight;
    public float yMax;
    public float yMin;

    private Vector3 Direction;
    public GameManager gm;
    public void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        Direction = Vector3.zero;
    }

    void Start()
    {
        thisAnimation = GetComponent<Animation>();
        thisAnimation["Flap_Legacy"].speed = 3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Direction = Vector3.up * Jumpheight;
            thisAnimation.Play();
        }
        Direction.y += fallspeed * Time.deltaTime;
        transform.position += Direction * Time.deltaTime;
        transform.position = new Vector3(0, Mathf.Clamp(transform.position.y, yMin, yMax), 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Score")
        {
            gm.UpdateScore(1);
        }
        if(collision.gameObject.tag=="Obstacle")
        {
            gm.GameOver();
        }
    }
}
