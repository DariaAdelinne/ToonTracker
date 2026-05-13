/**************************************************************************
 *                                                                        *
 *  File:        Header.cs                                                *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Enumerari folosite in domeniul aplicatiei.               *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

namespace ToonTracker.Domain;

/// <summary>
/// Reprezinta statusul curent al vizionarii unui serial.
/// </summary>
public enum WatchStatus { Planned, Watching, Finished, Dropped }

/// <summary>
/// Defineste clasificarile de varsta pentru continutul animat.
/// </summary>
public enum AgeRating { G, PG, PG13, TV14, TVMA }
