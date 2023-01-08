using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLoopRefined : MonoBehaviour
{
    public GameObject background;
    public Sprite spriteRef;
    private float spriteSizeX;
    private Vector3 currentWorldPosition;
    // Start is called before the first frame update
    void Start()
    {
        spriteSizeX = spriteRef.bounds.size.x;
        currentWorldPosition = background.transform.position;
    }
    void Update() 
    {
        transform.position = transform.position + Vector3.right * Time.deltaTime * 10f;
        background.transform.position = currentWorldPosition;
        float distance = Mathf.Abs(background.transform.localPosition.x);

        if (distance > spriteSizeX)
        {
            UpdateBgPosition(distance);
        }
    }

    void UpdateBgPosition(float distance)
    {
        float offset = distance - spriteSizeX;
        background.transform.localPosition = new Vector3(transform.position.x > 0 ? -offset : offset, currentWorldPosition.y, currentWorldPosition.z);
        currentWorldPosition = background.transform.position;
    }
}
