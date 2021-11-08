using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [FirestoreData]
    public struct CountFirebase
    {
        [FirestoreProperty]
        public int Count { get; set; }

        [FirestoreProperty]
        public string UpdatedBy { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
