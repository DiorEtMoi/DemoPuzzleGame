using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameManager manager;   
    public List<Bottle> bottles = new List<Bottle>();
    public Game_UI ui;

    public List<Sprite> sprites = new List<Sprite>();
    public Dictionary<FruitUIType,Sprite> dictSprite = new Dictionary<FruitUIType, Sprite>();

    public GameObject WinPanel;

    public int level;
    private int sizeCol = 4;
    public static Game instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        foreach (Sprite sprite in sprites)
        {
            switch (sprite.name)
            {
                case "1":
                    dictSprite.Add(FruitUIType.PINK, sprite);
                    break;
                case "2":
                    dictSprite.Add(FruitUIType.ORANGE, sprite);
                    break;
                case "3":
                    dictSprite.Add(FruitUIType.BLUE, sprite);
                    break;
                case "4":
                    dictSprite.Add(FruitUIType.GREEN, sprite);
                    break;
                case "5":
                    dictSprite.Add(FruitUIType.PURPLE, sprite);

                    break;
                case "6":
                    dictSprite.Add(FruitUIType.BROWN, sprite);

                    break;
                case "7":
                    dictSprite.Add(FruitUIType.RED, sprite);

                    break;
                case "8":
                    dictSprite.Add(FruitUIType.LEMON, sprite);

                    break;


            }
        }
    }
    public void Start()
    {
        LoadBottle();

        ui.Refresh(bottles);
    }
    public void RandomLevel()
    {
        level = Random.Range(0, GameManager.instance.levels.levels.Count);
        switch(level)
        {
            case 4:
                sizeCol = 5;
                break;
            case 6:
                sizeCol = 3;
                break;
            case 7:
                sizeCol = 5;
                break;
            case 8:
                sizeCol = 5;
                break;
            default: sizeCol = 4; break;
        }
        Debug.Log("Level " + level + "Size " +  sizeCol);
    }
    public void LoadBottle()
    {
        foreach (GameManager.Col c in GameManager.instance.levels.levels[level].data)
        {
            Bottle b = new Bottle();
            Stack<Fruit> stackF = new Stack<Fruit>();
            for (int i = 0; i < c.col.Count; i++)
            {
                if (c.col[i] != 0)
                {
                    stackF.Push(new Fruit { type = (FruitUIType)c.col[i] });
                }

            }
            b.fruits = stackF;
            bottles.Add(b);
        }
    }

   
    public void SwitchBall(Bottle bFrom, Bottle bTo)
    {
        Stack<Fruit> fruitsFrom = bFrom.fruits;
        Stack<Fruit> fruitsTo = bTo.fruits;

        if(fruitsFrom.Count == 0 ) { Debug.Log("Bottle From have nothing"); return; }
        if(fruitsTo.Count == sizeCol ) { Debug.Log("Bottle To is Fulled"); return; }

        
        Fruit fSwitch = fruitsFrom.Pop();
        if(fruitsTo.Count > 0 )
        {
            Fruit fruitToCheckType = fruitsTo.Pop();
            fruitsTo.Push(fruitToCheckType);
            
            if (fSwitch.type != fruitToCheckType.type)
            {
                Debug.Log("Bottle To not same type fruit");
                fruitsFrom.Push(fSwitch);
                return;
            }
        }
        fruitsTo.Push(fSwitch);
        Debug.Log("Switch success");
        CheckBottleIsDone(bTo);

        bool endgame = EndGame();
        ui.Refresh(bottles);
        if (endgame)
        {
            WinPanel.SetActive(true);
        }
    }
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 <= 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void CheckBottleIsDone(Bottle bTo)
    {
        Stack<Fruit> fTo = bTo.fruits;
        Stack<Fruit> fTemp = new Stack<Fruit>();

        Fruit fCheck = fTo.Pop();
        fTemp.Push(fCheck);
        int count = 0;
        while(fTo.Count > 0)
        {
            Fruit f = fTo.Pop();
            fTemp.Push(f);

            if (fCheck.type != f.type)
            {
                break;
            }
            count++;
        }
        while(fTemp.Count > 0)
        {
            bTo.fruits.Push(fTemp.Pop());
        }
        if(count == 3)
        {
            bTo.isDone = true;
        }
    }
    public void SwitchBall(int indexFrom, int indexTo)
    {
       
            
            //Get Bottle from click index
            Bottle bFrom = bottles[indexFrom];
            
            //Move Fruit to Bottle other

            Bottle bTo = bottles[indexTo];

            SwitchBall(bFrom, bTo);

        


    }   

    public bool CheckSwitchBall(int fromBottle,int toBottle)
    {
        Bottle bFrom = bottles[fromBottle];
        Bottle bTo = bottles[toBottle];

        Stack<Fruit> fruitsFrom = bFrom.fruits;
        Stack<Fruit> fruitsTo = bTo.fruits;

        if (fruitsFrom.Count == 0) { Debug.Log("Nothing fruits"); return false; }
        if (fruitsTo.Count == sizeCol) { Debug.Log("Bottle to Full fruit"); return false; }

        Fruit fSwitch = fruitsFrom.Pop();
        if (fruitsTo.Count > 0)
        {
            Fruit fruitToCheckType = fruitsTo.Pop();
            fruitsTo.Push(fruitToCheckType);

            if (fSwitch.type != fruitToCheckType.type)
            {
                Debug.Log("Bottle To not same type fruit");
                fruitsFrom.Push(fSwitch);
                return false;
            }
        }
        fruitsFrom.Push(fSwitch);
        return true;
    }
    public bool EndGame()
    {
        foreach(Bottle b in bottles)
        {
            if(b.fruits.Count == 0)
            {
                continue;
            }
            if (!b.isDone)
            {
                return false;
            }
        }
        return true;
    }
    public class Bottle
    {
        public Stack<Fruit> fruits;
        public bool isDone;
    }
    public class Fruit
    {
        public FruitUIType type;
        
    }

    public class SwitchBallCommand
    {
        public int fromBallIndex;
        public int fromBottle;

        public int toBallIndex;
        public int toBottle;
    }
}
