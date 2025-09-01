using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies metadata for a part.</summary>
	// Token: 0x02000051 RID: 81
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class PartMetadataAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.PartMetadataAttribute" /> class with the specified name and metadata value.</summary>
		/// <param name="name">A string that contains the name of the metadata value or <see langword="null" /> to use an empty string ("").</param>
		/// <param name="value">An object that contains the metadata value. This can be <see langword="null" />.</param>
		// Token: 0x06000224 RID: 548 RVA: 0x000069E3 File Offset: 0x00004BE3
		public PartMetadataAttribute(string name, object value)
		{
			this.Name = (name ?? string.Empty);
			this.Value = value;
		}

		/// <summary>Gets the name of the metadata value.</summary>
		/// <returns>A string that contains the name of the metadata value.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00006A02 File Offset: 0x00004C02
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00006A0A File Offset: 0x00004C0A
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		/// <summary>Gets the metadata value.</summary>
		/// <returns>An object that contains the metadata value.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00006A13 File Offset: 0x00004C13
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00006A1B File Offset: 0x00004C1B
		public object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x040000E6 RID: 230
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040000E7 RID: 231
		[CompilerGenerated]
		private object <Value>k__BackingField;
	}
}
