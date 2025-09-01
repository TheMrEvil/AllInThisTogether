using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Holds a reference to a value.</summary>
	/// <typeparam name="T">The type of the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</typeparam>
	// Token: 0x020002EB RID: 747
	public class StrongBox<T> : IStrongBox
	{
		/// <summary>Initializes a new StrongBox which can receive a value when used in a reference call.</summary>
		// Token: 0x060016AF RID: 5807 RVA: 0x00002162 File Offset: 0x00000362
		public StrongBox()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> class by using the supplied value. </summary>
		/// <param name="value">A value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> will reference.</param>
		// Token: 0x060016B0 RID: 5808 RVA: 0x0004C7DB File Offset: 0x0004A9DB
		public StrongBox(T value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</summary>
		/// <returns>The value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</returns>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0004C7EA File Offset: 0x0004A9EA
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x0004C7F7 File Offset: 0x0004A9F7
		object IStrongBox.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (T)((object)value);
			}
		}

		/// <summary>Represents the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</summary>
		// Token: 0x04000B63 RID: 2915
		public T Value;
	}
}
