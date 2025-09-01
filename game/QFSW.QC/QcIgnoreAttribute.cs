using System;

namespace QFSW.QC
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class QcIgnoreAttribute : Attribute
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022F2 File Offset: 0x000004F2
		public QcIgnoreAttribute()
		{
		}
	}
}
