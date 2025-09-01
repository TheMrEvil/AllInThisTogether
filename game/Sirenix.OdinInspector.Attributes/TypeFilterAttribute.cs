using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000075 RID: 117
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class TypeFilterAttribute : Attribute
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00003D6D File Offset: 0x00001F6D
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00003D75 File Offset: 0x00001F75
		[Obsolete("Use the FilterGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MemberName
		{
			get
			{
				return this.FilterGetter;
			}
			set
			{
				this.FilterGetter = value;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003D7E File Offset: 0x00001F7E
		public TypeFilterAttribute(string filterGetter)
		{
			this.FilterGetter = filterGetter;
		}

		// Token: 0x0400014B RID: 331
		public string FilterGetter;

		// Token: 0x0400014C RID: 332
		public string DropdownTitle;

		// Token: 0x0400014D RID: 333
		public bool DrawValueNormally;
	}
}
