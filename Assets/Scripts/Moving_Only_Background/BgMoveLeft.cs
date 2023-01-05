using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMoveLeft : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - 39.9f)
        {
            transform.position = startPos;
        }

        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
