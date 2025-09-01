using System;

namespace System
{
	// Token: 0x0200001C RID: 28
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002414 File Offset: 0x00000614
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
