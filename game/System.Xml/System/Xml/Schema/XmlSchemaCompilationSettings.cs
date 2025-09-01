using System;

namespace System.Xml.Schema
{
	/// <summary>Provides schema compilation options for the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class This class cannot be inherited.</summary>
	// Token: 0x0200059F RID: 1439
	public sealed class XmlSchemaCompilationSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> class. </summary>
		// Token: 0x06003A38 RID: 14904 RVA: 0x0014C86B File Offset: 0x0014AA6B
		public XmlSchemaCompilationSettings()
		{
			this.enableUpaCheck = true;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> should check for Unique Particle Attribution (UPA) violations.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> should check for Unique Particle Attribution (UPA) violations; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x0014C87A File Offset: 0x0014AA7A
		// (set) Token: 0x06003A3A RID: 14906 RVA: 0x0014C882 File Offset: 0x0014AA82
		public bool EnableUpaCheck
		{
			get
			{
				return this.enableUpaCheck;
			}
			set
			{
				this.enableUpaCheck = value;
			}
		}

		// Token: 0x04002AF7 RID: 10999
		private bool enableUpaCheck;
	}
}
