using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Chua reset brick on stage,
//playerprebs chua clear gach co san
//=> 
// trong mainmenu set lai disable joystick

//NewStageBox cham vao` ra tu dong spawn them gach

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

    private int levelIndex;

    private void Awake()
    {
        levelIndex = PlayerPrefs.GetInt("Level", 0);
    }

    public Vector3 FinishPoint => currentLevel.finishPoint.position;
    public int CharacterAmount => currentLevel.botAmount + 1;

    private void Start()
    {
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
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
        List<ColorType> colorDatas = Utilities.SortOrder(colorTypes, CharacterAmount);
        
        //Set position for player
        int rand = Random.Range(0, CharacterAmount);
        player.transform.position = startPoints[rand];
        player.transform.rotation = Quaternion.identity;
        startPoints.RemoveAt(rand);
        //Set color for player
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);

        player.OnInit();
        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            //Bot bot = Instantiate(botPrefabs, startPoints[i], Quaternion.identity);   //Using Instanciate to spawn bot 
            //Bot bot = SimplePool.Spawn<Bot>(botPrefabs, startPoints[i], Quaternion.identity);   //Using SimplePool.Spawn to spawn bot from prefabs
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, startPoints[i], Quaternion.identity);   //Using SimplePool.Spawn to spawn bot by PoolType
            bot.ChangeColor(colorDatas[i]);
            bot.OnInit();
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
        GameManager.Instance.ChangeState(GameState.Gameplay);
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
        //for (int i = 0; i < bots.Count; i++)
        //{
        //    Destroy(bots[i].gameObject);
        //}

        SimplePool.CollectAll();
        bots.Clear();
    }

    internal void OnNextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
    }

    internal void OnRetry()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
        UIManager.Instance.CloseUI<Gameplay>();
    }
}
