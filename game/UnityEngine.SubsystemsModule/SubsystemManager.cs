using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemManager
	{
		// Token: 0x06000033 RID: 51 RVA: 0x0000224C File Offset: 0x0000044C
		[RequiredByNativeCode]
		private static void ReloadSubsystemsStarted()
		{
			bool flag = SubsystemManager.reloadSubsytemsStarted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsStarted();
			}
			bool flag2 = SubsystemManager.beforeReloadSubsystems != null;
			if (flag2)
			{
				SubsystemManager.beforeReloadSubsystems();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002288 File Offset: 0x00000488
		[RequiredByNativeCode]
		private static void ReloadSubsystemsCompleted()
		{
			bool flag = SubsystemManager.reloadSubsytemsCompleted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsCompleted();
			}
			bool flag2 = SubsystemManager.afterReloadSubsystems != null;
			if (flag2)
			{
				SubsystemManager.afterReloadSubsystems();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000022C4 File Offset: 0x000004C4
		[RequiredByNativeCode]
		private static void InitializeIntegratedSubsystem(IntPtr ptr, IntegratedSubsystem subsystem)
		{
			subsystem.m_Ptr = ptr;
			subsystem.SetHandle(subsystem);
			SubsystemManager.s_IntegratedSubsystems.Add(subsystem);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000022E4 File Offset: 0x000004E4
		[RequiredByNativeCode]
		private static void ClearSubsystems()
		{
			foreach (IntegratedSubsystem integratedSubsystem in SubsystemManager.s_IntegratedSubsystems)
			{
				integratedSubsystem.m_Ptr = IntPtr.Zero;
			}
			SubsystemManager.s_IntegratedSubsystems.Clear();
			SubsystemManager.s_StandaloneSubsystems.Clear();
			SubsystemManager.s_DeprecatedSubsystems.Clear();
		}

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StaticConstructScriptingClassMap();

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportSingleSubsystemAnalytics(string id);

		// Token: 0x06000039 RID: 57 RVA: 0x00002360 File Offset: 0x00000560
		static SubsystemManager()
		{
			SubsystemManager.StaticConstructScriptingClassMap();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002386 File Offset: 0x00000586
		public static void GetAllSubsystemDescriptors(List<ISubsystemDescriptor> descriptors)
		{
			SubsystemDescriptorStore.GetAllSubsystemDescriptors(descriptors);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002390 File Offset: 0x00000590
		public static void GetSubsystemDescriptors<T>(List<T> descriptors) where T : ISubsystemDescriptor
		{
			SubsystemDescriptorStore.GetSubsystemDescriptors<T>(descriptors);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000239A File Offset: 0x0000059A
		public static void GetSubsystems<T>(List<T> subsystems) where T : ISubsystem
		{
			subsystems.Clear();
			SubsystemManager.AddSubsystemSubset<IntegratedSubsystem, T>(SubsystemManager.s_IntegratedSubsystems, subsystems);
			SubsystemManager.AddSubsystemSubset<SubsystemWithProvider, T>(SubsystemManager.s_StandaloneSubsystems, subsystems);
			SubsystemManager.AddSubsystemSubset<Subsystem, T>(SubsystemManager.s_DeprecatedSubsystems, subsystems);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000023C8 File Offset: 0x000005C8
		private static void AddSubsystemSubset<TBaseTypeInList, TQueryType>(List<TBaseTypeInList> copyFrom, List<TQueryType> copyTo) where TBaseTypeInList : ISubsystem where TQueryType : ISubsystem
		{
			foreach (TBaseTypeInList tbaseTypeInList in copyFrom)
			{
				TQueryType item;
				bool flag;
				if (tbaseTypeInList is TQueryType)
				{
					item = (tbaseTypeInList as TQueryType);
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					copyTo.Add(item);
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003E RID: 62 RVA: 0x00002444 File Offset: 0x00000644
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00002478 File Offset: 0x00000678
		public static event Action beforeReloadSubsystems
		{
			[CompilerGenerated]
			add
			{
				Action action = SubsystemManager.beforeReloadSubsystems;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SubsystemManager.beforeReloadSubsystems, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SubsystemManager.beforeReloadSubsystems;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SubsystemManager.beforeReloadSubsystems, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000040 RID: 64 RVA: 0x000024AC File Offset: 0x000006AC
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x000024E0 File Offset: 0x000006E0
		public static event Action afterReloadSubsystems
		{
			[CompilerGenerated]
			add
			{
				Action action = SubsystemManager.afterReloadSubsystems;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SubsystemManager.afterReloadSubsystems, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SubsystemManager.afterReloadSubsystems;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SubsystemManager.afterReloadSubsystems, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002514 File Offset: 0x00000714
		internal static IntegratedSubsystem GetIntegratedSubsystemByPtr(IntPtr ptr)
		{
			foreach (IntegratedSubsystem integratedSubsystem in SubsystemManager.s_IntegratedSubsystems)
			{
				bool flag = integratedSubsystem.m_Ptr == ptr;
				if (flag)
				{
					return integratedSubsystem;
				}
			}
			return null;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002580 File Offset: 0x00000780
		internal static void RemoveIntegratedSubsystemByPtr(IntPtr ptr)
		{
			for (int i = 0; i < SubsystemManager.s_IntegratedSubsystems.Count; i++)
			{
				bool flag = SubsystemManager.s_IntegratedSubsystems[i].m_Ptr != ptr;
				if (!flag)
				{
					SubsystemManager.s_IntegratedSubsystems[i].m_Ptr = IntPtr.Zero;
					SubsystemManager.s_IntegratedSubsystems.RemoveAt(i);
					break;
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000025E7 File Offset: 0x000007E7
		internal static void AddStandaloneSubsystem(SubsystemWithProvider subsystem)
		{
			SubsystemManager.s_StandaloneSubsystems.Add(subsystem);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000025F8 File Offset: 0x000007F8
		internal static bool RemoveStandaloneSubsystem(SubsystemWithProvider subsystem)
		{
			return SubsystemManager.s_StandaloneSubsystems.Remove(subsystem);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002618 File Offset: 0x00000818
		internal static SubsystemWithProvider FindStandaloneSubsystemByDescriptor(SubsystemDescriptorWithProvider descriptor)
		{
			foreach (SubsystemWithProvider subsystemWithProvider in SubsystemManager.s_StandaloneSubsystems)
			{
				bool flag = subsystemWithProvider.descriptor == descriptor;
				if (flag)
				{
					return subsystemWithProvider;
				}
			}
			return null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002680 File Offset: 0x00000880
		public static void GetInstances<T>(List<T> subsystems) where T : ISubsystem
		{
			SubsystemManager.GetSubsystems<T>(subsystems);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000268A File Offset: 0x0000088A
		internal static void AddDeprecatedSubsystem(Subsystem subsystem)
		{
			SubsystemManager.s_DeprecatedSubsystems.Add(subsystem);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002698 File Offset: 0x00000898
		internal static bool RemoveDeprecatedSubsystem(Subsystem subsystem)
		{
			return SubsystemManager.s_DeprecatedSubsystems.Remove(subsystem);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000026A8 File Offset: 0x000008A8
		internal static Subsystem FindDeprecatedSubsystemByDescriptor(SubsystemDescriptor descriptor)
		{
			foreach (Subsystem subsystem in SubsystemManager.s_DeprecatedSubsystems)
			{
				bool flag = subsystem.m_SubsystemDescriptor == descriptor;
				if (flag)
				{
					return subsystem;
				}
			}
			return null;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600004B RID: 75 RVA: 0x00002710 File Offset: 0x00000910
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x00002744 File Offset: 0x00000944
		public static event Action reloadSubsytemsStarted
		{
			[CompilerGenerated]
			add
			{
				Action action = SubsystemManager.reloadSubsytemsStarted;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SubsystemManager.reloadSubsytemsStarted, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SubsystemManager.reloadSubsytemsStarted;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SubsystemManager.reloadSubsytemsStarted, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600004D RID: 77 RVA: 0x00002778 File Offset: 0x00000978
		// (remove) Token: 0x0600004E RID: 78 RVA: 0x000027AC File Offset: 0x000009AC
		public static event Action reloadSubsytemsCompleted
		{
			[CompilerGenerated]
			add
			{
				Action action = SubsystemManager.reloadSubsytemsCompleted;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SubsystemManager.reloadSubsytemsCompleted, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SubsystemManager.reloadSubsytemsCompleted;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SubsystemManager.reloadSubsytemsCompleted, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x04000007 RID: 7
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action beforeReloadSubsystems;

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action afterReloadSubsystems;

		// Token: 0x04000009 RID: 9
		private static List<IntegratedSubsystem> s_IntegratedSubsystems = new List<IntegratedSubsystem>();

		// Token: 0x0400000A RID: 10
		private static List<SubsystemWithProvider> s_StandaloneSubsystems = new List<SubsystemWithProvider>();

		// Token: 0x0400000B RID: 11
		private static List<Subsystem> s_DeprecatedSubsystems = new List<Subsystem>();

		// Token: 0x0400000C RID: 12
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action reloadSubsytemsStarted;

		// Token: 0x0400000D RID: 13
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action reloadSubsytemsCompleted;
	}
}
