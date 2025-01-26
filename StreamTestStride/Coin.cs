using System;
using System.Linq;
using Stride.BepuPhysics;
using Stride.BepuPhysics.Definitions.Contacts;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.UI;
using Stride.UI.Controls;
using Stride.Engine;

namespace StreamTestStride
{
    public class Coin : SyncScript, IContactEventHandler
    {
        // Declared public member fields and properties will show in the game studio
        private Entity ui;
        private TextBlock ScoreUi;
        private Random rnd;
        private float Score;

        public override void Start()
        {
            ui = Entity.Scene.Entities.FirstOrDefault(e => e.Get<UIComponent>() != null && e.Name == "Page");

            rnd = new Random();
            
            if (ui != null)
            {
                UIComponent pageUI = ui.Get<UIComponent>();
                
                ScoreUi = pageUI.Page.RootElement.FindVisualChildOfType<TextBlock>("Score");
            }
            
            
        }
        public override void Update()
        {
            ScoreUi.Text = "Score: " + Score;
        }

        [DataMemberIgnore]
        public bool NoContactResponse => true;

        void IContactEventHandler.OnStartedTouching<TManifold>(CollidableComponent collidableComponent,
            CollidableComponent other,
            ref TManifold manifold,
            bool flippedManifold,
            int workerIndex,
            BepuSimulation bepuSimulation)
        {
            DebugText.Print("Collecting", new Int2(20, 70));
            Score += 1;
            Entity.Transform.Position = new Vector3(rnd.Next(-10, 10), .5f, rnd.Next(-10, 10));
        }
        void IContactEventHandler.OnStoppedTouching<TManifold>(CollidableComponent eventSource, CollidableComponent other,
            ref TManifold contactManifold,
            bool flippedManifold,
            int workerIndex,
            BepuSimulation bepuSimulation)
        {
            
        }

        
    }
}
