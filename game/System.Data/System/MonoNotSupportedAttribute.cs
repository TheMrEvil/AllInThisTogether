using System;

namespace System
{
	// Token: 0x02000071 RID: 113
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x00010D5D File Offset: 0x0000EF5D
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
