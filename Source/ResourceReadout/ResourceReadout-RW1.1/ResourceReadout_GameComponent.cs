using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace ResourceReadout
{
	public class ResourceReadout_GameComponent : GameComponent
	{
		public NodeOpenTracker NodeOpenTracker = new NodeOpenTracker();

		public ResourceReadout_GameComponent(Game game)
		{

		}

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Deep.Look(ref NodeOpenTracker, true, "nodeOpenTracker", new object[0]);
		}

		public override void FinalizeInit()
		{
			base.FinalizeInit();
		}

		public override void GameComponentOnGUI()
		{
			base.GameComponentOnGUI();
		}

		public override void GameComponentTick()
		{
			base.GameComponentTick();
		}

		public override void GameComponentUpdate()
		{
			base.GameComponentUpdate();
		}

		public override void LoadedGame()
		{
			base.LoadedGame();

#if DEBUG
			Log.Message("ResourceReadout_GameComponent::LoadedGame");

			Log.Message("Iterating over NodeOpenTracker...");

			foreach (var label in NodeOpenTracker)
			{
				Log.Message($"\tSaved: {label}");
			}

			var rootThingCategories = Find.ResourceReadout.GetRootThingCategories();

			foreach (var def in rootThingCategories)
			{
				var node = def.treeNode;
				node.SetOpen(TreeOpenMasks.ResourceReadout, NodeOpenTracker.Contains(node));
			}
#endif
		}

		public override void StartedNewGame()
		{
			base.StartedNewGame();
		}
	}
}
