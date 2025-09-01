using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of an argument passed to a method.</summary>
	// Token: 0x020002EF RID: 751
	[Serializable]
	public class CodeArgumentReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class.</summary>
		// Token: 0x06001807 RID: 6151 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeArgumentReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class using the specified parameter name.</summary>
		/// <param name="parameterName">The name of the parameter to reference.</param>
		// Token: 0x06001808 RID: 6152 RVA: 0x0005EE90 File Offset: 0x0005D090
		public CodeArgumentReferenceExpression(string parameterName)
		{
			this._parameterName = parameterName;
		}

		/// <summary>Gets or sets the name of the parameter this expression references.</summary>
		/// <returns>The name of the parameter to reference.</returns>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0005EE9F File Offset: 0x0005D09F
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x0005EEB0 File Offset: 0x0005D0B0
		public string ParameterName
		{
			get
			{
				return this._parameterName ?? string.Empty;
			}
			set
			{
				this._parameterName = value;
			}
		}

		// Token: 0x04000D46 RID: 3398
		private string _parameterName;
	}
}
