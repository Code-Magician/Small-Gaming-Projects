using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class Board : MonoBehaviour, IEnumerable<Tile>
{
   
    private RectTransform rectTransform;
  
    private List<Tile> tiles;

    public GameObject TilePrefab;

  
    [Range(0, 30)]
    public int Columns = 10;

  
    [Range(0, 30)]
    public int Rows = 15;

    [Range(0, 20f)]
    public float Margins = 3;

 
    public IEnumerable<IntVector2> Positions
    {
        get
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < Rows; i++)
            {
                x = 0;
                for (int j = 0; j < Columns; j++)
                {
                    yield return new IntVector2(x, y);

                    x++;
                }

                y++;
            }
        }
    }

     public IEnumerable<IntVector2> EmptyPositions
    {
        get
        {
            return Positions.Where((p) => { return this[p].Content == TileContent.Empty; });
        }
    }

    // Use this for initialization
    void Awake()
    {
        rectTransform = transform as RectTransform;

        // Calculate tile size (assuming board always have to fit whole panel's width).
        var width = rectTransform.rect.width;
        var tileSize = (width - Margins * 2) / Columns;
        var halfTileSize = tileSize / 2;

        // Change panel height to contain tiles.
        rectTransform.sizeDelta = new Vector2(width, tileSize * Rows + Margins * 2);

        // Fill the board with rows*columns number of tiles
        tiles = new List<Tile>();

        float x = Margins;
        float y = Margins;

        for (int i = 0; i < Rows; i++)
        {
            x = Margins;
            for (int j = 0; j < Columns; j++)
            {
                var tile = Instantiate(TilePrefab, new Vector3(x + halfTileSize, -y - halfTileSize, 0), Quaternion.identity).GetComponent<Tile>();
                tile.transform.SetParent(rectTransform, false);
                tile.RectTransform.sizeDelta = new Vector2(tileSize, tileSize);
                tiles.Add(tile);

                x += tileSize;
            }

            y += tileSize;
        }

        this[5, 5].Content = TileContent.Apple;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator<Tile> GetEnumerator()
    {
        return ((IEnumerable<Tile>)tiles).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<Tile>)tiles).GetEnumerator();
    }

   
    public Tile this[int x, int y]
    {
        get
        {
            if (!(x >= 0 && x < Columns))
            {
                throw new System.ArgumentOutOfRangeException("x", "x coordinate must be between 0 and the number of columns.");
            }

            if (!(y >= 0 && y < Rows))
            {
                throw new System.ArgumentOutOfRangeException("y", "y coordinate must be between 0 and the number of rows.");
            }

            return tiles[y * Columns + x];
        }
    }


    public Tile this[IntVector2 position]
    {
        get
        {
            return this[position.x, position.y];
        }
    }

    public void Reset()
    {
        foreach (var tile in this)
        {
            tile.Reset();
        }
    }
}
