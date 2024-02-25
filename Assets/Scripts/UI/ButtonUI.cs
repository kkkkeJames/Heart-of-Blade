using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// The wrapper class on Unity Button.
/// Provides API for enable, disable, add/remove listeners.
/// Implements animations on select, hover, and click. 
/// </summary>

[RequireComponent(typeof(Button))]
public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private Button button;
    
    [Header("Text")]
    [SerializeField] private Color textColorTransparent;
    [SerializeField] private Color textColorOpaque;
    [SerializeField] public TextMeshProUGUI buttonText;

    [Header("Text Fade Animation")] 
    private bool hasText;
    private readonly float textAlphaPeriod = 1.5f;
    private readonly float textAlphaMultiplier = 0.4f;
    private Coroutine textAlphaCycleOnSelect;
    
    [Header("Button Scale")]
    private readonly float scaleAmount = 1.1f;
    private readonly float scaleAnimationTime = .1f;
    private Coroutine scaleOnSelect;
    private Coroutine scaledownOnDeSelect;
    private Vector3 initialScale;
    private Vector3 endScale;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        hasText = buttonText;
    }

    private void Start()
    {
        initialScale = transform.localScale;
        endScale = initialScale * scaleAmount;

        AddDefaultListeners();
    }
    
    #region API
    
    /// Disables a button.  
    /// <param name="setTransparent">whether to set the text transparent</param>
    public void Disable(bool setTransparent = true)
    {
        button.interactable = false;
        if(setTransparent)
            buttonText.color = textColorTransparent;
        
        if (scaleOnSelect != null)
            StopCoroutine(scaleOnSelect);
        if (hasText && textAlphaCycleOnSelect != null)
            StopCoroutine(textAlphaCycleOnSelect);
    }

    /// Toggles a button to be interactive, set its color to textColorOpaque 
    public void Enable()
    {
        button.interactable = true;
        buttonText.color = textColorOpaque;
    }
    
    /// Adds a listener to the Button.
    /// <param name="call">The delayed call</param>
    public void AddListener(Action call)
    {
        button.onClick.AddListener(() => call());
    }
    
    /// Removes all *added* listeners to the button.
    /// <remarks>Default listeners include line scale animations, onclick/hover audio, etc</remarks> 
    /// <param name="removeDefault">if true, EVERY listener will be removed</param>
    public void RemoveAllListeners(bool removeDefault=false)
    {
        button.onClick.RemoveAllListeners();
        if (!removeDefault)
            AddDefaultListeners();
    }

    /// Sets the button text
    /// <param name="text">The text to set to the button</param>
    public void SetText(string text)
    {
        buttonText.text = text;
    }
    
    #endregion API
    
    #region ANIMATION COROUTINES
    
    /// animates a scaled up effect on the button
    private IEnumerator ScaleSelection(bool startAnimation)
    {
        float time = 0;
        
        if (!startAnimation) // scale down animation
        {
            while (time < scaleAnimationTime)
            {
                time += Time.deltaTime;
                
                float eval = time / scaleAnimationTime;
                transform.localScale = Vector3.Lerp(transform.localScale, initialScale, eval);
                
                yield return null;
            }
            scaledownOnDeSelect = null;
        }
        else
        {
            while (time < scaleAnimationTime)
            {
                time += Time.deltaTime;
                
                float eval = time / scaleAnimationTime;
                transform.localScale = Vector3.Lerp(transform.localScale, endScale, eval);
                
                yield return null;
            }
            scaleOnSelect = null;
        }
    }

    private IEnumerator TextAlphaCycle()
    {
        float t = 0;
        while (true)
        {
            t += Time.deltaTime / textAlphaPeriod;
            t %= 1;
            buttonText.alpha = Mathf.Lerp(1, textAlphaMultiplier, StaticInfoManager.Instance.TEXT_ALPHA_CYCLE_CURVE.Evaluate(t));
            yield return null;
        }
    }
    
    #endregion ANIMATION COROUTINES
    
    #region IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            eventData.selectedObject = gameObject;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable)
        {
            eventData.selectedObject = null;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (!button || !button.interactable || scaleOnSelect != null)
            return;
        
        DoSelect();
        
        if(hasText)
            textAlphaCycleOnSelect = StartCoroutine(TextAlphaCycle());
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!button || !button.interactable || scaledownOnDeSelect != null)
            return;
        
        DoDeselect();

        if (hasText && textAlphaCycleOnSelect != null)
        {
            StopCoroutine(textAlphaCycleOnSelect);
            buttonText.alpha = 1;
        }
    }

    private void DoSelect()
    {
        if (scaledownOnDeSelect != null)
        {
            StopCoroutine(scaledownOnDeSelect);
            scaledownOnDeSelect = null;
        }

        scaleOnSelect = StartCoroutine(ScaleSelection(true));
    }

    private void DoDeselect()
    {
        if (scaleOnSelect != null)
        {
            StopCoroutine(scaleOnSelect);
            scaleOnSelect = null;
        }

        scaledownOnDeSelect = StartCoroutine(ScaleSelection(false));
    }
    
    #endregion IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler

    #region HELPERS
    
    private void AddDefaultListeners()
    {
        button.onClick.AddListener(() =>
        {
            transform.localScale = initialScale;
            // AudioManager.Instance.PlayOnClick();
        });
    }
    #endregion
}
