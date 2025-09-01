using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Identifies a type as an object suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000394 RID: 916
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class.</summary>
		// Token: 0x06001E0D RID: 7693 RVA: 0x0006B4DE File Offset: 0x000696DE
		public DataObjectAttribute() : this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class and indicates whether an object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object.</summary>
		/// <param name="isDataObject">
		///   <see langword="true" /> if the object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, <see langword="false" />.</param>
		// Token: 0x06001E0E RID: 7694 RVA: 0x0006B4E7 File Offset: 0x000696E7
		public DataObjectAttribute(bool isDataObject)
		{
			this.IsDataObject = isDataObject;
		}

		/// <summary>Gets a value indicating whether an object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time.</summary>
		/// <returns>
		///   <see langword="true" /> if the object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x0006B4F6 File Offset: 0x000696F6
		public bool IsDataObject
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDataObject>k__BackingField;
			}
		}

		/// <summary>Determines whether this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" /> fits the pattern of another object.</summary>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E10 RID: 7696 RVA: 0x0006B500 File Offset: 0x00069700
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectAttribute dataObjectAttribute = obj as DataObjectAttribute;
			return dataObjectAttribute != null && dataObjectAttribute.IsDataObject == this.IsDataObject;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E11 RID: 7697 RVA: 0x0006B530 File Offset: 0x00069730
		public override int GetHashCode()
		{
			return this.IsDataObject.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E12 RID: 7698 RVA: 0x0006B54B File Offset: 0x0006974B
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DataObjectAttribute.Default);
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x0006B558 File Offset: 0x00069758
		// Note: this type is marked as 'beforefieldinit'.
		static DataObjectAttribute()
		{
		}

		/// <summary>Indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04000F06 RID: 3846
		public static readonly DataObjectAttribute DataObject = new DataObjectAttribute(true);

		/// <summary>Indicates that the class is not suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04000F07 RID: 3847
		public static readonly DataObjectAttribute NonDataObject = new DataObjectAttribute(false);

		/// <summary>Represents the default value of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class, which indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04000F08 RID: 3848
		public static readonly DataObjectAttribute Default = DataObjectAttribute.NonDataObject;

		// Token: 0x04000F09 RID: 3849
		[CompilerGenerated]
		private readonly bool <IsDataObject>k__BackingField;
	}
}
