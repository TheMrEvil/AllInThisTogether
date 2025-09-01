using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the editor to use to change a property. This class cannot be inherited.</summary>
	// Token: 0x020003A5 RID: 933
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class EditorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the default editor, which is no editor.</summary>
		// Token: 0x06001E7A RID: 7802 RVA: 0x0006C1A1 File Offset: 0x0006A3A1
		public EditorAttribute()
		{
			this.EditorTypeName = string.Empty;
			this.EditorBaseTypeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and base type name of the editor.</summary>
		/// <param name="typeName">The fully qualified type name of the editor.</param>
		/// <param name="baseTypeName">The fully qualified type name of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />.</param>
		// Token: 0x06001E7B RID: 7803 RVA: 0x0006C1BF File Offset: 0x0006A3BF
		public EditorAttribute(string typeName, string baseTypeName)
		{
			typeName.ToUpper(CultureInfo.InvariantCulture);
			this.EditorTypeName = typeName;
			this.EditorBaseTypeName = baseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and the base type.</summary>
		/// <param name="typeName">The fully qualified type name of the editor.</param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />.</param>
		// Token: 0x06001E7C RID: 7804 RVA: 0x0006C1E1 File Offset: 0x0006A3E1
		public EditorAttribute(string typeName, Type baseType)
		{
			typeName.ToUpper(CultureInfo.InvariantCulture);
			this.EditorTypeName = typeName;
			this.EditorBaseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type and the base type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the editor.</param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />.</param>
		// Token: 0x06001E7D RID: 7805 RVA: 0x0006C208 File Offset: 0x0006A408
		public EditorAttribute(Type type, Type baseType)
		{
			this.EditorTypeName = type.AssemblyQualifiedName;
			this.EditorBaseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of the base class or interface serving as a lookup key for this editor.</summary>
		/// <returns>The name of the base class or interface serving as a lookup key for this editor.</returns>
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0006C228 File Offset: 0x0006A428
		public string EditorBaseTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<EditorBaseTypeName>k__BackingField;
			}
		}

		/// <summary>Gets the name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</summary>
		/// <returns>The name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</returns>
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0006C230 File Offset: 0x0006A430
		public string EditorTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<EditorTypeName>k__BackingField;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0006C238 File Offset: 0x0006A438
		public override object TypeId
		{
			get
			{
				if (this._typeId == null)
				{
					string text = this.EditorBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this._typeId = base.GetType().FullName + text;
				}
				return this._typeId;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E81 RID: 7809 RVA: 0x0006C288 File Offset: 0x0006A488
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorAttribute editorAttribute = obj as EditorAttribute;
			return editorAttribute != null && editorAttribute.EditorTypeName == this.EditorTypeName && editorAttribute.EditorBaseTypeName == this.EditorBaseTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E82 RID: 7810 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000F36 RID: 3894
		private string _typeId;

		// Token: 0x04000F37 RID: 3895
		[CompilerGenerated]
		private readonly string <EditorBaseTypeName>k__BackingField;

		// Token: 0x04000F38 RID: 3896
		[CompilerGenerated]
		private readonly string <EditorTypeName>k__BackingField;
	}
}
