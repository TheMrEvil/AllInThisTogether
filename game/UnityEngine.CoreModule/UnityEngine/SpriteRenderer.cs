using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000265 RID: 613
	[RequireComponent(typeof(Transform))]
	[NativeType("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	public sealed class SpriteRenderer : Renderer
	{
		// Token: 0x06001AC1 RID: 6849 RVA: 0x0002B068 File Offset: 0x00029268
		public void RegisterSpriteChangeCallback(UnityAction<SpriteRenderer> callback)
		{
			bool flag = this.m_SpriteChangeEvent == null;
			if (flag)
			{
				this.m_SpriteChangeEvent = new UnityEvent<SpriteRenderer>();
			}
			this.m_SpriteChangeEvent.AddListener(callback);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0002B09C File Offset: 0x0002929C
		public void UnregisterSpriteChangeCallback(UnityAction<SpriteRenderer> callback)
		{
			bool flag = this.m_SpriteChangeEvent != null;
			if (flag)
			{
				this.m_SpriteChangeEvent.RemoveListener(callback);
			}
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0002B0C4 File Offset: 0x000292C4
		[RequiredByNativeCode]
		private void InvokeSpriteChanged()
		{
			try
			{
				UnityEvent<SpriteRenderer> spriteChangeEvent = this.m_SpriteChangeEvent;
				if (spriteChangeEvent != null)
				{
					spriteChangeEvent.Invoke(this);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception, this);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001AC4 RID: 6852
		internal extern bool shouldSupportTiling { [NativeMethod("ShouldSupportTiling")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001AC5 RID: 6853
		// (set) Token: 0x06001AC6 RID: 6854
		public extern Sprite sprite { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001AC7 RID: 6855
		// (set) Token: 0x06001AC8 RID: 6856
		public extern SpriteDrawMode drawMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0002B108 File Offset: 0x00029308
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x0002B11E File Offset: 0x0002931E
		public Vector2 size
		{
			get
			{
				Vector2 result;
				this.get_size_Injected(out result);
				return result;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001ACB RID: 6859
		// (set) Token: 0x06001ACC RID: 6860
		public extern float adaptiveModeThreshold { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001ACD RID: 6861
		// (set) Token: 0x06001ACE RID: 6862
		public extern SpriteTileMode tileMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0002B128 File Offset: 0x00029328
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x0002B13E File Offset: 0x0002933E
		public Color color
		{
			get
			{
				Color result;
				this.get_color_Injected(out result);
				return result;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001AD1 RID: 6865
		// (set) Token: 0x06001AD2 RID: 6866
		public extern SpriteMaskInteraction maskInteraction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001AD3 RID: 6867
		// (set) Token: 0x06001AD4 RID: 6868
		public extern bool flipX { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001AD5 RID: 6869
		// (set) Token: 0x06001AD6 RID: 6870
		public extern bool flipY { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001AD7 RID: 6871
		// (set) Token: 0x06001AD8 RID: 6872
		public extern SpriteSortPoint spriteSortPoint { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0002B148 File Offset: 0x00029348
		[NativeMethod(Name = "GetSpriteBounds")]
		internal Bounds Internal_GetSpriteBounds(SpriteDrawMode mode)
		{
			Bounds result;
			this.Internal_GetSpriteBounds_Injected(mode, out result);
			return result;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0002B160 File Offset: 0x00029360
		internal Bounds GetSpriteBounds()
		{
			return this.Internal_GetSpriteBounds(this.drawMode);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0000BF29 File Offset: 0x0000A129
		public SpriteRenderer()
		{
		}

		// Token: 0x06001ADC RID: 6876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector2 ret);

		// Token: 0x06001ADD RID: 6877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector2 value);

		// Token: 0x06001ADE RID: 6878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x06001ADF RID: 6879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_color_Injected(ref Color value);

		// Token: 0x06001AE0 RID: 6880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetSpriteBounds_Injected(SpriteDrawMode mode, out Bounds ret);

		// Token: 0x040008CD RID: 2253
		private UnityEvent<SpriteRenderer> m_SpriteChangeEvent;
	}
}
