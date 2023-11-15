using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class ObjectInformationSO : ScriptableObject
{
        public string objectName;
        public GameObject objectPrefab;
        public Sprite objectImage;
        public int objectValue;
        public int objectID;
}
