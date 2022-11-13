using GeoCoordinatePortable;
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
            Waypoint = 2,
            DestinationCheck = 3,
        }

        [SerializeField] private MeshFilter meshFilter = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private Dictionary<Type, Mesh> models = new Dictionary<Type, Mesh>();
        [SerializeField] private Dictionary<Type, Color> modelColors = new Dictionary<Type, Color>();
        [SerializeField] private Dictionary<Type, AnimationClip> modelAnimation = new Dictionary<Type, AnimationClip>();
        //[SerializeField] private Dictionary<Type, float> modelScaling = new Dictionary<Type, float>();
        private Material meshMaterial = null;
        private OnlineMapsMarker3D marker = null;
        private string id = string.Empty;
        //private Transform myTransform = null;
        private GeoCoordinate geoCoordinate = new GeoCoordinate();

        internal Type VarOut_ObjectType { get; private set; }



        // TODO: put that somewhere else!!!
        internal bool stationDone = false;

        internal void Init(string id, OnlineMapsMarker3D marker, Type objectType, GeoCoordinate geoCoordinate)
        {
            //this.myTransform = this.GetComponent<Transform>();
            this.id = id;
            this.gameObject.name = id;
            this.marker = marker;
            this.VarOut_ObjectType = objectType;
            this.geoCoordinate = geoCoordinate;
            this.marker.range = new OnlineMapsRange(10, 20);
            this.meshMaterial = this.meshFilter.GetComponent<MeshRenderer>().material;
            this.SetType(objectType);
        }

        internal void SetType(Type objectType)
        {
            this.meshFilter.mesh = this.models[objectType];
            this.meshMaterial.color = this.modelColors[objectType];
            Debug.Log("MAP MARKER anim: " + modelAnimation[objectType].name);
            this.animator.Play(modelAnimation[objectType].name);
            //float scale = this.modelScaling[objectType];
            //this.myTransform.localScale = new Vector3(scale, scale, scale);
        }

        internal void SetPosition(GeoCoordinate geoCoordinate)
        {
            this.geoCoordinate = geoCoordinate;
            marker.SetPosition(geoCoordinate.Longitude, geoCoordinate.Latitude);
        }

        internal GeoCoordinate GetPosition()
        {
            return this.geoCoordinate;
        }
    }
}
