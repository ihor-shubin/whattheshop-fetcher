using ClosedXML.Excel;

namespace WhatTheShop;

public class ExcelService
{
    public static void WriteZones(List<Zone> zones, XLWorkbook workbook)
    {
        var worksheet = workbook.Worksheets.Add("Zones");

        // Main header
        worksheet.Cell("A1").Value = "Available user zones linked to current account";
        worksheet.Range("A1:D1").Row(1).Merge();

        // Tables header
        worksheet.Cell("A2").Value = "Id";
        worksheet.Cell("B2").Value = "Type";
        worksheet.Cell("C2").Value = "Name";
        worksheet.Cell("D2").Value = "Full Name";

        for (int i = 0; i < zones.Count; i++)
        {
            worksheet.Cell($"A{i + 3}").Value = zones[i].Id;
            worksheet.Cell($"B{i + 3}").Value = zones[i].Type;
            worksheet.Cell($"C{i + 3}").Value = zones[i].Name;
            worksheet.Cell($"D{i + 3}").Value = zones[i].Fullname;
        }

        var rangeTable = worksheet.Range($"A2:D{zones.Count+2}");
        rangeTable.CreateTable();
        worksheet.Columns().AdjustToContents();
    }

    public static void WriteZoneInfos(List<ZoneInfo> zoneInfos, XLWorkbook workbook)
    {
        var worksheet = workbook.Worksheets.Add("Zone Infos");

        // Main header
        worksheet.Cell("A1").Value = "Informations about user zone in permission list";
        worksheet.Range("A1:D1").Row(1).Merge();

        // Tables header
        worksheet.Cell("A2").Value = "Id";
        worksheet.Cell("B2").Value = "Type";
        worksheet.Cell("C2").Value = "Name";
        worksheet.Cell("D2").Value = "Full Name";
        worksheet.Cell("E2").Value = "Address";
        worksheet.Cell("F2").Value = "Zip Code";
        worksheet.Cell("G2").Value = "City";
        worksheet.Cell("H2").Value = "Country";
        worksheet.Cell("K2").Value = "Lat";
        worksheet.Cell("L2").Value = "Lon";
        worksheet.Cell("M2").Value = "Children";
        worksheet.Cell("N2").Value = "Sensors";

        for (int i = 0; i < zoneInfos.Count; i++)
        {
            worksheet.Cell($"A{i + 3}").Value = zoneInfos[i].Id;
            worksheet.Cell($"B{i + 3}").Value = zoneInfos[i].Type;
            worksheet.Cell($"C{i + 3}").Value = zoneInfos[i].Name;
            worksheet.Cell($"D{i + 3}").Value = zoneInfos[i].Fullname;
            worksheet.Cell($"E{i + 3}").Value = zoneInfos[i].Address;
            worksheet.Cell($"F{i + 3}").Value = zoneInfos[i].ZipCode;
            worksheet.Cell($"G{i + 3}").Value = zoneInfos[i].City;
            worksheet.Cell($"H{i + 3}").Value = zoneInfos[i].Country;
            worksheet.Cell($"K{i + 3}").Value = zoneInfos[i].Lat;
            worksheet.Cell($"L{i + 3}").Value = zoneInfos[i].Lon;
            worksheet.Cell($"M{i + 3}").Value = zoneInfos[i].Children;
            worksheet.Cell($"N{i + 3}").Value = zoneInfos[i].Sensors;
        }

        var rangeTable = worksheet.Range($"A2:N{zoneInfos.Count+2}");
        rangeTable.CreateTable();
        worksheet.Columns().AdjustToContents();
    }

    public static void WriteDevices(List<Device> devices, XLWorkbook workbook)
    {
        var worksheet = workbook.Worksheets.Add("Devices");

        // Main header
        worksheet.Cell("A1").Value = "List of devices depending on zones permissions";
        worksheet.Range("A1:D1").Row(1).Merge();

        // Tables header
        worksheet.Cell("A2").Value = "Device";
        worksheet.Cell("B2").Value = "Ts";
        worksheet.Cell("C2").Value = "Max Duration";
        worksheet.Cell("D2").Value = "Crypt Token";

        for (int i = 0; i < devices.Count; i++)
        {
            worksheet.Cell($"A{i + 3}").Value = devices[i].Name;
            worksheet.Cell($"B{i + 3}").Value = devices[i].Ts;
            worksheet.Cell($"C{i + 3}").Value = devices[i].MaxDuration;
            worksheet.Cell($"D{i + 3}").Value = devices[i].CryptTokens;
        }

        var rangeTable = worksheet.Range($"A2:D{devices.Count+2}");
        rangeTable.CreateTable();
        worksheet.Columns().AdjustToContents();
    }

