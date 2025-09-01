using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Identifies a data operation method exposed by a type, what type of operation the method performs, and whether the method is the default data method. This class cannot be inherited.</summary>
	// Token: 0x02000396 RID: 918
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class DataObjectMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> class and identifies the type of data operation the method performs.</summary>
		/// <param name="methodType">One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that describes the data operation the method performs.</param>
		// Token: 0x06001E1E RID: 7710 RVA: 0x0006B63B File Offset: 0x0006983B
		public DataObjectMethodAttribute(DataObjectMethodType methodType) : this(methodType, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> class, identifies the type of data operation the method performs, and identifies whether the method is the default data method that the data object exposes.</summary>
		/// <param name="methodType">One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that describes the data operation the method performs.</param>
		/// <param name="isDefault">
		///   <see langword="true" /> to indicate the method that the attribute is applied to is the default method of the data object for the specified <paramref name="methodType" />; otherwise, <see langword="false" />.</param>
		// Token: 0x06001E1F RID: 7711 RVA: 0x0006B645 File Offset: 0x00069845
		public DataObjectMethodAttribute(DataObjectMethodType methodType, bool isDefault)
		{
			this.MethodType = methodType;
			this.IsDefault = isDefault;
		}

		/// <summary>Gets a value indicating whether the method that the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> is applied to is the default data method exposed by the data object for a specific method type.</summary>
		/// <returns>
		///   <see langword="true" /> if the method is the default method exposed by the object for a method type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x0006B65B File Offset: 0x0006985B
		public bool IsDefault
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDefault>k__BackingField;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.DataObjectMethodType" /> value indicating the type of data operation the method performs.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that identifies the type of data operation performed by the method to which the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> is applied.</returns>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0006B663 File Offset: 0x00069863
		public DataObjectMethodType MethodType
		{
			[CompilerGenerated]
			get
			{
				return this.<MethodType>k__BackingField;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectMethodAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E22 RID: 7714 RVA: 0x0006B66C File Offset: 0x0006986C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectMethodAttribute dataObjectMethodAttribute = obj as DataObjectMethodAttribute;
			return dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == this.MethodType && dataObjectMethodAttribute.IsDefault == this.IsDefault;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E23 RID: 7715 RVA: 0x0006B6A8 File Offset: 0x000698A8
		public override int GetHashCode()
		{
			return ((int)this.MethodType).GetHashCode() ^ this.IsDefault.GetHashCode();
		}

		/// <summary>Gets a value indicating whether this instance shares a common pattern with a specified attribute.</summary>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectMethodAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E24 RID: 7716 RVA: 0x0006B6D4 File Offset: 0x000698D4
		public override bool Match(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectMethodAttribute dataObjectMethodAttribute = obj as DataObjectMethodAttribute;
			return dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == this.MethodType;
		}

		// Token: 0x04000F0E RID: 3854
		[CompilerGenerated]
		private readonly bool <IsDefault>k__BackingField;

		// Token: 0x04000F0F RID: 3855
		[CompilerGenerated]
		private readonly DataObjectMethodType <MethodType>k__BackingField;
	}
}
