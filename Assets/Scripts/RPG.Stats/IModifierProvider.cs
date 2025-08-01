using System.Collections;
using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPercentageModifiers(Stat stat);
        float GetFlatModifier(Stat stat);
        float GetMultiplicativeModifier(Stat stat);
    }
}
