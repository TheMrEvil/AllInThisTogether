using System;

namespace System
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
