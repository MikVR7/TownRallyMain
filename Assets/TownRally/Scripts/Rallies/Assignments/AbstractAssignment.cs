using UnityEngine;

namespace TownRally
{
    internal class AbstractAssignment : MonoBehaviour
    {
        internal enum AssignmentType
        {
            None = 0,
            Cloze = 1,
            WordSearch = 2,
            BlockPuzzle = 3,
        }

        internal AssignmentType type = AssignmentType.None;

        internal virtual void Init()
        {

        }
    }
}
