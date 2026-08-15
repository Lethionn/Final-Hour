using Assets.Script.Runtime.Context.Menu.Scripts.View.MenuController;
using strange.extensions.mediation.impl;
using UnityEngine.InputSystem;

namespace Assets.Script.Runtime.Context.Menu.Scripts.View.InstructionsPanel
{
  public class InstructionsPanelView : EventView
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
      _optionsAction.performed += (_ => { dispatcher.Dispatch(InstructionsPanelEvent.Close); });
    }
    
    public void RemoveOptionsAction()
    {
      _optionsAction.Disable();
      _optionsAction.performed -= (_ => { dispatcher.Dispatch(InstructionsPanelEvent.Close); });
    }

    public void OnClose()
    {
      dispatcher.Dispatch(InstructionsPanelEvent.Close);
    }

    public void OnControls()
    {
      dispatcher.Dispatch(InstructionsPanelEvent.Controls);
    }
  }
}