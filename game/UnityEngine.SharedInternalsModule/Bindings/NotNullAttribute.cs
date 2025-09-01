using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200001D RID: 29
	[AttributeUsage(AttributeTargets.Parameter)]
	[VisibleToOtherModules]
	internal class NotNullAttribute : Attribute, IBindingsAttribute
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000024E5 File Offset: 0x000006E5
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000024ED File Offset: 0x000006ED
		public string Exception
		{
			[CompilerGenerated]
			get
			{
				return this.<Exception>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Exception>k__BackingField = value;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000024F6 File Offset: 0x000006F6
		public NotNullAttribute(string exception = "ArgumentNullException")
		{
			this.Exception = exception;
		}

		// Token: 0x0400001D RID: 29
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Exception>k__BackingField;
	}
}
