using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorUp, doorDown, doorLeft, doorRight;
    public bool roomUp, roomDown, roomLeft, roomRight;

    public Text text;
    public int stepToStart;

    public int doorNumber;

    [Header("敌人列表")]
    //public int enemyNum;
    public List<GameObject> enemies = new List<GameObject>();

    [Header("NPC列表")]
    //public int npcNum;
    public List<GameObject> npcs = new List<GameObject>();

    [Header("Tilemap列表")]
    public List<GameObject> tilemaps = new List<GameObject>();

    public GameObject existdoor;

    private void Start()
    {
        if(transform.position != Vector3.zero)//初始房间之外随机怪物
        {

            if (!GameManager.instance.isUpDown)
            {
                doorLeft.SetActive(roomLeft);
                doorRight.SetActive(roomRight);
                doorDown.SetActive(roomDown);
                doorUp.SetActive(roomUp);
                //随机地图
                int i = Random.Range(0, tilemaps.Count);
                tilemaps[i].SetActive(true);
            }
            else
            {
                doorLeft.SetActive(roomLeft);
                doorRight.SetActive(roomRight);
                doorDown.SetActive(roomDown);
                doorUp.SetActive(roomUp);
                //随机地图
                int i = Random.Range(0, tilemaps.Count);
                tilemaps[i].SetActive(true);
            }

            //随机怪物
            RandomEnemy();
            //随机NPC
            RandomNPC();
        }
        else//初始房间设置初始地图
        {
            tilemaps[1].SetActive(true);
        }



    }

    private void RandomNPC()
    {
        
        //每次循环生成一个Npc
        for (int i = 0; i < npcs.Count; i++)
        {
            //如果参数是float，那结果中会出现max，只是概率很小；如果参数是int，结果中不会出现max。
            int e = Random.Range(0, npcs.Count);
            int v = Random.Range(-2, 2);
            int h = Random.Range(-6, 6);
            Vector3 pos = new Vector3(h, v,0);
            GameObject obj = Instantiate(npcs[e]);
            obj.transform.position = transform.position+pos;
            GameObject father = GameObject.Find("NPC_Father");
            if (father != null)
                obj.transform.parent = father.transform;

        }
    }

    private void RandomEnemy()
    {
        //if (stepToStart == 0) return;//第一个房间不生成怪物
        for (int i = 0; i < enemies.Count; i++)
        {
            int e = Random.Range(0, enemies.Count-1);
            int v = Random.Range(-3, 3);
            int h = Random.Range(-7, 7);
            Vector3 pos = new Vector3(h, v, 0);
            GameObject obj = Instantiate(enemies[e]);
            obj.transform.position = transform.position + pos;

            GameObject father = GameObject.Find("Enemy_Father");
            if (father != null)
                obj.transform.parent = father.transform;
        }
    }

    public void UpdateRoom(float xOffset,float yOffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));
        text.text = stepToStart.ToString();
        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraUpDownController.instance.ChangeTarget(transform);
        }
    }
}
