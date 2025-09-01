using System;

namespace System.Drawing.Imaging
{
	/// <summary>Provides properties that get the frame dimensions of an image. Not inheritable.</summary>
	// Token: 0x02000106 RID: 262
	public sealed class FrameDimension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.FrameDimension" /> class using the specified <see langword="Guid" /> structure.</summary>
		/// <param name="guid">A <see langword="Guid" /> structure that contains a GUID for this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</param>
		// Token: 0x06000C73 RID: 3187 RVA: 0x0001D16A File Offset: 0x0001B36A
		public FrameDimension(Guid guid)
		{
			this._guid = guid;
		}

		/// <summary>Gets a globally unique identifier (GUID) that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
		/// <returns>A <see langword="Guid" /> structure that contains a GUID that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0001D179 File Offset: 0x0001B379
		public Guid Guid
		{
			get
			{
				return this._guid;
			}
		}

		/// <summary>Gets the time dimension.</summary>
		/// <returns>The time dimension.</returns>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0001D181 File Offset: 0x0001B381
		public static FrameDimension Time
		{
			get
			{
				return FrameDimension.s_time;
			}
		}

		/// <summary>Gets the resolution dimension.</summary>
		/// <returns>The resolution dimension.</returns>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0001D188 File Offset: 0x0001B388
		public static FrameDimension Resolution
		{
			get
			{
				return FrameDimension.s_resolution;
			}
		}

		/// <summary>Gets the page dimension.</summary>
		/// <returns>The page dimension.</returns>
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0001D18F File Offset: 0x0001B38F
		public static FrameDimension Page
		{
			get
			{
				return FrameDimension.s_page;
			}
		}

		/// <summary>Returns a value that indicates whether the specified object is a <see cref="T:System.Drawing.Imaging.FrameDimension" /> equivalent to this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
		/// <param name="o">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Drawing.Imaging.FrameDimension" /> equivalent to this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000C78 RID: 3192 RVA: 0x0001D198 File Offset: 0x0001B398
		public override bool Equals(object o)
		{
			FrameDimension frameDimension = o as FrameDimension;
			return frameDimension != null && this._guid == frameDimension._guid;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
		/// <returns>The hash code of this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
		// Token: 0x06000C79 RID: 3193 RVA: 0x0001D1C2 File Offset: 0x0001B3C2
		public override int GetHashCode()
		{
			return this._guid.GetHashCode();
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object to a human-readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
		// Token: 0x06000C7A RID: 3194 RVA: 0x0001D1D8 File Offset: 0x0001B3D8
		public override string ToString()
		{
			if (this == FrameDimension.s_time)
			{
				return "Time";
			}
			if (this == FrameDimension.s_resolution)
			{
				return "Resolution";
			}
			if (this == FrameDimension.s_page)
			{
				return "Page";
			}
			string str = "[FrameDimension: ";
			Guid guid = this._guid;
			return str + guid.ToString() + "]";
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0001D232 File Offset: 0x0001B432
		// Note: this type is marked as 'beforefieldinit'.
		static FrameDimension()
		{
		}

		// Token: 0x040009A6 RID: 2470
		private static FrameDimension s_time = new FrameDimension(new Guid("{6aedbd6d-3fb5-418a-83a6-7f45229dc872}"));

		// Token: 0x040009A7 RID: 2471
		private static FrameDimension s_resolution = new FrameDimension(new Guid("{84236f7b-3bd3-428f-8dab-4ea1439ca315}"));

		// Token: 0x040009A8 RID: 2472
		private static FrameDimension s_page = new FrameDimension(new Guid("{7462dc86-6180-4c7e-8e3f-ee7333a7a483}"));

		// Token: 0x040009A9 RID: 2473
		private Guid _guid;
	}
}
