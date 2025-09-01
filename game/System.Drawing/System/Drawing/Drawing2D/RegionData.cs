using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates the data that makes up a <see cref="T:System.Drawing.Region" /> object. This class cannot be inherited.</summary>
	// Token: 0x0200014F RID: 335
	public sealed class RegionData
	{
		// Token: 0x06000E4B RID: 3659 RVA: 0x000206F1 File Offset: 0x0001E8F1
		internal RegionData(byte[] data)
		{
			this.Data = data;
		}

		/// <summary>Gets or sets an array of bytes that specify the <see cref="T:System.Drawing.Region" /> object.</summary>
		/// <returns>An array of bytes that specify the <see cref="T:System.Drawing.Region" /> object.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00020700 File Offset: 0x0001E900
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x00020708 File Offset: 0x0001E908
		public byte[] Data
		{
			[CompilerGenerated]
			get
			{
				return this.<Data>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Data>k__BackingField = value;
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00005B7D File Offset: 0x00003D7D
		internal RegionData()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000B62 RID: 2914
		[CompilerGenerated]
		private byte[] <Data>k__BackingField;
	}
}
