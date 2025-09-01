using System;
using System.Reflection;

namespace QFSW.QC.Internal
{
	// Token: 0x02000065 RID: 101
	internal class FieldAutoMethod : FieldMethod
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00009ECC File Offset: 0x000080CC
		public FieldAutoMethod(FieldInfo fieldInfo, FieldAutoMethod.AccessType accessType) : base(fieldInfo)
		{
			this._accessType = accessType;
			if (this._accessType == FieldAutoMethod.AccessType.Read)
			{
				if (this._fieldInfo.IsStatic)
				{
					this._internalDelegate = new Func<FieldInfo, object>(FieldAutoMethod._StaticReader);
				}
				else
				{
					this._internalDelegate = new Func<object, object>(this._fieldInfo.GetValue);
				}
				this._parameters = Array.Empty<ParameterInfo>();
				return;
			}
			if (this._fieldInfo.IsStatic)
			{
				this._internalDelegate = new Action<FieldInfo, object>(FieldAutoMethod._StaticWriter);
			}
			else
			{
				this._internalDelegate = new Action<object, object>(this._fieldInfo.SetValue);
			}
			this._parameters = new ParameterInfo[]
			{
				new CustomParameter(this._internalDelegate.Method.GetParameters()[1], this._fieldInfo.FieldType, "value")
			};
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00009FA3 File Offset: 0x000081A3
		private static object _StaticReader(FieldInfo field)
		{
			return field.GetValue(null);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009FAC File Offset: 0x000081AC
		private static void _StaticWriter(FieldInfo field, object value)
		{
			field.SetValue(null, value);
		}

		// Token: 0x04000145 RID: 325
		private readonly FieldAutoMethod.AccessType _accessType;

		// Token: 0x020000B4 RID: 180
		public enum AccessType
		{
			// Token: 0x0400023D RID: 573
			Read,
			// Token: 0x0400023E RID: 574
			Write
		}
	}
}
