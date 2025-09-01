using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public struct InputDeviceMatcher
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00008520 File Offset: 0x00006720
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00008528 File Offset: 0x00006728
		public OptionalUInt16 VendorID
		{
			get
			{
				return this.vendorID;
			}
			set
			{
				this.vendorID = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00008531 File Offset: 0x00006731
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00008539 File Offset: 0x00006739
		public OptionalUInt16 ProductID
		{
			get
			{
				return this.productID;
			}
			set
			{
				this.productID = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00008542 File Offset: 0x00006742
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000854A File Offset: 0x0000674A
		public OptionalUInt32 VersionNumber
		{
			get
			{
				return this.versionNumber;
			}
			set
			{
				this.versionNumber = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00008553 File Offset: 0x00006753
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000855B File Offset: 0x0000675B
		public OptionalInputDeviceDriverType DriverType
		{
			get
			{
				return this.driverType;
			}
			set
			{
				this.driverType = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00008564 File Offset: 0x00006764
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000856C File Offset: 0x0000676C
		public OptionalInputDeviceTransportType TransportType
		{
			get
			{
				return this.transportType;
			}
			set
			{
				this.transportType = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00008575 File Offset: 0x00006775
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000857D File Offset: 0x0000677D
		public string NameLiteral
		{
			get
			{
				return this.nameLiteral;
			}
			set
			{
				this.nameLiteral = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00008586 File Offset: 0x00006786
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000858E File Offset: 0x0000678E
		public string NamePattern
		{
			get
			{
				return this.namePattern;
			}
			set
			{
				this.namePattern = value;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008598 File Offset: 0x00006798
		internal bool Matches(InputDeviceInfo deviceInfo)
		{
			return (!this.VendorID.HasValue || this.VendorID.Value == deviceInfo.vendorID) && (!this.ProductID.HasValue || this.ProductID.Value == deviceInfo.productID) && (!this.VersionNumber.HasValue || this.VersionNumber.Value == deviceInfo.versionNumber) && (!this.DriverType.HasValue || this.DriverType.Value == deviceInfo.driverType) && (!this.TransportType.HasValue || this.TransportType.Value == deviceInfo.transportType) && (this.NameLiteral == null || string.Equals(deviceInfo.name, this.NameLiteral, StringComparison.OrdinalIgnoreCase)) && (this.NamePattern == null || Regex.IsMatch(deviceInfo.name, this.NamePattern, RegexOptions.IgnoreCase));
		}

		// Token: 0x04000293 RID: 659
		[SerializeField]
		[Hexadecimal]
		private OptionalUInt16 vendorID;

		// Token: 0x04000294 RID: 660
		[SerializeField]
		private OptionalUInt16 productID;

		// Token: 0x04000295 RID: 661
		[SerializeField]
		[Hexadecimal]
		private OptionalUInt32 versionNumber;

		// Token: 0x04000296 RID: 662
		[SerializeField]
		private OptionalInputDeviceDriverType driverType;

		// Token: 0x04000297 RID: 663
		[SerializeField]
		private OptionalInputDeviceTransportType transportType;

		// Token: 0x04000298 RID: 664
		[SerializeField]
		private string nameLiteral;

		// Token: 0x04000299 RID: 665
		[SerializeField]
		private string namePattern;
	}
}
