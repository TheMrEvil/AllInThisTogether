using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Allows you to override property, field, and class attributes when you use the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to serialize or deserialize an object.</summary>
	// Token: 0x020002CA RID: 714
	public class XmlAttributeOverrides
	{
		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlAttributes" /> object to the collection of <see cref="T:System.Xml.Serialization.XmlAttributes" /> objects. The <paramref name="type" /> parameter specifies an object to be overridden by the <see cref="T:System.Xml.Serialization.XmlAttributes" /> object.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object that is overridden. </param>
		/// <param name="attributes">An <see cref="T:System.Xml.Serialization.XmlAttributes" /> object that represents the overriding attributes. </param>
		// Token: 0x06001B28 RID: 6952 RVA: 0x0009B7AF File Offset: 0x000999AF
		public void Add(Type type, XmlAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlAttributes" /> object to the collection of <see cref="T:System.Xml.Serialization.XmlAttributes" /> objects. The <paramref name="type" /> parameter specifies an object to be overridden. The <paramref name="member" /> parameter specifies the name of a member that is overridden.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to override. </param>
		/// <param name="member">The name of the member to override. </param>
		/// <param name="attributes">An <see cref="T:System.Xml.Serialization.XmlAttributes" /> object that represents the overriding attributes. </param>
		// Token: 0x06001B29 RID: 6953 RVA: 0x0009B7C0 File Offset: 0x000999C0
		public void Add(Type type, string member, XmlAttributes attributes)
		{
			Hashtable hashtable = (Hashtable)this.types[type];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.types.Add(type, hashtable);
			}
			else if (hashtable[member] != null)
			{
				throw new InvalidOperationException(Res.GetString("'{0}.{1}' already has attributes.", new object[]
				{
					type.FullName,
					member
				}));
			}
			hashtable.Add(member, attributes);
		}

		/// <summary>Gets the object associated with the specified, base-class, type.</summary>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want to retrieve. </param>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributes" /> that represents the collection of overriding attributes.</returns>
		// Token: 0x1700054D RID: 1357
		public XmlAttributes this[Type type]
		{
			get
			{
				return this[type, string.Empty];
			}
		}

		/// <summary>Gets the object associated with the specified (base-class) type. The member parameter specifies the base-class member that is overridden.</summary>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want. </param>
		/// <param name="member">The name of the overridden member that specifies the <see cref="T:System.Xml.Serialization.XmlAttributes" /> to return. </param>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributes" /> that represents the collection of overriding attributes.</returns>
		// Token: 0x1700054E RID: 1358
		public XmlAttributes this[Type type, string member]
		{
			get
			{
				Hashtable hashtable = (Hashtable)this.types[type];
				if (hashtable == null)
				{
					return null;
				}
				return (XmlAttributes)hashtable[member];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> class. </summary>
		// Token: 0x06001B2C RID: 6956 RVA: 0x0009B86C File Offset: 0x00099A6C
		public XmlAttributeOverrides()
		{
		}

		// Token: 0x040019CA RID: 6602
		private Hashtable types = new Hashtable();
	}
}
