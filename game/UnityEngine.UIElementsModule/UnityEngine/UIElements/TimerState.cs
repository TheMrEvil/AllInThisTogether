using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000067 RID: 103
	public struct TimerState : IEquatable<TimerState>
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000AB16 File Offset: 0x00008D16
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000AB1E File Offset: 0x00008D1E
		public long start
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<start>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<start>k__BackingField = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000AB27 File Offset: 0x00008D27
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000AB2F File Offset: 0x00008D2F
		public long now
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<now>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<now>k__BackingField = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000AB38 File Offset: 0x00008D38
		public long deltaTime
		{
			get
			{
				return this.now - this.start;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000AB58 File Offset: 0x00008D58
		public override bool Equals(object obj)
		{
			return obj is TimerState && this.Equals((TimerState)obj);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000AB84 File Offset: 0x00008D84
		public bool Equals(TimerState other)
		{
			return this.start == other.start && this.now == other.now && this.deltaTime == other.deltaTime;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public override int GetHashCode()
		{
			int num = 540054806;
			num = num * -1521134295 + this.start.GetHashCode();
			num = num * -1521134295 + this.now.GetHashCode();
			return num * -1521134295 + this.deltaTime.GetHashCode();
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000AC28 File Offset: 0x00008E28
		public static bool operator ==(TimerState state1, TimerState state2)
		{
			return state1.Equals(state2);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000AC44 File Offset: 0x00008E44
		public static bool operator !=(TimerState state1, TimerState state2)
		{
			return !(state1 == state2);
		}

		// Token: 0x04000152 RID: 338
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long <start>k__BackingField;

		// Token: 0x04000153 RID: 339
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private long <now>k__BackingField;
	}
}
