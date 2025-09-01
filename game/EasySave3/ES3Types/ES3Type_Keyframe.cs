using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000097 RID: 151
	[Preserve]
	[ES3Properties(new string[]
	{
		"time",
		"value",
		"inTangent",
		"outTangent"
	})]
	public class ES3Type_Keyframe : ES3Type
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00011A59 File Offset: 0x0000FC59
		public ES3Type_Keyframe() : base(typeof(Keyframe))
		{
			ES3Type_Keyframe.Instance = this;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00011A74 File Offset: 0x0000FC74
		public override void Write(object obj, ES3Writer writer)
		{
			Keyframe keyframe = (Keyframe)obj;
			writer.WriteProperty("time", keyframe.time, ES3Type_float.Instance);
			writer.WriteProperty("value", keyframe.value, ES3Type_float.Instance);
			writer.WriteProperty("inTangent", keyframe.inTangent, ES3Type_float.Instance);
			writer.WriteProperty("outTangent", keyframe.outTangent, ES3Type_float.Instance);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011AF8 File Offset: 0x0000FCF8
		public override object Read<T>(ES3Reader reader)
		{
			return new Keyframe(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000EA RID: 234
		public static ES3Type Instance;
	}
}
