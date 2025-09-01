using System;
using System.Collections;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000026 RID: 38
	[Preserve]
	[ES3Properties(new string[]
	{
		"_items",
		"_size",
		"_version"
	})]
	public class ES3Type_ArrayList : ES3ObjectType
	{
		// Token: 0x0600023B RID: 571 RVA: 0x00008A70 File Offset: 0x00006C70
		public ES3Type_ArrayList() : base(typeof(ArrayList))
		{
			ES3Type_ArrayList.Instance = this;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008A88 File Offset: 0x00006C88
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			ArrayList objectContainingField = (ArrayList)obj;
			writer.WritePrivateField("_items", objectContainingField);
			writer.WritePrivateField("_size", objectContainingField);
			writer.WritePrivateField("_version", objectContainingField);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008AC0 File Offset: 0x00006CC0
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			ArrayList objectContainingField = (ArrayList)obj;
			foreach (object obj2 in reader.Properties)
			{
				string a = (string)obj2;
				if (!(a == "_items"))
				{
					if (!(a == "_size"))
					{
						if (!(a == "_version"))
						{
							reader.Skip();
						}
						else
						{
							objectContainingField = (ArrayList)reader.SetPrivateField("_version", reader.Read<int>(), objectContainingField);
						}
					}
					else
					{
						objectContainingField = (ArrayList)reader.SetPrivateField("_size", reader.Read<int>(), objectContainingField);
					}
				}
				else
				{
					objectContainingField = (ArrayList)reader.SetPrivateField("_items", reader.Read<object[]>(), objectContainingField);
				}
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008BA8 File Offset: 0x00006DA8
		protected override object ReadObject<T>(ES3Reader reader)
		{
			ArrayList arrayList = new ArrayList();
			this.ReadObject<T>(reader, arrayList);
			return arrayList;
		}

		// Token: 0x04000069 RID: 105
		public static ES3Type Instance;
	}
}
