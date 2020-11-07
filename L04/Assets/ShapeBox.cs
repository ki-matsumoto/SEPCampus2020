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
        return other.HitTest(this);
    }

    override public bool HitTest(ShapeCircle other)
    {
        return other.HitTest(this);
    }

    override public bool HitTest(ShapeBox other)
    {
        float nx = Mathf.Abs(transform.position.x - other.transform.position.x);
        float ny = Mathf.Abs(transform.position.y - other.transform.position.y);

        return (nx < (size.x + other.size.x) / 2) && (ny < (size.y + other.size.y) / 2);
    }
    override public bool HitTest(ShapeCapsule other)
    {
        return false;
    }
}
