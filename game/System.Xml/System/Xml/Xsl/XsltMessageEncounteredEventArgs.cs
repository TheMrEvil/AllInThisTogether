using System;

namespace System.Xml.Xsl
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Xsl.XsltArgumentList.XsltMessageEncountered" /> event.</summary>
	// Token: 0x02000346 RID: 838
	public abstract class XsltMessageEncounteredEventArgs : EventArgs
	{
		/// <summary>Gets the contents of the xsl:message element.</summary>
		/// <returns>The contents of the xsl:message element.</returns>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060022B8 RID: 8888
		public abstract string Message { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltMessageEncounteredEventArgs" /> class.</summary>
		// Token: 0x060022B9 RID: 8889 RVA: 0x000DA5DC File Offset: 0x000D87DC
		protected XsltMessageEncounteredEventArgs()
		{
		}
	}
}
