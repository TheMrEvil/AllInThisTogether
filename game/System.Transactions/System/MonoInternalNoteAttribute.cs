using System;

namespace System
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002083 File Offset: 0x00000283
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
