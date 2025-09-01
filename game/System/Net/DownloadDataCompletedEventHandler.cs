using System;

namespace System.Net
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Net.DownloadDataCompletedEventArgs" /> containing event data.</param>
	// Token: 0x020005AF RID: 1455
	// (Invoke) Token: 0x06002F8F RID: 12175
	public delegate void DownloadDataCompletedEventHandler(object sender, DownloadDataCompletedEventArgs e);
}
