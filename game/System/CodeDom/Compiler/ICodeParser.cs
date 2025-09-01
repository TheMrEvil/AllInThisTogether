using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines an interface for parsing code into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
	// Token: 0x02000353 RID: 851
	public interface ICodeParser
	{
		/// <summary>When implemented in a derived class, compiles the specified text stream into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="codeStream">A <see cref="T:System.IO.TextReader" /> that can be used to read the code to be compiled.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> that contains a representation of the parsed code.</returns>
		// Token: 0x06001C1E RID: 7198
		CodeCompileUnit Parse(TextReader codeStream);
	}
}
