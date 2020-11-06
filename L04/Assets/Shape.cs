using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Shape : MonoBehaviour
{
    // Start is called before the first frame update

    abstract public bool HitTest(Shape other);

    abstract public bool HitTest(ShapeCircle other);

    abstract public bool HitTest(ShapeBox other);

    abstract public bool HitTest(ShapeCapsule other);

    //    abstract protected bool HitTestShapeSphere(Shape other);

    //    abstract protected bool HitTestShapeAABB(Shape other);
}
