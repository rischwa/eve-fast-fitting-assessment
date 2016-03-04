using System;

namespace CrestSharp.Model
{
    [Flags]
    public enum AuthenticatedCrestScope
    {
        CharacterFittingsRead = 1,
        CharacterFittingsWrite = 2,
        CharacterLocationRead = 4,
        CharacterNavigationWrite = 8,
        CharacterContactsRead = 16,
        CharacterContactsWrite = 32
    }
}