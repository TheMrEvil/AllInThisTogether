using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000079 RID: 121
	[Preserve]
	[ES3Properties(new string[]
	{
		"boneIndex0",
		"boneIndex1",
		"boneIndex2",
		"boneIndex3",
		"weight0",
		"weight1",
		"weight2",
		"weight3"
	})]
	public class ES3Type_BoneWeight : ES3Type
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000F635 File Offset: 0x0000D835
		public ES3Type_BoneWeight() : base(typeof(BoneWeight))
		{
			ES3Type_BoneWeight.Instance = this;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F650 File Offset: 0x0000D850
		public override void Write(object obj, ES3Writer writer)
		{
			BoneWeight boneWeight = (BoneWeight)obj;
			writer.WriteProperty("boneIndex0", boneWeight.boneIndex0, ES3Type_int.Instance);
			writer.WriteProperty("boneIndex1", boneWeight.boneIndex1, ES3Type_int.Instance);
			writer.WriteProperty("boneIndex2", boneWeight.boneIndex2, ES3Type_int.Instance);
			writer.WriteProperty("boneIndex3", boneWeight.boneIndex3, ES3Type_int.Instance);
			writer.WriteProperty("weight0", boneWeight.weight0, ES3Type_float.Instance);
			writer.WriteProperty("weight1", boneWeight.weight1, ES3Type_float.Instance);
			writer.WriteProperty("weight2", boneWeight.weight2, ES3Type_float.Instance);
			writer.WriteProperty("weight3", boneWeight.weight3, ES3Type_float.Instance);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000F744 File Offset: 0x0000D944
		public override object Read<T>(ES3Reader reader)
		{
			return new BoneWeight
			{
				boneIndex0 = reader.ReadProperty<int>(ES3Type_int.Instance),
				boneIndex1 = reader.ReadProperty<int>(ES3Type_int.Instance),
				boneIndex2 = reader.ReadProperty<int>(ES3Type_int.Instance),
				boneIndex3 = reader.ReadProperty<int>(ES3Type_int.Instance),
				weight0 = reader.ReadProperty<float>(ES3Type_float.Instance),
				weight1 = reader.ReadProperty<float>(ES3Type_float.Instance),
				weight2 = reader.ReadProperty<float>(ES3Type_float.Instance),
				weight3 = reader.ReadProperty<float>(ES3Type_float.Instance)
			};
		}

		// Token: 0x040000C9 RID: 201
		public static ES3Type Instance;
	}
}
