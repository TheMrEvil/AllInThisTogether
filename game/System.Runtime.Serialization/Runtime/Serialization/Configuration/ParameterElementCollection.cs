using System;
using System.Configuration;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure serialization by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
	// Token: 0x020001A7 RID: 423
	[ConfigurationCollection(typeof(ParameterElement), AddItemName = "parameter", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class ParameterElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.ParameterElementCollection" /> class.</summary>
		// Token: 0x0600154A RID: 5450 RVA: 0x000543E4 File Offset: 0x000525E4
		public ParameterElementCollection()
		{
			base.AddElementName = "parameter";
		}

		/// <summary>Gets or sets the element in the collection at the specified position.</summary>
		/// <param name="index">The position of the element in the collection to get or set.</param>
		/// <returns>A <see cref="T:System.Runtime.Serialization.Configuration.ParameterElement" /> from the collection.</returns>
		// Token: 0x1700046D RID: 1133
		public ParameterElement this[int index]
		{
			get
			{
				return (ParameterElement)base.BaseGet(index);
			}
			set
			{
				if (!this.IsReadOnly())
				{
					if (value == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
					}
					if (base.BaseGet(index) != null)
					{
						base.BaseRemoveAt(index);
					}
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Adds an element to the collection of parameter elements.</summary>
		/// <param name="element">The <see cref="T:System.Runtime.Serialization.Configuration.ParameterElement" /> element to add to the collection.</param>
		// Token: 0x0600154D RID: 5453 RVA: 0x00053FFA File Offset: 0x000521FA
		public void Add(ParameterElement element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			this.BaseAdd(element);
		}

		/// <summary>Removes all members of the collection.</summary>
		// Token: 0x0600154E RID: 5454 RVA: 0x00054019 File Offset: 0x00052219
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Gets the type of the parameters collection in configuration.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> that contains the type of the parameters collection in configuration.</returns>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00003127 File Offset: 0x00001327
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		/// <summary>Gets or sets a value specifying whether the named type is found in the collection.</summary>
		/// <param name="typeName">The name of the type to find.</param>
		/// <returns>
		///   <see langword="true" /> if the element is present; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001550 RID: 5456 RVA: 0x00054021 File Offset: 0x00052221
		public bool Contains(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
			}
			return base.BaseGet(typeName) != null;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00054405 File Offset: 0x00052605
		protected override ConfigurationElement CreateNewElement()
		{
			return new ParameterElement();
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0005440C File Offset: 0x0005260C
		protected override string ElementName
		{
			get
			{
				return "parameter";
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00054413 File Offset: 0x00052613
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return ((ParameterElement)element).identity;
		}

		/// <summary>Gets the position of the specified element in the collection.</summary>
		/// <param name="element">The <see cref="T:System.Runtime.Serialization.Configuration.ParameterElement" /> element to find.</param>
		/// <returns>The position of the specified element.</returns>
		// Token: 0x06001554 RID: 5460 RVA: 0x00054062 File Offset: 0x00052262
		public int IndexOf(ParameterElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Runtime.Serialization.Configuration.ParameterElement" /> to remove.</param>
		// Token: 0x06001555 RID: 5461 RVA: 0x00054079 File Offset: 0x00052279
		public void Remove(ParameterElement element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes the element at the specified position.</summary>
		/// <param name="index">The position of the element to remove.</param>
		// Token: 0x06001556 RID: 5462 RVA: 0x000540C2 File Offset: 0x000522C2
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
