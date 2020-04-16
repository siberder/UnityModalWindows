using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ModalWindow<T> : MonoBehaviour where T : ModalWindow<T>
{
    protected const string PREFABS_DIR_IN_RESOURCES = "Modal Windows/";
    protected const string BUTTONS_PATH= "Buttons/Button_";     

    [SerializeField] protected Animator animator;
    [SerializeField] protected Text headerText;
    [SerializeField] protected Text bodyText;

    [SerializeField] protected Button closeButton;
    [SerializeField] protected Button backgroundButton;

    [SerializeField] protected Transform buttonsRoot;

    protected T Instance { get; set; }

    protected List<ModalWindowButton> buttons = new List<ModalWindowButton>();

    protected bool ignorable;
    public virtual bool Ignorable 
    { 
        get => ignorable; 
        protected set
        {
            ignorable = value;
            if (backgroundButton) backgroundButton.interactable = value;
            if (closeButton) closeButton.gameObject.SetActive(value);
        }
    }

    public virtual bool Visible
    {
        get => animator.GetBool("Visible");
        set => animator.SetBool("Visible", value);
    }

    #region Prefabs Loading
    // Simple resources cache
    static protected Dictionary<string, MonoBehaviour> resourcesCache = new Dictionary<string, MonoBehaviour>();

    protected static ResType GetModalWindowResource<ResType>(string path) where ResType : MonoBehaviour
    {
        var fullResourcesPath = PREFABS_DIR_IN_RESOURCES + path;
        if (resourcesCache.TryGetValue(fullResourcesPath, out var res))
        {
            return res as ResType;
        }
        else
        {
            var loadedResource = Resources.Load<ResType>(fullResourcesPath);
            if (loadedResource)
            {
                resourcesCache.Add(fullResourcesPath, loadedResource);
            }

            return loadedResource;
        }
    }

    public static ModalWindowButton GetButtonPrefab(ModalButtonType buttonType)
    {
        return GetModalWindowResource<ModalWindowButton>(BUTTONS_PATH + buttonType.ToString());
    }

    #endregion

    public static T Create(bool ignorable = true)
    {
        var name = typeof(T).ToString();
        var modalWindow = Instantiate(GetModalWindowResource<T>(name));
        modalWindow.name = name;
        modalWindow.Ignorable = ignorable;
        modalWindow.Instance = modalWindow;

        return modalWindow.Instance;
    }

    private void Awake()
    {
        if (backgroundButton) backgroundButton.onClick.AddListener(new UnityEngine.Events.UnityAction(UI_IgnorePopup));
        if (closeButton) closeButton.onClick.AddListener(new UnityEngine.Events.UnityAction(UI_IgnorePopup));
    }

    public virtual T SetHeader(string text)
    {
        headerText.text = text;

        return Instance;
    }
    
    

    public virtual T SetBody(string text)
    {
        bodyText.text = text;

        return Instance;
    }

    public virtual T AddButton(string text, System.Action action = null, ModalButtonType type = ModalButtonType.Normal)
    {
        if (!buttonsRoot) return Instance;

        var button = Instantiate(GetButtonPrefab(type), buttonsRoot);
        button.Init(ButtonPressedCallback, text, action, type);
        buttons.Add(button);

        return Instance;
    }  

    protected virtual void OnBeforeShow()
    {
        if (buttons.Count == 0)
        {
            AddButton("OK");
        }
    }

    protected virtual void Update()
    {
        CheckIgnorableForClose();
    }

    protected virtual void CheckIgnorableForClose()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ReferenceEquals(Instance, null))
            {
                if (Instance.ignorable)
                    Instance.Close();
            }
        }
    }

    public virtual T Show()
    {
        OnBeforeShow();

        Visible = true;
        transform.SetAsLastSibling();
        return Instance;
    }

    public virtual T Close()
    {
        Visible = false;
        Destroy(gameObject, 1f);
        return Instance;
    }    

    public virtual void UI_IgnorePopup()
    {
        if (Ignorable)
        {
            Close();
        }
    }    

    public virtual void ButtonPressedCallback(ModalWindowButton modalWindowButton)
    {
        Close();
    }
}
