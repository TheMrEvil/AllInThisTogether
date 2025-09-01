using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006E RID: 110
	internal class DebugUpdater : MonoBehaviour
	{
		// Token: 0x06000386 RID: 902 RVA: 0x00011005 File Offset: 0x0000F205
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void RuntimeInit()
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011007 File Offset: 0x0000F207
		internal static void SetEnabled(bool enabled)
		{
			if (enabled)
			{
				DebugUpdater.EnableRuntime();
				return;
			}
			DebugUpdater.DisableRuntime();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011018 File Offset: 0x0000F218
		private static void EnableRuntime()
		{
			if (DebugUpdater.s_Instance != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "[Debug Updater]";
			DebugUpdater.s_Instance = gameObject.AddComponent<DebugUpdater>();
			DebugUpdater.s_Instance.m_Orientation = Screen.orientation;
			Object.DontDestroyOnLoad(gameObject);
			DebugManager.instance.EnableInputActions();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001106C File Offset: 0x0000F26C
		private static void DisableRuntime()
		{
			DebugManager instance = DebugManager.instance;
			instance.displayRuntimeUI = false;
			instance.displayPersistentRuntimeUI = false;
			if (DebugUpdater.s_Instance != null)
			{
				CoreUtils.Destroy(DebugUpdater.s_Instance.gameObject);
				DebugUpdater.s_Instance = null;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000110A2 File Offset: 0x0000F2A2
		internal static void HandleInternalEventSystemComponents(bool uiEnabled)
		{
			if (DebugUpdater.s_Instance == null)
			{
				return;
			}
			if (uiEnabled)
			{
				DebugUpdater.s_Instance.EnsureExactlyOneEventSystem();
				return;
			}
			DebugUpdater.s_Instance.DestroyDebugEventSystem();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000110CC File Offset: 0x0000F2CC
		private void EnsureExactlyOneEventSystem()
		{
			EventSystem[] array = Object.FindObjectsOfType<EventSystem>();
			EventSystem component = base.GetComponent<EventSystem>();
			if (array.Length > 1 && component != null)
			{
				Debug.Log("More than one EventSystem detected in scene. Destroying EventSystem owned by DebugUpdater.");
				this.DestroyDebugEventSystem();
				return;
			}
			if (array.Length == 0)
			{
				Debug.Log("No EventSystem available. Creating a new EventSystem to enable Rendering Debugger runtime UI.");
				this.CreateDebugEventSystem();
				return;
			}
			base.StartCoroutine(this.DoAfterInputModuleUpdated(new Action(this.CheckInputModuleExists)));
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011134 File Offset: 0x0000F334
		private IEnumerator DoAfterInputModuleUpdated(Action action)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			action();
			yield break;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011143 File Offset: 0x0000F343
		private void CheckInputModuleExists()
		{
			if (EventSystem.current != null && EventSystem.current.currentInputModule == null)
			{
				Debug.LogWarning("Found a game object with EventSystem component but no corresponding BaseInputModule component - Debug UI input might not work correctly.");
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001116E File Offset: 0x0000F36E
		private void CreateDebugEventSystem()
		{
			base.gameObject.AddComponent<EventSystem>();
			base.gameObject.AddComponent<StandaloneInputModule>();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00011188 File Offset: 0x0000F388
		private void DestroyDebugEventSystem()
		{
			Object component = base.GetComponent<EventSystem>();
			CoreUtils.Destroy(base.GetComponent<StandaloneInputModule>());
			CoreUtils.Destroy(base.GetComponent<BaseInput>());
			CoreUtils.Destroy(component);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000111AC File Offset: 0x0000F3AC
		private void Update()
		{
			DebugManager instance = DebugManager.instance;
			if (this.m_RuntimeUiWasVisibleLastFrame != instance.displayRuntimeUI)
			{
				DebugUpdater.HandleInternalEventSystemComponents(instance.displayRuntimeUI);
			}
			instance.UpdateActions();
			if (instance.GetAction(DebugAction.EnableDebugMenu) != 0f || instance.GetActionToggleDebugMenuWithTouch())
			{
				instance.displayRuntimeUI = !instance.displayRuntimeUI;
			}
			if (instance.displayRuntimeUI)
			{
				if (instance.GetAction(DebugAction.ResetAll) != 0f)
				{
					instance.Reset();
				}
				if (instance.GetActionReleaseScrollTarget())
				{
					instance.SetScrollTarget(null);
				}
			}
			if (this.m_Orientation != Screen.orientation)
			{
				base.StartCoroutine(DebugUpdater.RefreshRuntimeUINextFrame());
				this.m_Orientation = Screen.orientation;
			}
			this.m_RuntimeUiWasVisibleLastFrame = instance.displayRuntimeUI;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001125E File Offset: 0x0000F45E
		private static IEnumerator RefreshRuntimeUINextFrame()
		{
			yield return null;
			DebugManager.instance.ReDrawOnScreenDebug();
			yield break;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00011266 File Offset: 0x0000F466
		public DebugUpdater()
		{
		}

		// Token: 0x04000248 RID: 584
		private static DebugUpdater s_Instance;

		// Token: 0x04000249 RID: 585
		private ScreenOrientation m_Orientation;

		// Token: 0x0400024A RID: 586
		private bool m_RuntimeUiWasVisibleLastFrame;

		// Token: 0x02000163 RID: 355
		[CompilerGenerated]
		private sealed class <DoAfterInputModuleUpdated>d__9 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060008E1 RID: 2273 RVA: 0x00023B2E File Offset: 0x00021D2E
			[DebuggerHidden]
			public <DoAfterInputModuleUpdated>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x00023B3D File Offset: 0x00021D3D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060008E3 RID: 2275 RVA: 0x00023B40 File Offset: 0x00021D40
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = new WaitForEndOfFrame();
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					this.<>2__current = new WaitForEndOfFrame();
					this.<>1__state = 2;
					return true;
				case 2:
					this.<>1__state = -1;
					action();
					return false;
				default:
					return false;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00023BB1 File Offset: 0x00021DB1
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060008E5 RID: 2277 RVA: 0x00023BB9 File Offset: 0x00021DB9
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00023BC0 File Offset: 0x00021DC0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000560 RID: 1376
			private int <>1__state;

			// Token: 0x04000561 RID: 1377
			private object <>2__current;

			// Token: 0x04000562 RID: 1378
			public Action action;
		}

		// Token: 0x02000164 RID: 356
		[CompilerGenerated]
		private sealed class <RefreshRuntimeUINextFrame>d__14 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060008E7 RID: 2279 RVA: 0x00023BC8 File Offset: 0x00021DC8
			[DebuggerHidden]
			public <RefreshRuntimeUINextFrame>d__14(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060008E8 RID: 2280 RVA: 0x00023BD7 File Offset: 0x00021DD7
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060008E9 RID: 2281 RVA: 0x00023BDC File Offset: 0x00021DDC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				DebugManager.instance.ReDrawOnScreenDebug();
				return false;
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060008EA RID: 2282 RVA: 0x00023C22 File Offset: 0x00021E22
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060008EB RID: 2283 RVA: 0x00023C2A File Offset: 0x00021E2A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060008EC RID: 2284 RVA: 0x00023C31 File Offset: 0x00021E31
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000563 RID: 1379
			private int <>1__state;

			// Token: 0x04000564 RID: 1380
			private object <>2__current;
		}
	}
}
