using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public float minForce;
    public float maxForce;
    public float minTorque;
    public float maxTorque;
    public int number;

    private static Vector3[] num2face = {
        new Vector3(-90, 0, 0),
        Vector3.zero,
        new Vector3(0, 0, -90),
        new Vector3(0, 0, 90),
        new Vector3(0, 0, 180),
        new Vector3(90, 0, 0)
    };

    public void RollDice()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = -0.01f * Vector3.up;
            rb.angularVelocity = Vector3.zero;

            // Apply a random force upwards within a range and a random torque in all directions
            //Vector3 forceDirection = Vector3.up + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Vector3 torqueDirection = Random.onUnitSphere;

            //rb.AddForce(forceDirection.normalized * Random.Range(minForce, maxForce), ForceMode.Impulse);
            rb.AddTorque(torqueDirection * Random.Range(minTorque, maxTorque), ForceMode.Impulse);
        }
    }

    public void SetDiceFace(int n)
    {
        transform.rotation = Quaternion.Euler(num2face[n - 1]);
    }

    public int CheckResult() {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.magnitude > 0.001f)
        {
            return -1;
        }

        Transform diceTransform = this.transform;
        Vector3 up = diceTransform.up;
        Vector3 down = -diceTransform.up;
        Vector3 right = diceTransform.right;
        Vector3 left = -diceTransform.right;
        Vector3 forward = diceTransform.forward;
        Vector3 back = -diceTransform.forward;

        float maxDot = Mathf.Max(
            Vector3.Dot(up, Vector3.up),
            Vector3.Dot(down, Vector3.up),
            Vector3.Dot(right, Vector3.up),
            Vector3.Dot(left, Vector3.up),
            Vector3.Dot(forward, Vector3.up),
            Vector3.Dot(back, Vector3.up)
        );

        if (Mathf.Approximately(maxDot, Vector3.Dot(up, Vector3.up))) return 2;
        if (Mathf.Approximately(maxDot, Vector3.Dot(down, Vector3.up))) return 5;
        if (Mathf.Approximately(maxDot, Vector3.Dot(right, Vector3.up))) return 4;
        if (Mathf.Approximately(maxDot, Vector3.Dot(left, Vector3.up))) return 3;
        if (Mathf.Approximately(maxDot, Vector3.Dot(forward, Vector3.up))) return 1;
        if (Mathf.Approximately(maxDot, Vector3.Dot(back, Vector3.up))) return 6;

        throw new System.InvalidOperationException("Invalid dice number");
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    RollDice();
        //}

        //Rigidbody rb = GetComponent<Rigidbody>();
        //if (rb.velocity == Vector3.zero)
        //{
        //    int result = CheckResult();
        //    Debug.Log(result);
        //}
    }
}
