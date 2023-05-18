using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Controller : MonoBehaviour
{
  
    private IntVector2 _lastDirection;

   
    public LinkedList<IntVector2> queue;

    public IntVector2 LastDirection
    {
        get
        {
            if (queue.Count == 0)
                return _lastDirection;

            return queue.Last.Value;
        }
    }

  
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard controls
        if (Input.GetKeyDown("up") && LastDirection != Vector2.down)
        {
            Enqueue(Vector2.up);
        }
        else if (Input.GetKeyDown("down") && LastDirection != Vector2.up)
        {
            Enqueue(Vector2.down);
        }
        else if (Input.GetKeyDown("left") && LastDirection != Vector2.right)
        {
            Enqueue(Vector2.left);
        }
        else if (Input.GetKeyDown("right") && LastDirection != Vector2.left)
        {
            Enqueue(Vector2.right);
        }
    }

    public void Move(Vector2 direction)
    {
        Enqueue(direction);

        if (direction == Vector2.up && LastDirection != Vector2.down)
        {
            Enqueue(Vector2.up);
        }
        else if (direction == Vector2.down  && LastDirection != Vector2.up)
        {
            Enqueue(Vector2.down);
        }
        else if (direction == Vector2.left && LastDirection != Vector2.right)
        {
            Enqueue(Vector2.left);
        }
        else if (direction == Vector2.right && LastDirection != Vector2.left)
        {
            Enqueue(Vector2.right);
        }

    }

   
    private void Enqueue(IntVector2 direction)
    {
        queue.AddLast(direction);
        _lastDirection = direction;
    }

   
    public IntVector2 NextDirection()
    {
        if (queue.Count == 0)
            return _lastDirection;

        var first = queue.First.Value;
        queue.RemoveFirst();

        return first;
    }

   
    public void Reset()
    {
        queue = new LinkedList<IntVector2>();
        Enqueue(Vector2.up);
    }
}
