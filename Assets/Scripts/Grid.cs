using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    [SerializeField] int rows;
    [SerializeField] int cols;
    [SerializeField] int numOfMines;
    [SerializeField] GameObject btnCell;
    [SerializeField] GameObject wonPanel;
    static List<List<GameObject>> grid = new List<List<GameObject>>();
    float boardWidth, boardHeight;
    float cellWidth, cellHeight;
    Queue<GameObject> queue;
    // Start is called before the first frame update
    void Start()
    {
        //wonPanel.SetActive(false);
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
                grid[y][x].GetComponent<Button>().GetComponent<Image>().color = Color.red;
            }
            a++;
        }
    }

    //private void Update()
    //{
    //    if ((rows * cols) - numOfMines == Cell.visCount)
    //    {
    //        wonPanel.SetActive(true);
    //        print(wonPanel);
    //        print("GameOver");
    //        Cell.visCount = 0;
    //    }
    //}
    public void ChangeCellColorRed(int x, int y)
    {
        grid[y][x].GetComponent<Button>().GetComponent<Image>().color = Color.red;
    }
    public void ChangeCellColorGreen(int x, int y)
    {
        RevealCells(x,y);
    }
    private int GetAdjacentCells(int x, int y)
    {
        int adjacentMines = 0;
        if (!(y + 1 > rows - 1) && !(x + 1 > cols - 1))
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
        if (!(y + 1 > rows - 1) && !(x - 1 < 0))
        {
            if (grid[y + 1][x - 1].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        if (!(y - 1 < 0) && !(x + 1 > cols - 1))
        {
            if (grid[y - 1][x + 1].GetComponent<Cell>().GetState() == Cell.State.Bomb)
            {
                adjacentMines++;
            }
        }
        if (!(y + 1 > rows - 1))
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
        if (!(x + 1 > cols - 1))
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
        return adjacentMines;
    }
    private void RevealCells(int x, int y)
    {
        if (grid[y][x].GetComponent<Cell>().IsVisited() != true)
        {
            queue = new Queue<GameObject>();
            grid[y][x].GetComponent<Cell>().MakeVisitedTrue();
            queue.Enqueue(grid[y][x]);
            while (queue.Count != 0 && grid[y][x].GetComponent<Cell>().GetState() != Cell.State.Bomb)
            {
                GameObject cell = queue.Peek();
                x = cell.GetComponent<Cell>().x;
                y = cell.GetComponent<Cell>().y;
                int adjacentMines = GetAdjacentCells(x, y);
                grid[y][x].GetComponent<Button>().GetComponent<Image>().color = Color.gray;
                if (adjacentMines != 0)
                {
                    grid[y][x].GetComponentInChildren<Text>().text = adjacentMines.ToString();
                    grid[y][x].GetComponent<Button>().GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
                }

                if (adjacentMines == 0)
                {
                    if (x > 0)
                    {
                        if (y > 0 && !(grid[y - 1][x - 1].GetComponent<Cell>().IsVisited()) && grid[y - 1][x - 1].GetComponent<Cell>().GetState() != Cell.State.Bomb)
                        {
                            queue.Enqueue(grid[y - 1][x - 1]);
                            grid[y - 1][x - 1].GetComponent<Cell>().MakeVisitedTrue();
                        }
                        if (!grid[y][x - 1].GetComponent<Cell>().IsVisited() && grid[y][x - 1].GetComponent<Cell>().GetState() != Cell.State.Bomb)
                        {
                            queue.Enqueue(grid[y][x - 1]);
                            grid[y][x - 1].GetComponent<Cell>().MakeVisitedTrue();
                        }
                        if (y < rows - 1 && !(grid[y + 1][x - 1].GetComponent<Cell>().IsVisited()) && grid[y + 1][x - 1].GetComponent<Cell>().GetState() != Cell.State.Bomb)
                        {
                            queue.Enqueue(grid[y + 1][x - 1]);
                            grid[y + 1][x - 1].GetComponent<Cell>().MakeVisitedTrue();
                        }
                    }
                    if (y > 0 && !(grid[y - 1][x].GetComponent<Cell>().IsVisited()) && grid[y - 1][x].GetComponent<Cell>().GetState() != Cell.State.Bomb)
                    {
                        queue.Enqueue(grid[y - 1][x]);
                        grid[y - 1][x].GetComponent<Cell>().MakeVisitedTrue();
                    }
                    if (y < rows - 1 && !(grid[y + 1][x].GetComponent<Cell>().IsVisited()) && grid[y + 1][x].GetComponent<Cell>().GetState() != Cell.State.Bomb)
                    {
                        queue.Enqueue(grid[y + 1][x]);
                        grid[y + 1][x].GetComponent<Cell>().MakeVisitedTrue();
                    }
                    if (x < cols - 1)
                    {
                        if (y > 0 && !(grid[y - 1][x + 1].GetComponent<Cell>().IsVisited()) && grid[y - 1][x + 1].GetComponent<Cell>().GetState() != Cell.State.Bomb)
                        {
                            queue.Enqueue(grid[y - 1][x + 1]);
                            grid[y - 1][x + 1].GetComponent<Cell>().MakeVisitedTrue();
                        }
                        if (!grid[y][x + 1].GetComponent<Cell>().IsVisited() && grid[y][x + 1].GetComponent<Cell>().GetState() != Cell.State.Bomb)
                        {
                            queue.Enqueue(grid[y][x + 1]);
                            grid[y][x + 1].GetComponent<Cell>().MakeVisitedTrue();
                        }
                        if (y < rows - 1 && !(grid[y + 1][x + 1].GetComponent<Cell>().IsVisited()) && grid[y + 1][x + 1].GetComponent<Cell>().GetState() != Cell.State.Bomb)
                        {
                            queue.Enqueue(grid[y + 1][x + 1]);
                            grid[y + 1][x + 1].GetComponent<Cell>().MakeVisitedTrue();
                        }
                    }
                }
                queue.Dequeue();
            }
        }
    }
}
