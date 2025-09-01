using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000022 RID: 34
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class EnableIfAttribute : Attribute
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002705 File Offset: 0x00000905
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000270D File Offset: 0x0000090D
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

		// Token: 0x06000069 RID: 105 RVA: 0x00002716 File Offset: 0x00000916
		public EnableIfAttribute(string condition)
		{
			this.Condition = condition;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002725 File Offset: 0x00000925
		public EnableIfAttribute(string condition, object optionalValue)
		{
			this.Condition = condition;
			this.Value = optionalValue;
		}

		// Token: 0x0400004D RID: 77
		public string Condition;

		// Token: 0x0400004E RID: 78
		public object Value;
	}
}
