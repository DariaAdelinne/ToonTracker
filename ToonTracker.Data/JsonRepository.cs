/**************************************************************************
 *                                                                        *
 *  File:        JsonRepository.cs                                        *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Repository generic ce salveaza date in fisiere JSON.     *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System.Text.Json;
using ToonTracker.Domain;

namespace ToonTracker.Data;

/// <summary>
/// Implementare a repository-ului care persista datele intr-un fisier JSON de pe disc
/// </summary>
public class JsonRepository<T> : IRepository<T>
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    /// <summary>
    /// Constructorul repository-ului JSON
    /// </summary>
    /// <param name="filePath">Calea catre fisierul JSON folosit pentru persistenta datelor</param>
    public JsonRepository(string filePath)
    {
        _filePath = filePath;
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath) ?? ".");
    }

    /// <summary>
    /// Citeste si returneaza toate obiectele din fisierul JSON.
    /// Daca fisierul nu exista, returneaza o lista goala.
    /// </summary>
    /// <returns>Lista read-only a tuturor obiectelor deserializate</returns>
    /// <exception cref="DataAccessException">Daca fisierul este corupt sau nu poate fi citit de pe disc</exception>
    public IReadOnlyList<T> GetAll()
    {
        try
        {
            if (!File.Exists(_filePath)) return new List<T>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }
        catch (JsonException ex)
        {
            throw new DataAccessException("Fisierul de date este corupt sau are format invalid.", ex);
        }
        catch (IOException ex)
        {
            throw new DataAccessException("Datele nu au putut fi citite de pe disc.", ex);
        }
    }


    /// <summary>
    /// Serialializeaza si salveaza toate obiectele in fisierul JSON, suprascriind continutul anterior
    /// </summary>
    /// <param name="items">Colectia de obiecte de salvat</param>
    /// <exception cref="DataAccessException">Daca fisierul nu poate fi scris pe disc</exception>
    public void SaveAll(IEnumerable<T> items)
    {
        try
        {
            var json = JsonSerializer.Serialize(items, _options);
            File.WriteAllText(_filePath, json);
        }
        catch (IOException ex)
        {
            throw new DataAccessException("Datele nu au putut fi salvate pe disc.", ex);
        }
    }
}
