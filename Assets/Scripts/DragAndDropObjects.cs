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

    [SerializeField] private Vector3 minBounds;
    [SerializeField] private Vector3 maxBounds;

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
        newPosition = BoundsDrag(newPosition);
        transform.position = newPosition;
    }

    private void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private Vector3 BoundsDrag(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        position.z = Mathf.Clamp(position.z, minBounds.z, maxBounds.z);
        return position;
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
                        SoundManager.instance.Play("Merge");
                        Vector3 mergePosition = (transform.position + dragObj.transform.position) / 2;
                        Instantiate(effectMerge, mergePosition, Quaternion.identity);
                        Instantiate(prefabObject, mergePosition, Quaternion.identity);

                        Destroy(dragObj.gameObject);
                        Destroy(gameObject);

                        if (saveSystem != null)
                        {
                            saveSystem.UpdateDragObjectsArray();
                        }

                        if(Tutorial.instance.index == 1)
                        {
                            Tutorial.instance.NextTutorial();
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
