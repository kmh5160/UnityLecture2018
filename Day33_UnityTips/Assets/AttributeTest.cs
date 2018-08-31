using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
//public class Person
//{
//    public string address;
//}

public class AttributeTest : MonoBehaviour {
    [Range(0, 10)]
    public int DamageRange = 10;

    [Header("Health Settings")]
    public int health;
    public int maxHealth;

    [Header("Damage Settings")]
    public int Damage;

    [HideInInspector]
    public float hiddenValue;

    [SerializeField]
    int privateInt = 100;

    //public Person person;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
