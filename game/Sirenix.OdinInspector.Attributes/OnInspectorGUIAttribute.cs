using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000049 RID: 73
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class OnInspectorGUIAttribute : ShowInInspectorAttribute
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00002E69 File Offset: 0x00001069
		public OnInspectorGUIAttribute()
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002E80 File Offset: 0x00001080
		public OnInspectorGUIAttribute(string action, bool append = true)
		{
			if (append)
			{
				this.Append = action;
				return;
			}
			this.Prepend = action;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002E9A File Offset: 0x0000109A
		public OnInspectorGUIAttribute(string prepend, string append)
		{
			this.Prepend = prepend;
			this.Append = append;
		}

		// Token: 0x040000BC RID: 188
		public string Prepend;

		// Token: 0x040000BD RID: 189
		public string Append;

		// Token: 0x040000BE RID: 190
		[Obsolete("Use the Prepend member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string PrependMethodName;

		// Token: 0x040000BF RID: 191
		[Obsolete("Use the Append member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string AppendMethodName;
	}
}
