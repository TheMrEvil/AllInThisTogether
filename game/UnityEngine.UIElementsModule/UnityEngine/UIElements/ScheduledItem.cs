using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000069 RID: 105
	internal abstract class ScheduledItem
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000AC60 File Offset: 0x00008E60
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000AC68 File Offset: 0x00008E68
		public long startMs
		{
			[CompilerGenerated]
			get
			{
				return this.<startMs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<startMs>k__BackingField = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000AC71 File Offset: 0x00008E71
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000AC79 File Offset: 0x00008E79
		public long delayMs
		{
			[CompilerGenerated]
			get
			{
				return this.<delayMs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<delayMs>k__BackingField = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000AC82 File Offset: 0x00008E82
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000AC8A File Offset: 0x00008E8A
		public long intervalMs
		{
			[CompilerGenerated]
			get
			{
				return this.<intervalMs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<intervalMs>k__BackingField = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000AC93 File Offset: 0x00008E93
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000AC9B File Offset: 0x00008E9B
		public long endTimeMs
		{
			[CompilerGenerated]
			get
			{
				return this.<endTimeMs>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<endTimeMs>k__BackingField = value;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		public ScheduledItem()
		{
			this.ResetStartTime();
			this.timerUpdateStopCondition = ScheduledItem.OnceCondition;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		protected void ResetStartTime()
		{
			this.startMs = Panel.TimeSinceStartupMs();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000ACCF File Offset: 0x00008ECF
		public void SetDuration(long durationMs)
		{
			this.endTimeMs = this.startMs + durationMs;
		}

		// Token: 0x060002FF RID: 767
		public abstract void PerformTimerUpdate(TimerState state);

		// Token: 0x06000300 RID: 768 RVA: 0x00002166 File Offset: 0x00000366
		internal virtual void OnItemUnscheduled()
		{
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		public virtual bool ShouldUnschedule()
		{
			bool flag = this.timerUpdateStopCondition != null;
			return flag && this.timerUpdateStopCondition();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000AD13 File Offset: 0x00008F13
		// Note: this type is marked as 'beforefieldinit'.
		static ScheduledItem()
		{
		}

		// Token: 0x04000154 RID: 340
		public Func<bool> timerUpdateStopCondition;

		// Token: 0x04000155 RID: 341
		public static readonly Func<bool> OnceCondition = () => true;

		// Token: 0x04000156 RID: 342
		public static readonly Func<bool> ForeverCondition = () => false;

		// Token: 0x04000157 RID: 343
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private long <startMs>k__BackingField;

		// Token: 0x04000158 RID: 344
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long <delayMs>k__BackingField;

		// Token: 0x04000159 RID: 345
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private long <intervalMs>k__BackingField;

		// Token: 0x0400015A RID: 346
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long <endTimeMs>k__BackingField;

		// Token: 0x0200006A RID: 106
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000303 RID: 771 RVA: 0x0000AD3F File Offset: 0x00008F3F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000304 RID: 772 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000AD4B File Offset: 0x00008F4B
			internal bool <.cctor>b__25_0()
			{
				return true;
			}

			// Token: 0x06000306 RID: 774 RVA: 0x00004E8A File Offset: 0x0000308A
			internal bool <.cctor>b__25_1()
			{
				return false;
			}

			// Token: 0x0400015B RID: 347
			public static readonly ScheduledItem.<>c <>9 = new ScheduledItem.<>c();
		}
	}
}
