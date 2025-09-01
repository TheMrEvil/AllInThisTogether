using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x020005BF RID: 1471
	public class DownloadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06002FC6 RID: 12230 RVA: 0x000A5187 File Offset: 0x000A3387
		internal DownloadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesReceived, long totalBytesToReceive) : base(progressPercentage, userToken)
		{
			this.BytesReceived = bytesReceived;
			this.TotalBytesToReceive = totalBytesToReceive;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x000A51A0 File Offset: 0x000A33A0
		public long BytesReceived
		{
			[CompilerGenerated]
			get
			{
				return this.<BytesReceived>k__BackingField;
			}
		}

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data download operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002FC8 RID: 12232 RVA: 0x000A51A8 File Offset: 0x000A33A8
		public long TotalBytesToReceive
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalBytesToReceive>k__BackingField;
			}
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal DownloadProgressChangedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A4D RID: 6733
		[CompilerGenerated]
		private readonly long <BytesReceived>k__BackingField;

		// Token: 0x04001A4E RID: 6734
		[CompilerGenerated]
		private readonly long <TotalBytesToReceive>k__BackingField;
	}
}
