using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Allows you to override attributes applied to properties, fields, and classes when you use an <see cref="T:System.Xml.Serialization.XmlSerializer" /> to serialize or deserialize an object as encoded SOAP.</summary>
	// Token: 0x020002AB RID: 683
	public class SoapAttributeOverrides
	{
		/// <summary>Adds a <see cref="T:System.Xml.Serialization.SoapAttributes" /> to a collection of <see cref="T:System.Xml.Serialization.SoapAttributes" /> objects. The <paramref name="type" /> parameter specifies an object to be overridden by the <see cref="T:System.Xml.Serialization.SoapAttributes" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object that is overridden. </param>
		/// <param name="attributes">A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that represents the overriding attributes. </param>
		// Token: 0x060019C0 RID: 6592 RVA: 0x0009428D File Offset: 0x0009248D
		public void Add(Type type, SoapAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		/// <summary>Adds a <see cref="T:System.Xml.Serialization.SoapAttributes" /> to the collection of <see cref="T:System.Xml.Serialization.SoapAttributes" /> objects contained by the <see cref="T:System.Xml.Serialization.SoapAttributeOverrides" />. The <paramref name="type" /> parameter specifies the object to be overridden by the <see cref="T:System.Xml.Serialization.SoapAttributes" />. The <paramref name="member" /> parameter specifies the name of a member that is overridden.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to override. </param>
		/// <param name="member">The name of the member to override. </param>
		/// <param name="attributes">A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that represents the overriding attributes. </param>
		// Token: 0x060019C1 RID: 6593 RVA: 0x0009429C File Offset: 0x0009249C
		public void Add(Type type, string member, SoapAttributes attributes)
		{
			Hashtable hashtable = (Hashtable)this.types[type];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.types.Add(type, hashtable);
			}
			else if (hashtable[member] != null)
			{
				throw new InvalidOperationException(Res.GetString("{0}. {1} already has attributes.", new object[]
				{
					type.FullName,
					member
				}));
			}
			hashtable.Add(member, attributes);
		}

		/// <summary>Gets the object associated with the specified (base class) type.</summary>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want to retrieve. </param>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that represents the collection of overriding attributes.</returns>
		// Token: 0x170004EE RID: 1262
		public SoapAttributes this[Type type]
		{
			get
			{
				return this[type, string.Empty];
			}
		}

		/// <summary>Gets the object associated with the specified (base class) type. The <paramref name="member" /> parameter specifies the base class member that is overridden.</summary>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want to override. </param>
		/// <param name="member">The name of the overridden member that specifies the <see cref="T:System.Xml.Serialization.SoapAttributes" /> to return. </param>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that represents the collection of overriding attributes.</returns>
		// Token: 0x170004EF RID: 1263
		public SoapAttributes this[Type type, string member]
		{
			get
			{
				Hashtable hashtable = (Hashtable)this.types[type];
				if (hashtable == null)
				{
					return null;
				}
				return (SoapAttributes)hashtable[member];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapAttributeOverrides" /> class. </summary>
		// Token: 0x060019C4 RID: 6596 RVA: 0x00094348 File Offset: 0x00092548
		public SoapAttributeOverrides()
		{
		}

		// Token: 0x04001941 RID: 6465
		private Hashtable types = new Hashtable();
	}
}
