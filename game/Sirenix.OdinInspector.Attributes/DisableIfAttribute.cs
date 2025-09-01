using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000011 RID: 17
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class DisableIfAttribute : Attribute
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000256E File Offset: 0x0000076E
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002576 File Offset: 0x00000776
		[Obsolete("Use the Condition member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MemberName
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = value;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000257F File Offset: 0x0000077F
		public DisableIfAttribute(string condition)
		{
			this.Condition = condition;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000258E File Offset: 0x0000078E
		public DisableIfAttribute(string condition, object optionalValue)
		{
			this.Condition = condition;
			this.Value = optionalValue;
		}

		// Token: 0x04000042 RID: 66
		public string Condition;

		// Token: 0x04000043 RID: 67
		public object Value;
	}
}
