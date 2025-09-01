using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000032 RID: 50
	[Preserve]
	[ES3Properties(new string[]
	{

	})]
	public class ES3Type_Type : ES3Type
	{
		// Token: 0x06000277 RID: 631 RVA: 0x00009BB5 File Offset: 0x00007DB5
		public ES3Type_Type() : base(typeof(Type))
		{
			ES3Type_Type.Instance = this;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009BD0 File Offset: 0x00007DD0
		public override void Write(object obj, ES3Writer writer)
		{
			Type type = (Type)obj;
			writer.WriteProperty("assemblyQualifiedName", type.AssemblyQualifiedName);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009BF5 File Offset: 0x00007DF5
		public override object Read<T>(ES3Reader reader)
		{
			return Type.GetType(reader.ReadProperty<string>());
		}

		// Token: 0x04000086 RID: 134
		public static ES3Type Instance;
	}
}
