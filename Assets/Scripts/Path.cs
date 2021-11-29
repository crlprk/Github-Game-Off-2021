using UnityEngine;


// References:
// https://stackoverflow.com/questions/57538286/unity-follow-the-leader-behavior

public struct Point 
{
    public Point(Vector2 pos, Quaternion rot)
    {
        this.pos = pos;
        this.rot = rot;
    }
    public Vector2 pos;
    public Quaternion rot;
}

public class Path
{
    public Point[] points { get; private set; }
    public int capacity => points.Length;

    int head;

    public Path(int capacity)
    {
        head = 0;
        points = new Point[capacity];
    }

    public void Resize()
    {
        Point[] temp = new Point[capacity * 2];

        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = i < capacity ? Head(i + 1) : Tail();
        }

        head = capacity - 1;

        points = temp;
    }

    public void Add(Vector2 pos, Quaternion rot)
    {
        int prev = Mod(head, capacity);

        Next();

        int next = Mod(head, capacity);
        Point temp = new Point(pos, rot);
        points[next] = temp;
    }

    public void Remove(int index)
    {
        Point[] temp = new Point[capacity - 1];
        for (int i = 0; i < index; i++)
        {
            temp[i] = points[i];
        }
        for (int j = index + 1; j < capacity; j++)
        {
            temp[j] = points[j];
        }

        points = temp;
    }

    public Point Head()
    {
        return points[head];
    }

    public Point Head(int index)
    {
        return points[Mod(head + index, capacity)];
    }

    public Point Tail()
    {
        return points[Mod(head + 1, capacity)];
    }

    public Point Tail(int index)
    {
        return points[Mod(head + 1 + index, capacity)];
    }

    void Next()
    {
        head++;
        head %= capacity;
    }

    int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}