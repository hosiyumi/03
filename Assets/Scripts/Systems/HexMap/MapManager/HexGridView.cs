using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexGridView : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap highlightTilemap;

    [Header("Highlight Tile")]
    public TileBase highlightTile;

    void Awake()
    {
        Debug.Log($"[HexGridView:{name}] tilemap={highlightTilemap}, tile={highlightTile}", this);

        if (highlightTilemap == null)
            Debug.LogError($"[HexGridView:{name}] highlightTilemap is null.", this);

        if (highlightTile == null)
            Debug.LogError($"[HexGridView:{name}] highlightTile is null.", this);
    }

    public void ClearHighlight()
    {
        if (highlightTilemap == null) return;
        highlightTilemap.ClearAllTiles();
    }

    public void HighlightCells(Vector3Int center, List<Vector3Int> neighbors)
    {
        Debug.Log($"Highlight center={center}, neighbors={(neighbors == null ? 0 : neighbors.Count)}");
        if (highlightTilemap == null || highlightTile == null) return;

        highlightTilemap.SetTile(center, highlightTile);

        if (neighbors != null)
        {
            foreach (var n in neighbors)
                highlightTilemap.SetTile(n, highlightTile);
        }
    }
}
