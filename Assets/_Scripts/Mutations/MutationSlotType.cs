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

    // MutationRarity er defineret globalt i GameEnums.cs – ikke her.
}

