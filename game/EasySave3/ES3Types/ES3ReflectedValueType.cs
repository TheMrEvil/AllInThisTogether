using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200005E RID: 94
	[Preserve]
	internal class ES3ReflectedValueType : ES3Type
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000AB7A File Offset: 0x00008D7A
		public ES3ReflectedValueType(Type type) : base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000AB91 File Offset: 0x00008D91
		public override void Write(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public override object Read<T>(ES3Reader reader)
		{
			object obj = ES3Reflection.CreateInstance(this.type);
			if (obj == null)
			{
				string str = "Cannot create an instance of ";
				Type type = this.type;
				throw new NotSupportedException(str + ((type != null) ? type.ToString() : null) + ". However, you may be able to add support for it using a custom ES3Type file. For more information see: http://docs.moodkie.com/easy-save-3/es3-guides/controlling-serialization-using-es3types/");
			}
			return base.ReadProperties(reader, obj);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000ABE7 File Offset: 0x00008DE7
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			throw new NotSupportedException("Cannot perform self-assigning load on a value type.");
		}
	}
}
