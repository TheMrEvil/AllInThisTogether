using System;
using System.Runtime.InteropServices;

namespace InControl
{
	// Token: 0x02000036 RID: 54
	public struct InputDeviceInfo
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0000848F File Offset: 0x0000668F
		public bool HasSameVendorID(InputDeviceInfo deviceInfo)
		{
			return this.vendorID == deviceInfo.vendorID;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000849F File Offset: 0x0000669F
		public bool HasSameProductID(InputDeviceInfo deviceInfo)
		{
			return this.productID == deviceInfo.productID;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000084AF File Offset: 0x000066AF
		public bool HasSameVersionNumber(InputDeviceInfo deviceInfo)
		{
			return this.versionNumber == deviceInfo.versionNumber;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000084BF File Offset: 0x000066BF
		public bool HasSameLocation(InputDeviceInfo deviceInfo)
		{
			return !string.IsNullOrEmpty(this.location) && this.location == deviceInfo.location;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000084E1 File Offset: 0x000066E1
		public bool HasSameSerialNumber(InputDeviceInfo deviceInfo)
		{
			return !string.IsNullOrEmpty(this.serialNumber) && this.serialNumber == deviceInfo.serialNumber;
		}

		// Token: 0x04000288 RID: 648
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string name;

		// Token: 0x04000289 RID: 649
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string location;

		// Token: 0x0400028A RID: 650
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string serialNumber;

		// Token: 0x0400028B RID: 651
		public ushort vendorID;

		// Token: 0x0400028C RID: 652
		public ushort productID;

		// Token: 0x0400028D RID: 653
		public uint versionNumber;

		// Token: 0x0400028E RID: 654
		public InputDeviceDriverType driverType;

		// Token: 0x0400028F RID: 655
		public InputDeviceTransportType transportType;

		// Token: 0x04000290 RID: 656
		public uint numButtons;

		// Token: 0x04000291 RID: 657
		public uint numAnalogs;
	}
}
