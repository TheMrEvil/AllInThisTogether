using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002C8 RID: 712
	public class Tuple<T1, T2> : IEquatable<Tuple<T1, T2>>
	{
		// Token: 0x06002244 RID: 8772 RVA: 0x000A7E91 File Offset: 0x000A6091
		public Tuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000A7EA7 File Offset: 0x000A60A7
		// (set) Token: 0x06002246 RID: 8774 RVA: 0x000A7EAF File Offset: 0x000A60AF
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

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x000A7EB8 File Offset: 0x000A60B8
		// (set) Token: 0x06002248 RID: 8776 RVA: 0x000A7EC0 File Offset: 0x000A60C0
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

		// Token: 0x06002249 RID: 8777 RVA: 0x000A7ECC File Offset: 0x000A60CC
		public override int GetHashCode()
		{
			T1 item = this.Item1;
			int hashCode = item.GetHashCode();
			T2 item2 = this.Item2;
			return hashCode ^ item2.GetHashCode();
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000A7F02 File Offset: 0x000A6102
		public bool Equals(Tuple<T1, T2> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2);
		}

		// Token: 0x04000CA1 RID: 3233
		[CompilerGenerated]
		private T1 <Item1>k__BackingField;

		// Token: 0x04000CA2 RID: 3234
		[CompilerGenerated]
		private T2 <Item2>k__BackingField;
	}
}
