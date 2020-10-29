using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ShapeBox : Shape
{
    [SerializeField] public Vector2 size = Vector2.one;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

    override public bool HitTest(Shape other)
    {
        return other.HitTestShapeBox(this);
    }

    override public bool HitTestShapeCircle(ShapeCircle other)
    {
        return other.HitTestShapeBox(this);
    }

    override public bool HitTestShapeBox(ShapeBox other)
    {
        float x = transform.position.x - other.transform.position.x;
        float y = transform.position.y - other.transform.position.y;

        return ((size.x + other.size.x) < x) && ((size.y + other.size.y) < y);
    }
}
