using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public GameObject target;
    public GameObject[] targetList;
    public float launchForce = 10f;
    public int currTarget;
    Rigidbody rb;
    Vector3 startPos;
    public GameObject explosionEffect;
    public GameObject highlight;
    public GameObject spawnPoint;

    public bool endGame = false;

    public GameObject gameOverText;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f, 0);
        currTarget = 0;
    }

    void Update()
    {
        if (endGame)
        {
            return;
        }
        if (target == null || !target.activeInHierarchy)
        {
            SetNextTarget();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetNextTarget();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("spaced");
            Nullable<Vector3> aimVector = CalculateFiringSolution(transform.position, target.transform.position, launchForce, Physics.gravity);
            if (aimVector.HasValue)
            {
                Debug.Log("yeet");
                rb.AddForce(aimVector.Value * launchForce, ForceMode.VelocityChange);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.isKinematic = true;
            transform.position = startPos;
            rb.isKinematic = false;
        }
    }

    void SetNextTarget()
    {
        int startIdx = currTarget;
        bool targetFound = false;

        do
        {
            currTarget++;
            if (currTarget >= targetList.Length)
            {
                currTarget = 0;
            }

            if (targetList[currTarget] != null && targetList[currTarget].activeInHierarchy)
            {
                target = targetList[currTarget];
                highlight.transform.position = target.transform.position;
                targetFound = true;
                break;
            }

        } while (currTarget != startIdx);

        if (!targetFound)
        {
            GameOver();
        }

        void GameOver()
        {
            gameOverText.gameObject.SetActive(true);
            endGame = true;
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        endGame = false;
        gameOverText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Instantiate(explosionEffect, collision.contacts[0].point, Quaternion.identity);
            Destroy(collision.gameObject);
            Instantiate(gameObject, spawnPoint.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public Nullable<Vector3> CalculateFiringSolution(Vector3 start, Vector3 end, float muzzleV, Vector3 gravity)
    {
        Nullable<float> ttt = GetTimeToTarget(start, end, muzzleV, gravity);
        if (!ttt.HasValue)
        {
            Debug.Log("test1");
            return null;
        }
        Vector3 delta = end - start;

        Vector3 n1 = delta * 2;
        Vector3 n2 = gravity * (ttt.Value * ttt.Value);
        float d = 2 * muzzleV * ttt.Value;
        Vector3 solution = (n1 - n2) / d;

        return solution;
    }

    public Nullable<float> GetTimeToTarget(Vector3 start, Vector3 end, float muzzleV, Vector3 gravity)
    {
        Vector3 delta = start - end;

        float a = gravity.magnitude * gravity.magnitude;
        float b = -4 * (Vector3.Dot(gravity, delta) + muzzleV * muzzleV);
        float c = 4 * delta.magnitude * delta.magnitude;

        float b2minus4ac = (b * b) - (4 * a * c);
        if (b2minus4ac < 0)
        {
            Debug.Log("test2");
            return null;
        }

        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b2minus4ac)) / (2 * a));
        float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b2minus4ac)) / (2 * a));

        Nullable<float> ttt;
        if (time0 < 0)
        {
            if (time1 < 0)
            {
                Debug.Log("test3");
                return null;
            }
            else
            {
                ttt = time1;
            }
        }
        else if (time1 < 0)
        {
            ttt = time0;
        }
        else
        {
            ttt = Mathf.Min(time0, time1);
        }

        return ttt;
    }
}