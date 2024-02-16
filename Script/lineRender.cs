
using UnityEngine;
using System.Collections.Generic;
 public class Vector3EqualityComparer : IEqualityComparer<Vector3>
    {
        public bool Equals(Vector3 v1, Vector3 v2)
        {
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }

        public int GetHashCode(Vector3 vector)
        {
            return vector.GetHashCode();
        }
    }
public class lineRender : MonoBehaviour
{
    private bool isDragging = false;
    private GameObject target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector3.forward);

            if (hit.collider != null)
            {
                target = hit.collider.gameObject;
                target.GetComponent<dotStuff>().offset = target.transform.position - clickPosition;
                isDragging = true;
               //   target.GetComponent<dotStuff>().pointsList.Add(target.transform.position);
                // MoveObject(target);
                // target.GetComponent<dotStuff>().UpdateLineRenderer();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {   
            isDragging = false;
        }
        if(isDragging && target){
             MoveObject(target);
                target.GetComponent<dotStuff>().UpdateLineRenderer();
        }
    }

    void MoveObject(GameObject target)
    {
        if (target == null)
            return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float step = 2f;

        if (target.transform.position.x + step < mousePosition.x)
        {
            target.transform.position += new Vector3(step, 0, 0);
        }
        else if (target.transform.position.x - step > mousePosition.x)
        {
            target.transform.position -= new Vector3(step, 0, 0);
        }
        if (target.transform.position.y + step < mousePosition.y)
        {
            target.transform.position += new Vector3(0, step, 0);
        }
        else if (target.transform.position.y - step > mousePosition.y)
        {
            target.transform.position -= new Vector3(0, step, 0);
        }else{
            Debug.Log("this thing is not happening");
        }

        Vector3 currentPosition = new Vector3(target.transform.position.x, target.transform.position.y, 0);

        int overlapIndex = target.GetComponent<dotStuff>().pointsList.FindIndex(point => point == currentPosition);
        Debug.Log(overlapIndex);
        if (overlapIndex != -1 && target.GetComponent<dotStuff>().pointsList.Count>3)
        {
            target.GetComponent<dotStuff>().pointsList.RemoveRange(overlapIndex, target.GetComponent<dotStuff>().pointsList.Count - overlapIndex);

            List<Vector3> keysToRemove = new List<Vector3>();
            foreach (var entry in target.GetComponent<dotStuff>().vectorMap)
            {
                if (target.GetComponent<dotStuff>().pointsList.IndexOf(entry.Key) > overlapIndex)
                {
                    keysToRemove.Add(entry.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                target.GetComponent<dotStuff>().vectorMap.Remove(key);
            }
        }
        Debug.Log("overlapping index found");

        target.GetComponent<dotStuff>().pointsList.Add(currentPosition);
        target.GetComponent<dotStuff>().vectorMap[currentPosition] = 7;
    }
}




// using UnityEngine;
// using System.Collections.Generic;
//  public class Vector3EqualityComparer : IEqualityComparer<Vector3>
//     {
//         public bool Equals(Vector3 v1, Vector3 v2)
//         {
//             return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
//         }

//         public int GetHashCode(Vector3 vector)
//         {
//             return vector.GetHashCode();
//         }
//     }
// public class lineRender : MonoBehaviour
// {
//     // public float thickness = 0.2f;
//     // private List<Vector3> pointsList = new List<Vector3>();
//     // private LineRenderer lineRenderer;
//     private bool isDragging = false;
//     // private Vector3 offset;
//     // private GameObject target;
//     // private Dictionary<Vector3, int> vectorMap = new Dictionary<Vector3, int>(new Vector3EqualityComparer());
//     void Start()
//     {
//         // lineRenderer = GetComponent<LineRenderer>();
//         // lineRenderer.startWidth = thickness;
//         // lineRenderer.endWidth = thickness;
//         // lineRenderer.useWorldSpace = true;
//     }

//     void Update()
//     {
//         // if (isDragging)
//         // {
//         //     MoveObject();
//         // }

//         // UpdateLineRenderer();

//         if (Input.GetMouseButtonDown(0))
//         {
//             Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector3.forward);

//             if (hit.collider != null)
//             {
//                 GameObject target = hit.collider.gameObject;
//                 target.GetComponent<dotStuff>().offset = target.transform.position - clickPosition;
//                 isDragging = true;
//                 MoveObject(target);
//             }
//         }

//         if (Input.GetMouseButtonUp(0))
//         {
//             isDragging = false;
//         }
//     }

//     // void UpdateLineRenderer(GameObject target)
//     // {
//     //     target.GetComponent<LineRenderer>().positionCount = pointsList.Count;
//     //     target.GetComponent<LineRenderer>().SetPositions(pointsList.ToArray());
//     // }

//     void MoveObject(GameObject target)
//     {
//         if (target == null)
//             return;

//         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//         float step = 1f;

//         if (target.transform.position.x + step < mousePosition.x )
//         {
//             target.transform.position += new Vector3(step, 0, 0);
//         }
//         else if (target.transform.position.x - step > mousePosition.x  )
//         {
//             target.transform.position -= new Vector3(step, 0, 0);
//         }
//         if (target.transform.position.y + step < mousePosition.y   )
//         {
//             target.transform.position += new Vector3(0, step, 0);
//         }
//         else if (target.transform.position.y - step > mousePosition.y )
//         {
//             target.transform.position -= new Vector3(0, step, 0);
//         }

//         Vector3 currentPosition = new Vector3(target.transform.position.x, target.transform.position.y, 0);

//             int overlapIndex = target.GetComponent<dotStuff>().pointsList.FindIndex(point => point == currentPosition);
//             Debug.Log(overlapIndex);
//             if (overlapIndex != -1)
//             {
//                 target.GetComponent<dotStuff>().pointsList.RemoveRange(overlapIndex, target.GetComponent<dotStuff>().pointsList.Count - overlapIndex);

//                 List<Vector3> keysToRemove = new List<Vector3>();
//                 foreach (var entry in target.GetComponent<dotStuff>().vectorMap)
//                 {
//                     if (target.GetComponent<dotStuff>().pointsList.IndexOf(entry.Key) > overlapIndex)
//                     {
//                         keysToRemove.Add(entry.Key);
//                     }
//                 }

//                 foreach (var key in keysToRemove)
//                 {
//                     target.GetComponent<dotStuff>().vectorMap.Remove(key);
//                 }
//             }
//             Debug.Log("overllaping index found");
        
//         target.GetComponent<dotStuff>().pointsList.Add(currentPosition);
//         target.GetComponent<dotStuff>().vectorMap[currentPosition] = 7;
//         //UpdateLineRenderer(target);
//     }


    
// }
