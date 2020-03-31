using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContentSizeLimiter : UIBehaviour, ILayoutSelfController
{
    public float maxWidth = 100;

    //This handles horizontal aspects of the layout (derived from ILayoutController)
    public virtual void SetLayoutHorizontal()
    {
        //Move and Rotate the RectTransform appropriately
        UpdateRectTransform();
    }

    //This handles vertical aspects of the layout
    public virtual void SetLayoutVertical()
    {
        //Move and Rotate the RectTransform appropriately
        UpdateRectTransform();
    }

    //This tells when there is a change in the inspector
    #if UNITY_EDITOR
    protected override void OnValidate()
    {
        Debug.Log("Validate");
        //Update the RectTransform position, rotation and scale
        UpdateRectTransform();
    }

    #endif

    //This tells when there has been a change to the RectTransform's settings in the inspector
    protected override void OnRectTransformDimensionsChange()
    {
        //Update the RectTransform position, rotation and scale
        UpdateRectTransform();
    }

    void UpdateRectTransform()
    {
        //Fetch the RectTransform from the GameObject
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform.rect.width > this.maxWidth) {
            Debug.Log("rectTransform.rect.width: " + rectTransform.rect.width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.maxWidth);
        }
    }
}