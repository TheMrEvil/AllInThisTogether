using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C3 RID: 195
	internal static class UIEventRegistration
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x00018318 File Offset: 0x00016518
		static UIEventRegistration()
		{
			GUIUtility.takeCapture = (Action)Delegate.Combine(GUIUtility.takeCapture, new Action(delegate()
			{
				UIEventRegistration.TakeCapture();
			}));
			GUIUtility.releaseCapture = (Action)Delegate.Combine(GUIUtility.releaseCapture, new Action(delegate()
			{
				UIEventRegistration.ReleaseCapture();
			}));
			GUIUtility.processEvent = (Func<int, IntPtr, bool>)Delegate.Combine(GUIUtility.processEvent, new Func<int, IntPtr, bool>((int i, IntPtr ptr) => UIEventRegistration.ProcessEvent(i, ptr)));
			GUIUtility.cleanupRoots = (Action)Delegate.Combine(GUIUtility.cleanupRoots, new Action(delegate()
			{
				UIEventRegistration.CleanupRoots();
			}));
			GUIUtility.endContainerGUIFromException = (Func<Exception, bool>)Delegate.Combine(GUIUtility.endContainerGUIFromException, new Func<Exception, bool>((Exception exception) => UIEventRegistration.EndContainerGUIFromException(exception)));
			GUIUtility.guiChanged = (Action)Delegate.Combine(GUIUtility.guiChanged, new Action(delegate()
			{
				UIEventRegistration.MakeCurrentIMGUIContainerDirty();
			}));
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00018408 File Offset: 0x00016608
		internal static void RegisterUIElementSystem(IUIElementsUtility utility)
		{
			UIEventRegistration.s_Utilities.Insert(0, utility);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00018418 File Offset: 0x00016618
		private static void TakeCapture()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.TakeCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00018474 File Offset: 0x00016674
		private static void ReleaseCapture()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.ReleaseCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000184D0 File Offset: 0x000166D0
		private static bool EndContainerGUIFromException(Exception exception)
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.EndContainerGUIFromException(exception);
				if (flag)
				{
					return true;
				}
			}
			return GUIUtility.ShouldRethrowException(exception);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001853C File Offset: 0x0001673C
		private static bool ProcessEvent(int instanceID, IntPtr nativeEventPtr)
		{
			bool result = false;
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.ProcessEvent(instanceID, nativeEventPtr, ref result);
				if (flag)
				{
					return result;
				}
			}
			return false;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000185AC File Offset: 0x000167AC
		private static void CleanupRoots()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.CleanupRoots();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00018608 File Offset: 0x00016808
		internal static void MakeCurrentIMGUIContainerDirty()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.MakeCurrentIMGUIContainerDirty();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00018664 File Offset: 0x00016864
		internal static void UpdateSchedulers()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				iuielementsUtility.UpdateSchedulers();
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000186BC File Offset: 0x000168BC
		internal static void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback)
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				iuielementsUtility.RequestRepaintForPanels(repaintCallback);
			}
		}

		// Token: 0x04000290 RID: 656
		private static List<IUIElementsUtility> s_Utilities = new List<IUIElementsUtility>();

		// Token: 0x020000C4 RID: 196
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000688 RID: 1672 RVA: 0x00018714 File Offset: 0x00016914
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x00018720 File Offset: 0x00016920
			internal void <.cctor>b__1_0()
			{
				UIEventRegistration.TakeCapture();
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x00018728 File Offset: 0x00016928
			internal void <.cctor>b__1_1()
			{
				UIEventRegistration.ReleaseCapture();
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x00018730 File Offset: 0x00016930
			internal bool <.cctor>b__1_2(int i, IntPtr ptr)
			{
				return UIEventRegistration.ProcessEvent(i, ptr);
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x00018749 File Offset: 0x00016949
			internal void <.cctor>b__1_3()
			{
				UIEventRegistration.CleanupRoots();
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x00018751 File Offset: 0x00016951
			internal bool <.cctor>b__1_4(Exception exception)
			{
				return UIEventRegistration.EndContainerGUIFromException(exception);
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x00018759 File Offset: 0x00016959
			internal void <.cctor>b__1_5()
			{
				UIEventRegistration.MakeCurrentIMGUIContainerDirty();
			}

			// Token: 0x04000291 RID: 657
			public static readonly UIEventRegistration.<>c <>9 = new UIEventRegistration.<>c();
		}
	}
}
