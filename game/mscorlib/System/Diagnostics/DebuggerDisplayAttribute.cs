using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Determines how a class or field is displayed in the debugger variable windows.</summary>
	// Token: 0x020009BD RID: 2493
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerDisplayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerDisplayAttribute" /> class.</summary>
		/// <param name="value">The string to be displayed in the value column for instances of the type; an empty string ("") causes the value column to be hidden.</param>
		// Token: 0x060059AB RID: 22955 RVA: 0x0013309F File Offset: 0x0013129F
		public DebuggerDisplayAttribute(string value)
		{
			if (value == null)
			{
				this.value = "";
			}
			else
			{
				this.value = value;
			}
			this.name = "";
			this.type = "";
		}

		/// <summary>Gets the string to display in the value column of the debugger variable windows.</summary>
		/// <returns>The string to display in the value column of the debugger variable.</returns>
		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x001330D4 File Offset: 0x001312D4
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets or sets the name to display in the debugger variable windows.</summary>
		/// <returns>The name to display in the debugger variable windows.</returns>
		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060059AD RID: 22957 RVA: 0x001330DC File Offset: 0x001312DC
		// (set) Token: 0x060059AE RID: 22958 RVA: 0x001330E4 File Offset: 0x001312E4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the string to display in the type column of the debugger variable windows.</summary>
		/// <returns>The string to display in the type column of the debugger variable windows.</returns>
		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x060059AF RID: 22959 RVA: 0x001330ED File Offset: 0x001312ED
		// (set) Token: 0x060059B0 RID: 22960 RVA: 0x001330F5 File Offset: 0x001312F5
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the type of the attribute's target.</summary>
		/// <returns>The attribute's target type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DebuggerDisplayAttribute.Target" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x060059B2 RID: 22962 RVA: 0x00133127 File Offset: 0x00131327
		// (set) Token: 0x060059B1 RID: 22961 RVA: 0x001330FE File Offset: 0x001312FE
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

		/// <summary>Gets or sets the type name of the attribute's target.</summary>
		/// <returns>The name of the attribute's target type.</returns>
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x060059B3 RID: 22963 RVA: 0x0013312F File Offset: 0x0013132F
		// (set) Token: 0x060059B4 RID: 22964 RVA: 0x00133137 File Offset: 0x00131337
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

		// Token: 0x04003783 RID: 14211
		private string name;

		// Token: 0x04003784 RID: 14212
		private string value;

		// Token: 0x04003785 RID: 14213
		private string type;

		// Token: 0x04003786 RID: 14214
		private string targetName;

		// Token: 0x04003787 RID: 14215
		private Type target;
	}
}
