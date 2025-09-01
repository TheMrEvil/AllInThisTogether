using System;
using System.Collections;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003CE RID: 974
	internal class XsltOutput : CompiledAction
	{
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x000E9EF5 File Offset: 0x000E80F5
		internal XsltOutput.OutputMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x000E9EFD File Offset: 0x000E80FD
		internal bool OmitXmlDeclaration
		{
			get
			{
				return this.omitXmlDecl;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000E9F05 File Offset: 0x000E8105
		internal bool HasStandalone
		{
			get
			{
				return this.standaloneSId != int.MaxValue;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x000E9F17 File Offset: 0x000E8117
		internal bool Standalone
		{
			get
			{
				return this.standalone;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x000E9F1F File Offset: 0x000E811F
		internal string DoctypePublic
		{
			get
			{
				return this.doctypePublic;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x000E9F27 File Offset: 0x000E8127
		internal string DoctypeSystem
		{
			get
			{
				return this.doctypeSystem;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x000E9F2F File Offset: 0x000E812F
		internal Hashtable CDataElements
		{
			get
			{
				return this.cdataElements;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x000E9F37 File Offset: 0x000E8137
		internal bool Indent
		{
			get
			{
				return this.indent;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x000E9F3F File Offset: 0x000E813F
		internal Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x000E9F47 File Offset: 0x000E8147
		internal string MediaType
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000E9F50 File Offset: 0x000E8150
		internal XsltOutput CreateDerivedOutput(XsltOutput.OutputMethod method)
		{
			XsltOutput xsltOutput = (XsltOutput)base.MemberwiseClone();
			xsltOutput.method = method;
			if (method == XsltOutput.OutputMethod.Html && this.indentSId == 2147483647)
			{
				xsltOutput.indent = true;
			}
			return xsltOutput;
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000E9F89 File Offset: 0x000E8189
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckEmpty(compiler);
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000E9F9C File Offset: 0x000E819C
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Method))
			{
				if (compiler.Stylesheetid <= this.methodSId)
				{
					this.method = XsltOutput.ParseOutputMethod(value, compiler);
					this.methodSId = compiler.Stylesheetid;
					if (this.indentSId == 2147483647)
					{
						this.indent = (this.method == XsltOutput.OutputMethod.Html);
					}
				}
			}
			else if (Ref.Equal(localName, compiler.Atoms.Version))
			{
				if (compiler.Stylesheetid <= this.versionSId)
				{
					this.version = value;
					this.versionSId = compiler.Stylesheetid;
				}
			}
			else
			{
				if (Ref.Equal(localName, compiler.Atoms.Encoding))
				{
					if (compiler.Stylesheetid > this.encodingSId)
					{
						return true;
					}
					try
					{
						this.encoding = Encoding.GetEncoding(value);
						this.encodingSId = compiler.Stylesheetid;
						return true;
					}
					catch (NotSupportedException)
					{
						return true;
					}
					catch (ArgumentException)
					{
						return true;
					}
				}
				if (Ref.Equal(localName, compiler.Atoms.OmitXmlDeclaration))
				{
					if (compiler.Stylesheetid <= this.omitXmlDeclSId)
					{
						this.omitXmlDecl = compiler.GetYesNo(value);
						this.omitXmlDeclSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.Standalone))
				{
					if (compiler.Stylesheetid <= this.standaloneSId)
					{
						this.standalone = compiler.GetYesNo(value);
						this.standaloneSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.DocTypePublic))
				{
					if (compiler.Stylesheetid <= this.doctypePublicSId)
					{
						this.doctypePublic = value;
						this.doctypePublicSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.DocTypeSystem))
				{
					if (compiler.Stylesheetid <= this.doctypeSystemSId)
					{
						this.doctypeSystem = value;
						this.doctypeSystemSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.Indent))
				{
					if (compiler.Stylesheetid <= this.indentSId)
					{
						this.indent = compiler.GetYesNo(value);
						this.indentSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.MediaType))
				{
					if (compiler.Stylesheetid <= this.mediaTypeSId)
					{
						this.mediaType = value;
						this.mediaTypeSId = compiler.Stylesheetid;
					}
				}
				else
				{
					if (!Ref.Equal(localName, compiler.Atoms.CDataSectionElements))
					{
						return false;
					}
					string[] array = XmlConvert.SplitString(value);
					if (this.cdataElements == null)
					{
						this.cdataElements = new Hashtable(array.Length);
					}
					for (int i = 0; i < array.Length; i++)
					{
						XmlQualifiedName xmlQualifiedName = compiler.CreateXmlQName(array[i]);
						this.cdataElements[xmlQualifiedName] = xmlQualifiedName;
					}
				}
			}
			return true;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void Execute(Processor processor, ActionFrame frame)
		{
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000EA298 File Offset: 0x000E8498
		private static XsltOutput.OutputMethod ParseOutputMethod(string value, Compiler compiler)
		{
			XmlQualifiedName xmlQualifiedName = compiler.CreateXPathQName(value);
			if (xmlQualifiedName.Namespace.Length != 0)
			{
				return XsltOutput.OutputMethod.Other;
			}
			string name = xmlQualifiedName.Name;
			if (name == "xml")
			{
				return XsltOutput.OutputMethod.Xml;
			}
			if (name == "html")
			{
				return XsltOutput.OutputMethod.Html;
			}
			if (name == "text")
			{
				return XsltOutput.OutputMethod.Text;
			}
			if (compiler.ForwardCompatibility)
			{
				return XsltOutput.OutputMethod.Unknown;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
			{
				"method",
				value
			});
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000EA318 File Offset: 0x000E8518
		public XsltOutput()
		{
		}

		// Token: 0x04001E97 RID: 7831
		private XsltOutput.OutputMethod method = XsltOutput.OutputMethod.Unknown;

		// Token: 0x04001E98 RID: 7832
		private int methodSId = int.MaxValue;

		// Token: 0x04001E99 RID: 7833
		private Encoding encoding = Encoding.UTF8;

		// Token: 0x04001E9A RID: 7834
		private int encodingSId = int.MaxValue;

		// Token: 0x04001E9B RID: 7835
		private string version;

		// Token: 0x04001E9C RID: 7836
		private int versionSId = int.MaxValue;

		// Token: 0x04001E9D RID: 7837
		private bool omitXmlDecl;

		// Token: 0x04001E9E RID: 7838
		private int omitXmlDeclSId = int.MaxValue;

		// Token: 0x04001E9F RID: 7839
		private bool standalone;

		// Token: 0x04001EA0 RID: 7840
		private int standaloneSId = int.MaxValue;

		// Token: 0x04001EA1 RID: 7841
		private string doctypePublic;

		// Token: 0x04001EA2 RID: 7842
		private int doctypePublicSId = int.MaxValue;

		// Token: 0x04001EA3 RID: 7843
		private string doctypeSystem;

		// Token: 0x04001EA4 RID: 7844
		private int doctypeSystemSId = int.MaxValue;

		// Token: 0x04001EA5 RID: 7845
		private bool indent;

		// Token: 0x04001EA6 RID: 7846
		private int indentSId = int.MaxValue;

		// Token: 0x04001EA7 RID: 7847
		private string mediaType = "text/html";

		// Token: 0x04001EA8 RID: 7848
		private int mediaTypeSId = int.MaxValue;

		// Token: 0x04001EA9 RID: 7849
		private Hashtable cdataElements;

		// Token: 0x020003CF RID: 975
		internal enum OutputMethod
		{
			// Token: 0x04001EAB RID: 7851
			Xml,
			// Token: 0x04001EAC RID: 7852
			Html,
			// Token: 0x04001EAD RID: 7853
			Text,
			// Token: 0x04001EAE RID: 7854
			Other,
			// Token: 0x04001EAF RID: 7855
			Unknown
		}
	}
}
