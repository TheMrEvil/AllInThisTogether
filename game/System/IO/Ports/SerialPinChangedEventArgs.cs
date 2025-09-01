using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.PinChanged" /> event.</summary>
	// Token: 0x02000531 RID: 1329
	public class SerialPinChangedEventArgs : EventArgs
	{
		// Token: 0x06002AA1 RID: 10913 RVA: 0x00092E48 File Offset: 0x00091048
		internal SerialPinChangedEventArgs(SerialPinChange eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialPinChange" /> values.</returns>
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x00092E57 File Offset: 0x00091057
		public SerialPinChange EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal SerialPinChangedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400171F RID: 5919
		private SerialPinChange eventType;
	}
}
