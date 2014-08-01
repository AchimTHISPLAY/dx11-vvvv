﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VMath;
using BulletSharp;
using VVVV.DataTypes.Bullet;

namespace VVVV.Nodes.Bullet
{
	[PluginInfo(Name = "TriMesh", Category = "Bullet", Version = "SoftShape", Author = "vux")]
	public class BulletTriMeshShapeNode : AbstractSoftShapeNode
	{
		[Input("Vertices")]
        protected IDiffSpread<ISpread<Vector3D>> FVertices;

		[Input("Indices")]
        protected IDiffSpread<ISpread<int>> FIndices;

		[Input("Randomize Contraints")]
        protected IDiffSpread<bool> FRandomize;

		protected override bool SubPinsChanged
		{
			get 
			{
				return this.FVertices.IsChanged
					|| this.FIndices.IsChanged;
			}
		}

		protected override AbstractSoftShapeDefinition GetShapeDefinition(int slice)
		{
			Vector3[] verts = new Vector3[this.FVertices[slice].SliceCount];
			for (int i = 0; i < verts.Length; i++)
			{
				verts[i] = this.FVertices[slice][i].ToBulletVector();
			}

			int[] indices = new int[this.FIndices[slice].SliceCount];
			for (int i = 0; i < indices.Length;i++)
			{
				indices[i] = this.FIndices[slice][i];
			}

			return new TriMeshSoftShapeDefinition(verts, indices, this.FRandomize[slice]);
		}

		protected override int SubPinSpreadMax
		{
			get 
			{
				return Math.Max(this.FIndices.SliceCount, this.FVertices.SliceCount);		
			}
		}
	}
}
