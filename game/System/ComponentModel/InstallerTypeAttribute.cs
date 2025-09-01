using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the installer for a type that installs components.</summary>
	// Token: 0x020003BE RID: 958
	[AttributeUsage(AttributeTargets.Class)]
	public class InstallerTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstallerTypeAttribute" /> class, when given a <see cref="T:System.Type" /> that represents the installer for a component.</summary>
		/// <param name="installerType">A <see cref="T:System.Type" /> that represents the installer for the component this attribute is bound to. This class must implement <see cref="T:System.ComponentModel.Design.IDesigner" />.</param>
		// Token: 0x06001F24 RID: 7972 RVA: 0x0006CCAB File Offset: 0x0006AEAB
		public InstallerTypeAttribute(Type installerType)
		{
			this._typeName = installerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstallerTypeAttribute" /> class with the name of the component's installer type.</summary>
		/// <param name="typeName">The name of a <see cref="T:System.Type" /> that represents the installer for the component this attribute is bound to. This class must implement <see cref="T:System.ComponentModel.Design.IDesigner" />.</param>
		// Token: 0x06001F25 RID: 7973 RVA: 0x0006CCBF File Offset: 0x0006AEBF
		public InstallerTypeAttribute(string typeName)
		{
			this._typeName = typeName;
		}

		/// <summary>Gets the type of installer associated with this attribute.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of installer associated with this attribute, or <see langword="null" /> if an installer does not exist.</returns>
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0006CCCE File Offset: 0x0006AECE
		public virtual Type InstallerType
		{
			get
			{
				return Type.GetType(this._typeName);
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F27 RID: 7975 RVA: 0x0006CCDC File Offset: 0x0006AEDC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			InstallerTypeAttribute installerTypeAttribute = obj as InstallerTypeAttribute;
			return installerTypeAttribute != null && installerTypeAttribute._typeName == this._typeName;
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</returns>
		// Token: 0x06001F28 RID: 7976 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000F4A RID: 3914
		private string _typeName;
	}
}
