using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="STATDATA" /> structure.</summary>
	// Token: 0x0200018F RID: 399
	public struct STATDATA
	{
		/// <summary>Represents the <see cref="T:System.Runtime.InteropServices.ComTypes.ADVF" /> enumeration value that determines when the advisory sink is notified of changes in the data.</summary>
		// Token: 0x04000705 RID: 1797
		public ADVF advf;

		/// <summary>Represents the <see cref="T:System.Runtime.InteropServices.ComTypes.IAdviseSink" /> interface that will receive change notifications.</summary>
		// Token: 0x04000706 RID: 1798
		public IAdviseSink advSink;

		/// <summary>Represents the token that uniquely identifies the advisory connection. This token is returned by the method that sets up the advisory connection.</summary>
		// Token: 0x04000707 RID: 1799
		public int connection;

		/// <summary>Represents the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure for the data of interest to the advise sink. The advise sink receives notification of changes to the data specified by this <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure.</summary>
		// Token: 0x04000708 RID: 1800
		public FORMATETC formatetc;
	}
}
