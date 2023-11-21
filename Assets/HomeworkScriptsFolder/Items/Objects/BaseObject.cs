using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//Zabezpieczenie na OverlapSphere
[RequireComponent(typeof(Collider))]
//Zabezpieczenie do rzucania
[RequireComponent(typeof(Rigidbody))]
public class BaseObject : MonoBehaviour
{
    [SerializeField] protected ObjectInformationSO objectInfoSO;
    [SerializeField] protected float sphereItemDetectionRadius = 3f;
    private List<BaseObject> sameObjectsNearby = new List<BaseObject>();
    private Rigidbody rb;
    
    //Zamiast ręcznego ustawiania maski, policzyłbym bitmape żeby z każdym nowym prefabem tego nie robić
    [SerializeField] protected LayerMask objectLayerMask;
    
    protected int currentStackVal;
    protected int maxStackVal = 64;
    private bool isPickable = false;
    private Collider[] _hitColliders;

    protected void Awake()
    {
        _hitColliders = new Collider[maxStackVal];
        rb = GetComponent<Rigidbody>();
        currentStackVal = objectInfoSO.objectValue;
    }

    protected void Start()
    {
        ThrowVisual();
        StartCoroutine(PickupDelay());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, sphereItemDetectionRadius);
    }

    private void OnEnable()
    {
        ThrowVisual();
    }

    public void SetStackValue(int value)
    {
        currentStackVal = value;
    }

    protected virtual void Update()
    {
        SpinVisual();
        if (currentStackVal >= maxStackVal) return;
        MergeItems(GetSameNearbyItems());
    }

    public void MergeItems(List<BaseObject> baseObjects)
    {
        foreach (BaseObject baseObject in baseObjects)
        {
            //? StartCoroutine(MergeDelay());
            currentStackVal += baseObject.currentStackVal;
            if(baseObject.gameObject != null)
                Destroy(baseObject.gameObject);
        }
        baseObjects.Clear();
    }

    public List<BaseObject> GetSameNearbyItems()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, sphereItemDetectionRadius, _hitColliders, objectLayerMask);
        for (int i = 0; i < numColliders; i++)
        {
            if (_hitColliders[i].transform.root == transform) continue;
            if(_hitColliders[i].TryGetComponent<BaseObject>(out BaseObject baseObject))
            {
                if (sameObjectsNearby.Contains(baseObject)) continue;
                sameObjectsNearby.Add(baseObject);
            }
        }
        return sameObjectsNearby;
    }

    //Korutyna sprawia, że duża ilość przedmiotów w jednym miejscu nie spowoduje przyznania zbyt dużej wartości przedmiotom (dupe glitch)
    IEnumerator MergeDelay(float mergeDelayTime = .05f)
    {
        yield return new WaitForSeconds(mergeDelayTime);
    }

    IEnumerator PickupDelay(float pickupDelayTime = 1.5f)
    {
        yield return new WaitForSeconds(pickupDelayTime);
        isPickable = true;
    }
    
    public ObjectInformationSO GetObjectInfoSO()
    {
        return objectInfoSO;
    }

    public int GetStackValue()
    {
        return currentStackVal;
    }

    //Prawdopodobnie  te metode bym zdecouplował w jakimś innym skrypcie żeby oddzielić logic mechaniki
    //z logiką animacji itd.
    public void ThrowVisual()
    {
        Vector3 direction = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
        float force = Random.Range(15, 20);
        rb.AddForce(direction * force);
    }

    private void SpinVisual()
    {
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
    }

    public bool IsPickable()
    {
        return isPickable;
    }
}