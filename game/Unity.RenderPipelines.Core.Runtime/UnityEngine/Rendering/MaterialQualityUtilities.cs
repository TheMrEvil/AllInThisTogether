using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AD RID: 173
	[MovedFrom("Utilities")]
	public static class MaterialQualityUtilities
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001BA94 File Offset: 0x00019C94
		public static MaterialQuality GetHighestQuality(this MaterialQuality levels)
		{
			for (int i = MaterialQualityUtilities.Keywords.Length - 1; i >= 0; i--)
			{
				MaterialQuality materialQuality = (MaterialQuality)(1 << i);
				if ((levels & materialQuality) != (MaterialQuality)0)
				{
					return materialQuality;
				}
			}
			return (MaterialQuality)0;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001BAC4 File Offset: 0x00019CC4
		public static MaterialQuality GetClosestQuality(this MaterialQuality availableLevels, MaterialQuality requestedLevel)
		{
			if (availableLevels == (MaterialQuality)0)
			{
				return MaterialQuality.Low;
			}
			int num = requestedLevel.ToFirstIndex();
			MaterialQuality materialQuality = (MaterialQuality)0;
			for (int i = num; i >= 0; i--)
			{
				MaterialQuality materialQuality2 = MaterialQualityUtilities.FromIndex(i);
				if ((materialQuality2 & availableLevels) != (MaterialQuality)0)
				{
					materialQuality = materialQuality2;
					break;
				}
			}
			if (materialQuality != (MaterialQuality)0)
			{
				return materialQuality;
			}
			for (int j = num + 1; j < MaterialQualityUtilities.Keywords.Length; j++)
			{
				MaterialQuality materialQuality3 = MaterialQualityUtilities.FromIndex(j);
				Math.Abs(requestedLevel - materialQuality3);
				if ((materialQuality3 & availableLevels) != (MaterialQuality)0)
				{
					materialQuality = materialQuality3;
					break;
				}
			}
			return materialQuality;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001BB38 File Offset: 0x00019D38
		public static void SetGlobalShaderKeywords(this MaterialQuality level)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					Shader.EnableKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
				else
				{
					Shader.DisableKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001BB7C File Offset: 0x00019D7C
		public static void SetGlobalShaderKeywords(this MaterialQuality level, CommandBuffer cmd)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					cmd.EnableShaderKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
				else
				{
					cmd.DisableShaderKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001BBC4 File Offset: 0x00019DC4
		public static int ToFirstIndex(this MaterialQuality level)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		public static MaterialQuality FromIndex(int index)
		{
			return (MaterialQuality)(1 << index);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001BBF8 File Offset: 0x00019DF8
		// Note: this type is marked as 'beforefieldinit'.
		static MaterialQualityUtilities()
		{
		}

		// Token: 0x0400037A RID: 890
		public static string[] KeywordNames = new string[]
		{
			"MATERIAL_QUALITY_LOW",
			"MATERIAL_QUALITY_MEDIUM",
			"MATERIAL_QUALITY_HIGH"
		};

		// Token: 0x0400037B RID: 891
		public static string[] EnumNames = Enum.GetNames(typeof(MaterialQuality));

		// Token: 0x0400037C RID: 892
		public static ShaderKeyword[] Keywords = new ShaderKeyword[]
		{
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[0]),
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[1]),
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[2])
		};
	}
}
