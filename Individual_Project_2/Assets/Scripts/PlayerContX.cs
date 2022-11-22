using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContX : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private float moveSpeed = 0.1f;

    float distance = 1.1f;
    public GameObject selectedGO;
    public bool checkCollision = false;
    public bool isMouseDown = false;

    private int directionFlag = 1;
    public Camera mc;
    public Rigidbody rb;
    private float lx, ly, lz;
    private Vector3 origin;
    void Start()
    {
        if (mc == null) mc = Camera.main;
        if (rb == null) rb = GetComponent<Rigidbody>();
        origin = transform.position;

    }


    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = directionFlag * transform.forward;

        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(new Vector3(origin.x, origin.y, origin.z), directionFlag * direction, Color.yellow);

        if (isMouseDown && Physics.Raycast(ray, out RaycastHit h, distance))
        {
            GameObject go = h.collider.gameObject;
            if (go.CompareTag("Wall") || go.CompareTag("Player"))
            {
                checkCollision = true;
            }

        }

    }

    private void OnMouseDown()
    {
        RaycastHit hit = findingObjectRC();

        if (hit.collider.gameObject.CompareTag("Player"))
        {
            selectedGO = hit.collider.gameObject;
            Cursor.visible = false;


        }
        isMouseDown = true;
    }

    private void OnMouseDrag()
    {
        if ((origin.z - transform.position.z) <= 0)
            directionFlag = 1;
        else
            directionFlag = -1;


        if (!checkCollision && selectedGO != null)
        {
            lx = Input.mousePosition.x;
            ly = Input.mousePosition.y;
            lz = Input.mousePosition.z;
            Vector3 position = new Vector3(mc.WorldToScreenPoint(selectedGO.transform.position).x, ly,
                mc.WorldToScreenPoint(selectedGO.transform.position).z);
            Vector3 worldPos = mc.ScreenToWorldPoint(position);

            Vector3 subVecPosition = new Vector3(worldPos.x, 0.26f, worldPos.z);

            rb.MovePosition(subVecPosition);

        }
    }

    private void OnMouseUp()
    {

        checkCollision = false;

        rb.MovePosition(new Vector3(rb.transform.position.x,
           origin.y, rb.transform.position.z / 1.5f - directionFlag * 0.2f));

        rb.MovePosition(new Vector3(rb.transform.position.x,
            origin.y,
            Mathf.Round(rb.transform.position.z)));
        selectedGO = null;
        Cursor.visible = true;


        isMouseDown = false;

    }



    private RaycastHit findingObjectRC()
    {
        Vector2 mPosition = Input.mousePosition;
        Ray ray = mc.ScreenPointToRay(mPosition);
        Physics.Raycast(ray, out RaycastHit h);
        return h;

    }
}
