using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class encapsulates a pointer to the opaque data used to bind an authenticated transaction to a secure channel.</summary>
	// Token: 0x020002A4 RID: 676
	public abstract class ChannelBinding : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.ChannelBinding.Size" /> property gets the size, in bytes, of the channel binding token associated with the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> instance.</summary>
		/// <returns>The size, in bytes, of the channel binding token in the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> instance.</returns>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600152D RID: 5421
		public abstract int Size { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class.</summary>
		// Token: 0x0600152E RID: 5422 RVA: 0x00055AAB File Offset: 0x00053CAB
		protected ChannelBinding() : this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class.</summary>
		/// <param name="ownsHandle">A Boolean value that indicates if the application owns the safe handle to a native memory region containing the byte data that would be passed to native calls that provide extended protection for integrated windows authentication.</param>
		// Token: 0x0600152F RID: 5423 RVA: 0x00055AB4 File Offset: 0x00053CB4
		protected ChannelBinding(bool ownsHandle) : base(ownsHandle)
		{
		}
	}
}
