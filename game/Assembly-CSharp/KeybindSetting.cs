using System;
using System.Collections.Generic;
using InControl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000209 RID: 521
public class KeybindSetting : SettingElement, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x06001626 RID: 5670 RVA: 0x0008C0E8 File Offset: 0x0008A2E8
	public override void Setup(SettingsPanel.SettingOption settingInfo)
	{
		base.Setup(settingInfo);
		this.InputType = settingInfo.action;
		this.actionRef = global::InputManager.Actions.GetAction(this.InputType);
		if (this.actionRef != null)
		{
			BindingListenOptions listenOptions = this.actionRef.ListenOptions;
			listenOptions.OnBindingAdded = (Action<PlayerAction, BindingSource>)Delegate.Combine(listenOptions.OnBindingAdded, new Action<PlayerAction, BindingSource>(this.OnBindingAdded));
			BindingListenOptions listenOptions2 = this.actionRef.ListenOptions;
			listenOptions2.OnBindingRejected = (Action<PlayerAction, BindingSource, BindingSourceRejectionType>)Delegate.Combine(listenOptions2.OnBindingRejected, new Action<PlayerAction, BindingSource, BindingSourceRejectionType>(this.OnBindingRejected));
			BindingListenOptions listenOptions3 = this.actionRef.ListenOptions;
			listenOptions3.OnBindingFound = (Func<PlayerAction, BindingSource, bool>)Delegate.Combine(listenOptions3.OnBindingFound, new Func<PlayerAction, BindingSource, bool>(this.OnBindingFound));
		}
		this.ShowCurrentBindings();
		UINavReceiver component = base.GetComponent<UINavReceiver>();
		if (component != null)
		{
			UINavReceiver uinavReceiver = component;
			uinavReceiver.OnSecondaryInput = (Action)Delegate.Combine(uinavReceiver.OnSecondaryInput, new Action(this.Clear));
		}
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x0008C1E8 File Offset: 0x0008A3E8
	private void ShowCurrentBindings()
	{
		List<PlayerDB.InputSprite> bindings = PlayerDB.GetBindings(this.InputType, PlayerDB.SpriteType.Any);
		string text = "";
		foreach (PlayerDB.InputSprite inputSprite in bindings)
		{
			text = text + "<sprite name=" + inputSprite.SpriteID + ">,";
		}
		text = ((text.Length == 0) ? "None" : text.Substring(0, text.Length - 1));
		this.CurrentBindings.text = text;
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x0008C284 File Offset: 0x0008A484
	public void ButtonPress()
	{
		if (this.isListening)
		{
			return;
		}
		this.ListenNew();
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x0008C295 File Offset: 0x0008A495
	public void ListenNew()
	{
		this.isListening = true;
		global::InputManager.IsBindingKeys = true;
		global::InputManager.Actions.NewBinding(this.InputType);
		this.CurrentBindings.text = "...";
		SettingsPanel.instance.RayBlocker.SetActive(true);
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x0008C2D4 File Offset: 0x0008A4D4
	public void Clear()
	{
		if (this.isListening)
		{
			return;
		}
		Debug.Log("Removing Binding");
		PlayerAction action = global::InputManager.Actions.GetAction(this.InputType);
		if (action != null && action.Bindings.Count > 0)
		{
			action.RemoveBindingAt(0);
		}
		base.Invoke("ShowCurrentBindings", 0.033f);
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x0008C32D File Offset: 0x0008A52D
	public override void Reset()
	{
		this.ShowCurrentBindings();
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x0008C335 File Offset: 0x0008A535
	private void OnBindingAdded(PlayerAction action, BindingSource binding)
	{
		this.ShowCurrentBindings();
		this.isListening = false;
		global::InputManager.IsBindingKeys = false;
		SettingsPanel.instance.RayBlocker.SetActive(false);
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x0008C35A File Offset: 0x0008A55A
	private void OnBindingRejected(PlayerAction action, BindingSource binding, BindingSourceRejectionType reason)
	{
		this.ShowCurrentBindings();
		this.isListening = false;
		global::InputManager.IsBindingKeys = false;
		SettingsPanel.instance.RayBlocker.SetActive(false);
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x0008C37F File Offset: 0x0008A57F
	private bool OnBindingFound(PlayerAction action, BindingSource binding)
	{
		if (binding == new KeyBindingSource(new Key[]
		{
			Key.Escape
		}))
		{
			this.ShowCurrentBindings();
			this.isListening = false;
			global::InputManager.IsBindingKeys = false;
			SettingsPanel.instance.RayBlocker.SetActive(false);
			return false;
		}
		return true;
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x0008C3BF File Offset: 0x0008A5BF
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			this.Clear();
		}
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x0008C3D0 File Offset: 0x0008A5D0
	private void OnDestroy()
	{
		if (this.actionRef == null || this.actionRef.ListenOptions == null)
		{
			return;
		}
		BindingListenOptions listenOptions = this.actionRef.ListenOptions;
		listenOptions.OnBindingAdded = (Action<PlayerAction, BindingSource>)Delegate.Remove(listenOptions.OnBindingAdded, new Action<PlayerAction, BindingSource>(this.OnBindingAdded));
		BindingListenOptions listenOptions2 = this.actionRef.ListenOptions;
		listenOptions2.OnBindingRejected = (Action<PlayerAction, BindingSource, BindingSourceRejectionType>)Delegate.Remove(listenOptions2.OnBindingRejected, new Action<PlayerAction, BindingSource, BindingSourceRejectionType>(this.OnBindingRejected));
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x0008C44B File Offset: 0x0008A64B
	public KeybindSetting()
	{
	}

	// Token: 0x040015CE RID: 5582
	public InputActions.InputAction InputType;

	// Token: 0x040015CF RID: 5583
	public TextMeshProUGUI CurrentBindings;

	// Token: 0x040015D0 RID: 5584
	private PlayerAction actionRef;

	// Token: 0x040015D1 RID: 5585
	private bool isListening;
}
