using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    [SerializeField] int rows = 10;
    [SerializeField] int cols = 10;
    List<List<GameObject>> grid = new List<List<GameObject>>();
    [SerializeField] GameObject btnCell;
    float boardWidth, boardHeight;
    float cellWidth, cellHeight;
    // Start is called before the first frame update
    void Start()
    {
        boardWidth = gameObject.GetComponent<RectTransform>().rect.width;
        boardHeight = gameObject.GetComponent<RectTransform>().rect.height;
        Debug.Log(boardWidth);
        cellWidth = boardWidth / cols;
        cellHeight = boardHeight / rows;
        for (int i = 0; i < rows; i++)
        {
            List<GameObject> row = new List<GameObject>();
            for (int j = 0; j < cols; j++)
            {
                Vector3 cellPos = transform.TransformPoint(new Vector3((j*cellWidth+cellWidth/2)- (float)285.4,-(i*cellHeight+cellHeight/2) + (float)146.0,0));
                var cell = Instantiate(btnCell, cellPos, Quaternion.identity, this.transform) as GameObject;
                RectTransform rt = cell.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(cellWidth,cellHeight);
                row.Add(cell);
            }
            grid.Add(row);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
