using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a 5 x 5 matrix that contains the coordinates for the RGBAW space. Several methods of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color matrix. This class cannot be inherited.</summary>
	// Token: 0x020000FA RID: 250
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ColorMatrix
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMatrix" /> class.</summary>
		// Token: 0x06000C10 RID: 3088 RVA: 0x0001BC7A File Offset: 0x00019E7A
		public ColorMatrix()
		{
			this._matrix00 = 1f;
			this._matrix11 = 1f;
			this._matrix22 = 1f;
			this._matrix33 = 1f;
			this._matrix44 = 1f;
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the 0 row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0001BCB9 File Offset: 0x00019EB9
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x0001BCC1 File Offset: 0x00019EC1
		public float Matrix00
		{
			get
			{
				return this._matrix00;
			}
			set
			{
				this._matrix00 = value;
			}
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the 0 row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" /> .</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0001BCCA File Offset: 0x00019ECA
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x0001BCD2 File Offset: 0x00019ED2
		public float Matrix01
		{
			get
			{
				return this._matrix01;
			}
			set
			{
				this._matrix01 = value;
			}
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the 0 row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0001BCDB File Offset: 0x00019EDB
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x0001BCE3 File Offset: 0x00019EE3
		public float Matrix02
		{
			get
			{
				return this._matrix02;
			}
			set
			{
				this._matrix02 = value;
			}
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
		/// <returns>The element at the 0 row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0001BCEC File Offset: 0x00019EEC
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x0001BCF4 File Offset: 0x00019EF4
		public float Matrix03
		{
			get
			{
				return this._matrix03;
			}
			set
			{
				this._matrix03 = value;
			}
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the 0 row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0001BCFD File Offset: 0x00019EFD
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x0001BD05 File Offset: 0x00019F05
		public float Matrix04
		{
			get
			{
				return this._matrix04;
			}
			set
			{
				this._matrix04 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the first row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0001BD0E File Offset: 0x00019F0E
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x0001BD16 File Offset: 0x00019F16
		public float Matrix10
		{
			get
			{
				return this._matrix10;
			}
			set
			{
				this._matrix10 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the first row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0001BD1F File Offset: 0x00019F1F
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0001BD27 File Offset: 0x00019F27
		public float Matrix11
		{
			get
			{
				return this._matrix11;
			}
			set
			{
				this._matrix11 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the first row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0001BD30 File Offset: 0x00019F30
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0001BD38 File Offset: 0x00019F38
		public float Matrix12
		{
			get
			{
				return this._matrix12;
			}
			set
			{
				this._matrix12 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
		/// <returns>The element at the first row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0001BD41 File Offset: 0x00019F41
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0001BD49 File Offset: 0x00019F49
		public float Matrix13
		{
			get
			{
				return this._matrix13;
			}
			set
			{
				this._matrix13 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the first row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0001BD52 File Offset: 0x00019F52
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0001BD5A File Offset: 0x00019F5A
		public float Matrix14
		{
			get
			{
				return this._matrix14;
			}
			set
			{
				this._matrix14 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0001BD63 File Offset: 0x00019F63
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0001BD6B File Offset: 0x00019F6B
		public float Matrix20
		{
			get
			{
				return this._matrix20;
			}
			set
			{
				this._matrix20 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0001BD74 File Offset: 0x00019F74
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0001BD7C File Offset: 0x00019F7C
		public float Matrix21
		{
			get
			{
				return this._matrix21;
			}
			set
			{
				this._matrix21 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0001BD85 File Offset: 0x00019F85
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0001BD8D File Offset: 0x00019F8D
		public float Matrix22
		{
			get
			{
				return this._matrix22;
			}
			set
			{
				this._matrix22 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0001BD96 File Offset: 0x00019F96
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0001BD9E File Offset: 0x00019F9E
		public float Matrix23
		{
			get
			{
				return this._matrix23;
			}
			set
			{
				this._matrix23 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0001BDA7 File Offset: 0x00019FA7
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0001BDAF File Offset: 0x00019FAF
		public float Matrix24
		{
			get
			{
				return this._matrix24;
			}
			set
			{
				this._matrix24 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the third row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public float Matrix30
		{
			get
			{
				return this._matrix30;
			}
			set
			{
				this._matrix30 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the third row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0001BDC9 File Offset: 0x00019FC9
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0001BDD1 File Offset: 0x00019FD1
		public float Matrix31
		{
			get
			{
				return this._matrix31;
			}
			set
			{
				this._matrix31 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the third row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0001BDDA File Offset: 0x00019FDA
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0001BDE2 File Offset: 0x00019FE2
		public float Matrix32
		{
			get
			{
				return this._matrix32;
			}
			set
			{
				this._matrix32 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
		/// <returns>The element at the third row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0001BDEB File Offset: 0x00019FEB
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0001BDF3 File Offset: 0x00019FF3
		public float Matrix33
		{
			get
			{
				return this._matrix33;
			}
			set
			{
				this._matrix33 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the third row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0001BDFC File Offset: 0x00019FFC
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0001BE04 File Offset: 0x0001A004
		public float Matrix34
		{
			get
			{
				return this._matrix34;
			}
			set
			{
				this._matrix34 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the fourth row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0001BE0D File Offset: 0x0001A00D
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0001BE15 File Offset: 0x0001A015
		public float Matrix40
		{
			get
			{
				return this._matrix40;
			}
			set
			{
				this._matrix40 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the fourth row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0001BE1E File Offset: 0x0001A01E
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0001BE26 File Offset: 0x0001A026
		public float Matrix41
		{
			get
			{
				return this._matrix41;
			}
			set
			{
				this._matrix41 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the fourth row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0001BE2F File Offset: 0x0001A02F
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x0001BE37 File Offset: 0x0001A037
		public float Matrix42
		{
			get
			{
				return this._matrix42;
			}
			set
			{
				this._matrix42 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
		/// <returns>The element at the fourth row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0001BE40 File Offset: 0x0001A040
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x0001BE48 File Offset: 0x0001A048
		public float Matrix43
		{
			get
			{
				return this._matrix43;
			}
			set
			{
				this._matrix43 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the fourth row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0001BE51 File Offset: 0x0001A051
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0001BE59 File Offset: 0x0001A059
		public float Matrix44
		{
			get
			{
				return this._matrix44;
			}
			set
			{
				this._matrix44 = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMatrix" /> class using the elements in the specified matrix <paramref name="newColorMatrix" />.</summary>
		/// <param name="newColorMatrix">The values of the elements for the new <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</param>
		// Token: 0x06000C43 RID: 3139 RVA: 0x0001BE62 File Offset: 0x0001A062
		[CLSCompliant(false)]
		public ColorMatrix(float[][] newColorMatrix)
		{
			this.SetMatrix(newColorMatrix);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0001BE74 File Offset: 0x0001A074
		internal void SetMatrix(float[][] newColorMatrix)
		{
			this._matrix00 = newColorMatrix[0][0];
			this._matrix01 = newColorMatrix[0][1];
			this._matrix02 = newColorMatrix[0][2];
			this._matrix03 = newColorMatrix[0][3];
			this._matrix04 = newColorMatrix[0][4];
			this._matrix10 = newColorMatrix[1][0];
			this._matrix11 = newColorMatrix[1][1];
			this._matrix12 = newColorMatrix[1][2];
			this._matrix13 = newColorMatrix[1][3];
			this._matrix14 = newColorMatrix[1][4];
			this._matrix20 = newColorMatrix[2][0];
			this._matrix21 = newColorMatrix[2][1];
			this._matrix22 = newColorMatrix[2][2];
			this._matrix23 = newColorMatrix[2][3];
			this._matrix24 = newColorMatrix[2][4];
			this._matrix30 = newColorMatrix[3][0];
			this._matrix31 = newColorMatrix[3][1];
			this._matrix32 = newColorMatrix[3][2];
			this._matrix33 = newColorMatrix[3][3];
			this._matrix34 = newColorMatrix[3][4];
			this._matrix40 = newColorMatrix[4][0];
			this._matrix41 = newColorMatrix[4][1];
			this._matrix42 = newColorMatrix[4][2];
			this._matrix43 = newColorMatrix[4][3];
			this._matrix44 = newColorMatrix[4][4];
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0001BF94 File Offset: 0x0001A194
		internal float[][] GetMatrix()
		{
			float[][] array = new float[5][];
			for (int i = 0; i < 5; i++)
			{
				array[i] = new float[5];
			}
			array[0][0] = this._matrix00;
			array[0][1] = this._matrix01;
			array[0][2] = this._matrix02;
			array[0][3] = this._matrix03;
			array[0][4] = this._matrix04;
			array[1][0] = this._matrix10;
			array[1][1] = this._matrix11;
			array[1][2] = this._matrix12;
			array[1][3] = this._matrix13;
			array[1][4] = this._matrix14;
			array[2][0] = this._matrix20;
			array[2][1] = this._matrix21;
			array[2][2] = this._matrix22;
			array[2][3] = this._matrix23;
			array[2][4] = this._matrix24;
			array[3][0] = this._matrix30;
			array[3][1] = this._matrix31;
			array[3][2] = this._matrix32;
			array[3][3] = this._matrix33;
			array[3][4] = this._matrix34;
			array[4][0] = this._matrix40;
			array[4][1] = this._matrix41;
			array[4][2] = this._matrix42;
			array[4][3] = this._matrix43;
			array[4][4] = this._matrix44;
			return array;
		}

		/// <summary>Gets or sets the element at the specified row and column in the <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <param name="row">The row of the element.</param>
		/// <param name="column">The column of the element.</param>
		/// <returns>The element at the specified row and column.</returns>
		// Token: 0x17000355 RID: 853
		public float this[int row, int column]
		{
			get
			{
				return this.GetMatrix()[row][column];
			}
			set
			{
				float[][] matrix = this.GetMatrix();
				matrix[row][column] = value;
				this.SetMatrix(matrix);
			}
		}

		// Token: 0x0400084D RID: 2125
		private float _matrix00;

		// Token: 0x0400084E RID: 2126
		private float _matrix01;

		// Token: 0x0400084F RID: 2127
		private float _matrix02;

		// Token: 0x04000850 RID: 2128
		private float _matrix03;

		// Token: 0x04000851 RID: 2129
		private float _matrix04;

		// Token: 0x04000852 RID: 2130
		private float _matrix10;

		// Token: 0x04000853 RID: 2131
		private float _matrix11;

		// Token: 0x04000854 RID: 2132
		private float _matrix12;

		// Token: 0x04000855 RID: 2133
		private float _matrix13;

		// Token: 0x04000856 RID: 2134
		private float _matrix14;

		// Token: 0x04000857 RID: 2135
		private float _matrix20;

		// Token: 0x04000858 RID: 2136
		private float _matrix21;

		// Token: 0x04000859 RID: 2137
		private float _matrix22;

		// Token: 0x0400085A RID: 2138
		private float _matrix23;

		// Token: 0x0400085B RID: 2139
		private float _matrix24;

		// Token: 0x0400085C RID: 2140
		private float _matrix30;

		// Token: 0x0400085D RID: 2141
		private float _matrix31;

		// Token: 0x0400085E RID: 2142
		private float _matrix32;

		// Token: 0x0400085F RID: 2143
		private float _matrix33;

		// Token: 0x04000860 RID: 2144
		private float _matrix34;

		// Token: 0x04000861 RID: 2145
		private float _matrix40;

		// Token: 0x04000862 RID: 2146
		private float _matrix41;

		// Token: 0x04000863 RID: 2147
		private float _matrix42;

		// Token: 0x04000864 RID: 2148
		private float _matrix43;

		// Token: 0x04000865 RID: 2149
		private float _matrix44;
	}
}
