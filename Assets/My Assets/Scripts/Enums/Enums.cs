using System.ComponentModel;

//Used Niall McGuinness Example , https://github.com/nmcguinness/2024-25-GD3A-IntroToUnity/blob/main/IntroToUnity/Assets/GD/Common/Scripts/Enums/Enums.cs

namespace GD.Types
{
    /// <summary>
    /// Represents the various types of audio groups in the game.
    /// </summary>
    public enum AudioMixerGroupName : sbyte
    {
        [Description("Master audio group")]
        Master,

        [Description("Ambient sounds group")]
        Ambient,

        [Description("Background music group")]
        Background,

        [Description("Sound effects group")]
        SFX,

        [Description("UI sounds group")]
        UI,

        [Description("Voiceover group")]
        Voiceover,

        [Description("Weapon sounds group")]
        Weapon
    }

    /// <summary>
    /// Represents the state of a UI element, such as visible, hidden, or transitioning.
    /// </summary>
    public enum VisibilityState : sbyte
    {
        [Description("The UI element has tween applied.")]
        End,

        [Description("The UI element is transitioning to a visible state.")]
        Showing,

        [Description("The UI element is transitioning to a hidden state.")]
        Hiding,

        [Description("The UI element has not yet had tween applied.")]
        Start
    }

    /// <summary>
    /// Used in the StateManager to determine how to evaluate a condition.
    /// </summary>
    public enum ConditionType : sbyte
    {
        [Description("Evaluate all conditions and return true if all are met.")]
        And,

        [Description("Evaluate all conditions and return true if any are met.")]
        Or,

        [Description("Evaluate all conditions and return true if only one is met.")]
        Xor
    }

    /// <summary>
    /// Used in the StateManager to determine how to evaluate a condition.
    /// </summary>
    public enum EvaluateStrategy : sbyte
    {
        /// <summary>
        /// Always evaluate the condition, regardless of whether it is met.
        /// </summary>
        [Description("Always evaluate the condition, regardless of whether it is met.")]
        EvaluateAlways,

        /// <summary>
        /// Evaluate the condition until it is met, then stop evaluating.
        /// </summary>
        [Description("Evaluate the condition until it is met, then stop evaluating.")]
        EvaluateUntilMet
    }

    /// <summary>
    /// A five-level priority system used to determine the importance of a task, event, or object.
    /// </summary>
    public enum PriorityLevel : sbyte
    {
        [Description("This is the highest priority.")]
        Highest = 1,

        [Description("This is a high priority.")]
        High = 2,

        [Description("This is a medium priority.")]
        Medium = 3,

        [Description("This is a low priority.")]
        Low = 4,

        [Description("This is the lowest priority.")]
        Lowest = 5
    }

    /// <summary>
    /// Defines the various high-level categories of items available in a game.
    /// Each category groups similar item types under one classification.
    /// </summary>
    /// <see cref="GD.Items.ItemData"/>
    /// <see cref="GD.Inventory"/>
    public enum ItemCategoryType : sbyte
    {
        /// <summary>
        /// Items that restore health, provide armor, or give temporary boosts.
        /// </summary>
        [Description("Items that restore health, provide armor, or give temporary boosts")]
        Consumable,

        /// <summary>
        /// Items that can be equipped by the player, like weapons or armor.
        /// </summary>
        [Description("Items that can be equipped by the player, like weapons or armor")]
        Equippable,

        /// <summary>
        /// Items that can be interacted with to perform actions or affect the game environment.
        /// </summary>
        [Description("Items that can be interacted with to perform actions or affect the game environment")]
        Interactable,

        /// <summary>
        /// Items that provide information, lore, or serve as blueprints.
        /// </summary>
        [Description("Items that provide information or serve as instructions or blueprints")]
        Informative,

        /// <summary>
        /// Items that serve as key resources, such as food, water, building materials, or power.
        /// </summary>
        [Description("Items that serve as resources or crafting materials")]
        Resource,

        /// <summary>
        /// Items that are used for navigation or wayfinding.
        /// </summary>
        [Description("Items that are used for navigation or wayfinding")]
        Waypoint
    }

    /// <summary>
    /// Defines the various specific types of items available in a game.
    /// These types are grouped under the broader categories represented by ItemCategoryType.
    /// </summary>
    /// <see cref="GD.Items.ItemData"/>
    /// <see cref="GD.Inventory"/>
    public enum ItemType : sbyte
    {
        // Consumable Types
        /// <summary>
        /// Represents a key or access card used to unlock doors or access restricted areas.
        /// </summary>
        [Description("Key or access card used to unlock doors or access restricted areas")]
        Key,

        /// <summary>
        /// Represents a puzzle piece that is part of a larger puzzle.
        /// </summary>
        [Description("Puzzle piece used to complete a puzzle")]
        PuzzlePiece,

        // Resource Types
        /// <summary>
        /// Represents a building material such as wood or steel used for construction.
        /// </summary>
        [Description("Building material such as wood or steel used for construction")]
        BuildingMaterial,

        /// <summary>
        /// Represents a crafting component that can be used to create or upgrade items.
        /// </summary>
        [Description("Crafting component for creating or upgrading items")]
        CraftingComponent,

        /// <summary>
        /// Represents a general resource such as food, water, or fuel.
        /// </summary>
        [Description("Resource item used in simulation games, such as food, water, or fuel")]
        Resource

    }

    /// <summary>
    /// All interctable equipment types in the game

    public enum GymEquipmentType :sbyte
    {
        GymMat,
        ShoulderPress,
        SitUps,
        Squats
    }
}