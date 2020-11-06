﻿using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

class ShapeCircle : Shape
{
    [SerializeField] public float radius = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    override public bool HitTest(Shape other)
    {
        return other.HitTest(this);
    }

    override public bool HitTest(ShapeCircle other)
    {
        // お互いの半径を足す
        var r = radius + other.radius;

        // 距離を求める
        float nx = transform.position.x - other.transform.position.x;
        float ny = transform.position.y - other.transform.position.y;
        Vector2 n = new Vector2(nx, ny);

        var d = Mathf.Sqrt(Vector2.Dot(n, n));
        return d < r;
    }

    override public bool HitTest(ShapeBox other)
    {
        Vector2 hsize = other.size * 0.5f;

        float nx = Mathf.Abs(transform.position.x - other.transform.position.x);
        float ny = Mathf.Abs(transform.position.y - other.transform.position.y);
        Vector2 n = new Vector2(nx, ny);

        if (nx < hsize.x) return ny < (radius + hsize.y);
        if (ny < hsize.y) return nx < (radius + hsize.x);

        Vector2 d = n - hsize;
        float len = Mathf.Sqrt(Vector2.Dot(d, d));
        return len <= radius;
    }

#if false
    override public bool HitTest(ShapeBox other)
    {
        Vector2 d = other.size / 2;
        float nx = Mathf.Abs(transform.position.x - other.transform.position.x);
        float ny = Mathf.Abs(transform.position.y - other.transform.position.y);
        bool flag1 = nx < d.x;
        bool flag2 = ny < d.y;

        if (flag1 && flag2)
        {
            return true;
        }

        if (flag2)
        {
            return (nx < (radius + other.size.x / 2));
        }
        else if (flag1)
        {
            return (ny < (radius + other.size.y / 2));
        }
        return Mathf.Sqrt(nx * nx + ny * ny) <= radius + Mathf.Sqrt(d.x * d.x + d.y * d.y);
    }
#endif

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
