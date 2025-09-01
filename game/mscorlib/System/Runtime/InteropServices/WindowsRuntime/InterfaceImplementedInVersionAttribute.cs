using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Specifies the version of the target type that first implemented the specified interface.</summary>
	// Token: 0x02000784 RID: 1924
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
	public sealed class InterfaceImplementedInVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.InterfaceImplementedInVersionAttribute" /> class, specifying the interface that the target type implements and the version in which that interface was first implemented.</summary>
		/// <param name="interfaceType">The interface that was first implemented in the specified version of the target type.</param>
		/// <param name="majorVersion">The major component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		/// <param name="minorVersion">The minor component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		/// <param name="buildVersion">The build component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		/// <param name="revisionVersion">The revision component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		// Token: 0x0600447E RID: 17534 RVA: 0x000E3ACD File Offset: 0x000E1CCD
		public InterfaceImplementedInVersionAttribute(Type interfaceType, byte majorVersion, byte minorVersion, byte buildVersion, byte revisionVersion)
		{
			this.m_interfaceType = interfaceType;
			this.m_majorVersion = majorVersion;
			this.m_minorVersion = minorVersion;
			this.m_buildVersion = buildVersion;
			this.m_revisionVersion = revisionVersion;
		}

		/// <summary>Gets the type of the interface that the target type implements.</summary>
		/// <returns>The type of the interface.</returns>
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x000E3AFA File Offset: 0x000E1CFA
		public Type InterfaceType
		{
			get
			{
				return this.m_interfaceType;
			}
		}

		/// <summary>Gets the major component of the version of the target type that first implemented the interface.</summary>
		/// <returns>The major component of the version.</returns>
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x000E3B02 File Offset: 0x000E1D02
		public byte MajorVersion
		{
			get
			{
				return this.m_majorVersion;
			}
		}

		/// <summary>Gets the minor component of the version of the target type that first implemented the interface.</summary>
		/// <returns>The minor component of the version.</returns>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x000E3B0A File Offset: 0x000E1D0A
		public byte MinorVersion
		{
			get
			{
				return this.m_minorVersion;
			}
		}

		/// <summary>Gets the build component of the version of the target type that first implemented the interface.</summary>
		/// <returns>The build component of the version.</returns>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x000E3B12 File Offset: 0x000E1D12
		public byte BuildVersion
		{
			get
			{
				return this.m_buildVersion;
			}
		}

		/// <summary>Gets the revision component of the version of the target type that first implemented the interface.</summary>
		/// <returns>The revision component of the version.</returns>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x000E3B1A File Offset: 0x000E1D1A
		public byte RevisionVersion
		{
			get
			{
				return this.m_revisionVersion;
			}
		}

		// Token: 0x04002C1E RID: 11294
		private Type m_interfaceType;

		// Token: 0x04002C1F RID: 11295
		private byte m_majorVersion;

		// Token: 0x04002C20 RID: 11296
		private byte m_minorVersion;

		// Token: 0x04002C21 RID: 11297
		private byte m_buildVersion;

		// Token: 0x04002C22 RID: 11298
		private byte m_revisionVersion;
	}
}
