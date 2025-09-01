using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event.</summary>
	// Token: 0x02000537 RID: 1335
	public class SerialDataReceivedEventArgs : EventArgs
	{
		// Token: 0x06002B2D RID: 11053 RVA: 0x00093EDA File Offset: 0x000920DA
		internal SerialDataReceivedEventArgs(SerialData eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialData" /> values.</returns>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x00093EE9 File Offset: 0x000920E9
		public SerialData EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal SerialDataReceivedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400173F RID: 5951
		private SerialData eventType;
	}
}
