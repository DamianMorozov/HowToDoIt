﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Core.Models;

// Remove this class once your pages/features are using your data.
// This is used by the SampleDataService.
// It is the model class we use to display data on pages like Grid, Chart, and List Details.
public class SampleOrderDetail
{
	public long ProductID { get; set; }

	public string ProductName { get; set; }

	public int Quantity { get; set; }

	public double Discount { get; set; }

	public string QuantityPerUnit { get; set; }

	public double UnitPrice { get; set; }

	public string CategoryName { get; set; }

	public string CategoryDescription { get; set; }

	public double Total { get; set; }

	public string ShortDescription => $"Product ID: {ProductID} - {ProductName}";
}