using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200036A RID: 874
	internal class DbgData
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x000DF4D6 File Offset: 0x000DD6D6
		public XPathNavigator StyleSheet
		{
			get
			{
				return this.styleSheet;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x000DF4DE File Offset: 0x000DD6DE
		public VariableAction[] Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000DF4E8 File Offset: 0x000DD6E8
		public DbgData(Compiler compiler)
		{
			DbgCompiler dbgCompiler = (DbgCompiler)compiler;
			this.styleSheet = dbgCompiler.Input.Navigator.Clone();
			this.variables = dbgCompiler.LocalVariables;
			dbgCompiler.Debugger.OnInstructionCompile(this.StyleSheet);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000DF535 File Offset: 0x000DD735
		internal void ReplaceVariables(VariableAction[] vars)
		{
			this.variables = vars;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000DF53E File Offset: 0x000DD73E
		private DbgData()
		{
			this.styleSheet = null;
			this.variables = new VariableAction[0];
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x000DF559 File Offset: 0x000DD759
		public static DbgData Empty
		{
			get
			{
				return DbgData.s_nullDbgData;
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000DF560 File Offset: 0x000DD760
		// Note: this type is marked as 'beforefieldinit'.
		static DbgData()
		{
		}

		// Token: 0x04001CE0 RID: 7392
		private XPathNavigator styleSheet;

		// Token: 0x04001CE1 RID: 7393
		private VariableAction[] variables;

		// Token: 0x04001CE2 RID: 7394
		private static DbgData s_nullDbgData = new DbgData();
	}
}
