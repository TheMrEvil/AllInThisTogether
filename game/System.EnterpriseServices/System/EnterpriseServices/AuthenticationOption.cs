using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the remote procedure call (RPC) authentication mechanism. Applicable only when the <see cref="T:System.EnterpriseServices.ActivationOption" /> is set to <see langword="Server" />.</summary>
	// Token: 0x02000011 RID: 17
	[Serializable]
	public enum AuthenticationOption
	{
		/// <summary>Authenticates credentials at the beginning of every call.</summary>
		// Token: 0x0400003E RID: 62
		Call = 3,
		/// <summary>Authenticates credentials only when the connection is made.</summary>
		// Token: 0x0400003F RID: 63
		Connect = 2,
		/// <summary>Uses the default authentication level for the specified authentication service. In COM+, this setting is provided by the <see langword="DefaultAuthenticationLevel" /> property in the <see langword="LocalComputer" /> collection.</summary>
		// Token: 0x04000040 RID: 64
		Default = 0,
		/// <summary>Authenticates credentials and verifies that no call data has been modified in transit.</summary>
		// Token: 0x04000041 RID: 65
		Integrity = 5,
		/// <summary>Authentication does not occur.</summary>
		// Token: 0x04000042 RID: 66
		None = 1,
		/// <summary>Authenticates credentials and verifies that all call data is received.</summary>
		// Token: 0x04000043 RID: 67
		Packet = 4,
		/// <summary>Authenticates credentials and encrypts the packet, including the data and the sender's identity and signature.</summary>
		// Token: 0x04000044 RID: 68
		Privacy = 6
	}
}
