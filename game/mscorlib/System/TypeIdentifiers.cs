using System;

namespace System
{
	// Token: 0x0200025B RID: 603
	internal class TypeIdentifiers
	{
		// Token: 0x06001BB9 RID: 7097 RVA: 0x00067AE9 File Offset: 0x00065CE9
		internal static TypeIdentifier FromDisplay(string displayName)
		{
			return new TypeIdentifiers.Display(displayName);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00067AF1 File Offset: 0x00065CF1
		internal static TypeIdentifier FromInternal(string internalName)
		{
			return new TypeIdentifiers.Internal(internalName);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00067AF9 File Offset: 0x00065CF9
		internal static TypeIdentifier FromInternal(string internalNameSpace, TypeIdentifier typeName)
		{
			return new TypeIdentifiers.Internal(internalNameSpace, typeName);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00067B02 File Offset: 0x00065D02
		internal static TypeIdentifier WithoutEscape(string simpleName)
		{
			return new TypeIdentifiers.NoEscape(simpleName);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0000259F File Offset: 0x0000079F
		public TypeIdentifiers()
		{
		}

		// Token: 0x0200025C RID: 604
		private class Display : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001BBE RID: 7102 RVA: 0x00067B0A File Offset: 0x00065D0A
			internal Display(string displayName)
			{
				this.displayName = displayName;
				this.internal_name = null;
			}

			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06001BBF RID: 7103 RVA: 0x00067B20 File Offset: 0x00065D20
			public override string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x00067B28 File Offset: 0x00065D28
			public string InternalName
			{
				get
				{
					if (this.internal_name == null)
					{
						this.internal_name = this.GetInternalName();
					}
					return this.internal_name;
				}
			}

			// Token: 0x06001BC1 RID: 7105 RVA: 0x00067B44 File Offset: 0x00065D44
			private string GetInternalName()
			{
				return TypeSpec.UnescapeInternalName(this.displayName);
			}

			// Token: 0x06001BC2 RID: 7106 RVA: 0x00067B51 File Offset: 0x00065D51
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x0400198E RID: 6542
			private string displayName;

			// Token: 0x0400198F RID: 6543
			private string internal_name;
		}

		// Token: 0x0200025D RID: 605
		private class Internal : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001BC3 RID: 7107 RVA: 0x00067B6E File Offset: 0x00065D6E
			internal Internal(string internalName)
			{
				this.internalName = internalName;
				this.display_name = null;
			}

			// Token: 0x06001BC4 RID: 7108 RVA: 0x00067B84 File Offset: 0x00065D84
			internal Internal(string nameSpaceInternal, TypeIdentifier typeName)
			{
				this.internalName = nameSpaceInternal + "." + typeName.InternalName;
				this.display_name = null;
			}

			// Token: 0x17000331 RID: 817
			// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x00067BAA File Offset: 0x00065DAA
			public override string DisplayName
			{
				get
				{
					if (this.display_name == null)
					{
						this.display_name = this.GetDisplayName();
					}
					return this.display_name;
				}
			}

			// Token: 0x17000332 RID: 818
			// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00067BC6 File Offset: 0x00065DC6
			public string InternalName
			{
				get
				{
					return this.internalName;
				}
			}

			// Token: 0x06001BC7 RID: 7111 RVA: 0x00067BCE File Offset: 0x00065DCE
			private string GetDisplayName()
			{
				return TypeSpec.EscapeDisplayName(this.internalName);
			}

			// Token: 0x06001BC8 RID: 7112 RVA: 0x00067B51 File Offset: 0x00065D51
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04001990 RID: 6544
			private string internalName;

			// Token: 0x04001991 RID: 6545
			private string display_name;
		}

		// Token: 0x0200025E RID: 606
		private class NoEscape : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001BC9 RID: 7113 RVA: 0x00067BDB File Offset: 0x00065DDB
			internal NoEscape(string simpleName)
			{
				this.simpleName = simpleName;
			}

			// Token: 0x17000333 RID: 819
			// (get) Token: 0x06001BCA RID: 7114 RVA: 0x00067BEA File Offset: 0x00065DEA
			public override string DisplayName
			{
				get
				{
					return this.simpleName;
				}
			}

			// Token: 0x17000334 RID: 820
			// (get) Token: 0x06001BCB RID: 7115 RVA: 0x00067BEA File Offset: 0x00065DEA
			public string InternalName
			{
				get
				{
					return this.simpleName;
				}
			}

			// Token: 0x06001BCC RID: 7116 RVA: 0x00067B51 File Offset: 0x00065D51
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04001992 RID: 6546
			private string simpleName;
		}
	}
}
