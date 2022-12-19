using System;
using Oculus.Interaction;
using Quinbay.Catalog.Data;
using UnityEngine;

public class Item : MonoBehaviour, IPointable
{
    [SerializeField] protected CatalogItem catalogItem;

    private IGrabbable grabbable;
    private IPointable pointable;

    private void Awake()
    {
        RecalculateTriggerBounds();
        grabbable = this.GetComponent<IGrabbable>();
        pointable = this.GetComponent<IPointable>();
    }

    private void OnEnable()
    {
        pointable.WhenPointerEventRaised += HandlePointerStateChanged;
    }

    private void OnDisable()
    {
        pointable.WhenPointerEventRaised -= HandlePointerStateChanged;
    }

    private void Update()
    {
        TryShowDetailsWhenFocussed();
    }

    public void RecalculateTriggerBounds()
    {
        // https://forum.unity.com/threads/getting-the-bounds-of-the-group-of-objects.70979/#post-6440477
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        Transform referenceTransform = this.transform.GetChild(0);
        RecurseEncapsulate(referenceTransform, ref bounds);

        void RecurseEncapsulate(Transform child, ref Bounds rollingBounds)
        {
            MeshFilter mesh = child.GetComponent<MeshFilter>();
            if (mesh)
            {
                Bounds lsBounds = mesh.sharedMesh.bounds;
                Vector3 wsMin = child.TransformPoint(lsBounds.center - lsBounds.extents);
                Vector3 wsMax = child.TransformPoint(lsBounds.center + lsBounds.extents);
                rollingBounds.Encapsulate(referenceTransform.InverseTransformPoint(wsMin));
                rollingBounds.Encapsulate(referenceTransform.InverseTransformPoint(wsMax));
            }

            foreach (Transform grandChild in child.transform)
            {
                RecurseEncapsulate(grandChild, ref rollingBounds);
            }
        }

        BoxCollider myCollider = this.GetComponent<BoxCollider>();
        myCollider.center = bounds.center;
        myCollider.size = 2f * (catalogItem?.InteractionTriggerScale ?? 1f) * bounds.extents;
    }
    
    // TODO: show item details when pointed at
    private void TryShowDetailsWhenFocussed()
    {
    }

    private void HandlePointerStateChanged(PointerEvent pointerEvent)
    {
        if (pointerEvent.Type == PointerEventType.Hover)
        {
            Debug.Log("I'm being hovered");
        }
        else
        {
            Debug.Log("I'm not being hovered");
        }
    }
    
    // TODO: make item grabbable by pinch
    // TODO: add item to cart
    public event Action<PointerEvent> WhenPointerEventRaised;
}