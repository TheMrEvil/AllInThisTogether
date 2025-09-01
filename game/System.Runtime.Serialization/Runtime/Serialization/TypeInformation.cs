using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200012F RID: 303
	internal sealed class TypeInformation
	{
		// Token: 0x06000EFE RID: 3838 RVA: 0x0003CD27 File Offset: 0x0003AF27
		internal TypeInformation(string fullTypeName, string assemblyString, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = fullTypeName;
			this.assemblyString = assemblyString;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003CD44 File Offset: 0x0003AF44
		internal string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0003CD4C File Offset: 0x0003AF4C
		internal string AssemblyString
		{
			get
			{
				return this.assemblyString;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0003CD54 File Offset: 0x0003AF54
		internal bool HasTypeForwardedFrom
		{
			get
			{
				return this.hasTypeForwardedFrom;
			}
		}

		// Token: 0x04000682 RID: 1666
		private string fullTypeName;

		// Token: 0x04000683 RID: 1667
		private string assemblyString;

		// Token: 0x04000684 RID: 1668
		private bool hasTypeForwardedFrom;
	}
}
