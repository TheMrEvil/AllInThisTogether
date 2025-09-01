using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Represents a class member, such as a property or event. This is an abstract base class.</summary>
	// Token: 0x02000419 RID: 1049
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MemberDescriptor" /> class with the specified name of the member.</summary>
		/// <param name="name">The name of the member.</param>
		/// <exception cref="T:System.ArgumentException">The name is an empty string ("") or <see langword="null" />.</exception>
		// Token: 0x060021D5 RID: 8661 RVA: 0x00073C2A File Offset: 0x00071E2A
		protected MemberDescriptor(string name) : this(name, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MemberDescriptor" /> class with the specified name of the member and an array of attributes.</summary>
		/// <param name="name">The name of the member.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that contains the member attributes.</param>
		/// <exception cref="T:System.ArgumentException">The name is an empty string ("") or <see langword="null" />.</exception>
		// Token: 0x060021D6 RID: 8662 RVA: 0x00073C34 File Offset: 0x00071E34
		protected MemberDescriptor(string name, Attribute[] attributes)
		{
			this.lockCookie = new object();
			base..ctor();
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(SR.GetString("Invalid member name."));
				}
				this.name = name;
				this.displayName = name;
				this.nameHash = name.GetHashCode();
				if (attributes != null)
				{
					this.attributes = attributes;
					this.attributesFiltered = false;
				}
				this.originalAttributes = this.attributes;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MemberDescriptor" /> class with the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the member and its attributes.</param>
		// Token: 0x060021D7 RID: 8663 RVA: 0x00073CB8 File Offset: 0x00071EB8
		protected MemberDescriptor(MemberDescriptor descr)
		{
			this.lockCookie = new object();
			base..ctor();
			this.name = descr.Name;
			this.displayName = this.name;
			this.nameHash = this.name.GetHashCode();
			this.attributes = new Attribute[descr.Attributes.Count];
			descr.Attributes.CopyTo(this.attributes, 0);
			this.attributesFiltered = true;
			this.originalAttributes = this.attributes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MemberDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the old <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="oldMemberDescriptor">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that has the name of the member and its attributes.</param>
		/// <param name="newAttributes">An array of <see cref="T:System.Attribute" /> objects with the attributes you want to add to the member.</param>
		// Token: 0x060021D8 RID: 8664 RVA: 0x00073D3C File Offset: 0x00071F3C
		protected MemberDescriptor(MemberDescriptor oldMemberDescriptor, Attribute[] newAttributes)
		{
			this.lockCookie = new object();
			base..ctor();
			this.name = oldMemberDescriptor.Name;
			this.displayName = oldMemberDescriptor.DisplayName;
			this.nameHash = this.name.GetHashCode();
			ArrayList arrayList = new ArrayList();
			if (oldMemberDescriptor.Attributes.Count != 0)
			{
				foreach (object value in oldMemberDescriptor.Attributes)
				{
					arrayList.Add(value);
				}
			}
			if (newAttributes != null)
			{
				foreach (Attribute value2 in newAttributes)
				{
					arrayList.Add(value2);
				}
			}
			this.attributes = new Attribute[arrayList.Count];
			arrayList.CopyTo(this.attributes, 0);
			this.attributesFiltered = false;
			this.originalAttributes = this.attributes;
		}

		/// <summary>Gets or sets an array of attributes.</summary>
		/// <returns>An array of type <see cref="T:System.Attribute" /> that contains the attributes of this member.</returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00073E38 File Offset: 0x00072038
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x00073E4C File Offset: 0x0007204C
		protected virtual Attribute[] AttributeArray
		{
			get
			{
				this.CheckAttributesValid();
				this.FilterAttributesIfNeeded();
				return this.attributes;
			}
			set
			{
				object obj = this.lockCookie;
				lock (obj)
				{
					this.attributes = value;
					this.originalAttributes = value;
					this.attributesFiltered = false;
					this.attributeCollection = null;
				}
			}
		}

		/// <summary>Gets the collection of attributes for this member.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> that provides the attributes for this member, or an empty collection if there are no attributes in the <see cref="P:System.ComponentModel.MemberDescriptor.AttributeArray" />.</returns>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x00073EA4 File Offset: 0x000720A4
		public virtual AttributeCollection Attributes
		{
			get
			{
				this.CheckAttributesValid();
				AttributeCollection attributeCollection = this.attributeCollection;
				if (attributeCollection == null)
				{
					object obj = this.lockCookie;
					lock (obj)
					{
						attributeCollection = this.CreateAttributeCollection();
						this.attributeCollection = attributeCollection;
					}
				}
				return attributeCollection;
			}
		}

		/// <summary>Gets the name of the category to which the member belongs, as specified in the <see cref="T:System.ComponentModel.CategoryAttribute" />.</summary>
		/// <returns>The name of the category to which the member belongs. If there is no <see cref="T:System.ComponentModel.CategoryAttribute" />, the category name is set to the default category, <see langword="Misc" />.</returns>
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x00073F00 File Offset: 0x00072100
		public virtual string Category
		{
			get
			{
				if (this.category == null)
				{
					this.category = ((CategoryAttribute)this.Attributes[typeof(CategoryAttribute)]).Category;
				}
				return this.category;
			}
		}

		/// <summary>Gets the description of the member, as specified in the <see cref="T:System.ComponentModel.DescriptionAttribute" />.</summary>
		/// <returns>The description of the member. If there is no <see cref="T:System.ComponentModel.DescriptionAttribute" />, the property value is set to the default, which is an empty string ("").</returns>
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x00073F35 File Offset: 0x00072135
		public virtual string Description
		{
			get
			{
				if (this.description == null)
				{
					this.description = ((DescriptionAttribute)this.Attributes[typeof(DescriptionAttribute)]).Description;
				}
				return this.description;
			}
		}

		/// <summary>Gets a value indicating whether the member is browsable, as specified in the <see cref="T:System.ComponentModel.BrowsableAttribute" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the member is browsable; otherwise, <see langword="false" />. If there is no <see cref="T:System.ComponentModel.BrowsableAttribute" />, the property value is set to the default, which is <see langword="true" />.</returns>
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x00073F6A File Offset: 0x0007216A
		public virtual bool IsBrowsable
		{
			get
			{
				return ((BrowsableAttribute)this.Attributes[typeof(BrowsableAttribute)]).Browsable;
			}
		}

		/// <summary>Gets the name of the member.</summary>
		/// <returns>The name of the member.</returns>
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x00073F8B File Offset: 0x0007218B
		public virtual string Name
		{
			get
			{
				if (this.name == null)
				{
					return "";
				}
				return this.name;
			}
		}

		/// <summary>Gets the hash code for the name of the member, as specified in <see cref="M:System.String.GetHashCode" />.</summary>
		/// <returns>The hash code for the name of the member.</returns>
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x00073FA1 File Offset: 0x000721A1
		protected virtual int NameHashCode
		{
			get
			{
				return this.nameHash;
			}
		}

		/// <summary>Gets whether this member should be set only at design time, as specified in the <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>
		///   <see langword="true" /> if this member should be set only at design time; <see langword="false" /> if the member can be set during run time.</returns>
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x00073FA9 File Offset: 0x000721A9
		public virtual bool DesignTimeOnly
		{
			get
			{
				return DesignOnlyAttribute.Yes.Equals(this.Attributes[typeof(DesignOnlyAttribute)]);
			}
		}

		/// <summary>Gets the name that can be displayed in a window, such as a Properties window.</summary>
		/// <returns>The name to display for the member.</returns>
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x00073FCC File Offset: 0x000721CC
		public virtual string DisplayName
		{
			get
			{
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					return this.displayName;
				}
				return displayNameAttribute.DisplayName;
			}
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0007400C File Offset: 0x0007220C
		private void CheckAttributesValid()
		{
			if (this.attributesFiltered && this.metadataVersion != TypeDescriptor.MetadataVersion)
			{
				this.attributesFilled = false;
				this.attributesFiltered = false;
				this.attributeCollection = null;
			}
		}

		/// <summary>Creates a collection of attributes using the array of attributes passed to the constructor.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.AttributeCollection" /> that contains the <see cref="P:System.ComponentModel.MemberDescriptor.AttributeArray" /> attributes.</returns>
		// Token: 0x060021E4 RID: 8676 RVA: 0x00074038 File Offset: 0x00072238
		protected virtual AttributeCollection CreateAttributeCollection()
		{
			return new AttributeCollection(this.AttributeArray);
		}

		/// <summary>Compares this instance to the given object to see if they are equivalent.</summary>
		/// <param name="obj">The object to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if equivalent; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021E5 RID: 8677 RVA: 0x00074048 File Offset: 0x00072248
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			MemberDescriptor memberDescriptor = (MemberDescriptor)obj;
			this.FilterAttributesIfNeeded();
			memberDescriptor.FilterAttributesIfNeeded();
			if (memberDescriptor.nameHash != this.nameHash)
			{
				return false;
			}
			if (memberDescriptor.category == null != (this.category == null) || (this.category != null && !memberDescriptor.category.Equals(this.category)))
			{
				return false;
			}
			if (!LocalAppContextSwitches.MemberDescriptorEqualsReturnsFalseIfEquivalent)
			{
				if (memberDescriptor.description == null != (this.description == null) || (this.description != null && !memberDescriptor.description.Equals(this.description)))
				{
					return false;
				}
			}
			else if (memberDescriptor.description == null != (this.description == null) || (this.description != null && !memberDescriptor.category.Equals(this.description)))
			{
				return false;
			}
			if (memberDescriptor.attributes == null != (this.attributes == null))
			{
				return false;
			}
			bool result = true;
			if (this.attributes != null)
			{
				if (this.attributes.Length != memberDescriptor.attributes.Length)
				{
					return false;
				}
				for (int i = 0; i < this.attributes.Length; i++)
				{
					if (!this.attributes[i].Equals(memberDescriptor.attributes[i]))
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		/// <summary>When overridden in a derived class, adds the attributes of the inheriting class to the specified list of attributes in the parent class.</summary>
		/// <param name="attributeList">An <see cref="T:System.Collections.IList" /> that lists the attributes in the parent class. Initially, this is empty.</param>
		// Token: 0x060021E6 RID: 8678 RVA: 0x00074198 File Offset: 0x00072398
		protected virtual void FillAttributes(IList attributeList)
		{
			if (this.originalAttributes != null)
			{
				foreach (Attribute value in this.originalAttributes)
				{
					attributeList.Add(value);
				}
			}
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000741D0 File Offset: 0x000723D0
		private void FilterAttributesIfNeeded()
		{
			if (!this.attributesFiltered)
			{
				IList list;
				if (!this.attributesFilled)
				{
					list = new ArrayList();
					try
					{
						this.FillAttributes(list);
						goto IL_34;
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception)
					{
						goto IL_34;
					}
				}
				list = new ArrayList(this.attributes);
				IL_34:
				Hashtable hashtable = new Hashtable(list.Count);
				foreach (object obj in list)
				{
					Attribute attribute = (Attribute)obj;
					hashtable[attribute.TypeId] = attribute;
				}
				Attribute[] array = new Attribute[hashtable.Values.Count];
				hashtable.Values.CopyTo(array, 0);
				object obj2 = this.lockCookie;
				lock (obj2)
				{
					this.attributes = array;
					this.attributesFiltered = true;
					this.attributesFilled = true;
					this.metadataVersion = TypeDescriptor.MetadataVersion;
				}
			}
		}

		/// <summary>Finds the given method through reflection, searching only for public methods.</summary>
		/// <param name="componentClass">The component that contains the method.</param>
		/// <param name="name">The name of the method to find.</param>
		/// <param name="args">An array of parameters for the method, used to choose between overloaded methods.</param>
		/// <param name="returnType">The type to return for the method.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents the method, or <see langword="null" /> if the method is not found.</returns>
		// Token: 0x060021E8 RID: 8680 RVA: 0x000742F4 File Offset: 0x000724F4
		protected static MethodInfo FindMethod(Type componentClass, string name, Type[] args, Type returnType)
		{
			return MemberDescriptor.FindMethod(componentClass, name, args, returnType, true);
		}

		/// <summary>Finds the given method through reflection, with an option to search only public methods.</summary>
		/// <param name="componentClass">The component that contains the method.</param>
		/// <param name="name">The name of the method to find.</param>
		/// <param name="args">An array of parameters for the method, used to choose between overloaded methods.</param>
		/// <param name="returnType">The type to return for the method.</param>
		/// <param name="publicOnly">Whether to restrict search to public methods.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents the method, or <see langword="null" /> if the method is not found.</returns>
		// Token: 0x060021E9 RID: 8681 RVA: 0x00074300 File Offset: 0x00072500
		protected static MethodInfo FindMethod(Type componentClass, string name, Type[] args, Type returnType, bool publicOnly)
		{
			MethodInfo methodInfo;
			if (publicOnly)
			{
				methodInfo = componentClass.GetMethod(name, args);
			}
			else
			{
				methodInfo = componentClass.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, args, null);
			}
			if (methodInfo != null && !methodInfo.ReturnType.IsEquivalentTo(returnType))
			{
				methodInfo = null;
			}
			return methodInfo;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.MemberDescriptor" />.</returns>
		// Token: 0x060021EA RID: 8682 RVA: 0x00073FA1 File Offset: 0x000721A1
		public override int GetHashCode()
		{
			return this.nameHash;
		}

		/// <summary>Retrieves the object that should be used during invocation of members.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the invocation target.</param>
		/// <param name="instance">The potential invocation target.</param>
		/// <returns>The object to be used during member invocations.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="instance" /> is <see langword="null" />.</exception>
		// Token: 0x060021EB RID: 8683 RVA: 0x00074345 File Offset: 0x00072545
		protected virtual object GetInvocationTarget(Type type, object instance)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return TypeDescriptor.GetAssociation(type, instance);
		}

		/// <summary>Gets a component site for the given component.</summary>
		/// <param name="component">The component for which you want to find a site.</param>
		/// <returns>The site of the component, or <see langword="null" /> if a site does not exist.</returns>
		// Token: 0x060021EC RID: 8684 RVA: 0x00074370 File Offset: 0x00072570
		protected static ISite GetSite(object component)
		{
			if (!(component is IComponent))
			{
				return null;
			}
			return ((IComponent)component).Site;
		}

		/// <summary>Gets the component on which to invoke a method.</summary>
		/// <param name="componentClass">A <see cref="T:System.Type" /> representing the type of component this <see cref="T:System.ComponentModel.MemberDescriptor" /> is bound to. For example, if this <see cref="T:System.ComponentModel.MemberDescriptor" /> describes a property, this parameter should be the class that the property is declared on.</param>
		/// <param name="component">An instance of the object to call.</param>
		/// <returns>An instance of the component to invoke. This method returns a visual designer when the property is attached to a visual designer.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="componentClass" /> or <paramref name="component" /> is <see langword="null" />.</exception>
		// Token: 0x060021ED RID: 8685 RVA: 0x00074387 File Offset: 0x00072587
		[Obsolete("This method has been deprecated. Use GetInvocationTarget instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected static object GetInvokee(Type componentClass, object component)
		{
			if (componentClass == null)
			{
				throw new ArgumentNullException("componentClass");
			}
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return TypeDescriptor.GetAssociation(componentClass, component);
		}

		// Token: 0x0400102B RID: 4139
		private string name;

		// Token: 0x0400102C RID: 4140
		private string displayName;

		// Token: 0x0400102D RID: 4141
		private int nameHash;

		// Token: 0x0400102E RID: 4142
		private AttributeCollection attributeCollection;

		// Token: 0x0400102F RID: 4143
		private Attribute[] attributes;

		// Token: 0x04001030 RID: 4144
		private Attribute[] originalAttributes;

		// Token: 0x04001031 RID: 4145
		private bool attributesFiltered;

		// Token: 0x04001032 RID: 4146
		private bool attributesFilled;

		// Token: 0x04001033 RID: 4147
		private int metadataVersion;

		// Token: 0x04001034 RID: 4148
		private string category;

		// Token: 0x04001035 RID: 4149
		private string description;

		// Token: 0x04001036 RID: 4150
		private object lockCookie;
	}
}
