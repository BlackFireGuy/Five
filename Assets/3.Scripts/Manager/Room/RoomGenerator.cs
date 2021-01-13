using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WallType
{
    public GameObject singleLeft, singleRight, singleUp, singleDown,
                      doubleLU, doubleLR, doubleLD, doubleUR, doubleUD, doubleRD,
                      tripleLUR, tripleLRD, tripleRDU, tripleLDU,
                      fourDoors;
}


public class RoomGenerator : MonoBehaviour
{
    public enum Direction {up,down,left,right };
    public Direction direction;

    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;
    public int maxStep;
    public List<Room> rooms = new List<Room>();


    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessFarRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();

    public WallType wallType;
    [Header("随机Boss")]
    public List<GameObject> boss = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            GameObject obj = Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity);
            GameObject father = GameObject.Find("Room_Sum");
            if (father != null)
                obj.transform.parent = father.transform;
            rooms.Add(obj.GetComponent<Room>());

            //改变point位置
            ChangetPointPos();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        endRoom = rooms[0].gameObject;

        //找到最后房间
        foreach(var room in rooms)
        {
            SetupRoom(room, room.transform.position);
        }
        FindEndRoom();
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
        //最后一个房间设置随机Boss和随机奖励
        RandomBossAndGift();
    }

    public void Update()
    {
        if (GameManager.instance.isBossDead)
        {
            endRoom.GetComponent<Room>().existdoor.SetActive(true);
        }
    }

    public void ChangetPointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);
            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
        
    }

    public void SetupRoom(Room newRoom,Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset,0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);

        newRoom.UpdateRoom(xOffset,yOffset);

        switch (newRoom.doorNumber)
        {
            case 1:
                if (newRoom.roomUp)
                    Instantiate(wallType.singleUp, roomPosition, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(wallType.singleDown, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(wallType.singleLeft, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(wallType.singleRight, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(wallType.doubleLU, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomDown)
                    Instantiate(wallType.doubleLD, roomPosition, Quaternion.identity);

                if (newRoom.roomRight && newRoom.roomUp)
                    Instantiate(wallType.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomUp)
                    Instantiate(wallType.doubleUD, roomPosition, Quaternion.identity);

                if (newRoom.roomDown && newRoom.roomRight)
                    Instantiate(wallType.doubleRD, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.tripleLUR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.tripleLDU, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomDown && newRoom.roomRight)
                    Instantiate(wallType.tripleLRD, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.tripleRDU, roomPosition, Quaternion.identity);
                break;
            case 4:
                if (newRoom.roomDown && newRoom.roomUp && newRoom.roomRight && newRoom.roomLeft)
                    Instantiate(wallType.fourDoors, roomPosition, Quaternion.identity);
                break;
        }

    }

    public void FindEndRoom()
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }
        foreach (var item in rooms)
        {
            if (item.stepToStart == maxStep)
                farRooms.Add(item.gameObject);
            if (item.stepToStart == maxStep - 1)
                lessFarRooms.Add(item.gameObject);
        }
        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);
        }
        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);
        }

        if(oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
    }

    public void RandomBossAndGift()
    {
        if (boss == null) return;
        Vector3 pos = endRoom.transform.position;
        GameObject obj = Instantiate(boss[Random.Range(0,boss.Count)]);
        obj.transform.position = pos;
    }
}
