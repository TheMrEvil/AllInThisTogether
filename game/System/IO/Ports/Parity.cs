using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the parity bit for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200052C RID: 1324
	public enum Parity
	{
		/// <summary>No parity check occurs.</summary>
		// Token: 0x0400170A RID: 5898
		None,
		/// <summary>Sets the parity bit so that the count of bits set is an odd number.</summary>
		// Token: 0x0400170B RID: 5899
		Odd,
		/// <summary>Sets the parity bit so that the count of bits set is an even number.</summary>
		// Token: 0x0400170C RID: 5900
		Even,
		/// <summary>Leaves the parity bit set to 1.</summary>
		// Token: 0x0400170D RID: 5901
		Mark,
		/// <summary>Leaves the parity bit set to 0.</summary>
		// Token: 0x0400170E RID: 5902
		Space
	}
}
