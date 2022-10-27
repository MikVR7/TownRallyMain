using System;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal abstract class AUIElement : MonoBehaviour
    {
        internal static readonly string KEY_PREFIX_IMG = "IMG";
        internal static readonly string KEY_PREFIX_TXT = "TXT";
        internal static readonly Dictionary<string, Type> UIELEMENT_TYPE =
            new Dictionary<string, Type>()
            {
                { KEY_PREFIX_IMG, typeof(UIElementInfoPicture) },
                { KEY_PREFIX_TXT, typeof(UIElementInfoText) },
            };

        internal enum EType
        {
            Text = 0,
            Picture = 1,
        }

        internal abstract EType ElementType { get; } 

        protected string elementData = string.Empty;

        internal virtual void Init(string elementData)
        {
            this.elementData = elementData;
        }

        internal virtual void DestroyElement()
        {
            Destroy(this.gameObject);
        }
    }
}
