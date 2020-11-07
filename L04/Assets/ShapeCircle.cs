using System.Collections;
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
        // 距離を求める
        float nx = transform.position.x - other.transform.position.x;
        float ny = transform.position.y - other.transform.position.y;
        Vector2 n = new Vector2(nx, ny);

        var d = Mathf.Sqrt(Vector2.Dot(n, n));
        return d < radius + other.radius;
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

    override public bool HitTest(ShapeCapsule other)
    {
        var mat = Matrix4x4.Rotate(other.transform.rotation);
        var h = mat.MultiplyVector(new Vector3(other.length * 0.5f, 0, 0));
        // 2点の座標
        Vector3 p1 = other.transform.position - h;
        Vector3 p2 = other.transform.position + h;


        var ab = p2 - p1;
        var ac = new Vector3(transform.position.x, transform.position.y) - p1;

        var t = Vector2.Dot(ab, ac);
        Vector2 c;
        if (t <= 0)
        {
            c = p1;
        }
        else
        {
            float denom = Vector2.Dot(ab, ab);
            if (t >= denom)
            {
                c = p2;
            }
            else
            {
                t /= denom;
                c = p1 + ab * t;
            }
        }
        var tmp = c - new Vector2(transform.position.x, transform.position.y);
        return Mathf.Sqrt(Vector2.Dot(tmp, tmp)) <= radius + other.radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
