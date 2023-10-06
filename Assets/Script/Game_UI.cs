using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_UI : MonoBehaviour
{
    public int selectedIndex = -1;

    public List<Bottle_UI> bottles_Ui;

    public Game gamePlay;

    public GameObject PerfapFruit;

    public GameObject spawn;

    public bool isSwitch = false;
    Vector3 fruitPositon = Vector3.zero;
    public void Refresh(List<Game.Bottle> bottles)
    {
        for(int i = 0; i < bottles.Count; i++)
        {
            Game.Bottle b = bottles[i];
            Bottle_UI bottle_UI = bottles_Ui[i];
            if(!bottle_UI.gameObject.activeSelf)
            {
                bottles_Ui[i].gameObject.SetActive(true);
                bottles_Ui[i].id = i;
            }
            Stack<FruitUIType> fruitTypes = new Stack<FruitUIType>();

            Stack<Game.Fruit> stackTemp = new Stack<Game.Fruit>();
            while(b.fruits.Count > 0)
            {
                Game.Fruit f = b.fruits.Pop();
                fruitTypes.Push(f.type);
                stackTemp.Push(f);
            }
            while(stackTemp.Count > 0)
            {
                Game.Fruit f = stackTemp.Pop();
                b.fruits.Push(f);
            }
           
            bottle_UI.Set_UI(fruitTypes);
        }
    }

    public void OnChooseBottle(int bottleIndex)
    {
        if (!gamePlay.bottles[bottleIndex].isDone && isSwitch == false)
        {   
            if (selectedIndex == -1 && gamePlay.bottles[bottleIndex].fruits.Count > 0)
            {
                isSwitch = true;
                selectedIndex = bottleIndex;

                // Take Fruit from
                Game.Bottle bFrom = gamePlay.bottles[bottleIndex];
                Stack<Game.Fruit> stackf = bFrom.fruits;
                //Get Type fruit first of bottle
                Game.Fruit f = stackf.Pop();

                stackf.Push(f);

                //Get Bottle UI
                Bottle_UI bottle = bottles_Ui[bottleIndex];

                        fruitPositon = bottle.getBallPosition(f.type);
                Vector3 bottleUpPositoin = bottle.GetUpPosotion();

                //Refresh UI
                Game.Fruit f_First = stackf.Pop();

                Refresh(gamePlay.bottles);
                //Create a fruit in from position
                stackf.Push(f_First);
                PerfapFruit = Instantiate(spawn, fruitPositon, Quaternion.identity);

                Fruit_UI f_ui = PerfapFruit.GetComponent<Fruit_UI>();
                f_ui.setColor(f.type);
                
                PerfapFruit.transform.DOMove(bottleUpPositoin, 0.4f).OnComplete(() => isSwitch = false);
                
            }
            else if (selectedIndex != bottleIndex && selectedIndex != -1)
            {
                bool canSwitch = gamePlay.CheckSwitchBall(selectedIndex, bottleIndex);
                isSwitch = true;
                if (canSwitch)
                {

                    Vector3 fruitToPosition = bottles_Ui[bottleIndex].getEmptyPositon();

                    Vector3 bottleUpPosition = bottles_Ui[bottleIndex].GetUpPosotion();

                    if(PerfapFruit != null)
                    {
                        PerfapFruit.transform.DOMove(bottleUpPosition, 0.3f).OnComplete(() =>
                        {
                            PerfapFruit.transform.DOMove(fruitToPosition, 0.3f).OnComplete(() =>
                            {
                                Destroy(PerfapFruit);
                                PerfapFruit = null;
                                gamePlay.SwitchBall(selectedIndex, bottleIndex);
                                selectedIndex = -1;
                                isSwitch = false;
                            });
                        });
                    }
                }
                else
                {
                    Debug.Log("not");
                    BackFromFruit(fruitPositon, bottleIndex);
                    selectedIndex = -1;
                }
                


            }
            else if (bottleIndex == selectedIndex)
            {
                isSwitch = true;
                selectedIndex = -1;
                BackFromFruit(fruitPositon, bottleIndex);
            }
        }

    }

    public void BackFromFruit(Vector3 pos,int index)
    {
        if(PerfapFruit != null)
        {
            
            PerfapFruit.transform.DOMove(pos, 0.3f).OnComplete(() =>
            {
                Destroy(PerfapFruit);
                PerfapFruit = null;
                Refresh(gamePlay.bottles);
                isSwitch = false;
            }
            );
           
        }
    }
    
    private void SwitchBallAnimation(int from, int to)
    {
        bool canSwitch = gamePlay.CheckSwitchBall(from, to);
        if (!canSwitch)
        {
            Debug.Log("Can not swtich");
            return;
        }
        else
        {
            Debug.Log("can switch");
        }
    }
   /* public IEnumerator SwitchBallCor(int from, int to)
    {
        // tat ui ball o vi tri from
        // tao 1 fruit o vi tri from cung type
        // di chuyen ball theo dung duong
        // xoa ball di chuyen , va bat ball o vi tri to


    }*/
}
