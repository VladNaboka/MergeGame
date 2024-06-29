using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SavedData
{
    public DragAndDropObjects dragObject;
    public Vector3 position;

    public SavedData(DragAndDropObjects dragObject, Vector3 position)
    {
        this.dragObject = dragObject;
        this.position = position;
    }
    public override string ToString()
    {
        return $"{dragObject.name} at position {position}";
    }
}
