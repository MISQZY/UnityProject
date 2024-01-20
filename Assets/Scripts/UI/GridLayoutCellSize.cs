using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutCellSize : MonoBehaviour
{

    private float maxWidth;
    private float cellHeight = 50;
    private void Start()
    {
        UpdateCellSize();
    }

    private void UpdateCellSize()
    {
        maxWidth = gameObject.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2 (maxWidth, cellHeight);
        gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