    public static void WriteMonitoring(Monitoring monitoring, XLWorkbook workbook)
    {
        var worksheet = workbook.Worksheets.Add("Monitoring");

        // Main header
        worksheet.Cell("A1").Value = "Number of devices up/total and sensors";
        worksheet.Range("A1:D1").Row(1).Merge();

        // Tables header
        worksheet.Cell("A3").Value = "Devices On";
        worksheet.Cell("A3").Style.Font.Bold = true;
        worksheet.Cell("B3").Value = monitoring.DevicesOn;

        worksheet.Cell("A4").Value = "Devices Total";
        worksheet.Cell("A4").Style.Font.Bold = true;
        worksheet.Cell("B4").Value = monitoring.DevicesTotal;

        worksheet.Cell("A5").Value = "Sensors Total";
        worksheet.Cell("A5").Style.Font.Bold = true;
        worksheet.Cell("B5").Value = monitoring.SensorsTotal;

        worksheet.Columns(1,1).AdjustToContents();
    }

    public static void WriteAnalyticPasserby(AnalyticPasserbyCount analyticPasserbyCount, XLWorkbook workbook)
    {
        var worksheet = workbook.Worksheets.Add("Analytic Passerby Count");

        // Main header
        worksheet.Cell("A1").Value = "Passerby global count and frequency";
        worksheet.Range("A1:D1").Row(1).Merge();

        // Tables header
        /*worksheet.Cell("A3").Value = "New";
        worksheet.Cell("A3").Style.Font.Bold = true;
        worksheet.Cell("B3").Value = analyticPasserbyCount.New;

        worksheet.Cell("A4").Value = "Low";
        worksheet.Cell("A4").Style.Font.Bold = true;
        worksheet.Cell("B4").Value = analyticPasserbyCount.Low;

        worksheet.Cell("A5").Value = "Frequent";
        worksheet.Cell("A5").Style.Font.Bold = true;
        worksheet.Cell("B5").Value = analyticPasserbyCount.Frequent;

        worksheet.Cell("A6").Value = "Total";
        worksheet.Cell("A6").Style.Font.Bold = true;
        worksheet.Cell("B6").Value = analyticPasserbyCount.Total;*/

        worksheet.Columns(1,1).AdjustToContents();
    }

    public static void WriteAnalyticPasserbyDetails(List<AnalyticPasserbyCountDetails> analyticPasserbyDetails, XLWorkbook workbook)
    {
        var worksheet = workbook.Worksheets.Add("Analytic Passerby Count Details");

        // Main header
        worksheet.Cell("A1").Value = "Passerby per day count and frequency";
        worksheet.Range("A1:D1").Row(1).Merge();

        // Tables header
        worksheet.Cell("A2").Value = "Date";
        worksheet.Cell("B2").Value = "New";
        worksheet.Cell("C2").Value = "Low";
        worksheet.Cell("D2").Value = "Frequent";
        worksheet.Cell("E2").Value = "Total";

        for (int i = 0; i < analyticPasserbyDetails.Count; i++)
        {
            /*worksheet.Cell($"A{i + 3}").Value = analyticPasserbyDetails[i].Date;
            worksheet.Cell($"B{i + 3}").Value = analyticPasserbyDetails[i].AnalyticPasserbyCount.New;
            worksheet.Cell($"C{i + 3}").Value = analyticPasserbyDetails[i].AnalyticPasserbyCount.Low;
            worksheet.Cell($"D{i + 3}").Value = analyticPasserbyDetails[i].AnalyticPasserbyCount.Frequent;
            worksheet.Cell($"E{i + 3}").Value = analyticPasserbyDetails[i].AnalyticPasserbyCount.Total;*/
        }

        var rangeTable = worksheet.Range($"A2:E{analyticPasserbyDetails.Count+2}");
        rangeTable.CreateTable();
        worksheet.Columns().AdjustToContents();
    }

    public static void WriteAnalyticPasserbyHour(List<AnalyticPasserbyCountHour> analyticPasserbyPerHour, XLWorkbook workbook)
    {
        var worksheet = workbook.Worksheets.Add("Passerby per hour average");

        // Main header
        worksheet.Cell("A1").Value = "Passerby per hour average";
        worksheet.Range("A1:D1").Row(1).Merge();

        // Tables header
        worksheet.Cell("A2").Value = "Hour";
        worksheet.Cell("B2").Value = "Count";

        for (int i = 0; i < analyticPasserbyPerHour.Count; i++)
        {
            worksheet.Cell($"A{i + 3}").Value = analyticPasserbyPerHour[i].Hour;
            worksheet.Cell($"B{i + 3}").Value = analyticPasserbyPerHour[i].Count;
        }

        var rangeTable = worksheet.Range($"A2:D{analyticPasserbyPerHour.Count+2}");
        rangeTable.CreateTable();
        worksheet.Columns().AdjustToContents();
    }
}