using System;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Represents a read-only collection of <see cref="T:System.Security.Cryptography.ManifestSignatureInformation" /> objects.  </summary>
	// Token: 0x02000375 RID: 885
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManifestSignatureInformationCollection : ReadOnlyCollection<ManifestSignatureInformation>
	{
		// Token: 0x06001AED RID: 6893 RVA: 0x0000235B File Offset: 0x0000055B
		internal ManifestSignatureInformationCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
