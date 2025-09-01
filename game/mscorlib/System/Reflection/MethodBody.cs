using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides access to the metadata and MSIL for the body of a method.</summary>
	// Token: 0x020008F0 RID: 2288
	[ComVisible(true)]
	public class MethodBody
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.MethodBody" /> class.</summary>
		// Token: 0x06004CB0 RID: 19632 RVA: 0x0000259F File Offset: 0x0000079F
		protected MethodBody()
		{
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x000F3154 File Offset: 0x000F1354
		internal MethodBody(ExceptionHandlingClause[] clauses, LocalVariableInfo[] locals, byte[] il, bool init_locals, int sig_token, int max_stack)
		{
			this.clauses = clauses;
			this.locals = locals;
			this.il = il;
			this.init_locals = init_locals;
			this.sig_token = sig_token;
			this.max_stack = max_stack;
		}

		/// <summary>Gets a list that includes all the exception-handling clauses in the method body.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.ExceptionHandlingClause" /> objects representing the exception-handling clauses in the body of the method.</returns>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004CB2 RID: 19634 RVA: 0x000F3189 File Offset: 0x000F1389
		public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				return Array.AsReadOnly<ExceptionHandlingClause>(this.clauses);
			}
		}

		/// <summary>Gets the list of local variables declared in the method body.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.LocalVariableInfo" /> objects that describe the local variables declared in the method body.</returns>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004CB3 RID: 19635 RVA: 0x000F3196 File Offset: 0x000F1396
		public virtual IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				return Array.AsReadOnly<LocalVariableInfo>(this.locals);
			}
		}

		/// <summary>Gets a value indicating whether local variables in the method body are initialized to the default values for their types.</summary>
		/// <returns>
		///   <see langword="true" /> if the method body contains code to initialize local variables to <see langword="null" /> for reference types, or to the zero-initialized value for value types; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x000F31A3 File Offset: 0x000F13A3
		public virtual bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
		}

		/// <summary>Gets a metadata token for the signature that describes the local variables for the method in metadata.</summary>
		/// <returns>An integer that represents the metadata token.</returns>
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004CB5 RID: 19637 RVA: 0x000F31AB File Offset: 0x000F13AB
		public virtual int LocalSignatureMetadataToken
		{
			get
			{
				return this.sig_token;
			}
		}

		/// <summary>Gets the maximum number of items on the operand stack when the method is executing.</summary>
		/// <returns>The maximum number of items on the operand stack when the method is executing.</returns>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x000F31B3 File Offset: 0x000F13B3
		public virtual int MaxStackSize
		{
			get
			{
				return this.max_stack;
			}
		}

		/// <summary>Returns the MSIL for the method body, as an array of bytes.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains the MSIL for the method body.</returns>
		// Token: 0x06004CB7 RID: 19639 RVA: 0x000F31BB File Offset: 0x000F13BB
		public virtual byte[] GetILAsByteArray()
		{
			return this.il;
		}

		// Token: 0x04003035 RID: 12341
		private ExceptionHandlingClause[] clauses;

		// Token: 0x04003036 RID: 12342
		private LocalVariableInfo[] locals;

		// Token: 0x04003037 RID: 12343
		private byte[] il;

		// Token: 0x04003038 RID: 12344
		private bool init_locals;

		// Token: 0x04003039 RID: 12345
		private int sig_token;

		// Token: 0x0400303A RID: 12346
		private int max_stack;
	}
}
