using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000029 RID: 41
	[Preserve]
	public abstract class ES3GenericType : ES3Type
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00008E3C File Offset: 0x0000703C
		public ES3GenericType(Type type) : base(type)
		{
			this.genericArguments = ES3Reflection.GetGenericArguments(type);
			this.genericArgumentES3Types = new ES3Type[this.genericArguments.Length];
			for (int i = 0; i < this.genericArguments.Length; i++)
			{
				this.genericArgumentES3Types[i] = ES3TypeMgr.GetOrCreateES3Type(this.genericArguments[i], false);
				if (this.genericArgumentES3Types[i] == null || this.genericArgumentES3Types[i].isUnsupported)
				{
					this.isUnsupported = true;
				}
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008EB8 File Offset: 0x000070B8
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

		// Token: 0x0600024B RID: 587 RVA: 0x00008F10 File Offset: 0x00007110
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

		// Token: 0x0400006C RID: 108
		public Type[] genericArguments;

		// Token: 0x0400006D RID: 109
		public ES3Type[] genericArgumentES3Types;

		// Token: 0x0400006E RID: 110
		public ES3Type argumentES3Type;

		// Token: 0x0400006F RID: 111
		public Type genericArgument;

		// Token: 0x04000070 RID: 112
		private ES3Reflection.ES3ReflectedMember hasValueProperty;

		// Token: 0x04000071 RID: 113
		private ES3Reflection.ES3ReflectedMember valueProperty;
	}
}
