using System;

namespace System.Xml
{
	/// <summary>Specifies the state of the reader.</summary>
	// Token: 0x02000059 RID: 89
	public enum ReadState
	{
		/// <summary>The <see langword="Read" /> method has not been called.</summary>
		// Token: 0x04000691 RID: 1681
		Initial,
		/// <summary>The <see langword="Read" /> method has been called. Additional methods may be called on the reader.</summary>
		// Token: 0x04000692 RID: 1682
		Interactive,
		/// <summary>An error occurred that prevents the read operation from continuing.</summary>
		// Token: 0x04000693 RID: 1683
		Error,
		/// <summary>The end of the file has been reached successfully.</summary>
		// Token: 0x04000694 RID: 1684
		EndOfFile,
		/// <summary>The <see cref="M:System.Xml.XmlReader.Close" /> method has been called.</summary>
		// Token: 0x04000695 RID: 1685
		Closed
	}
}
