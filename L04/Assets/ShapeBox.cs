using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBox : MonoBehaviour
{
    [SerializeField] private Vector2 size = Vector2.one;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool HitTest(ShapeBox other)
    {
        float x = transform.position.x - other.transform.position.x;
        float y = transform.position.y - other.transform.position.y;

        return ((size.x + other.size.x)<x) && ((size.y + other.size.y)<y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

}
