using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the properties that support lookup-based binding. This class cannot be inherited.</summary>
	// Token: 0x020003D3 RID: 979
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LookupBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class using no parameters.</summary>
		// Token: 0x06001F98 RID: 8088 RVA: 0x0006D6C7 File Offset: 0x0006B8C7
		public LookupBindingPropertiesAttribute()
		{
			this.DataSource = null;
			this.DisplayMember = null;
			this.ValueMember = null;
			this.LookupMember = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class.</summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="displayMember">The name of the property to be used for the display name.</param>
		/// <param name="valueMember">The name of the property to be used as the source for values.</param>
		/// <param name="lookupMember">The name of the property to be used for lookups.</param>
		// Token: 0x06001F99 RID: 8089 RVA: 0x0006D6EB File Offset: 0x0006B8EB
		public LookupBindingPropertiesAttribute(string dataSource, string displayMember, string valueMember, string lookupMember)
		{
			this.DataSource = dataSource;
			this.DisplayMember = displayMember;
			this.ValueMember = valueMember;
			this.LookupMember = lookupMember;
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0006D710 File Offset: 0x0006B910
		public string DataSource
		{
			[CompilerGenerated]
			get
			{
				return this.<DataSource>k__BackingField;
			}
		}

		/// <summary>Gets the name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x0006D718 File Offset: 0x0006B918
		public string DisplayMember
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayMember>k__BackingField;
			}
		}

		/// <summary>Gets the name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x0006D720 File Offset: 0x0006B920
		public string ValueMember
		{
			[CompilerGenerated]
			get
			{
				return this.<ValueMember>k__BackingField;
			}
		}

		/// <summary>Gets the name of the lookup member for the component to which this attribute is bound.</summary>
		/// <returns>The name of the lookup member for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x0006D728 File Offset: 0x0006B928
		public string LookupMember
		{
			[CompilerGenerated]
			get
			{
				return this.<LookupMember>k__BackingField;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance</param>
		/// <returns>
		///   <see langword="true" /> if the object is equal to the current instance; otherwise, <see langword="false" />, indicating they are not equal.</returns>
		// Token: 0x06001F9E RID: 8094 RVA: 0x0006D730 File Offset: 0x0006B930
		public override bool Equals(object obj)
		{
			LookupBindingPropertiesAttribute lookupBindingPropertiesAttribute = obj as LookupBindingPropertiesAttribute;
			return lookupBindingPropertiesAttribute != null && lookupBindingPropertiesAttribute.DataSource == this.DataSource && lookupBindingPropertiesAttribute.DisplayMember == this.DisplayMember && lookupBindingPropertiesAttribute.ValueMember == this.ValueMember && lookupBindingPropertiesAttribute.LookupMember == this.LookupMember;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" />.</returns>
		// Token: 0x06001F9F RID: 8095 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0006D793 File Offset: 0x0006B993
		// Note: this type is marked as 'beforefieldinit'.
		static LookupBindingPropertiesAttribute()
		{
		}

		// Token: 0x04000F71 RID: 3953
		[CompilerGenerated]
		private readonly string <DataSource>k__BackingField;

		// Token: 0x04000F72 RID: 3954
		[CompilerGenerated]
		private readonly string <DisplayMember>k__BackingField;

		// Token: 0x04000F73 RID: 3955
		[CompilerGenerated]
		private readonly string <ValueMember>k__BackingField;

		// Token: 0x04000F74 RID: 3956
		[CompilerGenerated]
		private readonly string <LookupMember>k__BackingField;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x04000F75 RID: 3957
		public static readonly LookupBindingPropertiesAttribute Default = new LookupBindingPropertiesAttribute();
	}
}
