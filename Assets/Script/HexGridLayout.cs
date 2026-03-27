using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;

public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;
    [Header("Tile Settings")]
    public float outersize = 1f;
    public float innersize = 0f;
    public float height = 1f;
    public bool isFlatTopped;
    // public Material material;

    private void OnEnable()
    {
        LayoutGrid();
    }

    private void OnValidate() 
    {
        if (Application.isPlaying)
        {
            LayoutGrid();
        }
    }

    private void LayoutGrid()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject tile = new GameObject($"Hex{x},{y}",typeof(HexRendererV2));
                tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x,y));

                HexRendererV2 hexRenderer = tile.GetComponent<HexRendererV2>();
                hexRenderer.isFlatTopped = isFlatTopped;
                hexRenderer.outersize = outersize;
                hexRenderer.innersize = innersize;
                hexRenderer.height = height;
                // hexRenderer.material = material;
                hexRenderer.DrawMesh();

                tile.transform.SetParent(transform,true);
            }
        }
    }
    public Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x,row = coordinate.y;
        float width, height, xPosition, yPosition;
        bool shouldOffset;
        float horizontalDistance, verticalDistance, offset, size = outersize;

        if (!isFlatTopped)
        {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f/4f);

            offset = (shouldOffset) ? width / 2 : 0;

            xPosition = (column * (horizontalDistance)) + offset;
            yPosition = (row * verticalDistance) ;

        }
        else
        {
            shouldOffset = (column % 2) == 0;
            width = 2f * size;
            height =  Mathf.Sqrt(3) * size;

            horizontalDistance = width * (3f / 4f); 
            verticalDistance = height;

            offset = (shouldOffset) ? height / 2 : 0;

            xPosition = (column * (horizontalDistance));
            yPosition = (row * verticalDistance) - offset;

        }


        return new Vector3(xPosition , 0 , -yPosition);
    }
}
