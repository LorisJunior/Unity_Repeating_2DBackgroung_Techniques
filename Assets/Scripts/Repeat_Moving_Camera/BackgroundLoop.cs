using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public GameObject[] sprites;
    private Camera mainCamera;
    private float screenSizeX;

    void Start() 
    {
        mainCamera = Camera.main;
        float screenPoint1 = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, Mathf.Abs(mainCamera.transform.position.z))).x;
        float screenPoint2 = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, Mathf.Abs(mainCamera.transform.position.z))).x;
        screenSizeX = screenPoint2 - screenPoint1;
        
        foreach(GameObject obj in sprites)
        {
            LoadChildObjects(obj);
        }
    }

    void LateUpdate()
    {
        foreach (GameObject obj in sprites)
        {
            RepositionObjects(obj);
        }
    }

    void LoadChildObjects(GameObject obj)
    {
        float spriteWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childsNeeded = Mathf.CeilToInt(screenSizeX * 2 / spriteWidth);
        GameObject clone = Instantiate(obj) as GameObject;

        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject child = Instantiate(clone) as GameObject;
            child.transform.SetParent(obj.transform);
            child.transform.position = new Vector3(spriteWidth * i, obj.transform.position.y, obj.transform.position.z);
            child.name = obj.name + i;
        }

        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void RepositionObjects(GameObject obj)
    {
        Transform[] childrens = obj.GetComponentsInChildren<Transform>();

        if (childrens.Length > 1)
        {
            GameObject firstChild = childrens[1].gameObject;
            GameObject lastChild = childrens[childrens.Length - 1].gameObject;
            float halfSpriteWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            float rightBound = lastChild.transform.position.x + halfSpriteWidth;
            float leftBound = firstChild.transform.position.x - halfSpriteWidth;

            if (transform.position.x + screenSizeX > rightBound)
            {
                MoveFirstChildToLast(firstChild, lastChild);
            }
            else if (transform.position.x - screenSizeX < leftBound)
            {
                MoveLastChildToFirst(firstChild, lastChild);
            }
        }
    }
    
    void MoveFirstChildToLast(GameObject firstChild, GameObject lastChild)
    {
        float spriteWidth = lastChild.GetComponent<SpriteRenderer>().bounds.size.x;

        firstChild.transform.SetAsLastSibling();
        Vector3 lastPosition = new Vector3(lastChild.transform.position.x + spriteWidth, lastChild.transform.position.y, lastChild.transform.position.z);
        firstChild.transform.position = lastPosition;
    }

    void MoveLastChildToFirst(GameObject firstChild, GameObject lastChild)
    {
        float spriteWidth = lastChild.GetComponent<SpriteRenderer>().bounds.size.x;

        lastChild.transform.SetAsFirstSibling();
        Vector3 firstPosition = new Vector3(firstChild.transform.position.x - spriteWidth, firstChild.transform.position.y, firstChild.transform.position.z);
        lastChild.transform.position = firstPosition;
    }
}
