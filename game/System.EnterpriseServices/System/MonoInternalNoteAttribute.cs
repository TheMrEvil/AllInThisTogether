using System;

namespace System
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000206F File Offset: 0x0000026F
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
