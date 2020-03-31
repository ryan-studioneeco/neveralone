//Copyright (c) 2018 - @QuantumCalzone
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LayoutElementExtendedValue
{
    public enum ReferenceTypes { None, Width, Height }

    public bool enabled = false;
    public RectTransform reference = null;
    public ReferenceTypes referenceType = ReferenceTypes.None;
    public float referenceDelta = 0;
    public float targetValue = 0;

    public void ProcessTargetValue(Object context)
    {
        if (!reference && referenceType != ReferenceTypes.None)
        {
            Debug.Log("This needs a reference to process your target Layout values", context);
            return;
        }

        switch (referenceType)
        {
            case ReferenceTypes.None:
                break;
            case ReferenceTypes.Width:
                targetValue = reference.rect.width;
                break;
            case ReferenceTypes.Height:
                targetValue = reference.rect.height;
                break;
            default:
                Debug.LogError(string.Format("No support for a referenceType of: {0}", referenceType), context);
                break;
        }

        targetValue += referenceDelta;
    }
}

[AddComponentMenu("Layout/Extended/Layout Element Extended")]
[RequireComponent(typeof(RectTransform))]
public class LayoutElementExtended : LayoutElement
{
    #region Fields

    public LayoutElementExtendedValue minWidthExtended = new LayoutElementExtendedValue();
    public LayoutElementExtendedValue minHeightExtended = new LayoutElementExtendedValue();
    public LayoutElementExtendedValue preferredWidthExtended = new LayoutElementExtendedValue();
    public LayoutElementExtendedValue preferredHeightExtended = new LayoutElementExtendedValue();
    public LayoutElementExtendedValue flexibleWidthExtended = new LayoutElementExtendedValue();
    public LayoutElementExtendedValue flexibleHeightExtended = new LayoutElementExtendedValue();

    public LayoutElementExtendedValue maxWidth = new LayoutElementExtendedValue();
    public LayoutElementExtendedValue maxHeight = new LayoutElementExtendedValue();

    #endregion

    #region Properties

    private RectTransform rectTransform = null;
    public RectTransform GetRectTransform {
        get {
            if (!rectTransform) rectTransform = GetComponent<RectTransform>();
            return rectTransform;
        }
    }

    public float Width {
        get {
            var valueToReturn = 0f;

            if (minWidthExtended.enabled && !preferredWidthExtended.enabled)
            {
                valueToReturn = MinWidth;
            }
            else if (!minWidthExtended.enabled && preferredWidthExtended.enabled)
            {
                valueToReturn = PreferredWidth;
            }
            else if (minWidthExtended.enabled && preferredWidthExtended.enabled)
            {
                var min = MinWidth;
                var preferred = PreferredWidth;

                if (preferred > min)
                {

                    valueToReturn = preferred;

                }
                else valueToReturn = min;

            }

            return valueToReturn;
        }
    }

    public float Height {
        get {
            var valueToReturn = 0f;

            if (minHeightExtended.enabled && !preferredHeightExtended.enabled)
            {
                valueToReturn = MinHeight;
            }
            else if (!minHeightExtended.enabled && preferredHeightExtended.enabled)
            {
                valueToReturn = PreferredHeight;
            }
            else if (minHeightExtended.enabled && preferredHeightExtended.enabled)
            {
                var min = MinHeight;
                var preferred = PreferredHeight;

                if (preferred > min)
                {

                    valueToReturn = preferred;

                }
                else valueToReturn = min;

            }

            return valueToReturn;
        }
    }

    #region Min

    private float MinWidth {
        get {

            var valueToReturn = 0f;

            if (minWidthExtended.enabled)
            {
                minWidthExtended.ProcessTargetValue(gameObject);

                valueToReturn = minWidthExtended.targetValue;

                if (maxWidth.enabled)
                {
                    maxWidth.ProcessTargetValue(gameObject);

                    if (valueToReturn > maxWidth.targetValue)
                    {
                        valueToReturn = maxWidth.targetValue;
                    }
                }

            }

            return valueToReturn;
        }
    }

    private float MinHeight {
        get {
            var valueToReturn = 0f;

            if (minHeightExtended.enabled)
            {

                minHeightExtended.ProcessTargetValue(gameObject);

                valueToReturn = minHeightExtended.targetValue;

                if (maxHeight.enabled)
                {

                    maxHeight.ProcessTargetValue(gameObject);

                    if (valueToReturn > maxHeight.targetValue)
                        valueToReturn = maxHeight.targetValue;

                }

            }

            return valueToReturn;
        }
    }

    #endregion

    #region Preferred

    private float PreferredWidth {
        get {
            float preferredWidth = 0f;

            if (preferredWidthExtended.enabled)
            {
                preferredWidthExtended.ProcessTargetValue(gameObject);

                preferredWidth = preferredWidthExtended.targetValue;

                if (maxWidth.enabled)
                {

                    maxWidth.ProcessTargetValue(gameObject);

                    if (preferredWidth > maxWidth.targetValue)
                        preferredWidth = maxWidth.targetValue;

                }

            }

            return preferredWidth;
        }
    }

    private float PreferredHeight {
        get {
            var preferredHeight = 0f;

            if (preferredHeightExtended.enabled)
            {
                preferredHeightExtended.ProcessTargetValue(gameObject);

                preferredHeight = preferredHeightExtended.targetValue;

                if (maxHeight.enabled)
                {

                    maxHeight.ProcessTargetValue(gameObject);

                    if (preferredHeight > maxHeight.targetValue)
                    {
                        preferredHeight = maxHeight.targetValue;
                    }
                }
            }

            return preferredHeight;
        }
    }

    #endregion

    #region Flexible

    private float FlexibleWidth {
        get {
            var flexibleWidth = 0f;

            if (flexibleWidthExtended.enabled)
            {
                flexibleWidthExtended.ProcessTargetValue(gameObject);

                flexibleWidth = flexibleWidthExtended.targetValue;
            }

            return flexibleWidth;
        }
    }

    private float FlexibleHeight {
        get {
            var flexibleHeight = 0f;

            if (flexibleHeightExtended.enabled)
            {
                flexibleHeightExtended.ProcessTargetValue(gameObject);

                flexibleHeight = flexibleHeightExtended.targetValue;
            }

            return flexibleHeight;
        }
    }

    #endregion

    #endregion

    #region Unity Methods

    public override void CalculateLayoutInputHorizontal()
    {
        minWidth = minWidthExtended.enabled ? MinWidth : -1;
        preferredWidth = preferredWidthExtended.enabled ? PreferredWidth : -1;
        flexibleWidth = flexibleWidthExtended.enabled ? FlexibleWidth : -1;

        base.CalculateLayoutInputHorizontal();
    }

    public override void CalculateLayoutInputVertical()
    {
        minHeight = minHeightExtended.enabled ? MinHeight : -1;
        preferredHeight = preferredHeightExtended.enabled ? PreferredHeight : -1;
        flexibleHeight = flexibleHeightExtended.enabled ? FlexibleHeight : -1;

        base.CalculateLayoutInputVertical();
    }

#if UNITY_EDITOR

    private void Update()
    {
        CalculateLayoutInputHorizontal();
        CalculateLayoutInputVertical();
    }

#endif

    #endregion
}
