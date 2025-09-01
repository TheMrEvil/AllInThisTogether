using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Logs tracing messages when the .NET Framework serialization infrastructure is compiled.</summary>
	// Token: 0x0200067D RID: 1661
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class InternalRM
	{
		/// <summary>Prints SOAP trace messages.</summary>
		/// <param name="messages">An array of trace messages to print.</param>
		// Token: 0x06003DD6 RID: 15830 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		/// <summary>Checks if SOAP tracing is enabled.</summary>
		/// <returns>
		///   <see langword="true" />, if tracing is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003DD7 RID: 15831 RVA: 0x000D5998 File Offset: 0x000D3B98
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("SOAP");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.InternalRM" /> class.</summary>
		// Token: 0x06003DD8 RID: 15832 RVA: 0x0000259F File Offset: 0x0000079F
		public InternalRM()
		{
		}
	}
}
