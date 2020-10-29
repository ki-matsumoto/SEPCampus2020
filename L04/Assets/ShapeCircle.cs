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
        float nx = Mathf.Abs(transform.position.x - other.transform.position.x);
        float ny = Mathf.Abs(transform.position.y - other.transform.position.y);
        return (nx < (radius + other.size.x / 2)) && (ny < (radius + other.size.y / 2));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
