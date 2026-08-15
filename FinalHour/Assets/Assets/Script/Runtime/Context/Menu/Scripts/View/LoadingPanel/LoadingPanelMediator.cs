using Assets.Script.Runtime.Context.Menu.Scripts.Enum;
using strange.extensions.mediation.impl;

namespace Assets.Script.Runtime.Context.Menu.Scripts.View.LoadingPanel
{
  public class LoadingPanelMediator : EventMediator
  {
    [Inject]
    public LoadingPanelView view { get; set; }
    
    public override void OnRegister()
    {
      dispatcher.AddListener(GameEvent.ShowLoading, OnShow);
      dispatcher.AddListener(GameEvent.HideLoading, OnHide);
    }
    
    public void OnShow( )
    {
      view.Show();
    }
    
    public void OnHide()
    {
      view.Hide();
    }
    
    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameEvent.ShowLoading, OnShow);
      dispatcher.RemoveListener(GameEvent.HideLoading, OnHide);
    }
  }
}