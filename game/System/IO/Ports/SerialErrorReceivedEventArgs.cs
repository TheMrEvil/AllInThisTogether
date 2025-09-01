using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Prepares data for the <see cref="E:System.IO.Ports.SerialPort.ErrorReceived" /> event.</summary>
	// Token: 0x0200052F RID: 1327
	public class SerialErrorReceivedEventArgs : EventArgs
	{
		// Token: 0x06002A9E RID: 10910 RVA: 0x00092E31 File Offset: 0x00091031
		internal SerialErrorReceivedEventArgs(SerialError eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialError" /> values.</returns>
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x00092E40 File Offset: 0x00091040
		public SerialError EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal SerialErrorReceivedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001718 RID: 5912
		private SerialError eventType;
	}
}
