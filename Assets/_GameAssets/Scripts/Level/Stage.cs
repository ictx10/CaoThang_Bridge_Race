using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorType { Default, Blue, Green, Red, Orange, Purple, Yellow };
public class Stage : MonoBehaviour
{
    public List<Vector3> emptyPoint = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();

    [SerializeField] Brick brickPrefabs;

    public Transform[] brickPoint;

    internal void OnInit()
    {
        for (int i = 0; i < brickPoint.Length; i++)
        {
            emptyPoint.Add(brickPoint[i].position);
        }
    }
    public void InitColor(ColorType color)
    {
        int amount = brickPoint.Length / LevelManager.Instance.CharacterAmount;
        for(int i = 0;i < amount; i++)
        {
            NewBrick(color);
        }
    }
    public void NewBrick(ColorType colorType)
    {
        int rand = Random.Range(0, emptyPoint.Count);
        if (emptyPoint.Count > 0)
        {
            //Brick brick = Instantiate(brickPrefabs, emptyPoint[rand], Quaternion.identity);
            Brick brick = SimplePool.Spawn<Brick>(brickPrefabs, emptyPoint[rand], Quaternion.identity);
            brick.stage = this;
            brick.ChangeColor(colorType);
            emptyPoint.RemoveAt(rand);
            bricks.Add(brick);
        }
    }

    internal void RemoveBrick(Brick brick)
    {
        emptyPoint.Add(brick.transform.position);
        bricks.Remove(brick);
    }

    internal Brick SeekBrickPoint(ColorType colorType)
    {
        Brick brick = null; 
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                brick = bricks[i];
                break;
            }
        }
        return brick;
    }
}
