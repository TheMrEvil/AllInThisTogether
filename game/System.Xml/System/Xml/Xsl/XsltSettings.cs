using System;
using System.CodeDom.Compiler;

namespace System.Xml.Xsl
{
	/// <summary>Specifies the XSLT features to support during execution of the XSLT style sheet.</summary>
	// Token: 0x0200034E RID: 846
	public sealed class XsltSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltSettings" /> class with default settings.</summary>
		// Token: 0x060022EE RID: 8942 RVA: 0x000DAAD7 File Offset: 0x000D8CD7
		public XsltSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltSettings" /> class with the specified settings.</summary>
		/// <param name="enableDocumentFunction">
		///       <see langword="true" /> to enable support for the XSLT document() function; otherwise, <see langword="false" />.</param>
		/// <param name="enableScript">
		///       <see langword="true" /> to enable support for embedded scripts blocks; otherwise, <see langword="false" />.</param>
		// Token: 0x060022EF RID: 8943 RVA: 0x000DAAE6 File Offset: 0x000D8CE6
		public XsltSettings(bool enableDocumentFunction, bool enableScript)
		{
			this.enableDocumentFunction = enableDocumentFunction;
			this.enableScript = enableScript;
		}

		/// <summary>Gets an <see cref="T:System.Xml.Xsl.XsltSettings" /> object with default settings. Support for the XSLT document() function and embedded script blocks is disabled.</summary>
		/// <returns>An <see cref="T:System.Xml.Xsl.XsltSettings" /> object with the <see cref="P:System.Xml.Xsl.XsltSettings.EnableDocumentFunction" /> and <see cref="P:System.Xml.Xsl.XsltSettings.EnableScript" /> properties set to <see langword="false" />.</returns>
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x000DAB03 File Offset: 0x000D8D03
		public static XsltSettings Default
		{
			get
			{
				return new XsltSettings(false, false);
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Xsl.XsltSettings" /> object that enables support for the XSLT document() function and embedded script blocks.</summary>
		/// <returns>An <see cref="T:System.Xml.Xsl.XsltSettings" /> object with the <see cref="P:System.Xml.Xsl.XsltSettings.EnableDocumentFunction" /> and <see cref="P:System.Xml.Xsl.XsltSettings.EnableScript" /> properties set to <see langword="true" />.</returns>
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x000DAB0C File Offset: 0x000D8D0C
		public static XsltSettings TrustedXslt
		{
			get
			{
				return new XsltSettings(true, true);
			}
		}

		/// <summary>Gets or sets a value indicating whether to enable support for the XSLT document() function.</summary>
		/// <returns>
		///     <see langword="true" /> to support the XSLT document() function; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x000DAB15 File Offset: 0x000D8D15
		// (set) Token: 0x060022F3 RID: 8947 RVA: 0x000DAB1D File Offset: 0x000D8D1D
		public bool EnableDocumentFunction
		{
			get
			{
				return this.enableDocumentFunction;
			}
			set
			{
				this.enableDocumentFunction = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to enable support for embedded script blocks.</summary>
		/// <returns>
		///     <see langword="true" /> to support script blocks in XSLT style sheets; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x000DAB26 File Offset: 0x000D8D26
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x000DAB2E File Offset: 0x000D8D2E
		public bool EnableScript
		{
			get
			{
				return this.enableScript;
			}
			set
			{
				this.enableScript = value;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x000DAB37 File Offset: 0x000D8D37
		// (set) Token: 0x060022F7 RID: 8951 RVA: 0x000DAB3F File Offset: 0x000D8D3F
		internal bool CheckOnly
		{
			get
			{
				return this.checkOnly;
			}
			set
			{
				this.checkOnly = value;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x000DAB48 File Offset: 0x000D8D48
		// (set) Token: 0x060022F9 RID: 8953 RVA: 0x000DAB50 File Offset: 0x000D8D50
		internal bool IncludeDebugInformation
		{
			get
			{
				return this.includeDebugInformation;
			}
			set
			{
				this.includeDebugInformation = value;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x000DAB59 File Offset: 0x000D8D59
		// (set) Token: 0x060022FB RID: 8955 RVA: 0x000DAB61 File Offset: 0x000D8D61
		internal int WarningLevel
		{
			get
			{
				return this.warningLevel;
			}
			set
			{
				this.warningLevel = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x000DAB6A File Offset: 0x000D8D6A
		// (set) Token: 0x060022FD RID: 8957 RVA: 0x000DAB72 File Offset: 0x000D8D72
		internal bool TreatWarningsAsErrors
		{
			get
			{
				return this.treatWarningsAsErrors;
			}
			set
			{
				this.treatWarningsAsErrors = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x000DAB7B File Offset: 0x000D8D7B
		// (set) Token: 0x060022FF RID: 8959 RVA: 0x000DAB83 File Offset: 0x000D8D83
		internal TempFileCollection TempFiles
		{
			get
			{
				return this.tempFiles;
			}
			set
			{
				this.tempFiles = value;
			}
		}

		// Token: 0x04001C58 RID: 7256
		private bool enableDocumentFunction;

		// Token: 0x04001C59 RID: 7257
		private bool enableScript;

		// Token: 0x04001C5A RID: 7258
		private bool checkOnly;

		// Token: 0x04001C5B RID: 7259
		private bool includeDebugInformation;

		// Token: 0x04001C5C RID: 7260
		private int warningLevel = -1;

		// Token: 0x04001C5D RID: 7261
		private bool treatWarningsAsErrors;

		// Token: 0x04001C5E RID: 7262
		private TempFileCollection tempFiles;
	}
}
