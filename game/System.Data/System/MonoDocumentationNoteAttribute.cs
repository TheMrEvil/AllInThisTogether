using System;

namespace System
{
	// Token: 0x0200006D RID: 109
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00010D5D File Offset: 0x0000EF5D
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
