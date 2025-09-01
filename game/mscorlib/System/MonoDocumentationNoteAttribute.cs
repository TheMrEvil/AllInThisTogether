using System;

namespace System
{
	// Token: 0x020001DB RID: 475
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060014A6 RID: 5286 RVA: 0x00051778 File Offset: 0x0004F978
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
