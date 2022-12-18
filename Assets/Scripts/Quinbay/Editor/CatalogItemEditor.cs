using System;
using Quinbay.Catalog.Data;
using UnityEditor;
using UnityEngine;

namespace Quinbay.Editor
{
    [CustomEditor(typeof(Item))]
    [CanEditMultipleObjects]
    public class CatalogItemEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Set Trigger Bounds for Interaction"))
            {
                Item item = target as Item;
                item!.RecalculateTriggerBounds();
            }
        }
    }
}
