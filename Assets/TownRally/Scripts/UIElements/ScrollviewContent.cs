using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using TheraBytes.BetterUi;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class ScrollviewContent : MonoBehaviour
    {
        private RectTransform myRectTransform = null;
        private float contentWidth = 0f;
        private float contentHeight = 0f;

        //private void Awake()
        //{
        //    this.myRectTransform = this.GetComponent<RectTransform>();
        //}
        [Button("Update")] private void OnBtnUpdate()
        {
            if(this.myRectTransform == null)
            {
                this.myRectTransform = this.GetComponent<RectTransform>();
            }

            this.contentWidth = this.myRectTransform.rect.width;
            this.contentHeight = 0f;

            // get all active child elements
            List<RectTransform> rtChildren = this.myRectTransform.GetComponentsInChildren<RectTransform>().ToList();
            for(int i = 0; i < rtChildren.Count; i++)
            {
                if (rtChildren[i].gameObject.activeSelf)
                {
                    this.FitElement(rtChildren[i]);
                }
            }
        }

        private void FitElement(RectTransform rtChild)
        {
            RawImage rawImageChild = rtChild.GetComponent<RawImage>();
            if((rawImageChild != null) && (rawImageChild.texture != null))
            {
                // raw images keep their aspect ratios
                rtChild.anchorMin = new Vector2(0, 1);
                rtChild.anchorMax = new Vector2(0, 1);
                rtChild.pivot = new Vector2(0, 1);
                float heightMultiplier = (float)rawImageChild.texture.width / (float)rawImageChild.texture.height;
                float height = Mathf.Abs(this.contentWidth / heightMultiplier);
                Debug.Log("HEIGHT: " + height + " " + heightMultiplier + " " + rawImageChild.texture.width + " " + rawImageChild.texture.height);
                rtChild.anchoredPosition = new Vector2(0f, -this.contentHeight);
                rtChild.sizeDelta = new Vector2(this.contentWidth, height);
                
                this.contentHeight += height;
            }
            else
            {
                BetterTextMeshProUGUI tmpChild = rtChild.GetComponent<BetterTextMeshProUGUI>();
                if(tmpChild != null)
                {
                    rtChild.anchorMin = new Vector2(0, 1);
                    rtChild.anchorMax = new Vector2(0, 1);
                    rtChild.pivot = new Vector2(0, 1);
                    rtChild.sizeDelta = new Vector2(this.contentWidth, rtChild.sizeDelta.y);
                    float height = tmpChild.preferredHeight;

                    Debug.Log("HEIGGH T: " + this.contentHeight + " "  + height +  " "+ tmpChild.margin + " " + tmpChild.renderedHeight + " " + tmpChild.maxHeight + " " + tmpChild.preferredHeight + " " + tmpChild.minHeight);

                    rtChild.anchoredPosition = new Vector2(0f, -this.contentHeight);
                    rtChild.sizeDelta = new Vector2(rtChild.sizeDelta.x, height);
                    this.contentHeight += height;
                }
            }
            Debug.Log("CONTENT HEIGHT: " + this.contentHeight);
        }
    }
}
