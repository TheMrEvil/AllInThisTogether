using System;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged" /> event.</summary>
	// Token: 0x020006F1 RID: 1777
	public class NetworkAvailabilityEventArgs : EventArgs
	{
		// Token: 0x06003938 RID: 14648 RVA: 0x000C82F4 File Offset: 0x000C64F4
		internal NetworkAvailabilityEventArgs(bool isAvailable)
		{
			this.isAvailable = isAvailable;
		}

		/// <summary>Gets the current status of the network connection.</summary>
		/// <returns>
		///   <see langword="true" /> if the network is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x000C8303 File Offset: 0x000C6503
		public bool IsAvailable
		{
			get
			{
				return this.isAvailable;
			}
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal NetworkAvailabilityEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002190 RID: 8592
		private bool isAvailable;
	}
}
