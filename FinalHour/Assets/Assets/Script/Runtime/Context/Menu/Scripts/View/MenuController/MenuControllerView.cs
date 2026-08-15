using Assets.Script.Runtime.Context.Game.Scripts.View.GameHud;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Runtime.Context.Menu.Scripts.View.MenuController
{
  public class MenuControllerView : EventView
  {
    public GameObject shadow;
    
    private PlayerInputActions _playerInputActions;

    private InputAction _optionsAction;

    protected override void Awake()
    {
      _playerInputActions = new PlayerInputActions();
    }
    
    public void SetOptionsAction()
    {
      _optionsAction = _playerInputActions.UI.Options;
      _optionsAction.Enable();
      _optionsAction.performed += (_ => { dispatcher.Dispatch(MenuControllerEvent.Settings); });
    }
    
    public void RemoveOptionsAction()
    {
      _optionsAction.Disable();
      _optionsAction.performed -= (_ => { dispatcher.Dispatch(MenuControllerEvent.Settings); });
    }
    
    public void OnPress()
    {
      dispatcher.Dispatch(MenuControllerEvent.Press);
    }

    public void OnSettings()
    {
      dispatcher.Dispatch(MenuControllerEvent.Settings);
    }
  }
}
