using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Provides a container for a CodeDOM program graph.</summary>
	// Token: 0x02000302 RID: 770
	[Serializable]
	public class CodeCompileUnit : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCompileUnit" /> class.</summary>
		// Token: 0x060018A1 RID: 6305 RVA: 0x0005F7C8 File Offset: 0x0005D9C8
		public CodeCompileUnit()
		{
		}

		/// <summary>Gets the collection of namespaces.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceCollection" /> that indicates the namespaces that the compile unit uses.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x0005F7DB File Offset: 0x0005D9DB
		public CodeNamespaceCollection Namespaces
		{
			[CompilerGenerated]
			get
			{
				return this.<Namespaces>k__BackingField;
			}
		} = new CodeNamespaceCollection();

		/// <summary>Gets the referenced assemblies.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the file names of the referenced assemblies.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0005F7E4 File Offset: 0x0005D9E4
		public StringCollection ReferencedAssemblies
		{
			get
			{
				StringCollection result;
				if ((result = this._assemblies) == null)
				{
					result = (this._assemblies = new StringCollection());
				}
				return result;
			}
		}

		/// <summary>Gets a collection of custom attributes for the generated assembly.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes for the generated assembly.</returns>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0005F80C File Offset: 0x0005DA0C
		public CodeAttributeDeclarationCollection AssemblyCustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection result;
				if ((result = this._attributes) == null)
				{
					result = (this._attributes = new CodeAttributeDeclarationCollection());
				}
				return result;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0005F834 File Offset: 0x0005DA34
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				CodeDirectiveCollection result;
				if ((result = this._startDirectives) == null)
				{
					result = (this._startDirectives = new CodeDirectiveCollection());
				}
				return result;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0005F85C File Offset: 0x0005DA5C
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				CodeDirectiveCollection result;
				if ((result = this._endDirectives) == null)
				{
					result = (this._endDirectives = new CodeDirectiveCollection());
				}
				return result;
			}
		}

		// Token: 0x04000D76 RID: 3446
		private StringCollection _assemblies;

		// Token: 0x04000D77 RID: 3447
		private CodeAttributeDeclarationCollection _attributes;

		// Token: 0x04000D78 RID: 3448
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04000D79 RID: 3449
		private CodeDirectiveCollection _endDirectives;

		// Token: 0x04000D7A RID: 3450
		[CompilerGenerated]
		private readonly CodeNamespaceCollection <Namespaces>k__BackingField;
	}
}
