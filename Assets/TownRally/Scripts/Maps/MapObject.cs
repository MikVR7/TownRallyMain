using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class MapObject : SerializedMonoBehaviour
    {
        internal enum Type
        {
            Character = 0,
            GotoDestination = 1,
        }

        [SerializeField] private MeshFilter meshFilter = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private Dictionary<Type, Mesh> models = new Dictionary<Type, Mesh>();
        [SerializeField] private Dictionary<Type, Color> modelColors = new Dictionary<Type, Color>();
        [SerializeField] private Dictionary<Type, AnimationClip> modelAnimation = new Dictionary<Type, AnimationClip>();
        private Type objectType;
        private Material meshMaterial = null;
        private OnlineMapsMarker3D marker = null;

        internal void Init(OnlineMapsMarker3D marker, Type objectType)
        {
            this.marker = marker;
            this.objectType = objectType;
            this.marker.range = new OnlineMapsRange(10, 20);
            this.meshMaterial = this.meshFilter.GetComponent<MeshRenderer>().material;
            this.SetType(objectType);
        }

        internal void SetType(Type objectType)
        {
            this.meshFilter.mesh = this.models[objectType];
            this.meshMaterial.color = this.modelColors[objectType];
            this.animator.Play(modelAnimation[objectType].name);
        }
    }
}
