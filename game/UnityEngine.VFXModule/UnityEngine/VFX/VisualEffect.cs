using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VisualEffectBindings.h")]
	[NativeHeader("Modules/VFX/Public/VisualEffect.h")]
	[RequireComponent(typeof(Transform))]
	public class VisualEffect : Behaviour
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000AB RID: 171
		// (set) Token: 0x060000AC RID: 172
		public extern bool pause { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AD RID: 173
		// (set) Token: 0x060000AE RID: 174
		public extern float playRate { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AF RID: 175
		// (set) Token: 0x060000B0 RID: 176
		public extern uint startSeed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B1 RID: 177
		// (set) Token: 0x060000B2 RID: 178
		public extern bool resetSeedOnPlay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B3 RID: 179
		// (set) Token: 0x060000B4 RID: 180
		public extern int initialEventID { [FreeFunction(Name = "VisualEffectBindings::GetInitialEventID", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(Name = "VisualEffectBindings::SetInitialEventID", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B5 RID: 181
		// (set) Token: 0x060000B6 RID: 182
		public extern string initialEventName { [FreeFunction(Name = "VisualEffectBindings::GetInitialEventName", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(Name = "VisualEffectBindings::SetInitialEventName", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B7 RID: 183
		public extern bool culled { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B8 RID: 184
		// (set) Token: 0x060000B9 RID: 185
		public extern VisualEffectAsset visualEffectAsset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000BA RID: 186 RVA: 0x00002A50 File Offset: 0x00000C50
		public VFXEventAttribute CreateVFXEventAttribute()
		{
			bool flag = this.visualEffectAsset == null;
			VFXEventAttribute result;
			if (flag)
			{
				result = null;
			}
			else
			{
				VFXEventAttribute vfxeventAttribute = VFXEventAttribute.Internal_InstanciateVFXEventAttribute(this.visualEffectAsset);
				result = vfxeventAttribute;
			}
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002A84 File Offset: 0x00000C84
		private void CheckValidVFXEventAttribute(VFXEventAttribute eventAttribute)
		{
			bool flag = eventAttribute != null && eventAttribute.vfxAsset != this.visualEffectAsset;
			if (flag)
			{
				throw new InvalidOperationException("Invalid VFXEventAttribute provided to VisualEffect. It has been created with another VisualEffectAsset. Use CreateVFXEventAttribute.");
			}
		}

		// Token: 0x060000BC RID: 188
		[FreeFunction(Name = "VisualEffectBindings::SendEventFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SendEventFromScript(int eventNameID, VFXEventAttribute eventAttribute);

		// Token: 0x060000BD RID: 189 RVA: 0x00002AB9 File Offset: 0x00000CB9
		public void SendEvent(int eventNameID, VFXEventAttribute eventAttribute)
		{
			this.CheckValidVFXEventAttribute(eventAttribute);
			this.SendEventFromScript(eventNameID, eventAttribute);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002ACD File Offset: 0x00000CCD
		public void SendEvent(string eventName, VFXEventAttribute eventAttribute)
		{
			this.SendEvent(Shader.PropertyToID(eventName), eventAttribute);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002ADE File Offset: 0x00000CDE
		public void SendEvent(int eventNameID)
		{
			this.SendEventFromScript(eventNameID, null);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002AEA File Offset: 0x00000CEA
		public void SendEvent(string eventName)
		{
			this.SendEvent(Shader.PropertyToID(eventName), null);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002AFB File Offset: 0x00000CFB
		public void Play(VFXEventAttribute eventAttribute)
		{
			this.SendEvent(VisualEffectAsset.PlayEventID, eventAttribute);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002B0B File Offset: 0x00000D0B
		public void Play()
		{
			this.SendEvent(VisualEffectAsset.PlayEventID);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002B1A File Offset: 0x00000D1A
		public void Stop(VFXEventAttribute eventAttribute)
		{
			this.SendEvent(VisualEffectAsset.StopEventID, eventAttribute);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002B2A File Offset: 0x00000D2A
		public void Stop()
		{
			this.SendEvent(VisualEffectAsset.StopEventID);
		}

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reinit();

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AdvanceOneFrame();

		// Token: 0x060000C7 RID: 199
		[FreeFunction(Name = "VisualEffectBindings::ResetOverrideFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetOverride(int nameID);

		// Token: 0x060000C8 RID: 200
		[FreeFunction(Name = "VisualEffectBindings::GetTextureDimensionFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TextureDimension GetTextureDimension(int nameID);

		// Token: 0x060000C9 RID: 201
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<bool>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasBool(int nameID);

		// Token: 0x060000CA RID: 202
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasInt(int nameID);

		// Token: 0x060000CB RID: 203
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<UInt32>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasUInt(int nameID);

		// Token: 0x060000CC RID: 204
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasFloat(int nameID);

		// Token: 0x060000CD RID: 205
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector2f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector2(int nameID);

		// Token: 0x060000CE RID: 206
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector3f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector3(int nameID);

		// Token: 0x060000CF RID: 207
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector4(int nameID);

		// Token: 0x060000D0 RID: 208
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasMatrix4x4(int nameID);

		// Token: 0x060000D1 RID: 209
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Texture*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasTexture(int nameID);

		// Token: 0x060000D2 RID: 210
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasAnimationCurve(int nameID);

		// Token: 0x060000D3 RID: 211
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Gradient*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasGradient(int nameID);

		// Token: 0x060000D4 RID: 212
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Mesh*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasMesh(int nameID);

		// Token: 0x060000D5 RID: 213
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<SkinnedMeshRenderer*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasSkinnedMeshRenderer(int nameID);

		// Token: 0x060000D6 RID: 214
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<GraphicsBuffer*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasGraphicsBuffer(int nameID);

		// Token: 0x060000D7 RID: 215
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<bool>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBool(int nameID, bool b);

		// Token: 0x060000D8 RID: 216
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(int nameID, int i);

		// Token: 0x060000D9 RID: 217
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<UInt32>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetUInt(int nameID, uint i);

		// Token: 0x060000DA RID: 218
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float f);

		// Token: 0x060000DB RID: 219 RVA: 0x00002B39 File Offset: 0x00000D39
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public void SetVector2(int nameID, Vector2 v)
		{
			this.SetVector2_Injected(nameID, ref v);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00002B44 File Offset: 0x00000D44
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public void SetVector3(int nameID, Vector3 v)
		{
			this.SetVector3_Injected(nameID, ref v);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002B4F File Offset: 0x00000D4F
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public void SetVector4(int nameID, Vector4 v)
		{
			this.SetVector4_Injected(nameID, ref v);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002B5A File Offset: 0x00000D5A
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			this.SetMatrix4x4_Injected(nameID, ref v);
		}

		// Token: 0x060000DF RID: 223
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Texture*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int nameID, [NotNull("ArgumentNullException")] Texture t);

		// Token: 0x060000E0 RID: 224
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAnimationCurve(int nameID, [NotNull("ArgumentNullException")] AnimationCurve c);

		// Token: 0x060000E1 RID: 225
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Gradient*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetGradient(int nameID, [NotNull("ArgumentNullException")] Gradient g);

		// Token: 0x060000E2 RID: 226
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Mesh*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMesh(int nameID, [NotNull("ArgumentNullException")] Mesh m);

		// Token: 0x060000E3 RID: 227
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<SkinnedMeshRenderer*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSkinnedMeshRenderer(int nameID, SkinnedMeshRenderer m);

		// Token: 0x060000E4 RID: 228
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<GraphicsBuffer*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetGraphicsBuffer(int nameID, GraphicsBuffer g);

		// Token: 0x060000E5 RID: 229
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<bool>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool(int nameID);

		// Token: 0x060000E6 RID: 230
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetInt(int nameID);

		// Token: 0x060000E7 RID: 231
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<UInt32>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetUInt(int nameID);

		// Token: 0x060000E8 RID: 232
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		// Token: 0x060000E9 RID: 233 RVA: 0x00002B68 File Offset: 0x00000D68
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public Vector2 GetVector2(int nameID)
		{
			Vector2 result;
			this.GetVector2_Injected(nameID, out result);
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002B80 File Offset: 0x00000D80
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public Vector3 GetVector3(int nameID)
		{
			Vector3 result;
			this.GetVector3_Injected(nameID, out result);
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002B98 File Offset: 0x00000D98
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public Vector4 GetVector4(int nameID)
		{
			Vector4 result;
			this.GetVector4_Injected(nameID, out result);
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002BB0 File Offset: 0x00000DB0
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			Matrix4x4 result;
			this.GetMatrix4x4_Injected(nameID, out result);
			return result;
		}

		// Token: 0x060000ED RID: 237
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Texture*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetTexture(int nameID);

		// Token: 0x060000EE RID: 238
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Mesh*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Mesh GetMesh(int nameID);

		// Token: 0x060000EF RID: 239
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<SkinnedMeshRenderer*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern SkinnedMeshRenderer GetSkinnedMeshRenderer(int nameID);

		// Token: 0x060000F0 RID: 240
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<GraphicsBuffer*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern GraphicsBuffer GetGraphicsBuffer(int nameID);

		// Token: 0x060000F1 RID: 241 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			this.Internal_GetGradient(nameID, gradient);
			return gradient;
		}

		// Token: 0x060000F2 RID: 242
		[FreeFunction(Name = "VisualEffectBindings::Internal_GetGradientFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetGradient(int nameID, Gradient gradient);

		// Token: 0x060000F3 RID: 243 RVA: 0x00002BEC File Offset: 0x00000DEC
		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.Internal_GetAnimationCurve(nameID, animationCurve);
			return animationCurve;
		}

		// Token: 0x060000F4 RID: 244
		[FreeFunction(Name = "VisualEffectBindings::Internal_GetAnimationCurveFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetAnimationCurve(int nameID, AnimationCurve curve);

		// Token: 0x060000F5 RID: 245
		[FreeFunction(Name = "VisualEffectBindings::HasSystemFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasSystem(int nameID);

		// Token: 0x060000F6 RID: 246 RVA: 0x00002C10 File Offset: 0x00000E10
		[FreeFunction(Name = "VisualEffectBindings::GetParticleSystemInfo", HasExplicitThis = true, ThrowsException = true)]
		public VFXParticleSystemInfo GetParticleSystemInfo(int nameID)
		{
			VFXParticleSystemInfo result;
			this.GetParticleSystemInfo_Injected(nameID, out result);
			return result;
		}

		// Token: 0x060000F7 RID: 247
		[FreeFunction(Name = "VisualEffectBindings::GetSpawnSystemInfo", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSpawnSystemInfo(int nameID, IntPtr spawnerState);

		// Token: 0x060000F8 RID: 248
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasAnySystemAwake();

		// Token: 0x060000F9 RID: 249 RVA: 0x00002C28 File Offset: 0x00000E28
		[FreeFunction(Name = "VisualEffectBindings::GetComputedBounds", HasExplicitThis = true)]
		internal Bounds GetComputedBounds(int nameID)
		{
			Bounds result;
			this.GetComputedBounds_Injected(nameID, out result);
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002C40 File Offset: 0x00000E40
		[FreeFunction(Name = "VisualEffectBindings::GetCurrentPadding", HasExplicitThis = true)]
		internal Vector3 GetCurrentPadding(int nameID)
		{
			Vector3 result;
			this.GetCurrentPadding_Injected(nameID, out result);
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002C58 File Offset: 0x00000E58
		public void GetSpawnSystemInfo(int nameID, VFXSpawnerState spawnState)
		{
			bool flag = spawnState == null;
			if (flag)
			{
				throw new NullReferenceException("GetSpawnSystemInfo expects a non null VFXSpawnerState.");
			}
			IntPtr ptr = spawnState.GetPtr();
			bool flag2 = ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new NullReferenceException("GetSpawnSystemInfo use an unexpected not owned VFXSpawnerState.");
			}
			this.GetSpawnSystemInfo(nameID, ptr);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public VFXSpawnerState GetSpawnSystemInfo(int nameID)
		{
			VFXSpawnerState vfxspawnerState = new VFXSpawnerState();
			this.GetSpawnSystemInfo(nameID, vfxspawnerState);
			return vfxspawnerState;
		}

		// Token: 0x060000FD RID: 253
		[FreeFunction(Name = "VisualEffectBindings::GetSystemNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSystemNames([NotNull("ArgumentNullException")] List<string> names);

		// Token: 0x060000FE RID: 254
		[FreeFunction(Name = "VisualEffectBindings::GetParticleSystemNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetParticleSystemNames([NotNull("ArgumentNullException")] List<string> names);

		// Token: 0x060000FF RID: 255
		[FreeFunction(Name = "VisualEffectBindings::GetOutputEventNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetOutputEventNames([NotNull("ArgumentNullException")] List<string> names);

		// Token: 0x06000100 RID: 256
		[FreeFunction(Name = "VisualEffectBindings::GetSpawnSystemNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSpawnSystemNames([NotNull("ArgumentNullException")] List<string> names);

		// Token: 0x06000101 RID: 257 RVA: 0x00002CC6 File Offset: 0x00000EC6
		public void ResetOverride(string name)
		{
			this.ResetOverride(Shader.PropertyToID(name));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public bool HasInt(string name)
		{
			return this.HasInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public bool HasUInt(string name)
		{
			return this.HasUInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00002D18 File Offset: 0x00000F18
		public bool HasFloat(string name)
		{
			return this.HasFloat(Shader.PropertyToID(name));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00002D38 File Offset: 0x00000F38
		public bool HasVector2(string name)
		{
			return this.HasVector2(Shader.PropertyToID(name));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002D58 File Offset: 0x00000F58
		public bool HasVector3(string name)
		{
			return this.HasVector3(Shader.PropertyToID(name));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002D78 File Offset: 0x00000F78
		public bool HasVector4(string name)
		{
			return this.HasVector4(Shader.PropertyToID(name));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00002D98 File Offset: 0x00000F98
		public bool HasMatrix4x4(string name)
		{
			return this.HasMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public bool HasTexture(string name)
		{
			return this.HasTexture(Shader.PropertyToID(name));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public TextureDimension GetTextureDimension(string name)
		{
			return this.GetTextureDimension(Shader.PropertyToID(name));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public bool HasAnimationCurve(string name)
		{
			return this.HasAnimationCurve(Shader.PropertyToID(name));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002E18 File Offset: 0x00001018
		public bool HasGradient(string name)
		{
			return this.HasGradient(Shader.PropertyToID(name));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00002E38 File Offset: 0x00001038
		public bool HasMesh(string name)
		{
			return this.HasMesh(Shader.PropertyToID(name));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002E58 File Offset: 0x00001058
		public bool HasSkinnedMeshRenderer(string name)
		{
			return this.HasSkinnedMeshRenderer(Shader.PropertyToID(name));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00002E78 File Offset: 0x00001078
		public bool HasGraphicsBuffer(string name)
		{
			return this.HasGraphicsBuffer(Shader.PropertyToID(name));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00002E98 File Offset: 0x00001098
		public bool HasBool(string name)
		{
			return this.HasBool(Shader.PropertyToID(name));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00002EB6 File Offset: 0x000010B6
		public void SetInt(string name, int i)
		{
			this.SetInt(Shader.PropertyToID(name), i);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00002EC7 File Offset: 0x000010C7
		public void SetUInt(string name, uint i)
		{
			this.SetUInt(Shader.PropertyToID(name), i);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00002ED8 File Offset: 0x000010D8
		public void SetFloat(string name, float f)
		{
			this.SetFloat(Shader.PropertyToID(name), f);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002EE9 File Offset: 0x000010E9
		public void SetVector2(string name, Vector2 v)
		{
			this.SetVector2(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002EFA File Offset: 0x000010FA
		public void SetVector3(string name, Vector3 v)
		{
			this.SetVector3(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002F0B File Offset: 0x0000110B
		public void SetVector4(string name, Vector4 v)
		{
			this.SetVector4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002F1C File Offset: 0x0000111C
		public void SetMatrix4x4(string name, Matrix4x4 v)
		{
			this.SetMatrix4x4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002F2D File Offset: 0x0000112D
		public void SetTexture(string name, Texture t)
		{
			this.SetTexture(Shader.PropertyToID(name), t);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00002F3E File Offset: 0x0000113E
		public void SetAnimationCurve(string name, AnimationCurve c)
		{
			this.SetAnimationCurve(Shader.PropertyToID(name), c);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00002F4F File Offset: 0x0000114F
		public void SetGradient(string name, Gradient g)
		{
			this.SetGradient(Shader.PropertyToID(name), g);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00002F60 File Offset: 0x00001160
		public void SetMesh(string name, Mesh m)
		{
			this.SetMesh(Shader.PropertyToID(name), m);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00002F71 File Offset: 0x00001171
		public void SetSkinnedMeshRenderer(string name, SkinnedMeshRenderer m)
		{
			this.SetSkinnedMeshRenderer(Shader.PropertyToID(name), m);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00002F82 File Offset: 0x00001182
		public void SetGraphicsBuffer(string name, GraphicsBuffer g)
		{
			this.SetGraphicsBuffer(Shader.PropertyToID(name), g);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00002F93 File Offset: 0x00001193
		public void SetBool(string name, bool b)
		{
			this.SetBool(Shader.PropertyToID(name), b);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00002FA4 File Offset: 0x000011A4
		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00002FC4 File Offset: 0x000011C4
		public uint GetUInt(string name)
		{
			return this.GetUInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00002FE4 File Offset: 0x000011E4
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003004 File Offset: 0x00001204
		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003024 File Offset: 0x00001224
		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003044 File Offset: 0x00001244
		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003064 File Offset: 0x00001264
		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003084 File Offset: 0x00001284
		public Texture GetTexture(string name)
		{
			return this.GetTexture(Shader.PropertyToID(name));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000030A4 File Offset: 0x000012A4
		public Mesh GetMesh(string name)
		{
			return this.GetMesh(Shader.PropertyToID(name));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000030C4 File Offset: 0x000012C4
		public SkinnedMeshRenderer GetSkinnedMeshRenderer(string name)
		{
			return this.GetSkinnedMeshRenderer(Shader.PropertyToID(name));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000030E4 File Offset: 0x000012E4
		internal GraphicsBuffer GetGraphicsBuffer(string name)
		{
			return this.GetGraphicsBuffer(Shader.PropertyToID(name));
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003104 File Offset: 0x00001304
		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003124 File Offset: 0x00001324
		public AnimationCurve GetAnimationCurve(string name)
		{
			return this.GetAnimationCurve(Shader.PropertyToID(name));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003144 File Offset: 0x00001344
		public Gradient GetGradient(string name)
		{
			return this.GetGradient(Shader.PropertyToID(name));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003164 File Offset: 0x00001364
		public bool HasSystem(string name)
		{
			return this.HasSystem(Shader.PropertyToID(name));
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003184 File Offset: 0x00001384
		public VFXParticleSystemInfo GetParticleSystemInfo(string name)
		{
			return this.GetParticleSystemInfo(Shader.PropertyToID(name));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000031A4 File Offset: 0x000013A4
		public VFXSpawnerState GetSpawnSystemInfo(string name)
		{
			return this.GetSpawnSystemInfo(Shader.PropertyToID(name));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000031C4 File Offset: 0x000013C4
		internal Bounds GetComputedBounds(string name)
		{
			return this.GetComputedBounds(Shader.PropertyToID(name));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000031E4 File Offset: 0x000013E4
		internal Vector3 GetCurrentPadding(string name)
		{
			return this.GetCurrentPadding(Shader.PropertyToID(name));
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000132 RID: 306
		public extern int aliveParticleCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000133 RID: 307
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Simulate(float stepDeltaTime, uint stepCount = 1U);

		// Token: 0x06000134 RID: 308 RVA: 0x00003204 File Offset: 0x00001404
		[RequiredByNativeCode]
		private static VFXEventAttribute InvokeGetCachedEventAttributeForOutputEvent_Internal(VisualEffect source)
		{
			bool flag = source.outputEventReceived == null;
			VFXEventAttribute result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = source.m_cachedEventAttribute == null;
				if (flag2)
				{
					source.m_cachedEventAttribute = source.CreateVFXEventAttribute();
				}
				result = source.m_cachedEventAttribute;
			}
			return result;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003248 File Offset: 0x00001448
		[RequiredByNativeCode]
		private static void InvokeOutputEventReceived_Internal(VisualEffect source, int eventNameId)
		{
			VFXOutputEventArgs obj = new VFXOutputEventArgs(eventNameId, source.m_cachedEventAttribute);
			source.outputEventReceived(obj);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003271 File Offset: 0x00001471
		public VisualEffect()
		{
		}

		// Token: 0x06000137 RID: 311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector2_Injected(int nameID, ref Vector2 v);

		// Token: 0x06000138 RID: 312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector3_Injected(int nameID, ref Vector3 v);

		// Token: 0x06000139 RID: 313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector4_Injected(int nameID, ref Vector4 v);

		// Token: 0x0600013A RID: 314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix4x4_Injected(int nameID, ref Matrix4x4 v);

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		// Token: 0x0600013C RID: 316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		// Token: 0x0600013D RID: 317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);

		// Token: 0x0600013F RID: 319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetParticleSystemInfo_Injected(int nameID, out VFXParticleSystemInfo ret);

		// Token: 0x06000140 RID: 320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetComputedBounds_Injected(int nameID, out Bounds ret);

		// Token: 0x06000141 RID: 321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCurrentPadding_Injected(int nameID, out Vector3 ret);

		// Token: 0x040000FC RID: 252
		private VFXEventAttribute m_cachedEventAttribute;

		// Token: 0x040000FD RID: 253
		public Action<VFXOutputEventArgs> outputEventReceived;
	}
}
