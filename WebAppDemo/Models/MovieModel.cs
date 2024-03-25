// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Models;

public sealed class MovieModel
{
	#region Public and private fields, properties, constructor

	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
	public int Duration { get; set; }

	#endregion

	#region Public and private methods

	public string GetInfo() => $"{nameof(Id)}: {Id} | {nameof(Title)}: {Title} | {nameof(Genre)}: {Genre} | {nameof(Duration)}: {Duration}";

	#endregion
}
