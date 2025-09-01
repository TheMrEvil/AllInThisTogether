using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000076 RID: 118
	[Preserve]
	[ES3Properties(new string[]
	{
		"keys",
		"preWrapMode",
		"postWrapMode"
	})]
	public class ES3Type_AnimationCurve : ES3Type
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		public ES3Type_AnimationCurve() : base(typeof(AnimationCurve))
		{
			ES3Type_AnimationCurve.Instance = this;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000F2E4 File Offset: 0x0000D4E4
		public override void Write(object obj, ES3Writer writer)
		{
			AnimationCurve animationCurve = (AnimationCurve)obj;
			writer.WriteProperty("keys", animationCurve.keys, ES3Type_KeyframeArray.Instance);
			writer.WriteProperty("preWrapMode", animationCurve.preWrapMode);
			writer.WriteProperty("postWrapMode", animationCurve.postWrapMode);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000F33C File Offset: 0x0000D53C
		public override object Read<T>(ES3Reader reader)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.ReadInto<T>(reader, animationCurve);
			return animationCurve;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000F358 File Offset: 0x0000D558
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			AnimationCurve animationCurve = (AnimationCurve)obj;
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (!(a == "keys"))
				{
					if (!(a == "preWrapMode"))
					{
						if (!(a == "postWrapMode"))
						{
							reader.Skip();
						}
						else
						{
							animationCurve.postWrapMode = reader.Read<WrapMode>();
						}
					}
					else
					{
						animationCurve.preWrapMode = reader.Read<WrapMode>();
					}
				}
				else
				{
					animationCurve.keys = reader.Read<Keyframe[]>();
				}
			}
		}

		// Token: 0x040000C6 RID: 198
		public static ES3Type Instance;
	}
}
