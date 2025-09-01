using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed.</summary>
	// Token: 0x020003E9 RID: 1001
	[AttributeUsage(AttributeTargets.Class)]
	public class RunInstallerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunInstallerAttribute" /> class.</summary>
		/// <param name="runInstaller">
		///   <see langword="true" /> if an installer should be invoked during installation of an assembly; otherwise, <see langword="false" />.</param>
		// Token: 0x060020D1 RID: 8401 RVA: 0x0007176A File Offset: 0x0006F96A
		public RunInstallerAttribute(bool runInstaller)
		{
			this.RunInstaller = runInstaller;
		}

		/// <summary>Gets a value indicating whether an installer should be invoked during installation of an assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if an installer should be invoked during installation of an assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x00071779 File Offset: 0x0006F979
		public bool RunInstaller
		{
			[CompilerGenerated]
			get
			{
				return this.<RunInstaller>k__BackingField;
			}
		}

		/// <summary>Determines whether the value of the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equivalent to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equal to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020D3 RID: 8403 RVA: 0x00071784 File Offset: 0x0006F984
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RunInstallerAttribute runInstallerAttribute = obj as RunInstallerAttribute;
			return runInstallerAttribute != null && runInstallerAttribute.RunInstaller == this.RunInstaller;
		}

		/// <summary>Generates a hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</returns>
		// Token: 0x060020D4 RID: 8404 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020D5 RID: 8405 RVA: 0x000717B1 File Offset: 0x0006F9B1
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RunInstallerAttribute.Default);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000717BE File Offset: 0x0006F9BE
		// Note: this type is marked as 'beforefieldinit'.
		static RunInstallerAttribute()
		{
		}

		// Token: 0x04000FE2 RID: 4066
		[CompilerGenerated]
		private readonly bool <RunInstaller>k__BackingField;

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000FE3 RID: 4067
		public static readonly RunInstallerAttribute Yes = new RunInstallerAttribute(true);

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should not be invoked when the assembly is installed. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000FE4 RID: 4068
		public static readonly RunInstallerAttribute No = new RunInstallerAttribute(false);

		/// <summary>Specifies the default visiblity, which is <see cref="F:System.ComponentModel.RunInstallerAttribute.No" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000FE5 RID: 4069
		public static readonly RunInstallerAttribute Default = RunInstallerAttribute.No;
	}
}
