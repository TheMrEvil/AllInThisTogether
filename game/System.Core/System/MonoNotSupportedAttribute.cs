using System;

namespace System
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002414 File Offset: 0x00000614
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
