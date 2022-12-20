using System;
using Oculus.Interaction;
using Quinbay.Catalog.Data;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public static UnityAction<Item> OnItemHovered;

    [SerializeField] protected CatalogItem catalogItem;
    public CatalogItem CatalogItem => catalogItem;

    private void Awake()
    {
        RecalculateTriggerBounds();
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
    
    #region Show item details when hovered

    public void HandleOnHover()
    {
        OnItemHovered?.Invoke(this);
    }
    
    #endregion
    
    // TODO: make item grabbable
    // TODO: add item to cart
}