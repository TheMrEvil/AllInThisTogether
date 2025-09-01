using System;

namespace System
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020C7 File Offset: 0x000002C7
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
