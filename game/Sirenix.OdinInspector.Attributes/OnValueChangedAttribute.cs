using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200004C RID: 76
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class OnValueChangedAttribute : Attribute
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002ECE File Offset: 0x000010CE
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00002ED6 File Offset: 0x000010D6
		[Obsolete("Use the Action member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MethodName
		{
			get
			{
				return this.Action;
			}
			set
			{
				this.Action = value;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002EDF File Offset: 0x000010DF
		public OnValueChangedAttribute(string action, bool includeChildren = false)
		{
			this.Action = action;
			this.IncludeChildren = includeChildren;
		}

		// Token: 0x040000C2 RID: 194
		public string Action;

		// Token: 0x040000C3 RID: 195
		public bool IncludeChildren;

		// Token: 0x040000C4 RID: 196
		public bool InvokeOnUndoRedo = true;

		// Token: 0x040000C5 RID: 197
		public bool InvokeOnInitialize;
	}
}
