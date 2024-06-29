using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropObjects : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private DragAndDropObjects prefabObject;
    [SerializeField] private GameObject effectMerge;
    public int id;

    private SaveSystem saveSystem;

    private void Awake()
    {
        saveSystem = FindObjectOfType<SaveSystem>();
    }

    private void OnMouseDown()
    {
        _offset = transform.position - GetMouseWorldPosition();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.position += new Vector3(0, 0.5f, 0);
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPosition() + _offset;
        newPosition.y = transform.position.y; 
        transform.position = newPosition;
    }

    private void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnCollisionStay(Collision collision)
    {
        DragAndDropObjects dragObj = collision.gameObject.GetComponent<DragAndDropObjects>();
        if (dragObj != null)
        {
            if (this.id == dragObj.id)
            {
                if (this.GetInstanceID() < dragObj.GetInstanceID())
                {
                    if (prefabObject != null)
                    {
                        Vector3 mergePosition = (transform.position + dragObj.transform.position) / 2;
                        Instantiate(effectMerge, mergePosition, Quaternion.identity);
                        Instantiate(prefabObject, mergePosition, Quaternion.identity);

                        Destroy(dragObj.gameObject);
                        Destroy(gameObject);

                        if (saveSystem != null)
                        {
                            saveSystem.UpdateDragObjectsArray();
                        }
                    }
                    else
                    {
                        Debug.Log("Некуда большеее");
                    }
                }
            }
        }
    }
}
