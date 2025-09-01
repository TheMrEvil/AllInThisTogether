using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000025 RID: 37
	[Preserve]
	public class ES3TupleType : ES3Type
	{
		// Token: 0x06000237 RID: 567 RVA: 0x000088F4 File Offset: 0x00006AF4
		public ES3TupleType(Type type) : base(type)
		{
			this.types = ES3Reflection.GetElementTypes(type);
			this.es3Types = new ES3Type[this.types.Length];
			for (int i = 0; i < this.types.Length; i++)
			{
				this.es3Types[i] = ES3TypeMgr.GetOrCreateES3Type(this.types[i], false);
				if (this.es3Types[i] == null)
				{
					this.isUnsupported = true;
				}
			}
			this.isTuple = true;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008968 File Offset: 0x00006B68
		public override void Write(object obj, ES3Writer writer)
		{
			this.Write(obj, writer, writer.settings.memberReferenceMode);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008980 File Offset: 0x00006B80
		public void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode)
		{
			if (obj == null)
			{
				writer.WriteNull();
				return;
			}
			writer.StartWriteCollection();
			for (int i = 0; i < this.es3Types.Length; i++)
			{
				object value = ES3Reflection.GetProperty(this.type, "Item" + (i + 1).ToString()).GetValue(obj);
				writer.StartWriteCollectionItem(i);
				writer.Write(value, this.es3Types[i], memberReferenceMode);
				writer.EndWriteCollectionItem(i);
			}
			writer.EndWriteCollection();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000089FC File Offset: 0x00006BFC
		public override object Read<T>(ES3Reader reader)
		{
			object[] array = new object[this.types.Length];
			if (reader.StartReadCollection())
			{
				return null;
			}
			for (int i = 0; i < this.types.Length; i++)
			{
				reader.StartReadCollectionItem();
				array[i] = reader.Read<object>(this.es3Types[i]);
				reader.EndReadCollectionItem();
			}
			reader.EndReadCollection();
			return this.type.GetConstructor(this.types).Invoke(array);
		}

		// Token: 0x04000065 RID: 101
		public ES3Type[] es3Types;

		// Token: 0x04000066 RID: 102
		public Type[] types;

		// Token: 0x04000067 RID: 103
		protected ES3Reflection.ES3ReflectedMethod readMethod;

		// Token: 0x04000068 RID: 104
		protected ES3Reflection.ES3ReflectedMethod readIntoMethod;
	}
}
