using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200018A RID: 394
public class UIPanel : MonoBehaviour
{
	// Token: 0x060010A0 RID: 4256 RVA: 0x00067A0C File Offset: 0x00065C0C
	private void Awake()
	{
		PanelManager.instance.panels.Add(this);
		this.opacityGrp = base.GetComponent<CanvasGroup>();
		this.subGroups = base.GetComponentsInChildren<CanvasGroup>();
		this.subCasters = base.GetComponentsInChildren<GraphicRaycaster>();
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.OnInputChanged));
		this.opacityGrp.alpha = 0f;
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x00067A7D File Offset: 0x00065C7D
	public void Leave()
	{
		if (PanelManager.CurPanel == this.panelType)
		{
			PanelManager.instance.PopPanel();
		}
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x00067A96 File Offset: 0x00065C96
	public void SetSelected()
	{
		this.SetSelected(false);
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x00067AA0 File Offset: 0x00065CA0
	public void SetSelected(bool force)
	{
		CanvasGroup[] array = this.subGroups;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].interactable = true;
		}
		GraphicRaycaster[] array2 = this.subCasters;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].enabled = true;
		}
		this.DidJustEnter = true;
		UISelector.SelectSelectable(null);
		base.StartCoroutine("SelectAfterFrame");
		if (this.OnEnteredPanel != null)
		{
			this.OnEnteredPanel();
		}
		AudioManager.PlayInterfaceSFX(CanvasController.instance.WindowChange.GetRandomClip(-1), 1f, 0f);
		this.OnInputChanged();
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x00067B3A File Offset: 0x00065D3A
	private IEnumerator SelectAfterFrame()
	{
		yield return true;
		if (this.defaultSelect != null)
		{
			UISelector.SelectSelectable(this.defaultSelect);
		}
		yield return true;
		if (this.defaultSelect != null)
		{
			UISelector.SelectSelectable(this.defaultSelect);
		}
		this.DidJustEnter = false;
		yield break;
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x00067B4C File Offset: 0x00065D4C
	public void FadeInUpdate()
	{
		if (this.opacityGrp == null)
		{
			GameObject gameObject = base.gameObject;
			UnityEngine.Debug.Log(((gameObject != null) ? gameObject.ToString() : null) + " has no CanvasGroup");
		}
		if (this.opacityGrp.alpha < 1f)
		{
			if (this.OverrideFade)
			{
				this.opacityGrp.alpha += Time.unscaledDeltaTime * this.FadeInCurve.Evaluate(this.opacityGrp.alpha);
			}
			else
			{
				this.opacityGrp.alpha += Time.unscaledDeltaTime * 3f;
			}
		}
		this.opacityGrp.blocksRaycasts = true;
		this.opacityGrp.interactable = true;
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x00067C08 File Offset: 0x00065E08
	public void FadeOutUpdate()
	{
		if (this.opacityGrp == null)
		{
			GameObject gameObject = base.gameObject;
			UnityEngine.Debug.Log(((gameObject != null) ? gameObject.ToString() : null) + " has no CanvasGroup");
		}
		if (this.opacityGrp.alpha > 0f)
		{
			if (this.OverrideFade)
			{
				this.opacityGrp.alpha -= Time.unscaledDeltaTime * this.FadeOutCurve.Evaluate(this.opacityGrp.alpha);
			}
			else
			{
				this.opacityGrp.alpha -= Time.unscaledDeltaTime * 3f;
			}
		}
		this.opacityGrp.blocksRaycasts = false;
		this.opacityGrp.interactable = false;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x00067CC4 File Offset: 0x00065EC4
	public void Deselect()
	{
		Selectable[] componentsInChildren = base.GetComponentsInChildren<Selectable>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].gameObject == EventSystem.current.currentSelectedGameObject)
			{
				EventSystem.current.SetSelectedGameObject(null);
				break;
			}
		}
		foreach (ParticleSystem particleSystem in base.GetComponentsInChildren<ParticleSystem>())
		{
			if (particleSystem.isPlaying)
			{
				particleSystem.Stop();
			}
			particleSystem.Clear();
		}
		CanvasGroup[] array = this.subGroups;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].interactable = false;
		}
		GraphicRaycaster[] array2 = this.subCasters;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].enabled = false;
		}
		if (PanelManager.curSelect == this)
		{
			PanelManager.curSelect = null;
		}
		if (this.OnLeftPanel != null)
		{
			this.OnLeftPanel();
		}
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x00067DA1 File Offset: 0x00065FA1
	public void ForceSelection()
	{
		if (this.defaultSelect != null)
		{
			EventSystem.current.SetSelectedGameObject(this.defaultSelect.gameObject);
		}
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x00067DC6 File Offset: 0x00065FC6
	public void GetControllerSelect()
	{
		if (this.defaultSelect != null)
		{
			UISelector.SelectSelectable(this.defaultSelect);
		}
		Action onControllerStarted = this.OnControllerStarted;
		if (onControllerStarted == null)
		{
			return;
		}
		onControllerStarted();
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x00067DF4 File Offset: 0x00065FF4
	private void OnInputChanged()
	{
		foreach (GameObject gameObject in this.ControllerObjects)
		{
			gameObject.SetActive(InputManager.IsUsingController);
		}
		foreach (GameObject gameObject2 in this.KBMObjects)
		{
			gameObject2.SetActive(!InputManager.IsUsingController);
		}
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x00067E90 File Offset: 0x00066090
	public void ExitPanel()
	{
		if (this.InExitPanel)
		{
			UnityEngine.Debug.LogError("Recursive call to UIPanel.ExitPanel - don't do this!");
			return;
		}
		this.InExitPanel = true;
		if (this.OnLeftPanel != null)
		{
			this.OnLeftPanel();
		}
		this.InExitPanel = false;
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x00067EC6 File Offset: 0x000660C6
	private void OnDestroy()
	{
		PanelManager.instance.panels.Remove(this);
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x00067ED9 File Offset: 0x000660D9
	public UIPanel()
	{
	}

	// Token: 0x04000ED5 RID: 3797
	public PanelType panelType;

	// Token: 0x04000ED6 RID: 3798
	public bool gameplayInteractable;

	// Token: 0x04000ED7 RID: 3799
	public bool NoBook;

	// Token: 0x04000ED8 RID: 3800
	public bool UseBlur;

	// Token: 0x04000ED9 RID: 3801
	public Vector3 BookOffset;

	// Token: 0x04000EDA RID: 3802
	public bool CanBackOut;

	// Token: 0x04000EDB RID: 3803
	public bool OverridesPlayerControl;

	// Token: 0x04000EDC RID: 3804
	public Selectable defaultSelect;

	// Token: 0x04000EDD RID: 3805
	public List<GameObject> ControllerObjects;

	// Token: 0x04000EDE RID: 3806
	public List<GameObject> KBMObjects;

	// Token: 0x04000EDF RID: 3807
	[NonSerialized]
	public CanvasGroup opacityGrp;

	// Token: 0x04000EE0 RID: 3808
	public bool OverrideFade;

	// Token: 0x04000EE1 RID: 3809
	public AnimationCurve FadeInCurve;

	// Token: 0x04000EE2 RID: 3810
	public AnimationCurve FadeOutCurve;

	// Token: 0x04000EE3 RID: 3811
	private CanvasGroup[] subGroups;

	// Token: 0x04000EE4 RID: 3812
	private GraphicRaycaster[] subCasters;

	// Token: 0x04000EE5 RID: 3813
	public Action OnEnteredPanel;

	// Token: 0x04000EE6 RID: 3814
	public Action OnLeftPanel;

	// Token: 0x04000EE7 RID: 3815
	public Action OnControllerStarted;

	// Token: 0x04000EE8 RID: 3816
	public Action OnNextTab;

	// Token: 0x04000EE9 RID: 3817
	public Action OnPrevTab;

	// Token: 0x04000EEA RID: 3818
	public Action OnPageNext;

	// Token: 0x04000EEB RID: 3819
	public Action OnPagePrev;

	// Token: 0x04000EEC RID: 3820
	public Action OnSecondaryAction;

	// Token: 0x04000EED RID: 3821
	public Action OnTertiaryAction;

	// Token: 0x04000EEE RID: 3822
	public Action OnRightStickAction;

	// Token: 0x04000EEF RID: 3823
	[NonSerialized]
	public bool DidJustEnter;

	// Token: 0x04000EF0 RID: 3824
	private bool InExitPanel;

	// Token: 0x0200055F RID: 1375
	[CompilerGenerated]
	private sealed class <SelectAfterFrame>d__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024A0 RID: 9376 RVA: 0x000CE946 File Offset: 0x000CCB46
		[DebuggerHidden]
		public <SelectAfterFrame>d__31(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000CE955 File Offset: 0x000CCB55
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000CE958 File Offset: 0x000CCB58
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			UIPanel uipanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				if (uipanel.defaultSelect != null)
				{
					UISelector.SelectSelectable(uipanel.defaultSelect);
				}
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				if (uipanel.defaultSelect != null)
				{
					UISelector.SelectSelectable(uipanel.defaultSelect);
				}
				uipanel.DidJustEnter = false;
				return false;
			default:
				return false;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000CEA00 File Offset: 0x000CCC00
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000CEA08 File Offset: 0x000CCC08
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x000CEA0F File Offset: 0x000CCC0F
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026E3 RID: 9955
		private int <>1__state;

		// Token: 0x040026E4 RID: 9956
		private object <>2__current;

		// Token: 0x040026E5 RID: 9957
		public UIPanel <>4__this;
	}
}
