using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using Unity;

namespace System.Drawing.Design
{
	/// <summary>Provides a base implementation of a toolbox item.</summary>
	// Token: 0x02000131 RID: 305
	[MonoTODO("Implementation is incomplete.")]
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public class ToolboxItem : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItem" /> class.</summary>
		// Token: 0x06000DCC RID: 3532 RVA: 0x0001F1CC File Offset: 0x0001D3CC
		public ToolboxItem()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItem" /> class that creates the specified type of component.</summary>
		/// <param name="toolType">The type of <see cref="T:System.ComponentModel.IComponent" /> that the toolbox item creates.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> was locked.</exception>
		// Token: 0x06000DCD RID: 3533 RVA: 0x0001F1DF File Offset: 0x0001D3DF
		public ToolboxItem(Type toolType)
		{
			this.Initialize(toolType);
		}

		/// <summary>Gets or sets the name of the assembly that contains the type or types that the toolbox item creates.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly containing the type or types to create.</returns>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0001F1F9 File Offset: 0x0001D3F9
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0001F210 File Offset: 0x0001D410
		public AssemblyName AssemblyName
		{
			get
			{
				return (AssemblyName)this.properties["AssemblyName"];
			}
			set
			{
				this.SetValue("AssemblyName", value);
			}
		}

		/// <summary>Gets or sets a bitmap to represent the toolbox item in the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the toolbox item in the toolbox.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0001F21E File Offset: 0x0001D41E
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0001F235 File Offset: 0x0001D435
		public Bitmap Bitmap
		{
			get
			{
				return (Bitmap)this.properties["Bitmap"];
			}
			set
			{
				this.SetValue("Bitmap", value);
			}
		}

		/// <summary>Gets or sets the display name for the toolbox item.</summary>
		/// <returns>The display name for the toolbox item.</returns>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0001F243 File Offset: 0x0001D443
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0001F250 File Offset: 0x0001D450
		public string DisplayName
		{
			get
			{
				return this.GetValue("DisplayName");
			}
			set
			{
				this.SetValue("DisplayName", value);
			}
		}

