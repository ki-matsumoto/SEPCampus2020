using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCircle : MonoBehaviour
{
    [SerializeField] private float radius = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool HitTest(ShapeCircle other)
    {
        // お互いの半径を足す
        var r = radius + other.radius;

        // 距離を求める
        var x = transform.position.x - other.transform.position.x;
        var y = transform.position.y - other.transform.position.y;
        var d = Mathf.Sqrt( x * x + y * y );

        return d < r ;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
