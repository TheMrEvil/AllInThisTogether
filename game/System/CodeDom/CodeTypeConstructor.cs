using System;

namespace System.CodeDom
{
	/// <summary>Represents a static constructor for a class.</summary>
	// Token: 0x02000332 RID: 818
	[Serializable]
	public class CodeTypeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeConstructor" /> class.</summary>
		// Token: 0x060019EB RID: 6635 RVA: 0x00060F80 File Offset: 0x0005F180
		public CodeTypeConstructor()
		{
			base.Name = ".cctor";
		}
	}
}
