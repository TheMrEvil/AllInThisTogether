using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a local variable.</summary>
	// Token: 0x0200033D RID: 829
	[Serializable]
	public class CodeVariableReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class.</summary>
		// Token: 0x06001A5B RID: 6747 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeVariableReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class using the specified local variable name.</summary>
		/// <param name="variableName">The name of the local variable to reference.</param>
		// Token: 0x06001A5C RID: 6748 RVA: 0x0006185F File Offset: 0x0005FA5F
		public CodeVariableReferenceExpression(string variableName)
		{
			this._variableName = variableName;
		}

		/// <summary>Gets or sets the name of the local variable to reference.</summary>
		/// <returns>The name of the local variable to reference.</returns>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0006186E File Offset: 0x0005FA6E
		// (set) Token: 0x06001A5E RID: 6750 RVA: 0x0006187F File Offset: 0x0005FA7F
		public string VariableName
		{
			get
			{
				return this._variableName ?? string.Empty;
			}
			set
			{
				this._variableName = value;
			}
		}

		// Token: 0x04000E00 RID: 3584
		private string _variableName;
	}
}
