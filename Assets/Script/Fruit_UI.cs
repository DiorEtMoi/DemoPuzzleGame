using UnityEngine;

public enum FruitUIType
{
    None,
    PINK,
    ORANGE,
    BLUE,
    GREEN,
    PURPLE,
    BROWN,
    RED,
    LEMON


}
public class Fruit_UI : MonoBehaviour
{
    public SpriteRenderer sp;
    public FruitUIType type;
    public void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    public void setColor(FruitUIType type)
    {
        switch (type)
        {
            case FruitUIType.None:
                sp.color = new Color(0,0,0,0);
                break;
            case FruitUIType.RED:
                sp.color = Color.white;
                sp.sprite = Game.instance.dictSprite[type];
                break;
            case FruitUIType.GREEN:
                sp.color = Color.white;
                sp.sprite = Game.instance.dictSprite[type];
                break;
            case FruitUIType.ORANGE:
                sp.color = Color.white;
                sp.sprite = Game.instance.dictSprite[type];
                break;
            case FruitUIType.PINK:
                sp.color = Color.white;
                sp.sprite = Game.instance.dictSprite[type];
                break;
            case FruitUIType.PURPLE:
                sp.color = Color.white;
                sp.sprite = Game.instance.dictSprite[type];
                break;
            case FruitUIType.BLUE:
                sp.color = Color.white;
                sp.sprite = Game.instance.dictSprite[type];
                break;
            case FruitUIType.BROWN:
                sp.color = Color.white;
                sp.sprite = Game.instance.dictSprite[type];
                break;
            case FruitUIType.LEMON:
                sp.color = Color.white;
                sp.sprite = Game.instance.dictSprite[type];
                break;
        }
        this.type = type;

    }
}
