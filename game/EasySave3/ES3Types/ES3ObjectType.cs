using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200002A RID: 42
	[Preserve]
	public abstract class ES3ObjectType : ES3Type
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00008F7D File Offset: 0x0000717D
		public ES3ObjectType(Type type) : base(type)
		{
		}

		// Token: 0x0600024D RID: 589
		protected abstract void WriteObject(object obj, ES3Writer writer);

		// Token: 0x0600024E RID: 590
		protected abstract object ReadObject<T>(ES3Reader reader);

		// Token: 0x0600024F RID: 591 RVA: 0x00008F86 File Offset: 0x00007186
		protected virtual void ReadObject<T>(ES3Reader reader, object obj)
		{
			string str = "ReadInto is not supported for type ";
			Type type = this.type;
			throw new NotSupportedException(str + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008FAC File Offset: 0x000071AC
		public override void Write(object obj, ES3Writer writer)
		{
			if (!base.WriteUsingDerivedType(obj, writer))
			{
				Type type = ES3Reflection.BaseType(obj.GetType());
				if (type != typeof(object))
				{
					ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(type, false);
					if (orCreateES3Type != null && (orCreateES3Type.isDictionary || orCreateES3Type.isCollection))
					{
						writer.WriteProperty("_Values", obj, orCreateES3Type);
					}
				}
				this.WriteObject(obj, writer);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009014 File Offset: 0x00007214
		public override object Read<T>(ES3Reader reader)
		{
			string text = base.ReadPropertyName(reader);
			if (text == "__type")
			{
				return ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).Read<T>(reader);
			}
			reader.overridePropertiesName = text;
			return this.ReadObject<T>(reader);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009058 File Offset: 0x00007258
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			for (;;)
			{
				string text = base.ReadPropertyName(reader);
				if (text == "__type")
				{
					break;
				}
				if (text == null)
				{
					return;
				}
				reader.overridePropertiesName = text;
				this.ReadObject<T>(reader, obj);
			}
			ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).ReadInto<T>(reader, obj);
		}
	}
}
