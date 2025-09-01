using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E6 RID: 742
	public class UxmlChildElementDescription
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x000654B8 File Offset: 0x000636B8
		public UxmlChildElementDescription(Type t)
		{
			bool flag = t == null;
			if (flag)
			{
				throw new ArgumentNullException("t");
			}
			this.elementName = t.Name;
			this.elementNamespace = t.Namespace;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x000654FB File Offset: 0x000636FB
		// (set) Token: 0x060018AD RID: 6317 RVA: 0x00065503 File Offset: 0x00063703
		public string elementName
		{
			[CompilerGenerated]
			get
			{
				return this.<elementName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<elementName>k__BackingField = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0006550C File Offset: 0x0006370C
		// (set) Token: 0x060018AF RID: 6319 RVA: 0x00065514 File Offset: 0x00063714
		public string elementNamespace
		{
			[CompilerGenerated]
			get
			{
				return this.<elementNamespace>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<elementNamespace>k__BackingField = value;
			}
		}

		// Token: 0x04000A98 RID: 2712
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <elementName>k__BackingField;

		// Token: 0x04000A99 RID: 2713
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <elementNamespace>k__BackingField;
	}
}
