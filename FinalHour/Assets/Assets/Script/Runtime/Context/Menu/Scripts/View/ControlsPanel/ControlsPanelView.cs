using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Runtime.Context.Menu.Scripts.View.ControlsPanel
{
  public class ControlsPanelView : EventView
  {
    public GameObject pcControls;

    public GameObject mobileControls;
    
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
      _optionsAction.performed += (_ => { dispatcher.Dispatch(ControlsPanelEvent.Close); });
    }
    
    public void RemoveOptionsAction()
    {
      _optionsAction.Disable();
      _optionsAction.performed -= (_ => { dispatcher.Dispatch(ControlsPanelEvent.Close); });
    }

    public void OnClose()
    {
      dispatcher.Dispatch(ControlsPanelEvent.Close);
    }
  }
}