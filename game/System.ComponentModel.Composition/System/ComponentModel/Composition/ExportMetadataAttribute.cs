using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies metadata for a type, property, field, or method marked with the <see cref="T:System.ComponentModel.Composition.ExportAttribute" />.</summary>
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	public sealed class ExportMetadataAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportMetadataAttribute" /> with the specified name and metadata value.</summary>
		/// <param name="name">A string that contains the name of the metadata value, or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.ExportMetadataAttribute.Name" /> property to an empty string ("").</param>
		/// <param name="value">An object that contains the metadata value. This can be <see langword="null" />.</param>
		// Token: 0x060001BB RID: 443 RVA: 0x00005955 File Offset: 0x00003B55
		public ExportMetadataAttribute(string name, object value)
		{
			this.Name = (name ?? string.Empty);
			this.Value = value;
		}

		/// <summary>Gets the name of the metadata value.</summary>
		/// <returns>A string that contains the name of the metadata value.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00005974 File Offset: 0x00003B74
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000597C File Offset: 0x00003B7C
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
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00005985 File Offset: 0x00003B85
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000598D File Offset: 0x00003B8D
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

		/// <summary>Gets or sets a value that indicates whether this item is marked with this attribute more than once.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is marked more than once; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005996 File Offset: 0x00003B96
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000599E File Offset: 0x00003B9E
		public bool IsMultiple
		{
			[CompilerGenerated]
			get
			{
				return this.<IsMultiple>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsMultiple>k__BackingField = value;
			}
		}

		// Token: 0x040000B8 RID: 184
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040000B9 RID: 185
		[CompilerGenerated]
		private object <Value>k__BackingField;

		// Token: 0x040000BA RID: 186
		[CompilerGenerated]
		private bool <IsMultiple>k__BackingField;
	}
}
