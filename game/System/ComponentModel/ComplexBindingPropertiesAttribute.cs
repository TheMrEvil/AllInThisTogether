using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the data source and data member properties for a component that supports complex data binding. This class cannot be inherited.</summary>
	// Token: 0x0200038D RID: 909
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ComplexBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using no parameters.</summary>
		// Token: 0x06001DE0 RID: 7648 RVA: 0x00003D9F File Offset: 0x00001F9F
		public ComplexBindingPropertiesAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source.</summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		// Token: 0x06001DE1 RID: 7649 RVA: 0x00069A52 File Offset: 0x00067C52
		public ComplexBindingPropertiesAttribute(string dataSource)
		{
			this.DataSource = dataSource;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source and data member.</summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="dataMember">The name of the property to be used as the source for data.</param>
		// Token: 0x06001DE2 RID: 7650 RVA: 0x00069A61 File Offset: 0x00067C61
		public ComplexBindingPropertiesAttribute(string dataSource, string dataMember)
		{
			this.DataSource = dataSource;
			this.DataMember = dataMember;
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data source property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00069A77 File Offset: 0x00067C77
		public string DataSource
		{
			[CompilerGenerated]
			get
			{
				return this.<DataSource>k__BackingField;
			}
		}

		/// <summary>Gets the name of the data member property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data member property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x00069A7F File Offset: 0x00067C7F
		public string DataMember
		{
			[CompilerGenerated]
			get
			{
				return this.<DataMember>k__BackingField;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance</param>
		/// <returns>
		///   <see langword="true" /> if the object is equal to the current instance; otherwise, <see langword="false" />, indicating they are not equal.</returns>
		// Token: 0x06001DE5 RID: 7653 RVA: 0x00069A88 File Offset: 0x00067C88
		public override bool Equals(object obj)
		{
			ComplexBindingPropertiesAttribute complexBindingPropertiesAttribute = obj as ComplexBindingPropertiesAttribute;
			return complexBindingPropertiesAttribute != null && complexBindingPropertiesAttribute.DataSource == this.DataSource && complexBindingPropertiesAttribute.DataMember == this.DataMember;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001DE6 RID: 7654 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00069AC5 File Offset: 0x00067CC5
		// Note: this type is marked as 'beforefieldinit'.
		static ComplexBindingPropertiesAttribute()
		{
		}

		// Token: 0x04000EFC RID: 3836
		[CompilerGenerated]
		private readonly string <DataSource>k__BackingField;

		// Token: 0x04000EFD RID: 3837
		[CompilerGenerated]
		private readonly string <DataMember>k__BackingField;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x04000EFE RID: 3838
		public static readonly ComplexBindingPropertiesAttribute Default = new ComplexBindingPropertiesAttribute();
	}
}
