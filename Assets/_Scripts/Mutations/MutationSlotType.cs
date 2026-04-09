namespace FarmGame.Mutations
{

    /// The three mutation slot types. Each slot governs a distinct game-stat.
    /// Slot 1 = HarvestDifficulty, Slot 2 = Yield, Slot 3 = Value.
  
    public enum MutationSlotType
    {
        HarvestDifficulty = 0,
        Yield = 1,
        Value = 2
    }

  
    /// Rarity tier for a mutation, used for UI display and crossbreed weighting.
    public enum MutationRarity
    {
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Legendary = 3
    }
}
