using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the filter string and filter type to use for a toolbox item.</summary>
	// Token: 0x020003F0 RID: 1008
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[Serializable]
	public sealed class ToolboxItemFilterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string.</summary>
		/// <param name="filterString">The filter string for the toolbox item.</param>
		// Token: 0x060020F2 RID: 8434 RVA: 0x00071A83 File Offset: 0x0006FC83
		public ToolboxItemFilterAttribute(string filterString) : this(filterString, ToolboxItemFilterType.Allow)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string and type.</summary>
		/// <param name="filterString">The filter string for the toolbox item.</param>
		/// <param name="filterType">A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> indicating the type of the filter.</param>
		// Token: 0x060020F3 RID: 8435 RVA: 0x00071A8D File Offset: 0x0006FC8D
		public ToolboxItemFilterAttribute(string filterString, ToolboxItemFilterType filterType)
		{
			this.FilterString = (filterString ?? string.Empty);
			this.FilterType = filterType;
		}

		/// <summary>Gets the filter string for the toolbox item.</summary>
		/// <returns>The filter string for the toolbox item.</returns>
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x00071AAC File Offset: 0x0006FCAC
		public string FilterString
		{
			[CompilerGenerated]
			get
			{
				return this.<FilterString>k__BackingField;
			}
		}

		/// <summary>Gets the type of the filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> that indicates the type of the filter.</returns>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00071AB4 File Offset: 0x0006FCB4
		public ToolboxItemFilterType FilterType
		{
			[CompilerGenerated]
			get
			{
				return this.<FilterType>k__BackingField;
			}
		}

		/// <summary>Gets the type ID for the attribute.</summary>
		/// <returns>The type ID for this attribute. All <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects with the same filter string return the same type ID.</returns>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x00071ABC File Offset: 0x0006FCBC
		public override object TypeId
		{
			get
			{
				string result;
				if ((result = this._typeId) == null)
				{
					result = (this._typeId = base.GetType().FullName + this.FilterString);
				}
				return result;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020F7 RID: 8439 RVA: 0x00071AF4 File Offset: 0x0006FCF4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterType.Equals(this.FilterType) && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060020F8 RID: 8440 RVA: 0x00071B45 File Offset: 0x0006FD45
		public override int GetHashCode()
		{
			return this.FilterString.GetHashCode();
		}

		/// <summary>Indicates whether the specified object has a matching filter string.</summary>
		/// <param name="obj">The object to test for a matching filter string.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object has a matching filter string; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020F9 RID: 8441 RVA: 0x00071B54 File Offset: 0x0006FD54
		public override bool Match(object obj)
		{
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060020FA RID: 8442 RVA: 0x00071B83 File Offset: 0x0006FD83
		public override string ToString()
		{
			return this.FilterString + "," + Enum.GetName(typeof(ToolboxItemFilterType), this.FilterType);
		}

		// Token: 0x04000FE9 RID: 4073
		private string _typeId;

		// Token: 0x04000FEA RID: 4074
		[CompilerGenerated]
		private readonly string <FilterString>k__BackingField;

		// Token: 0x04000FEB RID: 4075
		[CompilerGenerated]
		private readonly ToolboxItemFilterType <FilterType>k__BackingField;
	}
}
