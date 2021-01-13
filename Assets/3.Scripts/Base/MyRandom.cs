using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MyRandom : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        /*int iRnd = System.DateTime.Now.Millisecond;
        System.Random randomCoor = new System.Random(iRnd);
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(randomCoor.Next(1, 53) + "/");
        }

        for (int i = 0; i < 10; i++)
        {
            Debug.Log(UnityEngine.Random.Range(1,53) + "+");
        }*/

        for (int i = 0; i < 10; i++)
        {
            //Debug.Log(GetRandomNumber(0, 100));

            //Debug.Log(UnityEngine.Random.Range(0, 100));

            /*虚假的随机数
            System.Random r = new System.Random();
            r = new System.Random(1);
            Debug.Log(r.Next(0, 100));*/
        }

    }
    static int GetRandomNumber(int min, int max)
    {
        int rtn = 0;

        System.Random r = new System.Random();

        byte[] buffer = Guid.NewGuid().ToByteArray();
        int iSeed = BitConverter.ToInt32(buffer, 0);
        r = new System.Random(iSeed);

        rtn = r.Next(min, max + 1);

        return rtn;
    }

}
