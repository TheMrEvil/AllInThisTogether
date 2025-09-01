using System;
using System.CodeDom;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x020000B9 RID: 185
	internal class ContractCodeDomInfo
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0002CCCE File Offset: 0x0002AECE
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
		internal string ClrNamespace
		{
			get
			{
				if (!this.ReferencedTypeExists)
				{
					return this.clrNamespace;
				}
				return null;
			}
			set
			{
				if (this.ReferencedTypeExists)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Cannot set namespace for already referenced type. Base type is '{0}'.", new object[]
					{
						this.TypeReference.BaseType
					})));
				}
				this.clrNamespace = value;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002CD1C File Offset: 0x0002AF1C
		internal Dictionary<string, object> GetMemberNames()
		{
			if (this.ReferencedTypeExists)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Cannot set members for already referenced type. Base type is '{0}'.", new object[]
				{
					this.TypeReference.BaseType
				})));
			}
			if (this.memberNames == null)
			{
				this.memberNames = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			}
			return this.memberNames;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0000222F File Offset: 0x0000042F
		public ContractCodeDomInfo()
		{
		}

		// Token: 0x0400045C RID: 1116
		internal bool IsProcessed;

		// Token: 0x0400045D RID: 1117
		internal CodeTypeDeclaration TypeDeclaration;

		// Token: 0x0400045E RID: 1118
		internal CodeTypeReference TypeReference;

		// Token: 0x0400045F RID: 1119
		internal CodeNamespace CodeNamespace;

		// Token: 0x04000460 RID: 1120
		internal bool ReferencedTypeExists;

		// Token: 0x04000461 RID: 1121
		internal bool UsesWildcardNamespace;

		// Token: 0x04000462 RID: 1122
		private string clrNamespace;

		// Token: 0x04000463 RID: 1123
		private Dictionary<string, object> memberNames;
	}
}
