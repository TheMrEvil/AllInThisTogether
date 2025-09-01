using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003F5 RID: 1013
	internal class ScriptClass
	{
		// Token: 0x0600286E RID: 10350 RVA: 0x000F32F4 File Offset: 0x000F14F4
		public ScriptClass(string ns, CompilerInfo compilerInfo)
		{
			this.ns = ns;
			this.compilerInfo = compilerInfo;
			this.refAssemblies = new StringCollection();
			this.nsImports = new StringCollection();
			this.typeDecl = new CodeTypeDeclaration(ScriptClass.GenerateUniqueClassName());
			this.refAssembliesByHref = false;
			this.scriptUris = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000F3354 File Offset: 0x000F1554
		private static string GenerateUniqueClassName()
		{
			return "Script" + Interlocked.Increment(ref ScriptClass.scriptClassCounter).ToString();
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000F3380 File Offset: 0x000F1580
		public void AddScriptBlock(string source, string uriString, int lineNumber, Location end)
		{
			CodeSnippetTypeMember codeSnippetTypeMember = new CodeSnippetTypeMember(source);
			string fileName = SourceLineInfo.GetFileName(uriString);
			if (lineNumber > 0)
			{
				codeSnippetTypeMember.LinePragma = new CodeLinePragma(fileName, lineNumber);
				this.scriptUris[fileName] = uriString;
			}
			this.typeDecl.Members.Add(codeSnippetTypeMember);
			this.endUri = uriString;
			this.endLoc = end;
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x000F33DA File Offset: 0x000F15DA
		public ISourceLineInfo EndLineInfo
		{
			get
			{
				return new SourceLineInfo(this.endUri, this.endLoc, this.endLoc);
			}
		}

		// Token: 0x04001FEC RID: 8172
		public string ns;

		// Token: 0x04001FED RID: 8173
		public CompilerInfo compilerInfo;

		// Token: 0x04001FEE RID: 8174
		public StringCollection refAssemblies;

		// Token: 0x04001FEF RID: 8175
		public StringCollection nsImports;

		// Token: 0x04001FF0 RID: 8176
		public CodeTypeDeclaration typeDecl;

		// Token: 0x04001FF1 RID: 8177
		public bool refAssembliesByHref;

		// Token: 0x04001FF2 RID: 8178
		public Dictionary<string, string> scriptUris;

		// Token: 0x04001FF3 RID: 8179
		public string endUri;

		// Token: 0x04001FF4 RID: 8180
		public Location endLoc;

		// Token: 0x04001FF5 RID: 8181
		private static long scriptClassCounter;
	}
}
