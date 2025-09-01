using System;

namespace System
{
	// Token: 0x02000258 RID: 600
	internal class TypeNames
	{
		// Token: 0x06001BAE RID: 7086 RVA: 0x00067A7A File Offset: 0x00065C7A
		internal static TypeName FromDisplay(string displayName)
		{
			return new TypeNames.Display(displayName);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0000259F File Offset: 0x0000079F
		public TypeNames()
		{
		}

		// Token: 0x02000259 RID: 601
		internal abstract class ATypeName : TypeName, IEquatable<TypeName>
		{
			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06001BB0 RID: 7088
			public abstract string DisplayName { get; }

			// Token: 0x06001BB1 RID: 7089
			public abstract TypeName NestedName(TypeIdentifier innerName);

			// Token: 0x06001BB2 RID: 7090 RVA: 0x00067A82 File Offset: 0x00065C82
			public bool Equals(TypeName other)
			{
				return other != null && this.DisplayName == other.DisplayName;
			}

			// Token: 0x06001BB3 RID: 7091 RVA: 0x00067A9A File Offset: 0x00065C9A
			public override int GetHashCode()
			{
				return this.DisplayName.GetHashCode();
			}

			// Token: 0x06001BB4 RID: 7092 RVA: 0x00067AA7 File Offset: 0x00065CA7
			public override bool Equals(object other)
			{
				return this.Equals(other as TypeName);
			}

			// Token: 0x06001BB5 RID: 7093 RVA: 0x0000259F File Offset: 0x0000079F
			protected ATypeName()
			{
			}
		}

		// Token: 0x0200025A RID: 602
		private class Display : TypeNames.ATypeName
		{
			// Token: 0x06001BB6 RID: 7094 RVA: 0x00067AB5 File Offset: 0x00065CB5
			internal Display(string displayName)
			{
				this.displayName = displayName;
			}

			// Token: 0x1700032E RID: 814
			// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00067AC4 File Offset: 0x00065CC4
			public override string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x06001BB8 RID: 7096 RVA: 0x00067ACC File Offset: 0x00065CCC
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return new TypeNames.Display(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x0400198D RID: 6541
			private string displayName;
		}
	}
}
