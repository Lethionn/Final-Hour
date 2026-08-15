using Assets.Script.Runtime.Context.Menu.Scripts.View.InstructionsPanel;
using strange.extensions.mediation.impl;
using UnityEngine.InputSystem;

namespace Assets.Script.Runtime.Context.Menu.Scripts.View.CreditsPanel
{
  public class CreditsPanelView : EventView
  {
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
      _optionsAction.performed += (_ => { dispatcher.Dispatch(CreditsPanelEvent.Close); });
    }
    
    public void RemoveOptionsAction()
    {
      _optionsAction.Disable();
      _optionsAction.performed -= (_ => { dispatcher.Dispatch(CreditsPanelEvent.Close); });
    }
    
    public void OnClose()
    {
      dispatcher.Dispatch(CreditsPanelEvent.Close);
    }
  }
}