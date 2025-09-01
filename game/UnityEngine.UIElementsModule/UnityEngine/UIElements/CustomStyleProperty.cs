using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000273 RID: 627
	public struct CustomStyleProperty<T> : IEquatable<CustomStyleProperty<T>>
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00055BE7 File Offset: 0x00053DE7
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x00055BEF File Offset: 0x00053DEF
		public string name
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<name>k__BackingField = value;
			}
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00055BF8 File Offset: 0x00053DF8
		public CustomStyleProperty(string propertyName)
		{
			bool flag = !string.IsNullOrEmpty(propertyName) && !propertyName.StartsWith("--");
			if (flag)
			{
				throw new ArgumentException("Custom style property \"" + propertyName + "\" must start with \"--\" prefix.");
			}
			this.name = propertyName;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00055C44 File Offset: 0x00053E44
		public override bool Equals(object obj)
		{
			bool flag = !(obj is CustomStyleProperty<T>);
			return !flag && this.Equals((CustomStyleProperty<T>)obj);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00055C78 File Offset: 0x00053E78
		public bool Equals(CustomStyleProperty<T> other)
		{
			return this.name == other.name;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00055C9C File Offset: 0x00053E9C
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00055CBC File Offset: 0x00053EBC
		public static bool operator ==(CustomStyleProperty<T> a, CustomStyleProperty<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00055CD8 File Offset: 0x00053ED8
		public static bool operator !=(CustomStyleProperty<T> a, CustomStyleProperty<T> b)
		{
			return !(a == b);
		}

		// Token: 0x040008FD RID: 2301
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <name>k__BackingField;
	}
}
