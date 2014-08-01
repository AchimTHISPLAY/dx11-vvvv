﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VVVV.PluginInterfaces.V2;
using BulletSharp.SoftBody;

namespace VVVV.Nodes.Bullet
{
	[PluginInfo(Name = "SetMass", Category = "Bullet", Version = "SoftBody", Author = "vux",
		Help = "Updates a soft body mass", AutoEvaluate = true)]
	public class BulletSoftSetMassNode : AbstractBodyInteractionNode<SoftBody>
	{
		[Input("Node Index", DefaultValue = -1)]
        protected ISpread<int> FNodeIndex;

		[Input("Mass")]
        protected ISpread<float> FMass;

		protected override void ProcessObject(SoftBody obj, int slice)
		{
			if (this.FNodeIndex[slice] < 0)
			{
				obj.SetTotalMass(this.FMass[slice]);
				//obj.Cfg.
			}
			else
			{
				obj.SetMass(this.FNodeIndex[slice] % obj.Nodes.Count, this.FMass[slice]);
			}
		}
	}
}
