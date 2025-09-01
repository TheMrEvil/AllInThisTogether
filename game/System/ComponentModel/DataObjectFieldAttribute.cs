using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides metadata for a property representing a data field. This class cannot be inherited.</summary>
	// Token: 0x02000395 RID: 917
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DataObjectFieldAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row.</summary>
		/// <param name="primaryKey">
		///   <see langword="true" /> to indicate that the field is in the primary key of the data row; otherwise, <see langword="false" />.</param>
		// Token: 0x06001E14 RID: 7700 RVA: 0x0006B57A File Offset: 0x0006977A
		public DataObjectFieldAttribute(bool primaryKey) : this(primaryKey, false, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, and whether the field is a database identity field.</summary>
		/// <param name="primaryKey">
		///   <see langword="true" /> to indicate that the field is in the primary key of the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isIdentity">
		///   <see langword="true" /> to indicate that the field is an identity field that uniquely identifies the data row; otherwise, <see langword="false" />.</param>
		// Token: 0x06001E15 RID: 7701 RVA: 0x0006B586 File Offset: 0x00069786
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity) : this(primaryKey, isIdentity, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether the field is a database identity field, and whether the field can be null.</summary>
		/// <param name="primaryKey">
		///   <see langword="true" /> to indicate that the field is in the primary key of the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isIdentity">
		///   <see langword="true" /> to indicate that the field is an identity field that uniquely identifies the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isNullable">
		///   <see langword="true" /> to indicate that the field can be null in the data store; otherwise, <see langword="false" />.</param>
		// Token: 0x06001E16 RID: 7702 RVA: 0x0006B592 File Offset: 0x00069792
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable) : this(primaryKey, isIdentity, isNullable, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether it is a database identity field, and whether it can be null and sets the length of the field.</summary>
		/// <param name="primaryKey">
		///   <see langword="true" /> to indicate that the field is in the primary key of the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isIdentity">
		///   <see langword="true" /> to indicate that the field is an identity field that uniquely identifies the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isNullable">
		///   <see langword="true" /> to indicate that the field can be null in the data store; otherwise, <see langword="false" />.</param>
		/// <param name="length">The length of the field in bytes.</param>
		// Token: 0x06001E17 RID: 7703 RVA: 0x0006B59E File Offset: 0x0006979E
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable, int length)
		{
			this.PrimaryKey = primaryKey;
			this.IsIdentity = isIdentity;
			this.IsNullable = isNullable;
			this.Length = length;
		}

		/// <summary>Gets a value indicating whether a property represents an identity field in the underlying data.</summary>
		/// <returns>
		///   <see langword="true" /> if the property represents an identity field in the underlying data; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0006B5C3 File Offset: 0x000697C3
		public bool IsIdentity
		{
			[CompilerGenerated]
			get
			{
				return this.<IsIdentity>k__BackingField;
			}
		}

		/// <summary>Gets a value indicating whether a property represents a field that can be null in the underlying data store.</summary>
		/// <returns>
		///   <see langword="true" /> if the property represents a field that can be null in the underlying data store; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x0006B5CB File Offset: 0x000697CB
		public bool IsNullable
		{
			[CompilerGenerated]
			get
			{
				return this.<IsNullable>k__BackingField;
			}
		}

		/// <summary>Gets the length of the property in bytes.</summary>
		/// <returns>The length of the property in bytes, or -1 if not set.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x0006B5D3 File Offset: 0x000697D3
		public int Length
		{
			[CompilerGenerated]
			get
			{
				return this.<Length>k__BackingField;
			}
		}

		/// <summary>Gets a value indicating whether a property is in the primary key in the underlying data.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is in the primary key of the data store; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x0006B5DB File Offset: 0x000697DB
		public bool PrimaryKey
		{
			[CompilerGenerated]
			get
			{
				return this.<PrimaryKey>k__BackingField;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectFieldAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E1C RID: 7708 RVA: 0x0006B5E4 File Offset: 0x000697E4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectFieldAttribute dataObjectFieldAttribute = obj as DataObjectFieldAttribute;
			return dataObjectFieldAttribute != null && dataObjectFieldAttribute.IsIdentity == this.IsIdentity && dataObjectFieldAttribute.IsNullable == this.IsNullable && dataObjectFieldAttribute.Length == this.Length && dataObjectFieldAttribute.PrimaryKey == this.PrimaryKey;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E1D RID: 7709 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000F0A RID: 3850
		[CompilerGenerated]
		private readonly bool <IsIdentity>k__BackingField;

		// Token: 0x04000F0B RID: 3851
		[CompilerGenerated]
		private readonly bool <IsNullable>k__BackingField;

		// Token: 0x04000F0C RID: 3852
		[CompilerGenerated]
		private readonly int <Length>k__BackingField;

		// Token: 0x04000F0D RID: 3853
		[CompilerGenerated]
		private readonly bool <PrimaryKey>k__BackingField;
	}
}
