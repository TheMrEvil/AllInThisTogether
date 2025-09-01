using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002C9 RID: 713
	public class Tuple<T1, T2, T3> : IEquatable<Tuple<T1, T2, T3>>
	{
		// Token: 0x0600224B RID: 8779 RVA: 0x000A7F34 File Offset: 0x000A6134
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x000A7F51 File Offset: 0x000A6151
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x000A7F59 File Offset: 0x000A6159
		public T1 Item1
		{
			[CompilerGenerated]
			get
			{
				return this.<Item1>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Item1>k__BackingField = value;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x000A7F62 File Offset: 0x000A6162
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x000A7F6A File Offset: 0x000A616A
		public T2 Item2
		{
			[CompilerGenerated]
			get
			{
				return this.<Item2>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Item2>k__BackingField = value;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x000A7F73 File Offset: 0x000A6173
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x000A7F7B File Offset: 0x000A617B
		public T3 Item3
		{
			[CompilerGenerated]
			get
			{
				return this.<Item3>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Item3>k__BackingField = value;
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000A7F84 File Offset: 0x000A6184
		public override int GetHashCode()
		{
			T1 item = this.Item1;
			int hashCode = item.GetHashCode();
			T2 item2 = this.Item2;
			int num = hashCode ^ item2.GetHashCode();
			T3 item3 = this.Item3;
			return num ^ item3.GetHashCode();
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000A7FD0 File Offset: 0x000A61D0
		public bool Equals(Tuple<T1, T2, T3> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3);
		}

		// Token: 0x04000CA3 RID: 3235
		[CompilerGenerated]
		private T1 <Item1>k__BackingField;

		// Token: 0x04000CA4 RID: 3236
		[CompilerGenerated]
		private T2 <Item2>k__BackingField;

		// Token: 0x04000CA5 RID: 3237
		[CompilerGenerated]
		private T3 <Item3>k__BackingField;
	}
}
