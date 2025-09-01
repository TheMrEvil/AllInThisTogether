using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B3 RID: 179
	public struct StylePropertyName : IEquatable<StylePropertyName>
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00016943 File Offset: 0x00014B43
		internal readonly StylePropertyId id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001694B File Offset: 0x00014B4B
		private readonly string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00016954 File Offset: 0x00014B54
		internal static StylePropertyId StylePropertyIdFromString(string name)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, out stylePropertyId);
			StylePropertyId result;
			if (flag)
			{
				result = stylePropertyId;
			}
			else
			{
				result = StylePropertyId.Unknown;
			}
			return result;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00016980 File Offset: 0x00014B80
		internal StylePropertyName(StylePropertyId stylePropertyId)
		{
			this.id = stylePropertyId;
			this.name = null;
			string text;
			bool flag = StylePropertyUtil.s_IdToName.TryGetValue(stylePropertyId, out text);
			if (flag)
			{
				this.name = text;
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000169B8 File Offset: 0x00014BB8
		public StylePropertyName(string name)
		{
			this.id = StylePropertyName.StylePropertyIdFromString(name);
			this.name = null;
			bool flag = this.id > StylePropertyId.Unknown;
			if (flag)
			{
				this.name = name;
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000169F0 File Offset: 0x00014BF0
		public static bool IsNullOrEmpty(StylePropertyName propertyName)
		{
			return propertyName.id == StylePropertyId.Unknown;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00016A0C File Offset: 0x00014C0C
		public static bool operator ==(StylePropertyName lhs, StylePropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00016A30 File Offset: 0x00014C30
		public static bool operator !=(StylePropertyName lhs, StylePropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00016A58 File Offset: 0x00014C58
		public static implicit operator StylePropertyName(string name)
		{
			return new StylePropertyName(name);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00016A70 File Offset: 0x00014C70
		public override int GetHashCode()
		{
			return (int)this.id;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00016A88 File Offset: 0x00014C88
		public override bool Equals(object other)
		{
			return other is StylePropertyName && this.Equals((StylePropertyName)other);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00016AB4 File Offset: 0x00014CB4
		public bool Equals(StylePropertyName other)
		{
			return this == other;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00016AD4 File Offset: 0x00014CD4
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x0400025F RID: 607
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly StylePropertyId <id>k__BackingField;

		// Token: 0x04000260 RID: 608
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string <name>k__BackingField;
	}
}
