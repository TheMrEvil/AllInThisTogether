using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a local variable and provides access to local variable metadata.</summary>
	// Token: 0x020008EF RID: 2287
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class LocalVariableInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.LocalVariableInfo" /> class.</summary>
		// Token: 0x06004CAB RID: 19627 RVA: 0x0000259F File Offset: 0x0000079F
		protected LocalVariableInfo()
		{
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>
		///   <see langword="true" /> if the object referred to by the variable is pinned in memory; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004CAC RID: 19628 RVA: 0x000F30ED File Offset: 0x000F12ED
		public virtual bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		/// <summary>Gets the index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004CAD RID: 19629 RVA: 0x000F30F5 File Offset: 0x000F12F5
		public virtual int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>The type of the local variable.</returns>
		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004CAE RID: 19630 RVA: 0x000F30FD File Offset: 0x000F12FD
		public virtual Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns a user-readable string that describes the local variable.</summary>
		/// <returns>A string that displays information about the local variable, including the type name, index, and pinned status.</returns>
		// Token: 0x06004CAF RID: 19631 RVA: 0x000F3108 File Offset: 0x000F1308
		public override string ToString()
		{
			if (this.is_pinned)
			{
				return string.Format("{0} ({1}) (pinned)", this.type, this.position);
			}
			return string.Format("{0} ({1})", this.type, this.position);
		}

		// Token: 0x04003032 RID: 12338
		internal Type type;

		// Token: 0x04003033 RID: 12339
		internal bool is_pinned;

		// Token: 0x04003034 RID: 12340
		internal ushort position;
	}
}
