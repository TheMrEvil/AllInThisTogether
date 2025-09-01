using System;

namespace System
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002414 File Offset: 0x00000614
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
