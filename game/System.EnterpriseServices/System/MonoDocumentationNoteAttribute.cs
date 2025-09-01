using System;

namespace System
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
