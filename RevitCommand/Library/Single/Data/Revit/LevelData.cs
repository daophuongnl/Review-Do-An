using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SingleData
{
    public class LevelData
    {
        private static LevelData? instance;
        public static LevelData Instance
        {
            get => instance ??= new LevelData();
            set => instance = value;
        }

    }
}
