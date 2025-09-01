using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an empty implementation of the <see cref="T:System.CodeDom.Compiler.ICodeParser" /> interface.</summary>
	// Token: 0x02000347 RID: 839
	public abstract class CodeParser : ICodeParser
	{
		/// <summary>Compiles the specified text stream into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="codeStream">A <see cref="T:System.IO.TextReader" /> that is used to read the code to be parsed.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> containing the code model produced from parsing the code.</returns>
		// Token: 0x06001B51 RID: 6993
		public abstract CodeCompileUnit Parse(TextReader codeStream);

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CodeParser" /> class.</summary>
		// Token: 0x06001B52 RID: 6994 RVA: 0x0000219B File Offset: 0x0000039B
		protected CodeParser()
		{
		}
	}
}
