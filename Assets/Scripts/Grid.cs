using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    [SerializeField] int rows = 10;
    [SerializeField] int cols = 10;
    [SerializeField] int numOfMines = 60;
    [SerializeField] GameObject btnCell;
    static List<List<GameObject>> grid = new List<List<GameObject>>();
    float boardWidth, boardHeight;
    float cellWidth, cellHeight;
    // Start is called before the first frame update
    void Start()
    {
        boardWidth = gameObject.GetComponent<RectTransform>().rect.width;
        boardHeight = gameObject.GetComponent<RectTransform>().rect.height;
        cellWidth = boardWidth / cols;
        cellHeight = boardHeight / rows;
        for (int i = 0; i < rows; i++)
        {
            List<GameObject> row = new List<GameObject>();
            for (int j = 0; j < cols; j++)
            {
                //j=x cordinate & i=y cordinate
                Vector3 cellPos = transform.TransformPoint(new Vector3((j*cellWidth+cellWidth/2)- (float)285.4,-(i*cellHeight+cellHeight/2) + (float)146.0,0));
                var btn = Instantiate(btnCell, cellPos, Quaternion.identity, this.transform) as GameObject;
                RectTransform rt = btn.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(cellWidth,cellHeight);
                btn.GetComponent<Cell>().SetCoordinates(new Vector2(j, i));
                row.Add(btn);
            }
            grid.Add(row);
        }
        int a = 0;
        while (a < numOfMines)
        {
            int x = Random.Range(0, cols);
            int y = Random.Range(0, rows);
            if (grid[y][x].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                a--;
            }
            else
            {
                grid[y][x].GetComponent<Cell>().SetState(Cell.State.Bomb);
/*                Debug.Log(x + " " + y);
*/            }
            a++;
        }
    }

    public void ChangeCellColorRed(int x, int y)
    {
        grid[y][x].GetComponent<Button>().GetComponent<Image>().color = Color.red;
    }
    public void ChangeCellColorGreen(int x, int y)
    {
/*        Debug.Log(x + " " + y);
*/      int adjacentMines = 0;
        grid[y][x].GetComponent<Button>().GetComponent<Image>().color = Color.green;
        if (!(y + 1 > rows-1) && !(x + 1 > cols-1))
        {
            if (grid[y + 1][x + 1].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        if (!(y - 1 < 0) && !(x - 1 < 0))
        {
            if (grid[y - 1][x - 1].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        if (!(y + 1 > rows-1) && !(x - 1 < 0))
        {
            if (grid[y + 1][x - 1].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        if (!(y - 1 < 0) && !(x + 1 > cols-1))
        {
            if (grid[y - 1][x + 1].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        if (!(y + 1 > rows-1))
        {
            if (grid[y + 1][x].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        if (!(y - 1 < 0))
        {
            if (grid[y - 1][x].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        if (!(x + 1 > cols-1))
        {
            if (grid[y][x + 1].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        if (!(x - 1 < 0))
        {
            if (grid[y][x - 1].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        grid[y][x].GetComponentInChildren<Text>().text = adjacentMines.ToString();
    }
}
