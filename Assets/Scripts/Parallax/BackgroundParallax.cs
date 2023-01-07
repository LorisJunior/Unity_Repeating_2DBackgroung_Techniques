using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public Sprite spriteRef;
    public GameObject[] sprites;
    public float[] backgroundSpeed;
    private Vector3[] currentWorldPosition = new Vector3[5];
    private float spriteSizeX;
    private float oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position.x;
        spriteSizeX = spriteRef.bounds.size.x;

        for (int i = 0; i < currentWorldPosition.Length; i++)
        {
            currentWorldPosition[i] = sprites[i].transform.position;
        }
    }
    void Update() 
    {
        transform.position = transform.position + Vector3.right * Time.deltaTime * 3f;

        for (int i = 0; i < sprites.Length; i++)
        {
            float displacement = (transform.position.x - oldPosition) * backgroundSpeed[i];
            currentWorldPosition[i] = currentWorldPosition[i] + new Vector3(displacement, 0, 0);
            sprites[i].transform.position = currentWorldPosition[i];
            float distance = Mathf.Abs(sprites[i].transform.localPosition.x);

            if (distance > spriteSizeX)
            {
                UpdateBgPosition(distance, i);
            }
        }

        oldPosition = transform.position.x;
    }

    void UpdateBgPosition(float distance, int index)
    {
        float offset = distance - spriteSizeX;
        sprites[index].transform.localPosition = new Vector3(transform.position.x > 0 ? -offset : offset, currentWorldPosition[index].y, currentWorldPosition[index].z);
        currentWorldPosition[index] = sprites[index].transform.position;
    }
}
