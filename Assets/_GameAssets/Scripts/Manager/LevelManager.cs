using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() 
    {
        ColorType.Blue,
        ColorType.Green,
        ColorType.Red,
        ColorType.Orange,
        ColorType.Purple,
        ColorType.Yellow 
    };

    private List<Bot> bots = new List<Bot>();

    public Level[] levelPrefabs;
    public Bot botPrefabs;
    public Player player;

    private Level currentLevel;

    public Vector3 FinishPoint => currentLevel.finishPoint.position;
    public int CharacterAmount => currentLevel.botAmount + 1;

    private void Start()
    {
        LoadLevel(0);
        OnInit();
    }

    public void OnInit()
    {
        //Init postion whenever start game
        Vector3 index = currentLevel.startPoint.position;
        float space = 2f;
        Vector3 leftPoint = ((CharacterAmount / 2) + (CharacterAmount % 2) * 0.5f - 0.5f)
                                * space * Vector3.left + index;
        
        List<Vector3> startPoints = new List<Vector3>();

        for (int i = 0; i < CharacterAmount; i++)
        {
            startPoints.Add(leftPoint + space * Vector3.right * i);
        }

        //Init random color
        List<ColorType> colorDatas = Ultilities.SortOrder(colorTypes, CharacterAmount);
        
        //Set position for player
        int rand = Random.Range(0, CharacterAmount);
        player.transform.position = startPoints[0];
        //Set color for player
        player.ChangeColor(colorDatas[rand]);
        startPoints.RemoveAt(rand);


        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            Bot bot = Instantiate(botPrefabs, startPoints[i], Quaternion.identity);
            bot.ChangeColor(colorDatas[i]);
            bots.Add(bot);  
        }
    }
/*
 * Create Level, prefabs,...
 * Set position of Player and bot
 */
    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        if (level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level]);
            currentLevel.OnInit();
        }
        else
        {
            //TODO: Level surpass limit

        }
    }

    public void OnStartGame()
    {
        for (int i = 0; i < bots.Count; i++) {
            bots[i].ChangeState(new PatrolState());
        }
    }
    public void OnFinishGame()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(null);
            bots[i].MoveStop();
        }
    }
    public void OnReset()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            Destroy(bots[i]);
        }
        bots.Clear();
    }
}
