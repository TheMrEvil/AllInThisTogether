using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000031 RID: 49
	[Preserve]
	public class ES3Type_Nullable : ES3Type
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00009A73 File Offset: 0x00007C73
		public ES3Type_Nullable() : base(typeof(Nullable<>))
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009A88 File Offset: 0x00007C88
		public ES3Type_Nullable(Type type) : base(type)
		{
			this.hasValueProperty = ES3Reflection.GetES3ReflectedProperty(type, "HasValue");
			this.valueProperty = ES3Reflection.GetES3ReflectedProperty(type, "Value");
			this.genericArgument = ES3Reflection.GetGenericArguments(type)[0];
			this.argumentES3Type = ES3TypeMgr.GetOrCreateES3Type(this.genericArgument, false);
			this.isUnsupported = (this.argumentES3Type == null);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009AF0 File Offset: 0x00007CF0
		public override void Write(object obj, ES3Writer writer)
		{
			bool flag = (bool)this.hasValueProperty.GetValue(obj);
			writer.WriteProperty("HasValue", flag, ES3Type_bool.Instance);
			if (flag)
			{
				object value = this.valueProperty.GetValue(obj);
				writer.WriteProperty("Value", value, this.argumentES3Type);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009B48 File Offset: 0x00007D48
		public override object Read<T>(ES3Reader reader)
		{
			if (!reader.ReadProperty<bool>(ES3Type_bool.Instance))
			{
				return ES3Reflection.GetConstructor(this.type, new Type[0]).Invoke(new object[0]);
			}
			object obj = reader.ReadProperty<object>(this.argumentES3Type);
			return ES3Reflection.GetConstructor(this.type, new Type[]
			{
				this.genericArgument
			}).Invoke(new object[]
			{
				obj
			});
		}

		// Token: 0x04000082 RID: 130
		public ES3Type argumentES3Type;

		// Token: 0x04000083 RID: 131
		public Type genericArgument;

		// Token: 0x04000084 RID: 132
		private ES3Reflection.ES3ReflectedMember hasValueProperty;

		// Token: 0x04000085 RID: 133
		private ES3Reflection.ES3ReflectedMember valueProperty;
	}
}
