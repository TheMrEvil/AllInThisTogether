using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A3 RID: 163
	[Preserve]
	[ES3Properties(new string[]
	{
		"mode",
		"curveMultiplier",
		"curveMax",
		"curveMin",
		"constantMax",
		"constantMin",
		"constant",
		"curve"
	})]
	public class ES3Type_MinMaxCurve : ES3Type
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00014714 File Offset: 0x00012914
		public ES3Type_MinMaxCurve() : base(typeof(ParticleSystem.MinMaxCurve))
		{
			ES3Type_MinMaxCurve.Instance = this;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001472C File Offset: 0x0001292C
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.MinMaxCurve minMaxCurve = (ParticleSystem.MinMaxCurve)obj;
			writer.WriteProperty("mode", minMaxCurve.mode);
			writer.WriteProperty("curveMultiplier", minMaxCurve.curveMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("curveMax", minMaxCurve.curveMax, ES3Type_AnimationCurve.Instance);
			writer.WriteProperty("curveMin", minMaxCurve.curveMin, ES3Type_AnimationCurve.Instance);
			writer.WriteProperty("constantMax", minMaxCurve.constantMax, ES3Type_float.Instance);
			writer.WriteProperty("constantMin", minMaxCurve.constantMin, ES3Type_float.Instance);
			writer.WriteProperty("constant", minMaxCurve.constant, ES3Type_float.Instance);
			writer.WriteProperty("curve", minMaxCurve.curve, ES3Type_AnimationCurve.Instance);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001480C File Offset: 0x00012A0C
		[Preserve]
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.MinMaxCurve minMaxCurve = default(ParticleSystem.MinMaxCurve);
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 3569100738U)
				{
					if (num <= 740975083U)
					{
						if (num != 110225957U)
						{
							if (num == 740975083U)
							{
								if (text == "curveMultiplier")
								{
									minMaxCurve.curveMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "constant")
						{
							minMaxCurve.constant = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num != 2570585620U)
					{
						if (num == 3569100738U)
						{
							if (text == "curveMax")
							{
								minMaxCurve.curveMax = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "curve")
					{
						minMaxCurve.curve = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
						continue;
					}
				}
				else if (num <= 3735493832U)
				{
					if (num != 3713335191U)
					{
						if (num == 3735493832U)
						{
							if (text == "curveMin")
							{
								minMaxCurve.curveMin = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "constantMin")
					{
						minMaxCurve.constantMin = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 3949501785U)
				{
					if (num == 3966689298U)
					{
						if (text == "mode")
						{
							minMaxCurve.mode = reader.Read<ParticleSystemCurveMode>();
							continue;
						}
					}
				}
				else if (text == "constantMax")
				{
					minMaxCurve.constantMax = reader.Read<float>(ES3Type_float.Instance);
					continue;
				}
				reader.Skip();
			}
			return minMaxCurve;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00014A00 File Offset: 0x00012C00
		[Preserve]
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.MinMaxCurve minMaxCurve = (ParticleSystem.MinMaxCurve)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 3569100738U)
				{
					if (num <= 740975083U)
					{
						if (num != 110225957U)
						{
							if (num == 740975083U)
							{
								if (text == "curveMultiplier")
								{
									minMaxCurve.curveMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "constant")
						{
							minMaxCurve.constant = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num != 2570585620U)
					{
						if (num == 3569100738U)
						{
							if (text == "curveMax")
							{
								minMaxCurve.curveMax = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "curve")
					{
						minMaxCurve.curve = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
						continue;
					}
				}
				else if (num <= 3735493832U)
				{
					if (num != 3713335191U)
					{
						if (num == 3735493832U)
						{
							if (text == "curveMin")
							{
								minMaxCurve.curveMin = reader.Read<AnimationCurve>(ES3Type_AnimationCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "constantMin")
					{
						minMaxCurve.constantMin = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 3949501785U)
				{
					if (num == 3966689298U)
					{
						if (text == "mode")
						{
							minMaxCurve.mode = reader.Read<ParticleSystemCurveMode>();
							continue;
						}
					}
				}
				else if (text == "constantMax")
				{
					minMaxCurve.constantMax = reader.Read<float>(ES3Type_float.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000F6 RID: 246
		public static ES3Type Instance;
	}
}
