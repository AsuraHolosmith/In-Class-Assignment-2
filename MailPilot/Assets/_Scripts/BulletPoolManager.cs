using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    public GameObject bullet;

    //TODO: create a structure to contain a collection of bullets

    //array of bullets
    Stack<GameObject> BulletClip;
    //Vector3 PoolLoc = new Vector3(-1000.0f, -1000.0f);

    // Start is called before the first frame update
    void Start()
    {
        BulletClip = new Stack<GameObject>();
        // TODO: add a series of bullets to the Bullet Pool
        for (int i = 0; i < 25; i++)
        {
            var newBullet = Instantiate(bullet/*, PoolLoc, Quaternion.identity*/);
            newBullet.SetActive(false);
            BulletClip.Push(newBullet);
        }
    }

    //TODO: modify this function to return a bullet from the Pool
    public GameObject GetBullet()
    {
        return BulletClip.Pop();
    }

    //TODO: modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(GameObject bullet)
    {
        BulletClip.Push(bullet);
        //bullet.transform.position = PoolLoc;
        bullet.SetActive(false);
    }
}
