using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

class ShapeCapsule : Shape
{
    [SerializeField] public float length = 10;
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
        return other.HitTest(this);
    }

    override public bool HitTest(ShapeBox other)
    {
        return other.HitTest(this);
    }

    override public bool HitTest(ShapeCapsule other)
    {
        return false;
    }

    private void OnDrawGizmos()
    {
        var mat = Matrix4x4.Rotate(transform.rotation);
        // 高さ
        var h = mat.MultiplyVector(new Vector3(length * 0.5f, 0, 0));
        // 幅
        var w = mat.MultiplyVector(new Vector3(0, radius, 0));
        // 2点の座標
        Vector3 p1 = transform.position - h;
        Vector3 p2 = transform.position + h;

        Gizmos.DrawWireSphere(p1, radius);
        Gizmos.DrawWireSphere(p2, radius);
        Gizmos.DrawLine(p1 - w, p2 - w);
        Gizmos.DrawLine(p1 + w, p2 + w);
    }

}
