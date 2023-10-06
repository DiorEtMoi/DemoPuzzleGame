using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bottle_UI : MonoBehaviour
{
    public int id;
    public List<Fruit_UI> listUI;
    public GameObject upPosition;

    public Game_UI ui;
    public void Set_UI(Stack<FruitUIType> type)
    {
        int sizeStack = type.Count;
        for (int i = 0; i < listUI.Count; i++)
        {
            if(i >= sizeStack)
            {
                Set_UINone(i);
            }
            else
            {
                FruitUIType t = type.Pop();
                Set_UI(i,t);
            }
        }
    }
    public void Set_UI(int index,FruitUIType type)
    {
        listUI[index].setColor(type);
    }
    public void Set_UINone(int index)
    {
        listUI[index].setColor(FruitUIType.None);
    }
 
    public void OnMouseUpAsButton()
    {
        ui.OnChooseBottle(id);
    }
    public Vector3 GetUpPosotion()
    {
        return upPosition.transform.position;
    }
    public Vector3 getBallPosition(FruitUIType type)
    {
       for(int i = listUI.Count - 1; i >= 0; i--)
        {
            if (listUI[i].type == type)
            {
                Debug.Log("Found");
                return listUI[i].transform.position;
            }
        }
        Debug.Log("Not Found");

        return Vector3.zero;
    }

    public Vector3 getEmptyPositon()
    {
        for(int i = 0; i < listUI.Count; i++)
        {
            if (listUI[i].type == FruitUIType.None)
            {
                return listUI[i].transform.position;
            }
        }
        return Vector3.zero;
    }
}
