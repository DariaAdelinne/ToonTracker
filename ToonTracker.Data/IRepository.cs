/**************************************************************************
 *                                                                        *
 *  File:        IRepository.cs                                           *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Contract generic pentru persistenta datelor.             *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

namespace ToonTracker.Data;

/// <summary>
/// Interfata generica pentru persistenta si recuperarea datelor
/// </summary>
public interface IRepository<T>
{
    /// <summary>
    /// Returneaza toate obiectele din sursa de date
    /// </summary>
    /// <returns>Lista read-only a tuturor obiectelor</returns>
    IReadOnlyList<T> GetAll();

    /// <summary>
    /// Salveaza toate obiectele in sursa de date, suprascriind continutul anterior
    /// </summary>
    /// <param name="items">Colectia de obiecte de salvat</param>
    void SaveAll(IEnumerable<T> items);
}
