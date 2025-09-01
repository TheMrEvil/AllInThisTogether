using System;

namespace System
{
	// Token: 0x0200006F RID: 111
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x00010D5D File Offset: 0x0000EF5D
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
