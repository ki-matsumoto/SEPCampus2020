using System.Collections;
using System.Collections.Generic;
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
        return other.HitTestShapeCircle(this);
    }

    override public bool HitTestShapeCircle(ShapeCircle other)
    {
        // お互いの半径を足す
        var r = radius + other.radius;

        // 距離を求める
        var x = transform.position.x - other.transform.position.x;
        var y = transform.position.y - other.transform.position.y;
        var d = Mathf.Sqrt(x * x + y * y);

        return d < r;
    }
    override public bool HitTestShapeBox(ShapeBox other)
    {
        float x = transform.position.x - other.transform.position.x;
        float y = transform.position.y - other.transform.position.y;

        return ((radius + other.size.x) < x) && ((radius + other.size.y) < y);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
