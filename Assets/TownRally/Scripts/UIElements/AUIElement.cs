using System;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal abstract class AUIElement : MonoBehaviour
    {
        internal static readonly Dictionary<Rally.DescriptionType, Type> UIELEMENT_TYPE =
            new Dictionary<Rally.DescriptionType, Type>()
            {
                { Rally.DescriptionType.Image, typeof(UIElementInfoPicture) },
                { Rally.DescriptionType.Text, typeof(UIElementInfoText) },
            };

        internal Rally.DescriptionType ElementType { get; set; } 

        protected string descriptionData = string.Empty;
        protected int elementIndex = 0;

        internal virtual void Init(int elementIndex, Rally.DescriptionType descriptionType, string descriptionData)
        {
            this.descriptionData = descriptionData;
            this.ElementType = descriptionType;
            this.elementIndex = elementIndex;
            this.gameObject.name = ElementType.ToString().ToLower() + "_" + elementIndex;
        }

        internal virtual void DestroyElement()
        {
            Destroy(this.gameObject);
        }
    }
}
