using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class Snake : IEnumerable<IntVector2>
{
   
    private LinkedList<IntVector2> body;

  
    private HashSet<IntVector2> bulges;

  
    private Board board;

   
    public IntVector2 Head
    {
        get
        {
            return body.Last.Value;
        }
    }

  
    public IEnumerable<IntVector2> WithoutTail
    {
        get
        {
            return this.Where((p) => { return p != body.First.Value; });
        }
    }

    public Snake(Board board)
    {
        this.board = board;
        body = new LinkedList<IntVector2>();
        bulges = new HashSet<IntVector2>();
        Reset();
    }


    public void Hide()
    {
        foreach (var p in body)
        {
            board[p].ContentHidden = true;
        }
    }


    public void Show()
    {
        foreach (var p in body)
        {
            board[p].ContentHidden = false;
        }
    }

  
    public void Reset()
    {
        foreach (var p in body)
        {
            board[p].Content = TileContent.Empty;
        }

        body.Clear();
        bulges.Clear();

        var start = new IntVector2(5, 13);
        for (int i = 0; i < 5; i++)
        {
            var position = new IntVector2(start.x, start.y - i);
            body.AddLast(position);
        }

        UpdateSnakeState();
    }


    public void Move(IntVector2 direction, bool extend)
    {
        var newHead = NextHeadPosition(direction);

        body.AddLast(newHead);
        if (extend)
        {
            bulges.Add(newHead);
        }
        else
        {
            bulges.Remove(body.First.Value);
            board[body.First.Value].Content = TileContent.Empty;
            body.RemoveFirst();
        }

        UpdateSnakeState();
    }

 
    private void UpdateSnakeState()
    {
        // Handle head
        var headPosition = body.Last.Value;
        var nextPosition = body.Last.Previous.Value;

        var tile = board[headPosition];
        tile.Content = TileContent.SnakesHead;

        if (nextPosition.y > headPosition.y)
        {
            tile.ZRotation = 0;
        }
        else if (nextPosition.y < headPosition.y)
        {
            tile.ZRotation = 180;
        }
        else if (nextPosition.x > headPosition.x)
        {
            tile.ZRotation = 90;
        }
        else if (nextPosition.x < headPosition.x)
        {
            tile.ZRotation = -90;
        }

        // Handle middle section
        var previous = body.Last;
        var current = body.Last.Previous;
        while (current != body.First)
        {
            var next = current.Previous;
            tile = board[current.Value];
            if (previous.Value.x == next.Value.x)
            {
                if (bulges.Contains(current.Value))
                {
                    tile.Content = TileContent.SnakesBulge;
                }
                else
                {
                    tile.Content = TileContent.SnakesBody;
                }
                tile.ZRotation = 0;
            }
            else if (previous.Value.y == next.Value.y)
            {
                if (bulges.Contains(current.Value))
                {
                    tile.Content = TileContent.SnakesBulge;
                }
                else
                {
                    tile.Content = TileContent.SnakesBody;
                }
                tile.ZRotation = 90;
            }
            else
            {
                if (bulges.Contains(current.Value))
                {
                    tile.Content = TileContent.SnakesLBulged;
                }
                else
                {
                    tile.Content = TileContent.SnakesL;
                }
                if ((previous.Value.x > current.Value.x && next.Value.y < current.Value.y) || (next.Value.x > current.Value.x && previous.Value.y < current.Value.y))
                {
                    tile.ZRotation = 0;
                }
                else if ((previous.Value.x < current.Value.x && next.Value.y < current.Value.y) || (next.Value.x < current.Value.x && previous.Value.y < current.Value.y))
                {
                    tile.ZRotation = 90;
                }
                else if ((previous.Value.x < current.Value.x && next.Value.y > current.Value.y) || (next.Value.x < current.Value.x && previous.Value.y > current.Value.y))
                {
                    tile.ZRotation = 180;
                }
                else if ((previous.Value.x > current.Value.x && next.Value.y > current.Value.y) || (next.Value.x > current.Value.x && previous.Value.y > current.Value.y))
                {
                    tile.ZRotation = 270;
                }
                else
                {
                    tile.Content = TileContent.SnakesHead;
                }
            }
            previous = current;
            current = current.Previous;
        }

        var tailPosition = body.First.Value;
        var previousPosition = body.First.Next.Value;

        tile = board[tailPosition];
        tile.Content = TileContent.SnakesTail;

        if (previousPosition.y > tailPosition.y)
        {
            tile.ZRotation = 0;
        }
        else if (previousPosition.y < tailPosition.y)
        {
            tile.ZRotation = 180;
        }
        else if (previousPosition.x > tailPosition.x)
        {
            tile.ZRotation = 90;
        }
        else if (previousPosition.x < tailPosition.x)
        {
            tile.ZRotation = -90;
        }
    }


    public IntVector2 NextHeadPosition(IntVector2 direction)
    {
        return Head + new IntVector2(direction.x, -direction.y);
    }

    public IEnumerator<IntVector2> GetEnumerator()
    {
        return ((IEnumerable<IntVector2>)body).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<IntVector2>)body).GetEnumerator();
    }
}
