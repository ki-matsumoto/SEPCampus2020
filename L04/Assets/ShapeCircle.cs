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
        var nx = transform.position.x - other.transform.position.x;
        var ny = transform.position.y - other.transform.position.y;
        var d = Mathf.Sqrt(nx * nx + ny * ny);

        return d < r;
    }

    override public bool HitTest(ShapeBox other)
    {
        Vector2 d = other.size / 2;
        float x1 = other.transform.position.x - d.x;
        float x2 = other.transform.position.x + d.x;
        float y1 = other.transform.position.y - d.y;
        float y2 = other.transform.position.y + d.y;

        bool flag1 = x1 <= transform.position.x && transform.position.x <= x2;
        bool flag2 = y1 <= transform.position.y && transform.position.y <= y2;

        if (flag1 && flag2)
        {
            return true;
        }

        float nx = Mathf.Abs(transform.position.x - other.transform.position.x);
        float ny = Mathf.Abs(transform.position.y - other.transform.position.y);
        if (flag2)
        {
            return (nx < (radius + other.size.x / 2));
        }
        else if ( flag1 )
        {
            return (ny < (radius + other.size.y / 2));
        }
        return  Mathf.Sqrt(nx * nx + ny * ny) <= Mathf.Sqrt(d.x * d.x + d.y * d.y) + radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
