using UnityEngine;

public class Trailer : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private GameObject target;
    private Collider2D collider2D;

    void Start(){
          collider2D = GetComponent<Collider2D>();
          
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector3.forward);

            if (hit.collider != null)
            {
                Debug.Log("Object Detected");
                target = hit.collider.gameObject;
                offset = target.transform.position - clickPosition;
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if (target == null)
            return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Constrain movement to steps of 1 unit
        float step = 2.0f;

        // Check x-axis movement
        if (target.transform.position.x + step < mousePosition.x)
        {
            target.transform.position += new Vector3(step, 0, 0);
        }
        else if (target.transform.position.x - step > mousePosition.x)
        {
            target.transform.position -= new Vector3(step, 0, 0);
        }

        // Check y-axis movement
        if (target.transform.position.y + step < mousePosition.y)
        {
            target.transform.position += new Vector3(0, step, 0);
        }
        else if (target.transform.position.y - step > mousePosition.y)
        {
            target.transform.position -= new Vector3(0, step, 0);
        }
    }

}
