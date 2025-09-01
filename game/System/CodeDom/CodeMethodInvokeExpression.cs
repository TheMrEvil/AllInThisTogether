using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that invokes a method.</summary>
	// Token: 0x0200031A RID: 794
	[Serializable]
	public class CodeMethodInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class.</summary>
		// Token: 0x06001940 RID: 6464 RVA: 0x00060336 File Offset: 0x0005E536
		public CodeMethodInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified method and parameters.</summary>
		/// <param name="method">A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters with which to invoke the method.</param>
		// Token: 0x06001941 RID: 6465 RVA: 0x00060349 File Offset: 0x0005E549
		public CodeMethodInvokeExpression(CodeMethodReferenceExpression method, params CodeExpression[] parameters)
		{
			this._method = method;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified target object, method name, and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object with the method to invoke.</param>
		/// <param name="methodName">The name of the method to invoke.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters to call the method with.</param>
		// Token: 0x06001942 RID: 6466 RVA: 0x0006036F File Offset: 0x0005E56F
		public CodeMethodInvokeExpression(CodeExpression targetObject, string methodName, params CodeExpression[] parameters)
		{
			this._method = new CodeMethodReferenceExpression(targetObject, methodName);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the method to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke.</returns>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x0006039C File Offset: 0x0005E59C
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x000603C1 File Offset: 0x0005E5C1
		public CodeMethodReferenceExpression Method
		{
			get
			{
				CodeMethodReferenceExpression result;
				if ((result = this._method) == null)
				{
					result = (this._method = new CodeMethodReferenceExpression());
				}
				return result;
			}
			set
			{
				this._method = value;
			}
		}

		/// <summary>Gets the parameters to invoke the method with.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to invoke the method with.</returns>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x000603CA File Offset: 0x0005E5CA
		public CodeExpressionCollection Parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<Parameters>k__BackingField;
			}
		} = new CodeExpressionCollection();

		// Token: 0x04000DB3 RID: 3507
		private CodeMethodReferenceExpression _method;

		// Token: 0x04000DB4 RID: 3508
		[CompilerGenerated]
		private readonly CodeExpressionCollection <Parameters>k__BackingField;
	}
}
