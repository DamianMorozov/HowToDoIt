// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Models;

public sealed class ProductCategoryModel
{
    #region Public and private fields, properties, constructor

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    #endregion
}
