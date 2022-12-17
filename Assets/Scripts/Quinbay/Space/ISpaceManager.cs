using System.Collections.Generic;
using Quinbay.Data;

namespace Quinbay.Space
{
    public interface ISpaceManager
    {
        List<BlibliSpace> Spaces { get; }

        void SelectSpace(string spaceName);
        void SelectSpace(BlibliSpace space);
    }
}