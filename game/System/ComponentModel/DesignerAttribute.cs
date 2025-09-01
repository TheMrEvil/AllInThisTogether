using System;
using System.ComponentModel.Design;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies the class used to implement design-time services for a component.</summary>
	// Token: 0x02000412 RID: 1042
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the name of the type that provides design-time services.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in.</param>
		// Token: 0x060021AA RID: 8618 RVA: 0x00073334 File Offset: 0x00071534
		public DesignerAttribute(string designerTypeName)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the type that provides design-time services.</summary>
		/// <param name="designerType">A <see cref="T:System.Type" /> that represents the class that provides design-time services for the component this attribute is bound to.</param>
		// Token: 0x060021AB RID: 8619 RVA: 0x00073364 File Offset: 0x00071564
		public DesignerAttribute(Type designerType)
		{
			this.designerTypeName = designerType.AssemblyQualifiedName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the designer type and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in.</param>
		/// <param name="designerBaseTypeName">The fully qualified name of the base class to associate with the designer class.</param>
		// Token: 0x060021AC RID: 8620 RVA: 0x0007338D File Offset: 0x0007158D
		public DesignerAttribute(string designerTypeName, string designerBaseTypeName)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class, using the name of the designer class and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in.</param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the base class to associate with the <paramref name="designerTypeName" />.</param>
		// Token: 0x060021AD RID: 8621 RVA: 0x000733AF File Offset: 0x000715AF
		public DesignerAttribute(string designerTypeName, Type designerBaseType)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the types of the designer and designer base class.</summary>
		/// <param name="designerType">A <see cref="T:System.Type" /> that represents the class that provides design-time services for the component this attribute is bound to.</param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the base class to associate with the <paramref name="designerType" />.</param>
		// Token: 0x060021AE RID: 8622 RVA: 0x000733D6 File Offset: 0x000715D6
		public DesignerAttribute(Type designerType, Type designerBaseType)
		{
			this.designerTypeName = designerType.AssemblyQualifiedName;
			this.designerBaseTypeName = designerBaseType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of the base type of this designer.</summary>
		/// <returns>The name of the base type of this designer.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000733F6 File Offset: 0x000715F6
		public string DesignerBaseTypeName
		{
			get
			{
				return this.designerBaseTypeName;
			}
		}

		/// <summary>Gets the name of the designer type associated with this designer attribute.</summary>
		/// <returns>The name of the designer type associated with this designer attribute.</returns>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000733FE File Offset: 0x000715FE
		public string DesignerTypeName
		{
			get
			{
				return this.designerTypeName;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x00073408 File Offset: 0x00071608
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.designerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this.typeId = base.GetType().FullName + text;
				}
				return this.typeId;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignerAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021B2 RID: 8626 RVA: 0x00073458 File Offset: 0x00071658
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerAttribute designerAttribute = obj as DesignerAttribute;
			return designerAttribute != null && designerAttribute.designerBaseTypeName == this.designerBaseTypeName && designerAttribute.designerTypeName == this.designerTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060021B3 RID: 8627 RVA: 0x0007349B File Offset: 0x0007169B
		public override int GetHashCode()
		{
			return this.designerTypeName.GetHashCode() ^ this.designerBaseTypeName.GetHashCode();
		}

		// Token: 0x04001022 RID: 4130
		private readonly string designerTypeName;

		// Token: 0x04001023 RID: 4131
		private readonly string designerBaseTypeName;

		// Token: 0x04001024 RID: 4132
		private string typeId;
	}
}
