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
        for (int i = 0; i < shapes.Count-1; i++)
        {
            int no1 = i;
            for (int j = i+1; j < shapes.Count; j++)
            {
                int no2 = j;
                bool hit = shapes[no1].HitTest(shapes[no2]);
                if( hit )
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(shapes[no1].transform.position, shapes[no2].transform.position);
                }
            }

            //text.text = string.Format("{0}", shapes[no1].transform.position.ToString());
        }
#endif
    }
}
