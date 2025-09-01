using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003D7 RID: 983
	internal class Output
	{
		// Token: 0x0600275F RID: 10079 RVA: 0x000EAB44 File Offset: 0x000E8D44
		public Output()
		{
			this.Settings = new XmlWriterSettings();
			this.Settings.OutputMethod = XmlOutputMethod.AutoDetect;
			this.Settings.AutoXmlDeclaration = true;
			this.Settings.ConformanceLevel = ConformanceLevel.Auto;
			this.Settings.MergeCDataSections = true;
		}

		// Token: 0x04001ECE RID: 7886
		public XmlWriterSettings Settings;

		// Token: 0x04001ECF RID: 7887
		public string Version;

		// Token: 0x04001ED0 RID: 7888
		public string Encoding;

		// Token: 0x04001ED1 RID: 7889
		public XmlQualifiedName Method;

		// Token: 0x04001ED2 RID: 7890
		public const int NeverDeclaredPrec = -2147483648;

		// Token: 0x04001ED3 RID: 7891
		public int MethodPrec = int.MinValue;

		// Token: 0x04001ED4 RID: 7892
		public int VersionPrec = int.MinValue;

		// Token: 0x04001ED5 RID: 7893
		public int EncodingPrec = int.MinValue;

		// Token: 0x04001ED6 RID: 7894
		public int OmitXmlDeclarationPrec = int.MinValue;

		// Token: 0x04001ED7 RID: 7895
		public int StandalonePrec = int.MinValue;

		// Token: 0x04001ED8 RID: 7896
		public int DocTypePublicPrec = int.MinValue;

		// Token: 0x04001ED9 RID: 7897
		public int DocTypeSystemPrec = int.MinValue;

		// Token: 0x04001EDA RID: 7898
		public int IndentPrec = int.MinValue;

		// Token: 0x04001EDB RID: 7899
		public int MediaTypePrec = int.MinValue;
	}
}
