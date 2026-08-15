using Assets.Script.Runtime.Context.Game.Scripts.Model;
using Assets.Script.Runtime.Context.Menu.Scripts.Enum;
using Assets.Script.Runtime.Context.Menu.Scripts.Model;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Script.Runtime.Context.Menu.Scripts.View.InstructionsPanel
{
  public enum InstructionsPanelEvent
  { 
    Controls,
    Close
  }
  public class InstructionsPanelMediator : EventMediator
  {
    [Inject]
    public InstructionsPanelView view { get; set; }
   
    [Inject]
    public IUIModel uiModel { get; set; }
    
    [Inject]
    public ISpeedModel speedModel { get; set; }
    
    [Inject]
    public IPlayerModel playerModel { get; set; }

    public override void OnRegister()
    { 
      view.dispatcher.AddListener(InstructionsPanelEvent.Close, OnClose);
      view.dispatcher.AddListener(InstructionsPanelEvent.Controls, OnControls);
    }
    
    public override void OnInitialize()
    {
      view.SetOptionsAction();
    }
    
    private void OnClose()
    {
      if (uiModel.openPanels.Count > 2)
      {
        return;
      }

      if (playerModel.tutorialActive && PlayerPrefs.GetInt(SettingKeys.CompletedTutorialSteps) == 0)
      {
        speedModel.Continue();
      }
      
      uiModel.ClosePanel(PanelKeys.InstructionsPanel);
    }
    
    private void OnControls()
    { 
      uiModel.OpenPanel(PanelKeys.ControlsPanel, transform.parent);
    }
    
    public override void OnRemove()
    {
      view.RemoveOptionsAction();
      
      view.dispatcher.RemoveListener(InstructionsPanelEvent.Close, OnClose);
      view.dispatcher.RemoveListener(InstructionsPanelEvent.Controls, OnControls);
    }
  }
}