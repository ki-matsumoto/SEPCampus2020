using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foo : MonoBehaviour
{
    [SerializeField] private ShapeCircle shape1 = default;
    [SerializeField] private ShapeCircle shape2 = default;

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
#if true
        var clr1 = shape1.HitTest(shape2) ? Color.red : Color.white;
        Gizmos.color = clr1;
        Gizmos.DrawLine(shape1.transform.position, shape2.transform.position);
#endif
    }
}
