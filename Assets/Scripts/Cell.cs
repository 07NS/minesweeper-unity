using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] Grid grid;
    public int x;
    public int y;
    bool isVisited = false;
    public static int visCount;
    public enum State
    {
        Bomb,
        Flag,
        Neutral
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        if (state == State.Bomb)
        {
            grid.ChangeCellColorRed(x, y);
        }else if (state == State.Neutral)
        {
            grid.ChangeCellColorGreen(x, y);
        }
    }
    State state = State.Neutral;
    public void SetState(State state)
    {
        this.state = state;
    }
    public State GetState()
    {
        return state;
    }
    public void SetCoordinates(Vector2 vector)
    {
        this.x = (int)vector.x;
        this.y = (int)vector.y;
    }
    public void MakeVisitedTrue()
    {
        isVisited = true;
        visCount++;
    }
    public bool IsVisited()
    {
        return isVisited;
    }
}
