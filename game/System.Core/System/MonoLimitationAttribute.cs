using System;

namespace System
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002414 File Offset: 0x00000614
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
