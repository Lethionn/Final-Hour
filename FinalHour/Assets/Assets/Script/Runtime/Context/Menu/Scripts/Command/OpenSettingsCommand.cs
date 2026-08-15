using System.Linq;
using Assets.Script.Runtime.Context.Game.Scripts.Config;
using Assets.Script.Runtime.Context.Game.Scripts.Model;
using Assets.Script.Runtime.Context.Menu.Scripts.Enum;
using Assets.Script.Runtime.Context.Menu.Scripts.Model;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.Script.Runtime.Context.Menu.Scripts.Command
{
  public class OpenOptionsCommand : EventCommand
  {
    [Inject]
    public IUIModel uiModel { get; set; }

    [Inject]
    public ISpeedModel speedModel { get; set; }

    public override void Execute()
    {
      if (uiModel.openPanels.All(obj => obj.Value != PanelKeys.OptionsPanel && obj.Value != PanelKeys.OptionsPanelWeb))
      {
        Transform layer = (Transform)evt.data;
        speedModel.Pause();

        dispatcher.Dispatch(GameEvent.Click);
        
#if UNITY_WEBGL
        uiModel.OpenPanel(PanelKeys.OptionsPanelWeb, layer);
#else
        uiModel.OpenPanel(PanelKeys.OptionsPanel, layer);
#endif
      }
      else
      {
#if UNITY_WEBGL
        uiModel.ClosePanel(PanelKeys.OptionsPanelWeb);
#else
        uiModel.ClosePanel(PanelKeys.OptionsPanel);
#endif

        speedModel.Continue();
      }
    }
  }
}