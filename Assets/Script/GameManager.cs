using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextAsset text;
    public Levels levels;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            levels = JsonUtility.FromJson<Levels>(text.text);
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    [System.Serializable]
    public class Levels
    {
        public List<Level> levels;
    }
    [System.Serializable]

    public class Level
    {
        public int no;
        public List<Col> data;
    }
    [System.Serializable]

    public class Col
    {
        public List<int> col;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
