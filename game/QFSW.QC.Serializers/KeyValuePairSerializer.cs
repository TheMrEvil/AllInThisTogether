using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Serializers
{
	// Token: 0x02000007 RID: 7
	public class KeyValuePairSerializer : GenericQcSerializer
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002209 File Offset: 0x00000409
		protected override Type GenericType
		{
			[CompilerGenerated]
			get
			{
				return this.<GenericType>k__BackingField;
			}
		} = typeof(KeyValuePair<, >);

		// Token: 0x0600000E RID: 14 RVA: 0x00002214 File Offset: 0x00000414
		public override string SerializeFormatted(object value, QuantumTheme theme)
		{
			Type type = value.GetType();
			PropertyInfo propertyInfo;
			if (this._keyPropertyLookup.ContainsKey(type))
			{
				propertyInfo = this._keyPropertyLookup[type];
			}
			else
			{
				propertyInfo = type.GetProperty("Key");
				this._keyPropertyLookup[type] = propertyInfo;
			}
			PropertyInfo propertyInfo2;
			if (this._valuePropertyLookup.ContainsKey(type))
			{
				propertyInfo2 = this._valuePropertyLookup[type];
			}
			else
			{
				propertyInfo2 = type.GetProperty("Value");
				this._valuePropertyLookup[type] = propertyInfo2;
			}
			string str = base.SerializeRecursive(propertyInfo.GetValue(value, null), theme);
			string str2 = base.SerializeRecursive(propertyInfo2.GetValue(value, null), theme);
			return str + ": " + str2;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022BE File Offset: 0x000004BE
		public KeyValuePairSerializer()
		{
		}

		// Token: 0x04000002 RID: 2
		[CompilerGenerated]
		private readonly Type <GenericType>k__BackingField;

		// Token: 0x04000003 RID: 3
		private readonly Dictionary<Type, PropertyInfo> _keyPropertyLookup = new Dictionary<Type, PropertyInfo>();

		// Token: 0x04000004 RID: 4
		private readonly Dictionary<Type, PropertyInfo> _valuePropertyLookup = new Dictionary<Type, PropertyInfo>();
	}
}
