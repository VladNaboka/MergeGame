using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalsScript", menuName = "Add Animals/Animals")]
public class AnimalScript : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private GameObject _prefab;

    public int id => this._id;
    public GameObject prefab => this._prefab;
}
