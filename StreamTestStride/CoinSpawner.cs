﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace StreamTestStride
{
    public class CoinSpawner : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        public Prefab coinPrefab;

        public override void Start()
        {
            var coinPrefabInstance = coinPrefab.Instantiate();
            Entity.Scene.Entities.AddRange(coinPrefabInstance);
        }
        public override void Update()
        {
            
        }
    }
}
