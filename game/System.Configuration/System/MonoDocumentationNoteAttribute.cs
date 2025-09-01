using System;

namespace System
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020C7 File Offset: 0x000002C7
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