		/// <summary>Gets or sets the filter that determines whether the toolbox item can be used on a destination component.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0001F260 File Offset: 0x0001D460
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0001F28E File Offset: 0x0001D48E
		public ICollection Filter
		{
			get
			{
				ICollection collection = (ICollection)this.properties["Filter"];
				if (collection == null)
				{
					collection = new ToolboxItemFilterAttribute[0];
				}
				return collection;
			}
			set
			{
				this.SetValue("Filter", value);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Drawing.Design.ToolboxItem" /> is currently locked.</summary>
		/// <returns>
		///   <see langword="true" /> if the toolbox item is locked; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0001F29C File Offset: 0x0001D49C
		public virtual bool Locked
		{
			get
			{
				return this.locked;
			}
		}

		/// <summary>Gets or sets the fully qualified name of the type of <see cref="T:System.ComponentModel.IComponent" /> that the toolbox item creates when invoked.</summary>
		/// <returns>The fully qualified type name of the type of component that this toolbox item creates.</returns>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0001F2A4 File Offset: 0x0001D4A4
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x0001F2B1 File Offset: 0x0001D4B1
		public string TypeName
		{
			get
			{
				return this.GetValue("TypeName");
			}
			set
			{
				this.SetValue("TypeName", value);
			}
		}

		/// <summary>Gets or sets the company name for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the company for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0001F2BF File Offset: 0x0001D4BF
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0001F2D6 File Offset: 0x0001D4D6
		public string Company
		{
			get
			{
				return (string)this.properties["Company"];
			}
			set
			{
				this.SetValue("Company", value);
			}
		}

		/// <summary>Gets the component type for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the component type for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0001F2E4 File Offset: 0x0001D4E4
		public virtual string ComponentType
		{
			get
			{
				return ".NET Component";
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Reflection.AssemblyName" /> for the toolbox item.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.AssemblyName" /> objects.</returns>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0001F2EB File Offset: 0x0001D4EB
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x0001F304 File Offset: 0x0001D504
		public AssemblyName[] DependentAssemblies
		{
			get
			{
				return (AssemblyName[])this.properties["DependentAssemblies"];
			}
			set
			{
				AssemblyName[] array = new AssemblyName[value.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = value[i];
				}
				this.SetValue("DependentAssemblies", array);
			}
		}

		/// <summary>Gets or sets the description for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the description for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0001F33A File Offset: 0x0001D53A
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0001F351 File Offset: 0x0001D551
		public string Description
		{
			get
			{
				return (string)this.properties["Description"];
			}
			set
			{
				this.SetValue("Description", value);
			}
		}

		/// <summary>Gets a value indicating whether the toolbox item is transient.</summary>
		/// <returns>
		///   <see langword="true" />, if this toolbox item should not be stored in any toolbox database when an application that is providing a toolbox closes; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0001F360 File Offset: 0x0001D560
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0001F389 File Offset: 0x0001D589
		public bool IsTransient
		{
			get
			{
				object obj = this.properties["IsTransient"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.SetValue("IsTransient", value);
			}
		}

		/// <summary>Gets a dictionary of properties.</summary>
		/// <returns>A dictionary of name/value pairs (the names are property names and the values are property values).</returns>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0001F39C File Offset: 0x0001D59C
		public IDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets the version for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the version for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0001F3A4 File Offset: 0x0001D5A4
		public virtual string Version
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Throws an exception if the toolbox item is currently locked.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> is locked.</exception>
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0001F3AB File Offset: 0x0001D5AB
		protected void CheckUnlocked()
		{
			if (this.locked)
			{
				throw new InvalidOperationException("The ToolboxItem is locked");
			}
		}

		/// <summary>Creates the components that the toolbox item is configured to create.</summary>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000DE5 RID: 3557 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		public IComponent[] CreateComponents()
		{
			return this.CreateComponents(null);
		}

		/// <summary>Creates the components that the toolbox item is configured to create, using the specified designer host.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to use when creating the components.</param>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000DE6 RID: 3558 RVA: 0x0001F3CC File Offset: 0x0001D5CC
		public IComponent[] CreateComponents(IDesignerHost host)
		{
			this.OnComponentsCreating(new ToolboxComponentsCreatingEventArgs(host));
			IComponent[] array = this.CreateComponentsCore(host);
			this.OnComponentsCreated(new ToolboxComponentsCreatedEventArgs(array));
			return array;
		}

		/// <summary>Creates a component or an array of components when the toolbox item is invoked.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to host the toolbox item.</param>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000DE7 RID: 3559 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		protected virtual IComponent[] CreateComponentsCore(IDesignerHost host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			Type type = this.GetType(host, this.AssemblyName, this.TypeName, true);
			IComponent[] result;
			if (type == null)
			{
				result = new IComponent[0];
			}
			else
			{
				result = new IComponent[]
				{
					host.CreateComponent(type)
				};
			}
			return result;
		}

		/// <summary>Creates an array of components when the toolbox item is invoked.</summary>
		/// <param name="host">The designer host to use when creating components.</param>
		/// <param name="defaultValues">A dictionary of property name/value pairs of default values with which to initialize the component.</param>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x0001F450 File Offset: 0x0001D650
		protected virtual IComponent[] CreateComponentsCore(IDesignerHost host, IDictionary defaultValues)
		{
			IComponent[] array = this.CreateComponentsCore(host);
			foreach (Component component in array)
			{
				(host.GetDesigner(component) as IComponentInitializer).InitializeNewComponent(defaultValues);
			}
			return array;
		}

		/// <summary>Creates the components that the toolbox item is configured to create, using the specified designer host and default values.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to use when creating the components.</param>
		/// <param name="defaultValues">A dictionary of property name/value pairs of default values with which to initialize the component.</param>
		/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
		// Token: 0x06000DE9 RID: 3561 RVA: 0x0001F494 File Offset: 0x0001D694
		public IComponent[] CreateComponents(IDesignerHost host, IDictionary defaultValues)
		{
			this.OnComponentsCreating(new ToolboxComponentsCreatingEventArgs(host));
			IComponent[] array = this.CreateComponentsCore(host, defaultValues);
			this.OnComponentsCreated(new ToolboxComponentsCreatedEventArgs(array));
			return array;
		}

		/// <summary>Filters a property value before returning it.</summary>
		/// <param name="propertyName">The name of the property to filter.</param>
		/// <param name="value">The value against which to filter the property.</param>
		/// <returns>A filtered property value.</returns>
		// Token: 0x06000DEA RID: 3562 RVA: 0x0001F4C4 File Offset: 0x0001D6C4
		protected virtual object FilterPropertyValue(string propertyName, object value)
		{
			if (!(propertyName == "AssemblyName"))
			{
				if (!(propertyName == "DisplayName") && !(propertyName == "TypeName"))
				{
					if (!(propertyName == "Filter"))
					{
						return value;
					}
					if (value != null)
					{
						return value;
					}
					return new ToolboxItemFilterAttribute[0];
				}
				else
				{
					if (value != null)
					{
						return value;
					}
					return string.Empty;
				}
			}
			else
			{
				if (value != null)
				{
					return (value as ICloneable).Clone();
				}
				return null;
			}
		}

		/// <summary>Loads the state of the toolbox item from the specified serialization information object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to load from.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the stream characteristics.</param>
		// Token: 0x06000DEB RID: 3563 RVA: 0x0001F530 File Offset: 0x0001D730
		protected virtual void Deserialize(SerializationInfo info, StreamingContext context)
		{
			this.AssemblyName = (AssemblyName)info.GetValue("AssemblyName", typeof(AssemblyName));
			this.Bitmap = (Bitmap)info.GetValue("Bitmap", typeof(Bitmap));
			this.Filter = (ICollection)info.GetValue("Filter", typeof(ICollection));
			this.DisplayName = info.GetString("DisplayName");
			this.locked = info.GetBoolean("Locked");
			this.TypeName = info.GetString("TypeName");
		}

		/// <summary>Determines whether two <see cref="T:System.Drawing.Design.ToolboxItem" /> instances are equal.</summary>
		/// <param name="obj">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to compare with the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Drawing.Design.ToolboxItem" /> is equal to the current <see cref="T:System.Drawing.Design.ToolboxItem" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DEC RID: 3564 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		public override bool Equals(object obj)
		{
			ToolboxItem toolboxItem = obj as ToolboxItem;
			return toolboxItem != null && (obj == this || (toolboxItem.AssemblyName.Equals(this.AssemblyName) && toolboxItem.Locked.Equals(this.locked) && toolboxItem.TypeName.Equals(this.TypeName) && toolboxItem.DisplayName.Equals(this.DisplayName) && toolboxItem.Bitmap.Equals(this.Bitmap)));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x06000DED RID: 3565 RVA: 0x0001F651 File Offset: 0x0001D851
		public override int GetHashCode()
		{
			return (this.TypeName + this.DisplayName).GetHashCode();
		}

		/// <summary>Enables access to the type associated with the toolbox item.</summary>
		/// <param name="host">The designer host to query for <see cref="T:System.ComponentModel.Design.ITypeResolutionService" />.</param>
		/// <returns>The type associated with the toolbox item.</returns>
		// Token: 0x06000DEE RID: 3566 RVA: 0x0001F669 File Offset: 0x0001D869
		public Type GetType(IDesignerHost host)
		{
			return this.GetType(host, this.AssemblyName, this.TypeName, false);
		}

		/// <summary>Creates an instance of the specified type, optionally using a specified designer host and assembly name.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current document. This can be <see langword="null" />.</param>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly that contains the type to load. This can be <see langword="null" />.</param>
		/// <param name="typeName">The name of the type to create an instance of.</param>
		/// <param name="reference">A value indicating whether or not to add a reference to the assembly that contains the specified type to the designer host's set of references.</param>
		/// <returns>An instance of the specified type, if it can be located.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is not specified.</exception>
		// Token: 0x06000DEF RID: 3567 RVA: 0x0001F680 File Offset: 0x0001D880
		protected virtual Type GetType(IDesignerHost host, AssemblyName assemblyName, string typeName, bool reference)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (host == null)
			{
				return null;
			}
			ITypeResolutionService typeResolutionService = host.GetService(typeof(ITypeResolutionService)) as ITypeResolutionService;
			Type result = null;
			if (typeResolutionService != null)
			{
				typeResolutionService.GetAssembly(assemblyName, true);
				if (reference)
				{
					typeResolutionService.ReferenceAssembly(assemblyName);
				}
				result = typeResolutionService.GetType(typeName, true);
			}
			else
			{
				Assembly assembly = Assembly.Load(assemblyName);
				if (assembly != null)
				{
					result = assembly.GetType(typeName);
				}
			}
			return result;
		}

		/// <summary>Initializes the current toolbox item with the specified type to create.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> that the toolbox item creates.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> was locked.</exception>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		public virtual void Initialize(Type type)
		{
			this.CheckUnlocked();
			if (type == null)
			{
				return;
			}
			this.AssemblyName = type.Assembly.GetName();
			this.DisplayName = type.Name;
			this.TypeName = type.FullName;
			Image image = null;
			object[] customAttributes = type.GetCustomAttributes(true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				ToolboxBitmapAttribute toolboxBitmapAttribute = customAttributes[i] as ToolboxBitmapAttribute;
				if (toolboxBitmapAttribute != null)
				{
					image = toolboxBitmapAttribute.GetImage(type);
					break;
				}
			}
			if (image == null)
			{
				image = ToolboxBitmapAttribute.GetImageFromResource(type, null, false);
			}
			if (image != null)
			{
				this.Bitmap = (image as Bitmap);
				if (this.Bitmap == null)
				{
					this.Bitmap = new Bitmap(image);
				}
			}
			this.Filter = type.GetCustomAttributes(typeof(ToolboxItemFilterAttribute), true);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x0001F7AE File Offset: 0x0001D9AE
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.Serialize(info, context);
		}

		/// <summary>Locks the toolbox item and prevents changes to its properties.</summary>
		// Token: 0x06000DF2 RID: 3570 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		public virtual void Lock()
		{
			this.locked = true;
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreated" /> event.</summary>
		/// <param name="args">A <see cref="T:System.Drawing.Design.ToolboxComponentsCreatedEventArgs" /> that provides data for the event.</param>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x0001F7C1 File Offset: 0x0001D9C1
		protected virtual void OnComponentsCreated(ToolboxComponentsCreatedEventArgs args)
		{
			if (this.ComponentsCreated != null)
			{
				this.ComponentsCreated(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreating" /> event.</summary>
		/// <param name="args">A <see cref="T:System.Drawing.Design.ToolboxComponentsCreatingEventArgs" /> that provides data for the event.</param>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		protected virtual void OnComponentsCreating(ToolboxComponentsCreatingEventArgs args)
		{
			if (this.ComponentsCreating != null)
			{
				this.ComponentsCreating(this, args);
			}
		}

		/// <summary>Saves the state of the toolbox item to the specified serialization information object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to save to.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the stream characteristics.</param>
		// Token: 0x06000DF5 RID: 3573 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		protected virtual void Serialize(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("AssemblyName", this.AssemblyName);
			info.AddValue("Bitmap", this.Bitmap);
			info.AddValue("Filter", this.Filter);
			info.AddValue("DisplayName", this.DisplayName);
			info.AddValue("Locked", this.locked);
			info.AddValue("TypeName", this.TypeName);
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x0001F863 File Offset: 0x0001DA63
		public override string ToString()
		{
			return this.DisplayName;
		}

		/// <summary>Validates that an object is of a given type.</summary>
		/// <param name="propertyName">The name of the property to validate.</param>
		/// <param name="value">Optional value against which to validate.</param>
		/// <param name="expectedType">The expected type of the property.</param>
		/// <param name="allowNull">
		///   <see langword="true" /> to allow <see langword="null" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="allowNull" /> is <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not the type specified by <paramref name="expectedType" />.</exception>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0001F86C File Offset: 0x0001DA6C
		protected void ValidatePropertyType(string propertyName, object value, Type expectedType, bool allowNull)
		{
			if (!allowNull && value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value != null && !expectedType.Equals(value.GetType()))
			{
				throw new ArgumentException(Locale.GetText("Type mismatch between value ({0}) and expected type ({1}).", new object[]
				{
					value.GetType(),
					expectedType
				}), "value");
			}
		}

		/// <summary>Validates a property before it is assigned to the property dictionary.</summary>
		/// <param name="propertyName">The name of the property to validate.</param>
		/// <param name="value">The value against which to validate.</param>
		/// <returns>The value used to perform validation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="propertyName" /> is "IsTransient".</exception>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
		protected virtual object ValidatePropertyValue(string propertyName, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(propertyName);
			if (num <= 1629252038U)
			{
				if (num <= 982935374U)
				{
					if (num != 278446637U)
					{
						if (num != 982935374U)
						{
							return value;
						}
						if (!(propertyName == "TypeName"))
						{
							return value;
						}
					}
					else
					{
						if (!(propertyName == "IsTransient"))
						{
							return value;
						}
						this.ValidatePropertyType(propertyName, value, typeof(bool), false);
						return value;
					}
				}
				else if (num != 1561053712U)
				{
					if (num != 1629252038U)
					{
						return value;
					}
					if (!(propertyName == "AssemblyName"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(AssemblyName), true);
					return value;
				}
				else
				{
					if (!(propertyName == "DependentAssemblies"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(AssemblyName[]), true);
					return value;
				}
			}
			else if (num <= 1725856265U)
			{
				if (num != 1651150918U)
				{
					if (num != 1725856265U)
					{
						return value;
					}
					if (!(propertyName == "Description"))
					{
						return value;
					}
				}
				else
				{
					if (!(propertyName == "Bitmap"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(Bitmap), true);
					return value;
				}
			}
			else if (num != 3250523996U)
			{
				if (num != 4104765591U)
				{
					if (num != 4176258230U)
					{
						return value;
					}
					if (!(propertyName == "DisplayName"))
					{
						return value;
					}
				}
				else
				{
					if (!(propertyName == "Filter"))
					{
						return value;
					}
					this.ValidatePropertyType(propertyName, value, typeof(ToolboxItemFilterAttribute[]), true);
					if (value == null)
					{
						return new ToolboxItemFilterAttribute[0];
					}
					return value;
				}
			}
			else if (!(propertyName == "Company"))
			{
				return value;
			}
			this.ValidatePropertyType(propertyName, value, typeof(string), true);
			if (value == null)
			{
				value = string.Empty;
			}
			return value;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0001FA9B File Offset: 0x0001DC9B
		private void SetValue(string propertyName, object value)
		{
			this.CheckUnlocked();
			this.properties[propertyName] = this.ValidatePropertyValue(propertyName, value);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0001FAB8 File Offset: 0x0001DCB8
		private string GetValue(string propertyName)
		{
			string text = (string)this.properties[propertyName];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		/// <summary>Occurs immediately after components are created.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000DFB RID: 3579 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		// (remove) Token: 0x06000DFC RID: 3580 RVA: 0x0001FB1C File Offset: 0x0001DD1C
		public event ToolboxComponentsCreatedEventHandler ComponentsCreated
		{
			[CompilerGenerated]
			add
			{
				ToolboxComponentsCreatedEventHandler toolboxComponentsCreatedEventHandler = this.ComponentsCreated;
				ToolboxComponentsCreatedEventHandler toolboxComponentsCreatedEventHandler2;
				do
				{
					toolboxComponentsCreatedEventHandler2 = toolboxComponentsCreatedEventHandler;
					ToolboxComponentsCreatedEventHandler value2 = (ToolboxComponentsCreatedEventHandler)Delegate.Combine(toolboxComponentsCreatedEventHandler2, value);
					toolboxComponentsCreatedEventHandler = Interlocked.CompareExchange<ToolboxComponentsCreatedEventHandler>(ref this.ComponentsCreated, value2, toolboxComponentsCreatedEventHandler2);
				}
				while (toolboxComponentsCreatedEventHandler != toolboxComponentsCreatedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ToolboxComponentsCreatedEventHandler toolboxComponentsCreatedEventHandler = this.ComponentsCreated;
				ToolboxComponentsCreatedEventHandler toolboxComponentsCreatedEventHandler2;
				do
				{
					toolboxComponentsCreatedEventHandler2 = toolboxComponentsCreatedEventHandler;
					ToolboxComponentsCreatedEventHandler value2 = (ToolboxComponentsCreatedEventHandler)Delegate.Remove(toolboxComponentsCreatedEventHandler2, value);
					toolboxComponentsCreatedEventHandler = Interlocked.CompareExchange<ToolboxComponentsCreatedEventHandler>(ref this.ComponentsCreated, value2, toolboxComponentsCreatedEventHandler2);
				}
				while (toolboxComponentsCreatedEventHandler != toolboxComponentsCreatedEventHandler2);
			}
		}

		/// <summary>Occurs when components are about to be created.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000DFD RID: 3581 RVA: 0x0001FB54 File Offset: 0x0001DD54
		// (remove) Token: 0x06000DFE RID: 3582 RVA: 0x0001FB8C File Offset: 0x0001DD8C
		public event ToolboxComponentsCreatingEventHandler ComponentsCreating
		{
			[CompilerGenerated]
			add
			{
				ToolboxComponentsCreatingEventHandler toolboxComponentsCreatingEventHandler = this.ComponentsCreating;
				ToolboxComponentsCreatingEventHandler toolboxComponentsCreatingEventHandler2;
				do
				{
					toolboxComponentsCreatingEventHandler2 = toolboxComponentsCreatingEventHandler;
					ToolboxComponentsCreatingEventHandler value2 = (ToolboxComponentsCreatingEventHandler)Delegate.Combine(toolboxComponentsCreatingEventHandler2, value);
					toolboxComponentsCreatingEventHandler = Interlocked.CompareExchange<ToolboxComponentsCreatingEventHandler>(ref this.ComponentsCreating, value2, toolboxComponentsCreatingEventHandler2);
				}
				while (toolboxComponentsCreatingEventHandler != toolboxComponentsCreatingEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ToolboxComponentsCreatingEventHandler toolboxComponentsCreatingEventHandler = this.ComponentsCreating;
				ToolboxComponentsCreatingEventHandler toolboxComponentsCreatingEventHandler2;
				do
				{
					toolboxComponentsCreatingEventHandler2 = toolboxComponentsCreatingEventHandler;
					ToolboxComponentsCreatingEventHandler value2 = (ToolboxComponentsCreatingEventHandler)Delegate.Remove(toolboxComponentsCreatingEventHandler2, value);
					toolboxComponentsCreatingEventHandler = Interlocked.CompareExchange<ToolboxComponentsCreatingEventHandler>(ref this.ComponentsCreating, value2, toolboxComponentsCreatingEventHandler2);
				}
				while (toolboxComponentsCreatingEventHandler != toolboxComponentsCreatingEventHandler2);
			}
		}

		/// <summary>Gets or sets the original bitmap that will be used in the toolbox for this item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the toolbox item in the toolbox.</returns>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0001FBC1 File Offset: 0x0001DDC1
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x00005B7D File Offset: 0x00003D7D
		public Bitmap OriginalBitmap
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04000AA8 RID: 2728
		private bool locked;

		// Token: 0x04000AA9 RID: 2729
		private Hashtable properties = new Hashtable();

		// Token: 0x04000AAA RID: 2730
		[CompilerGenerated]
		private ToolboxComponentsCreatedEventHandler ComponentsCreated;

		// Token: 0x04000AAB RID: 2731
		[CompilerGenerated]
		private ToolboxComponentsCreatingEventHandler ComponentsCreating;
	}
}
