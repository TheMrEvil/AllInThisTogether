using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using InControl;
using QFSW.QC;
using UnityEngine;

// Token: 0x02000188 RID: 392
public class PanelManager : MonoBehaviour
{
	// Token: 0x17000134 RID: 308
	// (get) Token: 0x0600107E RID: 4222 RVA: 0x0006709C File Offset: 0x0006529C
	public static List<PanelType> CurrentStackPanels
	{
		get
		{
			return (from v in PanelManager.instance.activeStack
			select v.panelType).ToList<PanelType>();
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x0600107F RID: 4223 RVA: 0x000670D1 File Offset: 0x000652D1
	public static PanelType CurPanel
	{
		get
		{
			if (PanelManager.curSelect != null)
			{
				return PanelManager.curSelect.panelType;
			}
			return PanelType.Main;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06001080 RID: 4224 RVA: 0x000670EC File Offset: 0x000652EC
	private UIActions actions
	{
		get
		{
			return global::InputManager.UIAct;
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x000670F3 File Offset: 0x000652F3
	private PanelManager()
	{
		PanelManager.instance = this;
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0006711E File Offset: 0x0006531E
	private void Awake()
	{
		PanelManager.instance = this;
		PanelManager.curSelect = null;
		PanelManager.inFocus = true;
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x00067134 File Offset: 0x00065334
	private void Start()
	{
		this.InputChanged();
		global::InputManager.OnInputMethodChanged = (Action)Delegate.Combine(global::InputManager.OnInputMethodChanged, new Action(this.InputChanged));
		InControl.InputManager.OnDeviceDetached += this.ControllerDisconnected;
		if (GlobalSetup.StartedExternal)
		{
			this.GoToPanel(PanelType.GameInvisible);
			MainPanel.instance.GetComponent<UIPanel>().Deselect();
			return;
		}
		if (LibraryManager.DidLoad)
		{
			this.PushPanel(PanelType.Main);
			return;
		}
		this.PushPanel(PanelType.Splash);
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x000671B0 File Offset: 0x000653B0
	private void InputChanged()
	{
		this.KBM_Module.SetActive(!global::InputManager.IsUsingController);
		this.Controller_Module.SetActive(global::InputManager.IsUsingController);
		if (!global::InputManager.IsUsingController)
		{
			return;
		}
		if (PlayerControl.myInstance != null && !PanelManager.curSelect.gameplayInteractable)
		{
			return;
		}
		PanelManager.curSelect.GetControllerSelect();
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x0006720C File Offset: 0x0006540C
	private void ControllerDisconnected(InputDevice device)
	{
		if (PanelManager.CurPanel == PanelType.GameInvisible)
		{
			CanvasController.GoToPause();
		}
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x0006721C File Offset: 0x0006541C
	public void GoToPanel(PanelType panelType)
	{
		UIPanel panel = this.GetPanel(panelType);
		if (panel == null)
		{
			UnityEngine.Debug.LogError("No panel " + panelType.ToString() + " exists!");
			return;
		}
		int num = this.activeStack.IndexOf(panel);
		if (num >= 0)
		{
			if (num != this.activeStack.Count - 1)
			{
				this.activeStack[this.activeStack.Count - 1].Deselect();
			}
			for (int i = this.activeStack.Count - 1; i > num; i--)
			{
				this.activeStack.RemoveAt(i);
			}
		}
		else
		{
			if (this.activeStack.Count > 1)
			{
				this.activeStack[this.activeStack.Count - 1].Deselect();
				this.activeStack.RemoveAt(this.activeStack.Count - 1);
			}
			this.activeStack.Add(panel);
		}
		this.OnSetSelectedPanel(panel);
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x00067318 File Offset: 0x00065518
	public void PushOrGoTo(PanelType panelType)
	{
		UIPanel panel = this.GetPanel(panelType);
		if (panel == null)
		{
			UnityEngine.Debug.LogError("No panel " + panelType.ToString() + " exists!");
			return;
		}
		if (this.activeStack.IndexOf(panel) >= 0)
		{
			this.GoToPanel(panelType);
			return;
		}
		this.PushPanel(panelType);
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x00067378 File Offset: 0x00065578
	public void PushPanel(PanelType panelType)
	{
		if (PanelManager.CurPanel == panelType && this.activeStack.Count > 0)
		{
			return;
		}
		UIPanel panel = this.GetPanel(panelType);
		if (!panel)
		{
			UnityEngine.Debug.LogError("Tried to push UIPanel '" + panelType.ToString() + "' but couldn't find it.");
			return;
		}
		if (this.activeStack.Count > 0)
		{
			this.activeStack[this.activeStack.Count - 1].Deselect();
		}
		this.activeStack.Add(panel);
		this.OnSetSelectedPanel(panel);
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x0006740C File Offset: 0x0006560C
	public void PopPanel()
	{
		if (this.activeStack.Count <= 1)
		{
			UnityEngine.Debug.LogError("Tried to pop panel, but stack only had  " + this.activeStack.Count.ToString() + " items.");
			return;
		}
		this.activeStack[this.activeStack.Count - 1].Deselect();
		this.activeStack.RemoveAt(this.activeStack.Count - 1);
		Tooltip.Release();
		if (this.activeStack.Count > 0)
		{
			this.OnSetSelectedPanel(this.activeStack[this.activeStack.Count - 1]);
			return;
		}
		UnityEngine.Debug.LogError("No more UIPanels to back out of!");
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x000674C0 File Offset: 0x000656C0
	public void ClearStack()
	{
		if (this.activeStack.Count > 0)
		{
			this.activeStack[this.activeStack.Count - 1].Deselect();
		}
		this.activeStack.Clear();
		PanelManager.curSelect = null;
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x00067500 File Offset: 0x00065700
	public void RemoveFromStack(PanelType panelType)
	{
		if (PanelManager.CurPanel == panelType)
		{
			this.PopPanel();
		}
		if (this.activeStack.Count == 0)
		{
			return;
		}
		UIPanel panel = this.GetPanel(panelType);
		this.activeStack.Remove(panel);
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x00067540 File Offset: 0x00065740
	public void HardOverridePush(PanelType panelType)
	{
		UIPanel panel = this.GetPanel(panelType);
		this.activeStack.Add(panel);
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x00067561 File Offset: 0x00065761
	public void HardOverridePop()
	{
		if (this.activeStack.Count > 0)
		{
			this.activeStack.RemoveAt(this.activeStack.Count - 1);
		}
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x0006758C File Offset: 0x0006578C
	public void PopWithoutBack()
	{
		if (this.activeStack.Count > 0)
		{
			this.activeStack[this.activeStack.Count - 1].Deselect();
			this.activeStack.RemoveAt(this.activeStack.Count - 1);
		}
		PanelManager.curSelect = null;
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x000675E4 File Offset: 0x000657E4
	public UIPanel GetPanel(PanelType panelType)
	{
		foreach (UIPanel uipanel in this.panels)
		{
			if (!(uipanel == null) && uipanel.panelType == panelType)
			{
				return uipanel;
			}
		}
		return null;
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0006764C File Offset: 0x0006584C
	public UIPanel PreviousPanel()
	{
		if (this.activeStack.Count <= 1)
		{
			return null;
		}
		return this.activeStack[this.activeStack.Count - 2];
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x00067678 File Offset: 0x00065878
	public static bool StackContains(PanelType pType)
	{
		using (List<UIPanel>.Enumerator enumerator = PanelManager.instance.activeStack.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.panelType == pType)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x000676D8 File Offset: 0x000658D8
	private void OnSetSelectedPanel(UIPanel panel)
	{
		if (panel == null)
		{
			return;
		}
		PanelManager.curSelect = panel;
		panel.SetSelected();
		Tooltip.Release();
		Action<PanelType, PanelType> onPanelChanged = this.OnPanelChanged;
		if (onPanelChanged != null)
		{
			onPanelChanged(this.lastPanel, PanelManager.CurPanel);
		}
		this.lastPanel = PanelManager.CurPanel;
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x00067728 File Offset: 0x00065928
	private void Update()
	{
		PanelManager.CheckApplicationIsActivated();
		this.UpdatePanelVisibility();
		this.CheckInputs();
		if (!PanelManager.inFocus || !Application.isFocused)
		{
			Cursor.lockState = CursorLockMode.None;
			return;
		}
		bool flag = PanelManager.ShouldLockCursor();
		Cursor.visible = (!flag && !global::InputManager.IsUsingController);
		CursorLockMode lockState = Cursor.lockState;
		Cursor.lockState = (flag ? CursorLockMode.Locked : CursorLockMode.None);
		if (!flag && lockState == CursorLockMode.Locked && Screen.fullScreenMode != FullScreenMode.Windowed)
		{
			this.MoveCursor(new Vector2(0.5f, 0.65f));
		}
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x000677A8 File Offset: 0x000659A8
	public static bool ShouldLockCursor()
	{
		return global::InputManager.IsUsingController || (!(PlayerControl.myInstance == null) && !PanelManager.curSelect.gameplayInteractable);
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x000677D4 File Offset: 0x000659D4
	private void UpdatePanelVisibility()
	{
		foreach (UIPanel uipanel in this.panels)
		{
			if (!(uipanel == null))
			{
				if (uipanel == PanelManager.curSelect)
				{
					uipanel.FadeInUpdate();
				}
				else
				{
					uipanel.FadeOutUpdate();
				}
			}
		}
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x00067844 File Offset: 0x00065A44
	public static string GetCurrentPanelName()
	{
		if (PanelManager.curSelect != null)
		{
			return PanelManager.curSelect.panelType.ToString();
		}
		return "Main Menu";
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x0006786E File Offset: 0x00065A6E
	public static bool isGameplayInteractable()
	{
		return PanelManager.curSelect != null && PanelManager.curSelect.gameplayInteractable;
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x00067889 File Offset: 0x00065A89
	private static void FocusChanged(bool inFocus)
	{
		if (inFocus)
		{
			Application.targetFrameRate = 0;
			return;
		}
		Application.targetFrameRate = 30;
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x0006789C File Offset: 0x00065A9C
	private void CheckInputs()
	{
		if (QuantumConsole.Instance.IsActive || UITutorial.InTutorial || ConfirmationPrompt.IsInPrompt || ErrorPrompt.IsInPrompt)
		{
			return;
		}
		if (this.actions.UIBack.WasPressed && PanelManager.curSelect.CanBackOut)
		{
			PanelManager.instance.PopPanel();
			if (PanelManager.CurPanel == PanelType.GameInvisible)
			{
				CanvasController.Resume();
				return;
			}
		}
		else if (global::InputManager.Actions.Pause.WasPressed && GameplayUI.InGame)
		{
			if (PanelManager.CurPanel == PanelType.GameInvisible && !PlayerNook.IsInEditMode)
			{
				CanvasController.GoToPause();
				return;
			}
			if (PanelManager.CurPanel == PanelType.Pause)
			{
				CanvasController.Resume();
				return;
			}
		}
		else if (global::InputManager.Actions.Augments.WasPressed)
		{
			if (MapManager.InLobbyScene && PlayerNook.IsPlayerInside)
			{
				PlayerNook.ToggleEditMode();
				return;
			}
			AugmentsPanel.TryToggle();
		}
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x00067968 File Offset: 0x00065B68
	private void MoveCursor(Vector2 pt)
	{
		float num = (float)Screen.currentResolution.width;
		float num2 = (float)Screen.currentResolution.height;
		PanelManager.SetCursorPos((int)(pt.x * num), (int)(pt.y * num2));
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x000679AC File Offset: 0x00065BAC
	public static void CheckApplicationIsActivated()
	{
		IntPtr foregroundWindow = PanelManager.GetForegroundWindow();
		bool flag;
		if (foregroundWindow == IntPtr.Zero)
		{
			flag = false;
		}
		else
		{
			int id = Process.GetCurrentProcess().Id;
			int num;
			PanelManager.GetWindowThreadProcessId(foregroundWindow, out num);
			flag = (num == id);
		}
		if (PanelManager.inFocus != flag)
		{
			PanelManager.inFocus = flag;
			PanelManager.FocusChanged(PanelManager.inFocus);
		}
	}

	// Token: 0x0600109C RID: 4252
	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	private static extern IntPtr GetForegroundWindow();

	// Token: 0x0600109D RID: 4253
	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

	// Token: 0x0600109E RID: 4254
	[DllImport("user32.dll")]
	private static extern bool SetCursorPos(int X, int Y);

	// Token: 0x0600109F RID: 4255 RVA: 0x00067A03 File Offset: 0x00065C03
	// Note: this type is marked as 'beforefieldinit'.
	static PanelManager()
	{
	}

	// Token: 0x04000EA5 RID: 3749
	public List<UIPanel> panels = new List<UIPanel>();

	// Token: 0x04000EA6 RID: 3750
	public static UIPanel curSelect;

	// Token: 0x04000EA7 RID: 3751
	public static PanelManager instance;

	// Token: 0x04000EA8 RID: 3752
	public static bool inFocus = true;

	// Token: 0x04000EA9 RID: 3753
	public GameObject KBM_Module;

	// Token: 0x04000EAA RID: 3754
	public GameObject Controller_Module;

	// Token: 0x04000EAB RID: 3755
	public List<UIPanel> activeStack = new List<UIPanel>();

	// Token: 0x04000EAC RID: 3756
	public Action<PanelType, PanelType> OnPanelChanged;

	// Token: 0x04000EAD RID: 3757
	private PanelType lastPanel = PanelType.__;

	// Token: 0x0200055E RID: 1374
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600249D RID: 9373 RVA: 0x000CE92A File Offset: 0x000CCB2A
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000CE936 File Offset: 0x000CCB36
		public <>c()
		{
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000CE93E File Offset: 0x000CCB3E
		internal PanelType <get_CurrentStackPanels>b__10_0(UIPanel v)
		{
			return v.panelType;
		}

		// Token: 0x040026E1 RID: 9953
		public static readonly PanelManager.<>c <>9 = new PanelManager.<>c();

		// Token: 0x040026E2 RID: 9954
		public static Func<UIPanel, PanelType> <>9__10_0;
	}
}
