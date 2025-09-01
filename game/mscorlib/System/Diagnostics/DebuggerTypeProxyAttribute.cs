using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies the display proxy for a type.</summary>
	// Token: 0x020009BC RID: 2492
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type of the proxy.</summary>
		/// <param name="type">The proxy type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x060059A4 RID: 22948 RVA: 0x0013301E File Offset: 0x0013121E
		public DebuggerTypeProxyAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type name of the proxy.</summary>
		/// <param name="typeName">The type name of the proxy type.</param>
		// Token: 0x060059A5 RID: 22949 RVA: 0x00133046 File Offset: 0x00131246
		public DebuggerTypeProxyAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		/// <summary>Gets the type name of the proxy type.</summary>
		/// <returns>The type name of the proxy type.</returns>
		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060059A6 RID: 22950 RVA: 0x00133055 File Offset: 0x00131255
		public string ProxyTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Gets or sets the target type for the attribute.</summary>
		/// <returns>The target type for the attribute.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DebuggerTypeProxyAttribute.Target" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060059A8 RID: 22952 RVA: 0x00133086 File Offset: 0x00131286
		// (set) Token: 0x060059A7 RID: 22951 RVA: 0x0013305D File Offset: 0x0013125D
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		/// <summary>Gets or sets the name of the target type.</summary>
		/// <returns>The name of the target type.</returns>
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060059A9 RID: 22953 RVA: 0x0013308E File Offset: 0x0013128E
		// (set) Token: 0x060059AA RID: 22954 RVA: 0x00133096 File Offset: 0x00131296
		public string TargetTypeName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x04003780 RID: 14208
		private string typeName;

		// Token: 0x04003781 RID: 14209
		private string targetName;

		// Token: 0x04003782 RID: 14210
		private Type target;
	}
}
