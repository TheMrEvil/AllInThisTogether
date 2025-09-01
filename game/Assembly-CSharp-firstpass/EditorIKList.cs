using System;
using System.Collections.Generic;
using System.Reflection;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x02000004 RID: 4
[ExecuteInEditMode]
public class EditorIKList : MonoBehaviour
{
	// Token: 0x0600000F RID: 15 RVA: 0x00002794 File Offset: 0x00000994
	private void CollectIK()
	{
		this.ClearIK();
		this.ResetTransforms();
		foreach (IK ik in base.GetComponents<IK>())
		{
			this.IKs.Add(ik);
			ik.fixTransforms = true;
			ik.GetIKSolver().Initiate(ik.transform);
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000027EC File Offset: 0x000009EC
	public void ClearIK()
	{
		foreach (IK ik in this.IKs)
		{
			ik.fixTransforms = false;
		}
		this.IKs.Clear();
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002848 File Offset: 0x00000A48
	public void ResetTransforms()
	{
		base.GetComponentInChildren<SkinnedMeshRenderer>();
		Type type = Type.GetType("UnityEditor.AvatarSetupTool, UnityEditor");
		if (type != null)
		{
			MethodInfo method = type.GetMethod("SampleBindPose", BindingFlags.Static | BindingFlags.Public);
			if (method != null)
			{
				method.Invoke(null, new object[]
				{
					base.gameObject
				});
			}
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000028A0 File Offset: 0x00000AA0
	private void Update()
	{
		if (this.IKs == null || this.IKs.Count == 0)
		{
			return;
		}
		foreach (IK ik in this.IKs)
		{
			if (!(ik == null))
			{
				if (ik.fixTransforms)
				{
					ik.GetIKSolver().FixTransforms();
				}
				ik.GetIKSolver().Update();
			}
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000292C File Offset: 0x00000B2C
	private void OnDestroy()
	{
		this.ClearIK();
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002934 File Offset: 0x00000B34
	public EditorIKList()
	{
	}

	// Token: 0x04000020 RID: 32
	public List<IK> IKs = new List<IK>();
}
