using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the operational state of a network interface.</summary>
	// Token: 0x020006FA RID: 1786
	public enum OperationalStatus
	{
		/// <summary>The network interface is up; it can transmit data packets.</summary>
		// Token: 0x0400219D RID: 8605
		Up = 1,
		/// <summary>The network interface is unable to transmit data packets.</summary>
		// Token: 0x0400219E RID: 8606
		Down,
		/// <summary>The network interface is running tests.</summary>
		// Token: 0x0400219F RID: 8607
		Testing,
		/// <summary>The network interface status is not known.</summary>
		// Token: 0x040021A0 RID: 8608
		Unknown,
		/// <summary>The network interface is not in a condition to transmit data packets; it is waiting for an external event.</summary>
		// Token: 0x040021A1 RID: 8609
		Dormant,
		/// <summary>The network interface is unable to transmit data packets because of a missing component, typically a hardware component.</summary>
		// Token: 0x040021A2 RID: 8610
		NotPresent,
		/// <summary>The network interface is unable to transmit data packets because it runs on top of one or more other interfaces, and at least one of these "lower layer" interfaces is down.</summary>
		// Token: 0x040021A3 RID: 8611
		LowerLayerDown
	}
}
