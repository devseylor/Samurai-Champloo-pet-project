﻿using System;

namespace CodeBase.Data
{
    [Serializable]
    public class State
    {
        public float CurrentHp;
        public float MaxHP;

        public void ResetHP() => CurrentHp= MaxHP;
    }
}
