using System;
using Quinbay.Catalog.Data;
using UnityEngine;

// TODO: make item grabbable by pinch
[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour
{
    [SerializeField] protected CatalogItem catalogItem;

    private void Awake()
    {
        RecalculateTriggerBounds();
    }

    // ReSharper disable Unity.PerformanceAnalysis
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
}
