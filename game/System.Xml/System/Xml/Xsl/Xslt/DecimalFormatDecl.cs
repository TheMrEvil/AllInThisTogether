using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003D9 RID: 985
	internal class DecimalFormatDecl
	{
		// Token: 0x06002762 RID: 10082 RVA: 0x000EAC05 File Offset: 0x000E8E05
		public DecimalFormatDecl(XmlQualifiedName name, string infinitySymbol, string nanSymbol, string characters)
		{
			this.Name = name;
			this.InfinitySymbol = infinitySymbol;
			this.NanSymbol = nanSymbol;
			this.Characters = characters.ToCharArray();
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000EAC2F File Offset: 0x000E8E2F
		// Note: this type is marked as 'beforefieldinit'.
		static DecimalFormatDecl()
		{
		}

		// Token: 0x04001EDC RID: 7900
		public readonly XmlQualifiedName Name;

		// Token: 0x04001EDD RID: 7901
		public readonly string InfinitySymbol;

		// Token: 0x04001EDE RID: 7902
		public readonly string NanSymbol;

		// Token: 0x04001EDF RID: 7903
		public readonly char[] Characters;

		// Token: 0x04001EE0 RID: 7904
		public static DecimalFormatDecl Default = new DecimalFormatDecl(new XmlQualifiedName(), "Infinity", "NaN", ".,%‰0#;-");
	}
}
