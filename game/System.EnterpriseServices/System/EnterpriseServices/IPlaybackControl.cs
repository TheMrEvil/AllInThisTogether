using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Functions in Queued Components in the abnormal handling of server-side playback errors and client-side failures of the Message Queuing delivery mechanism.</summary>
	// Token: 0x02000021 RID: 33
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("51372AFD-CAE7-11CF-BE81-00AA00A2FA25")]
	[ComImport]
	public interface IPlaybackControl
	{
		/// <summary>Informs the client-side exception-handling component that all Message Queuing attempts to deliver the message to the server were rejected, and the message ended up on the client-side Xact Dead Letter queue.</summary>
		// Token: 0x06000072 RID: 114
		void FinalClientRetry();

		/// <summary>Informs the server-side exception class implementation that all attempts to play back the deferred activation to the server have failed, and the message is about to be moved to its final resting queue.</summary>
		// Token: 0x06000073 RID: 115
		void FinalServerRetry();
	}
}
