using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x020002D7 RID: 727
	[NativeHeader("Runtime/Scripting/GarbageCollector.h")]
	public static class GarbageCollector
	{
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001DF7 RID: 7671 RVA: 0x00030CB8 File Offset: 0x0002EEB8
		// (remove) Token: 0x06001DF8 RID: 7672 RVA: 0x00030CEC File Offset: 0x0002EEEC
		public static event Action<GarbageCollector.Mode> GCModeChanged
		{
			[CompilerGenerated]
			add
			{
				Action<GarbageCollector.Mode> action = GarbageCollector.GCModeChanged;
				Action<GarbageCollector.Mode> action2;
				do
				{
					action2 = action;
					Action<GarbageCollector.Mode> value2 = (Action<GarbageCollector.Mode>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<GarbageCollector.Mode>>(ref GarbageCollector.GCModeChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<GarbageCollector.Mode> action = GarbageCollector.GCModeChanged;
				Action<GarbageCollector.Mode> action2;
				do
				{
					action2 = action;
					Action<GarbageCollector.Mode> value2 = (Action<GarbageCollector.Mode>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<GarbageCollector.Mode>>(ref GarbageCollector.GCModeChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x00030D20 File Offset: 0x0002EF20
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x00030D38 File Offset: 0x0002EF38
		public static GarbageCollector.Mode GCMode
		{
			get
			{
				return GarbageCollector.GetMode();
			}
			set
			{
				bool flag = value == GarbageCollector.GetMode();
				if (!flag)
				{
					GarbageCollector.SetMode(value);
					bool flag2 = GarbageCollector.GCModeChanged != null;
					if (flag2)
					{
						GarbageCollector.GCModeChanged(value);
					}
				}
			}
		}

		// Token: 0x06001DFB RID: 7675
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMode(GarbageCollector.Mode mode);

		// Token: 0x06001DFC RID: 7676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GarbageCollector.Mode GetMode();

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001DFD RID: 7677
		public static extern bool isIncremental { [NativeMethod("GetIncrementalEnabled")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001DFE RID: 7678
		// (set) Token: 0x06001DFF RID: 7679
		public static extern ulong incrementalTimeSliceNanoseconds { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001E00 RID: 7680
		[NativeMethod("CollectIncrementalWrapper")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CollectIncremental(ulong nanoseconds = 0UL);

		// Token: 0x040009D2 RID: 2514
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<GarbageCollector.Mode> GCModeChanged;

		// Token: 0x020002D8 RID: 728
		public enum Mode
		{
			// Token: 0x040009D4 RID: 2516
			Disabled,
			// Token: 0x040009D5 RID: 2517
			Enabled,
			// Token: 0x040009D6 RID: 2518
			Manual
		}
	}
}
