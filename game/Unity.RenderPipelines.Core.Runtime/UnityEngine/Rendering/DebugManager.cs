using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Rendering.UI;

namespace UnityEngine.Rendering
{
	// Token: 0x02000068 RID: 104
	public sealed class DebugManager
	{
		// Token: 0x06000339 RID: 825 RVA: 0x0000F124 File Offset: 0x0000D324
		private void RegisterActions()
		{
			this.m_DebugActions = new DebugActionDesc[9];
			this.m_DebugActionStates = new DebugActionState[9];
			this.AddAction(DebugAction.EnableDebugMenu, new DebugActionDesc
			{
				buttonTriggerList = 
				{
					new string[]
					{
						"Enable Debug Button 1",
						"Enable Debug Button 2"
					}
				},
				keyTriggerList = 
				{
					new KeyCode[]
					{
						KeyCode.LeftControl,
						KeyCode.Backspace
					}
				},
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.ResetAll, new DebugActionDesc
			{
				keyTriggerList = 
				{
					new KeyCode[]
					{
						KeyCode.LeftAlt,
						KeyCode.Backspace
					}
				},
				buttonTriggerList = 
				{
					new string[]
					{
						"Debug Reset",
						"Enable Debug Button 2"
					}
				},
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.NextDebugPanel, new DebugActionDesc
			{
				buttonTriggerList = 
				{
					new string[]
					{
						"Debug Next"
					}
				},
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.PreviousDebugPanel, new DebugActionDesc
			{
				buttonTriggerList = 
				{
					new string[]
					{
						"Debug Previous"
					}
				},
				repeatMode = DebugActionRepeatMode.Never
			});
			DebugActionDesc debugActionDesc = new DebugActionDesc();
			debugActionDesc.buttonTriggerList.Add(new string[]
			{
				"Debug Validate"
			});
			debugActionDesc.repeatMode = DebugActionRepeatMode.Never;
			this.AddAction(DebugAction.Action, debugActionDesc);
			this.AddAction(DebugAction.MakePersistent, new DebugActionDesc
			{
				buttonTriggerList = 
				{
					new string[]
					{
						"Debug Persistent"
					}
				},
				repeatMode = DebugActionRepeatMode.Never
			});
			DebugActionDesc debugActionDesc2 = new DebugActionDesc();
			debugActionDesc2.buttonTriggerList.Add(new string[]
			{
				"Debug Multiplier"
			});
			debugActionDesc2.repeatMode = DebugActionRepeatMode.Delay;
			debugActionDesc.repeatDelay = 0f;
			this.AddAction(DebugAction.Multiplier, debugActionDesc2);
			this.AddAction(DebugAction.MoveVertical, new DebugActionDesc
			{
				axisTrigger = "Debug Vertical",
				repeatMode = DebugActionRepeatMode.Delay,
				repeatDelay = 0.16f
			});
			this.AddAction(DebugAction.MoveHorizontal, new DebugActionDesc
			{
				axisTrigger = "Debug Horizontal",
				repeatMode = DebugActionRepeatMode.Delay,
				repeatDelay = 0.16f
			});
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F34F File Offset: 0x0000D54F
		internal void EnableInputActions()
		{
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F354 File Offset: 0x0000D554
		private void AddAction(DebugAction action, DebugActionDesc desc)
		{
			this.m_DebugActions[(int)action] = desc;
			this.m_DebugActionStates[(int)action] = new DebugActionState();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F37C File Offset: 0x0000D57C
		private void SampleAction(int actionIndex)
		{
			DebugActionDesc debugActionDesc = this.m_DebugActions[actionIndex];
			DebugActionState debugActionState = this.m_DebugActionStates[actionIndex];
			if (!debugActionState.runningAction)
			{
				for (int i = 0; i < debugActionDesc.buttonTriggerList.Count; i++)
				{
					string[] array = debugActionDesc.buttonTriggerList[i];
					bool flag = true;
					try
					{
						string[] array2 = array;
						for (int j = 0; j < array2.Length; j++)
						{
							flag = Input.GetButton(array2[j]);
							if (!flag)
							{
								break;
							}
						}
					}
					catch (ArgumentException)
					{
						flag = false;
					}
					if (flag)
					{
						debugActionState.TriggerWithButton(array, 1f);
						break;
					}
				}
				if (debugActionDesc.axisTrigger != "")
				{
					try
					{
						float axis = Input.GetAxis(debugActionDesc.axisTrigger);
						if (axis != 0f)
						{
							debugActionState.TriggerWithAxis(debugActionDesc.axisTrigger, axis);
						}
					}
					catch (ArgumentException)
					{
					}
				}
				for (int k = 0; k < debugActionDesc.keyTriggerList.Count; k++)
				{
					bool flag2 = true;
					KeyCode[] array3 = debugActionDesc.keyTriggerList[k];
					try
					{
						KeyCode[] array4 = array3;
						for (int j = 0; j < array4.Length; j++)
						{
							flag2 = Input.GetKey(array4[j]);
							if (!flag2)
							{
								break;
							}
						}
					}
					catch (ArgumentException)
					{
						flag2 = false;
					}
					if (flag2)
					{
						debugActionState.TriggerWithKey(array3, 1f);
						return;
					}
				}
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		private void UpdateAction(int actionIndex)
		{
			DebugActionDesc desc = this.m_DebugActions[actionIndex];
			DebugActionState debugActionState = this.m_DebugActionStates[actionIndex];
			if (debugActionState.runningAction)
			{
				debugActionState.Update(desc);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F50C File Offset: 0x0000D70C
		internal void UpdateActions()
		{
			for (int i = 0; i < this.m_DebugActions.Length; i++)
			{
				this.UpdateAction(i);
				this.SampleAction(i);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F53A File Offset: 0x0000D73A
		internal float GetAction(DebugAction action)
		{
			return this.m_DebugActionStates[(int)action].actionState;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F54C File Offset: 0x0000D74C
		internal bool GetActionToggleDebugMenuWithTouch()
		{
			Touch[] touches = Input.touches;
			int touchCount = Input.touchCount;
			TouchPhase? touchPhase = new TouchPhase?(TouchPhase.Began);
			if (touchCount == 3)
			{
				foreach (Touch touch in touches)
				{
					if ((touchPhase == null || touch.phase == touchPhase.Value) && touch.tapCount == 2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		internal bool GetActionReleaseScrollTarget()
		{
			bool flag = Input.mouseScrollDelta != Vector2.zero;
			bool touchSupported = Input.touchSupported;
			return flag || touchSupported;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		private void RegisterInputs()
		{
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000F5D6 File Offset: 0x0000D7D6
		public static DebugManager instance
		{
			get
			{
				return DebugManager.s_Instance.Value;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F5E2 File Offset: 0x0000D7E2
		private void UpdateReadOnlyCollection()
		{
			this.m_Panels.Sort();
			this.m_ReadOnlyPanels = this.m_Panels.AsReadOnly();
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000F600 File Offset: 0x0000D800
		public ReadOnlyCollection<DebugUI.Panel> panels
		{
			get
			{
				if (this.m_ReadOnlyPanels == null)
				{
					this.UpdateReadOnlyCollection();
				}
				return this.m_ReadOnlyPanels;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000346 RID: 838 RVA: 0x0000F618 File Offset: 0x0000D818
		// (remove) Token: 0x06000347 RID: 839 RVA: 0x0000F650 File Offset: 0x0000D850
		public event Action<bool> onDisplayRuntimeUIChanged
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = this.onDisplayRuntimeUIChanged;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.onDisplayRuntimeUIChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = this.onDisplayRuntimeUIChanged;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.onDisplayRuntimeUIChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000348 RID: 840 RVA: 0x0000F688 File Offset: 0x0000D888
		// (remove) Token: 0x06000349 RID: 841 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		public event Action onSetDirty
		{
			[CompilerGenerated]
			add
			{
				Action action = this.onSetDirty;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.onSetDirty, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.onSetDirty;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.onSetDirty, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600034A RID: 842 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		// (remove) Token: 0x0600034B RID: 843 RVA: 0x0000F730 File Offset: 0x0000D930
		private event Action resetData
		{
			[CompilerGenerated]
			add
			{
				Action action = this.resetData;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.resetData, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.resetData;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.resetData, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000F765 File Offset: 0x0000D965
		public bool displayEditorUI
		{
			get
			{
				return this.m_EditorOpen;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000F76D File Offset: 0x0000D96D
		public void ToggleEditorUI(bool open)
		{
			this.m_EditorOpen = open;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000F776 File Offset: 0x0000D976
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000F77E File Offset: 0x0000D97E
		public bool enableRuntimeUI
		{
			get
			{
				return this.m_EnableRuntimeUI;
			}
			set
			{
				if (value != this.m_EnableRuntimeUI)
				{
					this.m_EnableRuntimeUI = value;
					DebugUpdater.SetEnabled(value);
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000F796 File Offset: 0x0000D996
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		public bool displayRuntimeUI
		{
			get
			{
				return this.m_Root != null && this.m_Root.activeInHierarchy;
			}
			set
			{
				if (value)
				{
					this.m_Root = Object.Instantiate<Transform>(Resources.Load<Transform>("DebugUICanvas")).gameObject;
					this.m_Root.name = "[Debug Canvas]";
					this.m_Root.transform.localPosition = Vector3.zero;
					this.m_RootUICanvas = this.m_Root.GetComponent<DebugUIHandlerCanvas>();
					this.m_Root.SetActive(true);
				}
				else
				{
					CoreUtils.Destroy(this.m_Root);
					this.m_Root = null;
					this.m_RootUICanvas = null;
				}
				this.onDisplayRuntimeUIChanged(value);
				DebugUpdater.HandleInternalEventSystemComponents(value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000F84D File Offset: 0x0000DA4D
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000F86A File Offset: 0x0000DA6A
		public bool displayPersistentRuntimeUI
		{
			get
			{
				return this.m_RootUIPersistentCanvas != null && this.m_PersistentRoot.activeInHierarchy;
			}
			set
			{
				if (value)
				{
					this.EnsurePersistentCanvas();
					return;
				}
				CoreUtils.Destroy(this.m_PersistentRoot);
				this.m_PersistentRoot = null;
				this.m_RootUIPersistentCanvas = null;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F890 File Offset: 0x0000DA90
		private DebugManager()
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000F8FF File Offset: 0x0000DAFF
		public void RefreshEditor()
		{
			this.refreshEditorRequested = true;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F908 File Offset: 0x0000DB08
		public void Reset()
		{
			Action action = this.resetData;
			if (action != null)
			{
				action();
			}
			this.ReDrawOnScreenDebug();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F921 File Offset: 0x0000DB21
		public void ReDrawOnScreenDebug()
		{
			if (this.displayRuntimeUI)
			{
				DebugUIHandlerCanvas rootUICanvas = this.m_RootUICanvas;
				if (rootUICanvas == null)
				{
					return;
				}
				rootUICanvas.RequestHierarchyReset();
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F93B File Offset: 0x0000DB3B
		public void RegisterData(IDebugData data)
		{
			this.resetData += data.GetReset();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000F949 File Offset: 0x0000DB49
		public void UnregisterData(IDebugData data)
		{
			this.resetData -= data.GetReset();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000F958 File Offset: 0x0000DB58
		public int GetState()
		{
			int num = 17;
			foreach (DebugUI.Panel panel in this.m_Panels)
			{
				num = num * 23 + panel.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
		internal void RegisterRootCanvas(DebugUIHandlerCanvas root)
		{
			this.m_Root = root.gameObject;
			this.m_RootUICanvas = root;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000F9C9 File Offset: 0x0000DBC9
		internal void ChangeSelection(DebugUIHandlerWidget widget, bool fromNext)
		{
			this.m_RootUICanvas.ChangeSelection(widget, fromNext);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		internal void SetScrollTarget(DebugUIHandlerWidget widget)
		{
			if (this.m_RootUICanvas != null)
			{
				this.m_RootUICanvas.SetScrollTarget(widget);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		private void EnsurePersistentCanvas()
		{
			if (this.m_RootUIPersistentCanvas == null)
			{
				DebugUIHandlerPersistentCanvas debugUIHandlerPersistentCanvas = Object.FindObjectOfType<DebugUIHandlerPersistentCanvas>();
				if (debugUIHandlerPersistentCanvas == null)
				{
					this.m_PersistentRoot = Object.Instantiate<Transform>(Resources.Load<Transform>("DebugUIPersistentCanvas")).gameObject;
					this.m_PersistentRoot.name = "[Debug Canvas - Persistent]";
					this.m_PersistentRoot.transform.localPosition = Vector3.zero;
				}
				else
				{
					this.m_PersistentRoot = debugUIHandlerPersistentCanvas.gameObject;
				}
				this.m_RootUIPersistentCanvas = this.m_PersistentRoot.GetComponent<DebugUIHandlerPersistentCanvas>();
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000FA7C File Offset: 0x0000DC7C
		internal void TogglePersistent(DebugUI.Widget widget)
		{
			if (widget == null)
			{
				return;
			}
			DebugUI.Value value = widget as DebugUI.Value;
			if (value == null)
			{
				Debug.Log("Only DebugUI.Value items can be made persistent.");
				return;
			}
			this.EnsurePersistentCanvas();
			this.m_RootUIPersistentCanvas.Toggle(value);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
		private void OnPanelDirty(DebugUI.Panel panel)
		{
			this.onSetDirty();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
		public int PanelIndex([DisallowNull] string displayName)
		{
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			for (int i = 0; i < this.m_Panels.Count; i++)
			{
				if (displayName.Equals(this.m_Panels[i].displayName, StringComparison.InvariantCultureIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000FB0E File Offset: 0x0000DD0E
		public string PanelDiplayName([DisallowNull] int panelIndex)
		{
			if (panelIndex < 0 || panelIndex > this.m_Panels.Count - 1)
			{
				return string.Empty;
			}
			return this.m_Panels[panelIndex].displayName;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000FB3B File Offset: 0x0000DD3B
		public void RequestEditorWindowPanelIndex(int index)
		{
			this.m_RequestedPanelIndex = new int?(index);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000FB49 File Offset: 0x0000DD49
		internal int? GetRequestedEditorWindowPanelIndex()
		{
			int? requestedPanelIndex = this.m_RequestedPanelIndex;
			this.m_RequestedPanelIndex = null;
			return requestedPanelIndex;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000FB60 File Offset: 0x0000DD60
		public DebugUI.Panel GetPanel(string displayName, bool createIfNull = false, int groupIndex = 0, bool overrideIfExist = false)
		{
			int num = this.PanelIndex(displayName);
			DebugUI.Panel panel = (num >= 0) ? this.m_Panels[num] : null;
			if (panel != null)
			{
				if (!overrideIfExist)
				{
					return panel;
				}
				panel.onSetDirty -= this.OnPanelDirty;
				this.RemovePanel(panel);
				panel = null;
			}
			if (createIfNull)
			{
				panel = new DebugUI.Panel
				{
					displayName = displayName,
					groupIndex = groupIndex
				};
				panel.onSetDirty += this.OnPanelDirty;
				this.m_Panels.Add(panel);
				this.UpdateReadOnlyCollection();
			}
			return panel;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000FBEC File Offset: 0x0000DDEC
		public int FindPanelIndex(string displayName)
		{
			return this.m_Panels.FindIndex((DebugUI.Panel p) => p.displayName == displayName);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000FC20 File Offset: 0x0000DE20
		public void RemovePanel(string displayName)
		{
			DebugUI.Panel panel = null;
			foreach (DebugUI.Panel panel2 in this.m_Panels)
			{
				if (panel2.displayName == displayName)
				{
					panel2.onSetDirty -= this.OnPanelDirty;
					panel = panel2;
					break;
				}
			}
			this.RemovePanel(panel);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		public void RemovePanel(DebugUI.Panel panel)
		{
			if (panel == null)
			{
				return;
			}
			this.m_Panels.Remove(panel);
			this.UpdateReadOnlyCollection();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		public DebugUI.Widget GetItem(string queryPath)
		{
			foreach (DebugUI.Panel container in this.m_Panels)
			{
				DebugUI.Widget item = this.GetItem(queryPath, container);
				if (item != null)
				{
					return item;
				}
			}
			return null;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000FD18 File Offset: 0x0000DF18
		private DebugUI.Widget GetItem(string queryPath, DebugUI.IContainer container)
		{
			foreach (DebugUI.Widget widget in container.children)
			{
				if (widget.queryPath == queryPath)
				{
					return widget;
				}
				DebugUI.IContainer container2 = widget as DebugUI.IContainer;
				if (container2 != null)
				{
					DebugUI.Widget item = this.GetItem(queryPath, container2);
					if (item != null)
					{
						return item;
					}
				}
			}
			return null;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000FD90 File Offset: 0x0000DF90
		// Note: this type is marked as 'beforefieldinit'.
		static DebugManager()
		{
		}

		// Token: 0x0400021B RID: 539
		private const string kEnableDebugBtn1 = "Enable Debug Button 1";

		// Token: 0x0400021C RID: 540
		private const string kEnableDebugBtn2 = "Enable Debug Button 2";

		// Token: 0x0400021D RID: 541
		private const string kDebugPreviousBtn = "Debug Previous";

		// Token: 0x0400021E RID: 542
		private const string kDebugNextBtn = "Debug Next";

		// Token: 0x0400021F RID: 543
		private const string kValidateBtn = "Debug Validate";

		// Token: 0x04000220 RID: 544
		private const string kPersistentBtn = "Debug Persistent";

		// Token: 0x04000221 RID: 545
		private const string kDPadVertical = "Debug Vertical";

		// Token: 0x04000222 RID: 546
		private const string kDPadHorizontal = "Debug Horizontal";

		// Token: 0x04000223 RID: 547
		private const string kMultiplierBtn = "Debug Multiplier";

		// Token: 0x04000224 RID: 548
		private const string kResetBtn = "Debug Reset";

		// Token: 0x04000225 RID: 549
		private const string kEnableDebug = "Enable Debug";

		// Token: 0x04000226 RID: 550
		private DebugActionDesc[] m_DebugActions;

		// Token: 0x04000227 RID: 551
		private DebugActionState[] m_DebugActionStates;

		// Token: 0x04000228 RID: 552
		private static readonly Lazy<DebugManager> s_Instance = new Lazy<DebugManager>(() => new DebugManager());

		// Token: 0x04000229 RID: 553
		private ReadOnlyCollection<DebugUI.Panel> m_ReadOnlyPanels;

		// Token: 0x0400022A RID: 554
		private readonly List<DebugUI.Panel> m_Panels = new List<DebugUI.Panel>();

		// Token: 0x0400022B RID: 555
		[CompilerGenerated]
		private Action<bool> onDisplayRuntimeUIChanged = delegate(bool <p0>)
		{
		};

		// Token: 0x0400022C RID: 556
		[CompilerGenerated]
		private Action onSetDirty = delegate()
		{
		};

		// Token: 0x0400022D RID: 557
		[CompilerGenerated]
		private Action resetData;

		// Token: 0x0400022E RID: 558
		public bool refreshEditorRequested;

		// Token: 0x0400022F RID: 559
		private int? m_RequestedPanelIndex;

		// Token: 0x04000230 RID: 560
		private GameObject m_Root;

		// Token: 0x04000231 RID: 561
		private DebugUIHandlerCanvas m_RootUICanvas;

		// Token: 0x04000232 RID: 562
		private GameObject m_PersistentRoot;

		// Token: 0x04000233 RID: 563
		private DebugUIHandlerPersistentCanvas m_RootUIPersistentCanvas;

		// Token: 0x04000234 RID: 564
		private bool m_EditorOpen;

		// Token: 0x04000235 RID: 565
		private bool m_EnableRuntimeUI = true;

		// Token: 0x02000145 RID: 325
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000850 RID: 2128 RVA: 0x00022D89 File Offset: 0x00020F89
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000851 RID: 2129 RVA: 0x00022D95 File Offset: 0x00020F95
			public <>c()
			{
			}

			// Token: 0x06000852 RID: 2130 RVA: 0x00022D9D File Offset: 0x00020F9D
			internal void <.ctor>b__60_0(bool <p0>)
			{
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x00022D9F File Offset: 0x00020F9F
			internal void <.ctor>b__60_1()
			{
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x00022DA1 File Offset: 0x00020FA1
			internal DebugManager <.cctor>b__83_0()
			{
				return new DebugManager();
			}

			// Token: 0x04000511 RID: 1297
			public static readonly DebugManager.<>c <>9 = new DebugManager.<>c();

			// Token: 0x04000512 RID: 1298
			public static Action<bool> <>9__60_0;

			// Token: 0x04000513 RID: 1299
			public static Action <>9__60_1;
		}

		// Token: 0x02000146 RID: 326
		[CompilerGenerated]
		private sealed class <>c__DisplayClass78_0
		{
			// Token: 0x06000855 RID: 2133 RVA: 0x00022DA8 File Offset: 0x00020FA8
			public <>c__DisplayClass78_0()
			{
			}

			// Token: 0x06000856 RID: 2134 RVA: 0x00022DB0 File Offset: 0x00020FB0
			internal bool <FindPanelIndex>b__0(DebugUI.Panel p)
			{
				return p.displayName == this.displayName;
			}

			// Token: 0x04000514 RID: 1300
			public string displayName;
		}
	}
}
