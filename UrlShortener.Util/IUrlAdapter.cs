using System;
using System.Collections;
using System.Collections.Generic;

namespace UrlShortener.Util
{
    /// <summary>
    /// Comment from the author (Michael Nguyen): This interface allows us to change our hashing algorithm down the line, without affecting other code
    /// Demonstrates: GoF strategy pattern, SOLID liskov, GRASP protected variation, IoC
    /// </summary>
    public interface IUrlAdapter
    {
        string TryGetUrlHash(string input);
    }
}
