using System;
using UnityEngine;

namespace UnityEditor.Rendering.BuiltIn.ShaderGraph
{
	// Token: 0x02000074 RID: 116
	internal static class MaterialAccess
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x0001BA7E File Offset: 0x00019C7E
		internal static int ReadMaterialRawRenderQueue(Material mat)
		{
			return mat.rawRenderQueue;
		}
	}
}
