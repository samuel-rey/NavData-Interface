﻿using AviationCalcUtilNet.GeoTools;
using NavData_Interface.Objects;
using NavData_Interface.Objects.Fixes;
using System.Collections;
using System.Collections.Generic;

namespace NavData_Interface.DataSources
{
    public class CombinedSource : DataSource, IEnumerable<KeyValuePair<int, DataSource>>
    {
        private SortedList<int, DataSource> _sources = new SortedList<int, DataSource>();

        private string _id;

        public override string GetId()
        {
            return _id;
        }

        public CombinedSource(string id)
        {
            _id = id;
        }

        public CombinedSource(string id, DataSource dataSource) : this(id)
        {
            _sources = new SortedList<int, DataSource>();

            _sources.Add(0, dataSource);
        }

        /// <summary>
        /// Creates a new combined source from a list of DataSources
        /// </summary>
        /// <param name="sources">Sources to add on construction, going from highest priority (leftmost argument) to lowest priority (rightmost argument)</param>
        public CombinedSource(string id, params DataSource[] sources) : this(id)
        {
            _sources = new SortedList<int, DataSource>();

            int priority = 0;
            foreach (var source in sources)
            {
                _sources.Add(priority, source);

                priority++;
            }
        }

        /// <summary>
        /// Adds the specified source to the sources, with the lowest priority
        /// </summary>
        /// <param name="source">The source to be added</param>
        /// <returns>true if the source was added, or false if the CombinedSource already contains this source</returns>
        public bool AddSource(DataSource source)
        {
            var lastPriority = _sources.Keys[_sources.Keys.Count - 1];

            return AddSource(source, lastPriority);
        }

        /// <summary>
        /// Adds the specified source to the sources, with the specified priority. Throws ArgumentException if there already is a source with that priority.
        /// </summary>
        /// <param name="source">The source to be added</param>
        /// <param name="priority">The priority of the source</param>
        /// <returns>true if the source was added, or false if the CombinedSource already contains this source</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool AddSource(DataSource source, int priority)
        {
            if (!_sources.ContainsValue(source))
            {
                _sources.Add(priority, source);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Changes the priority of an already-added source
        /// </summary>
        /// <param name="sourceId">The string-id of the source to modify</param>
        /// <param name="newPriority">The new priority that the source will have</param>
        /// <returns>true if the priority was changed, false if the source was not found</returns>
        public bool ChangePriority(string sourceId, int newPriority)
        {
            foreach(var source in _sources)
            {
                if (source.Value.GetId() == sourceId)
                {
                    _sources.Remove(source.Key);

                    _sources.Add(newPriority, source.Value);

                    return true;
                }
            }

            return false;
        }

        public override List<Fix> GetFixesByIdentifier(string identifier)
        {
            var fixes = new List<Fix>();

            int lastIndexToCheck = 0;

            foreach(var source in _sources.Values)
            {
                fixesInSource: foreach(var fix in source.GetFixesByIdentifier(identifier))
                {
                    for (int i = 0; i < lastIndexToCheck; i++)
                    {
                        if (GeoPoint.DistanceM(fixes[i].Location, fix.Location) < 1000)
                        {
                            goto fixesInSource;
                        }
                    }

                    fixes.Add(fix);
                }

                lastIndexToCheck = fixes.Count - 1;
            }

            return fixes;
        }

        public override Localizer GetLocalizerFromAirportRunway(string airportIdentifier, string runwayIdentifier)
        {
            foreach (var source in _sources.Values)
            {
                var localizer = source.GetLocalizerFromAirportRunway(airportIdentifier, runwayIdentifier);

                if (localizer != null)
                {
                    return localizer;
                }
            }

            return null;
        }

        public override Airport GetClosestAirportWithinRadius(GeoPoint position, double radiusM)
        {
            double closestDistance = double.MaxValue;
            Airport closestAirport = null;

            foreach (var source in _sources.Values)
            {
                Airport currentSourceAirport = source.GetClosestAirportWithinRadius(position, radiusM);
                double currentDistance = GeoPoint.DistanceM(position, currentSourceAirport.Location);

                if (currentDistance < closestDistance)
                {
                    if (closestAirport != null && closestAirport.Identifier == currentSourceAirport.Identifier)
                    {
                        continue;
                    }

                    closestDistance = currentDistance;
                    closestAirport = currentSourceAirport;
                }
            }

            return closestAirport;
        }

        public IEnumerator<KeyValuePair<int, DataSource>> GetEnumerator()
        {
            return _sources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sources.GetEnumerator();
        }
    }
}