using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropObjects : MonoBehaviour
{
    private Vector3 mousePositions;
    [SerializeField] private DragAndDropObjects prefabObject;
    [SerializeField] private GameObject effectMerge;

    private Vector3 mousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
    private void OnMouseDown()
    {
        mousePositions = Input.mousePosition - mousePos();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.position += new Vector3(0, 0.5f, 0);
    }
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePositions);
        newPosition.y = transform.position.y; 
        transform.position = newPosition;
    }
    private void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        DragAndDropObjects dragObj = collision.gameObject.GetComponent<DragAndDropObjects>();
        if (dragObj != null)
        {
            if (this.GetInstanceID() < dragObj.GetInstanceID())
            {
                Vector3 mergePosition = (transform.position + dragObj.transform.position) / 2;
                Instantiate(effectMerge, mergePosition, Quaternion.identity);
                Instantiate(prefabObject, mergePosition, Quaternion.identity);

                Destroy(dragObj.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
