using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the physical position of fields within the unmanaged representation of a class or structure.</summary>
	// Token: 0x02000708 RID: 1800
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FieldOffsetAttribute : Attribute
	{
		// Token: 0x060040A9 RID: 16553 RVA: 0x000E14BC File Offset: 0x000DF6BC
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			int fieldOffset;
			if (field.DeclaringType != null && (fieldOffset = field.GetFieldOffset()) >= 0)
			{
				return new FieldOffsetAttribute(fieldOffset);
			}
			return null;
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x000E14EA File Offset: 0x000DF6EA
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return FieldOffsetAttribute.GetCustomAttribute(field) != null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.FieldOffsetAttribute" /> class with the offset in the structure to the beginning of the field.</summary>
		/// <param name="offset">The offset in bytes from the beginning of the structure to the beginning of the field.</param>
		// Token: 0x060040AB RID: 16555 RVA: 0x000E14F5 File Offset: 0x000DF6F5
		public FieldOffsetAttribute(int offset)
		{
			this._val = offset;
		}

		/// <summary>Gets the offset from the beginning of the structure to the beginning of the field.</summary>
		/// <returns>The offset from the beginning of the structure to the beginning of the field.</returns>
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x000E1504 File Offset: 0x000DF704
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002ADF RID: 10975
		internal int _val;
	}
}
