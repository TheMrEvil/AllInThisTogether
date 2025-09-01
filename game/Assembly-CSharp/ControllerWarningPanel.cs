using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020001D9 RID: 473
public class ControllerWarningPanel : MonoBehaviour
{
	// Token: 0x060013AE RID: 5038 RVA: 0x0007A14C File Offset: 0x0007834C
	private void Start()
	{
		this.panel = base.GetComponent<UIPanel>();
		UIPanel uipanel = this.panel;
		uipanel.OnEnteredPanel = (Action)Delegate.Combine(uipanel.OnEnteredPanel, new Action(this.OnPanelSelected));
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.CheckInput));
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x0007A1AC File Offset: 0x000783AC
	private void CheckInput()
	{
		if (this.DidShow)
		{
			return;
		}
		if (InputManager.IsUsingController)
		{
			PanelManager.instance.PushPanel(PanelType.ControllerWarning);
		}
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x0007A1CA File Offset: 0x000783CA
	private void OnPanelSelected()
	{
		this.DidShow = true;
		this.panel.defaultSelect.interactable = false;
		base.Invoke("DelayActivate", 1f);
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x0007A1F4 File Offset: 0x000783F4
	public void ConfirmButton()
	{
		if (PanelManager.CurPanel == PanelType.ControllerWarning)
		{
			PanelManager.instance.PopPanel();
		}
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x0007A209 File Offset: 0x00078409
	private void DelayActivate()
	{
		this.panel.defaultSelect.interactable = true;
		EventSystem.current.SetSelectedGameObject(this.panel.defaultSelect.gameObject);
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x0007A236 File Offset: 0x00078436
	public ControllerWarningPanel()
	{
	}

	// Token: 0x040012CF RID: 4815
	private UIPanel panel;

	// Token: 0x040012D0 RID: 4816
	public bool DidShow;
}
