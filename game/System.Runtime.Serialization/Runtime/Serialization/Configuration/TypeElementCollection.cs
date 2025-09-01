using System;
using System.Configuration;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure the known types used for serialization by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
	// Token: 0x020001AA RID: 426
	[ConfigurationCollection(typeof(TypeElement), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class TypeElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.TypeElementCollection" /> class.</summary>
		// Token: 0x06001567 RID: 5479 RVA: 0x00053F24 File Offset: 0x00052124
		public TypeElementCollection()
		{
		}

		/// <summary>Returns a specific member of the collection by its position.</summary>
		/// <param name="index">The position of the item to return.</param>
		/// <returns>The element at the specified position.</returns>
		// Token: 0x17000477 RID: 1143
		public TypeElement this[int index]
		{
			get
			{
				return (TypeElement)base.BaseGet(index);
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

		/// <summary>Adds the specified element to the collection.</summary>
		/// <param name="element">A <see cref="T:System.Runtime.Serialization.Configuration.TypeElement" /> that represents the known type to add.</param>
		// Token: 0x0600156A RID: 5482 RVA: 0x00053FFA File Offset: 0x000521FA
		public void Add(TypeElement element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			this.BaseAdd(element);
		}

		/// <summary>Removes all members of the collection.</summary>
		// Token: 0x0600156B RID: 5483 RVA: 0x00054019 File Offset: 0x00052219
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Gets the collection of elements that represents the types using known types.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> that contains the element objects.</returns>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00003127 File Offset: 0x00001327
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x000546F4 File Offset: 0x000528F4
		protected override ConfigurationElement CreateNewElement()
		{
			return new TypeElement();
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x000546FB File Offset: 0x000528FB
		protected override string ElementName
		{
			get
			{
				return "knownType";
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00054702 File Offset: 0x00052902
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return ((TypeElement)element).Key;
		}

		/// <summary>Returns the position of the specified element.</summary>
		/// <param name="element">The <see cref="T:System.Runtime.Serialization.Configuration.TypeElement" /> to find in the collection.</param>
		/// <returns>The position of the specified element.</returns>
		// Token: 0x06001570 RID: 5488 RVA: 0x00054062 File Offset: 0x00052262
		public int IndexOf(TypeElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Runtime.Serialization.Configuration.TypeElement" /> to remove.</param>
		// Token: 0x06001571 RID: 5489 RVA: 0x00054079 File Offset: 0x00052279
		public void Remove(TypeElement element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes the element at the specified position.</summary>
		/// <param name="index">The position in the collection from which to remove the element.</param>
		// Token: 0x06001572 RID: 5490 RVA: 0x000540C2 File Offset: 0x000522C2
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x04000A8A RID: 2698
		private const string KnownTypeConfig = "knownType";
	}
}
