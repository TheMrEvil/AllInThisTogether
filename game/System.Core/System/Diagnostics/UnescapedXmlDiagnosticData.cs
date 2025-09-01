using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Provides unescaped XML data for the logging of user-provided trace data.</summary>
	// Token: 0x0200038C RID: 908
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class UnescapedXmlDiagnosticData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.UnescapedXmlDiagnosticData" /> class by using the specified XML data string.</summary>
		/// <param name="xmlPayload">The XML data to be logged in the <see langword="UserData" /> node of the event schema.  </param>
		// Token: 0x06001B38 RID: 6968 RVA: 0x0000235B File Offset: 0x0000055B
		public UnescapedXmlDiagnosticData(string xmlPayload)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the unescaped XML data string.</summary>
		/// <returns>An unescaped XML string.</returns>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x0000235B File Offset: 0x0000055B
		public string UnescapedXml
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
