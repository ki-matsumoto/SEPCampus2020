using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foo : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text text;
    [SerializeField] private List<Shape> shapes = default;


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
        for (int i = 0; i < shapes.Count; i++)
        {
            int no1 = i;
            int no2 = i + 1;
            if (no2 == shapes.Count) no2 = 0;
            var clr1 = shapes[no1].HitTest(shapes[no2]) ? Color.red : Color.white;
            Gizmos.color = clr1;
            Gizmos.DrawLine(shapes[no1].transform.position, shapes[no2].transform.position);

            //text.text = string.Format("{0}", shapes[no1].transform.position.ToString());
        }
#endif
    }
}
