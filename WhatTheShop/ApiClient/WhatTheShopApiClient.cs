using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using WhatTheShop.Models;

namespace WhatTheShop.ApiClient;

public class WhatTheShopApiClient
{
    /*
     https://api.whattheshop.net/doc/1/index.html
     */
    private readonly string _url;
    private readonly string _userName;
    private readonly string _userPassword;
    private readonly DateTime _startDateParam;

    private readonly string _token;

    public WhatTheShopApiClient(string url, string userName, string password)
    {
        //_startDateParam = DateTime.Now.AddYears(-2).AddDays(1); // the only 2 last years available
        _startDateParam = DateTime.Now.AddMonths(0).AddDays(-2); // the only 2 last years available

        _url = url;
        _userName = userName;
        _userPassword = password;
        _token = GetToken().GetAwaiter().GetResult();
    }

    public async Task<string> GetToken()
    {
        var urlPart = "/1/user/auth";

        var response = await _url.AppendPathSegment(urlPart)
            .PostMultipartAsync(mp => mp.AddString("login", _userName).AddString("password", _userPassword))
            .ReceiveJson();

        return response.token;
    }

    public async Task<List<Zone>> GetZones()
    {
        var urlPart = "/1/user/zones";

        var response = await _url.AppendPathSegment(urlPart)
            .WithHeader("wts_token", _token)
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, JObject>>();

        var zones = resultObj
            .Select(x => new Zone(
                x.Value["id"].Value<string>(),
                x.Value["type"].Value<string>(),
                x.Value["name"].Value<string>(),
                x.Value["fullname"].Value<string>()))
            .ToList();

        return zones;
    }

    public async Task<List<ZoneInfo>> GetZonesInfo(List<Zone> zones)
    {
        var urlPart = "/1/user/zoneinfos";

        var result = new List<ZoneInfo>();

        foreach (var zone in zones)
        {
            var response = await _url.AppendPathSegment(urlPart)
                .WithHeader("wts_token", _token)
                .SetQueryParam("zoneId", zone.Id)
                .GetStringAsync();

            var responseObj = JObject.Parse(response);
            var resultObj = responseObj["result"].Value<JObject>();

            result.Add(new ZoneInfo(
                resultObj.Value<string>("id"),
                resultObj.Value<string>("type"),
                resultObj.Value<string>("name"),
                resultObj.Value<string>("fullname"),
                resultObj.Value<string>("address"),
                resultObj.Value<string>("zipcode"),
                resultObj.Value<string>("city"),
                resultObj.Value<string>("country"),
                resultObj.Value<string>("lat"),
                resultObj.Value<string>("lon"),
                string.Join(',', resultObj.Value<JArray>("children").Values<string>()),
                string.Join(',', resultObj.Value<JArray>("sensors").Values<string>())
            ));
        }

        return result;
    }

    public async Task<List<Device>> GetDevices()
    {
        var urlPart = "/1/user/devices";

        var response = await _url.AppendPathSegment(urlPart)
            .WithHeader("wts_token", _token)
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"]["auth"].ToObject<Dictionary<string, JObject>>();

        var result = resultObj
            .Select(x => new Device(
                x.Key,
                x.Value["ts"].Value<int>(),
                x.Value["maxDuration"].Value<int>(),
                string.Join(',', x.Value.Value<JArray>("cryptToken").Values<string>())
            ))
            .ToList();

        return result;
    }

    public async Task<List<Monitoring>> GetMonitoring(List<Zone> zones)
    {
        var urlPart = "/1/user/monitoring";

        var result = new List<Monitoring>();

        foreach (var zone in zones)
        {
            var response = await _url.AppendPathSegment(urlPart)
                .WithHeader("wts_token", _token)
                .SetQueryParam("zoneId", zone.Id)
                .SetQueryParam("userId", "") // TODO: What is UserID in scope of the API ?!?
                .GetStringAsync();

            var responseObj = JObject.Parse(response);
            var resultObj = responseObj["result"].Value<JObject>();

            result.Add(new Monitoring(
                zone.Id,
                zone.Name,
                resultObj["devices"]["on"].Value<int>(),
                resultObj["devices"]["total"].Value<int>(),
                resultObj["sensors"]["total"].Value<int>()
            ));
        }

        return result;
    }

    /// <summary>
    /// Very slow!
    /// </summary>
    public async Task<AnalyticPasserbyCount> GetAnalyticPasserbyCount(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passerby/count";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].Value<JObject>();

        return new AnalyticPasserbyCount(
            zoneId,
            resultObj.Value<int>("new"),
            resultObj.Value<int>("low"),
            resultObj.Value<int>("frequent"),
            resultObj.Value<int>("total")
        );
    }

    public async Task<List<AnalyticPasserbyCountDetails>> GetAnalyticPasserbyCountDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passerby/countdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticPasserbyCountDetails>();
        }

        var responseObj = JObject.Parse(response);

        var resultNew = responseObj["result"]["new"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();
        var resultLow = responseObj["result"]["low"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();
        var resultFrq = responseObj["result"]["frequent"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();
        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();

        return resultNew.Select(x => x.Key)
            .Concat(resultLow.Select(x => x.Key))
            .Concat(resultFrq.Select(x => x.Key))
            .Concat(resultAll.Select(x => x.Key))
            .Distinct()
            .Select(d => new AnalyticPasserbyCountDetails(
                Guid.NewGuid(),
                zoneId,
                d,
                resultNew.ContainsKey(d) ? resultNew[d] : 0,
                resultLow.ContainsKey(d) ? resultLow[d] : 0,
                resultFrq.ContainsKey(d) ? resultFrq[d] : 0,
                resultAll.ContainsKey(d) ? resultAll[d] : 0))
            .ToList();
    }

    public async Task<List<AnalyticPasserbyCountHour>> GetAnalyticPasserbyHour(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passerby/counthour";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, double>>();

        var result = resultObj
            .Select(x => new AnalyticPasserbyCountHour(
                Guid.NewGuid(),
                zoneId,
                x.Key,
                x.Value))
            .ToList();

        return result;
    }

    public async Task<AnalyticSensorCount> GetAnalyticSensorCount(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/sensor/count";

        var response = await _url.AppendPathSegment(urlPart)
            .WithHeader("wts_token", _token)
            .WithTimeout(60 * 60) /* 1 hour */
            .SetQueryParam("zoneId", zoneId)
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].Value<JObject>();

        return new AnalyticSensorCount(
            zoneId,
            resultObj["presence"]?.Value<int>("NeedUpdate"),
            resultObj["presence"]?.Value<int>("maxValue"),
            resultObj["presence"]?.Value<string>("lastUpdate"),
            resultObj["int"]?.Value<int>("NeedUpdate"),
            resultObj["out"]?.Value<int>("NeedUpdate"),
            resultObj["absolute"]?.Value<int>("NeedUpdate"));
    }

    public async Task<AnalyticPassingCount> GetAnalyticPassingCount(string zoneId, DateTime startDate, DateTime endDate)
    {
        var urlPart = "/1/analytic/passing/count";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .SetQueryParam("startDate", startDate)
            .SetQueryParam("endDate", endDate)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .WithHeader("wts_token", _token)
            .GetStringAsync();

        var responseObj = JObject.Parse(response);

        var result = new AnalyticPassingCount(zoneId, responseObj["result"]["total"].Value<int>());

        return result;
    }

    public async Task<List<CampaignList>> GetCampaignList(List<Zone> zones)
    {
        var urlPart = "/1/media/campaign/list";

        var result = new List<CampaignList>();

        foreach (var zone in zones)
        {
            var response = await _url.AppendPathSegment(urlPart)
                .WithHeader("wts_token", _token)
                .WithTimeout(60 * 60) /* 1 hour */
                .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
                .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
                .SetQueryParam("zones", $"[{zone.Id}]")
                .GetStringAsync();

            var responseObj = JObject.Parse(response);

            result.Add(new CampaignList(zone.Id, zone.Name, responseObj["result"].Values<string>().ToList()));
        }

        return result;
    }

    public async Task<List<CampaignCount>> GetCampaignCount(List<CampaignList> campaignLists)
    {
        var urlPart = "/1/media/campaign/count";

        var result = new List<CampaignCount>();

        foreach (var campaignWithZone in campaignLists)
        {
            foreach (var campaign in campaignWithZone.Campaigns)
            {
                var response = await _url.AppendPathSegment(urlPart)
                    .WithHeader("wts_token", _token)
                    .WithTimeout(60 * 60) /* 1 hour */
                    .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
                    .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
                    .SetQueryParam("zones", $"[{campaignWithZone.ZoneId}]")
                    .SetQueryParam("campaign", campaign)
                    .GetStringAsync();

                var responseObj = JObject.Parse(response);
                var resultObj = responseObj["result"].ToObject<Dictionary<string, JObject>>();

                result.Add(new CampaignCount(
                    campaignWithZone.ZoneId,
                    campaignWithZone.ZoneName,
                    campaign,
                    resultObj["total"].Value<int>(),
                    resultObj["confirmed"].Value<int>(),
                    resultObj["target"].Value<int>(),
                    resultObj["target-duration"].Value<int>()
                ));
            }
        }

        return result;
    }

    public async Task<List<AnalyticPasserbyCountDay>> GetAnalyticPasserbyCountDay(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passerby/counthourday";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        return null;
    }

    public async Task<List<AnalyticPasserbyCountSum>> GetAnalyticPasserbyCountSum(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passerby/countsum";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        // returns {"result":[627,1062,1424,1761,2083,2403,2755,3279,4279,5418,6822,8515,10631,12971,15233,17359,19452,21559,23780,26146,28362,30245,31797,32848]}
        // I don't know what to do with it
        return null;
    }

    public async Task<AnalyticPasserbyBestTimes> GetAnalyticPasserbyBestTimes(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passerby/besttimes";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();


        if (response is @"{""result"":{""best"":[],""peak"":[]}}")
        {
            return new AnalyticPasserbyBestTimes(zoneId, "n/a", "n/a", "n/a", "n/a");
        }

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        return new AnalyticPasserbyBestTimes(
            zoneId,
            resultObj["best"]["startTime"].Value<string>(),
            resultObj["best"]["endTime"].Value<string>(),
            resultObj["peak"]["startTime"].Value<string>(),
            resultObj["peak"]["endTime"].Value<string>());
    }

    public async Task<AnalyticPasserbyCountCommon> GetAnalyticPasserbyCountCommons(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passerby/countcommon";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        // always return empty array, but should a number
        return null;
    }

    public async Task<List<AnalyticPassingCountDetails>> GetAnalyticPassingCountDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passerby/countdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticPassingCountDetails>();
        }

        var responseObj = JObject.Parse(response);

        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticPassingCountDetails(Guid.NewGuid(), zoneId, d, resultAll[d]))
            .ToList();
    }

    public async Task<List<AnalyticPassingCountHourDetails>> GetAnalyticPassingCountHourDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passing/counthourdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response is @"{""result"":[]}" or @"{""result"":{""all"":[]}}")
        {
            return new List<AnalyticPassingCountHourDetails>();
        }

        var responseObj = JObject.Parse(response);

        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticPassingCountHourDetails(Guid.NewGuid(), zoneId, d, resultAll[d]))
            .ToList();
    }

    public async Task<AnalyticVisitorCount> GetAnalyticVisitorCount(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/passerby/count";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].Value<JObject>();

        return new AnalyticVisitorCount(
            zoneId,
            resultObj.Value<int>("new"),
            resultObj.Value<int>("low"),
            resultObj.Value<int>("frequent"),
            resultObj.Value<int>("total")
        );
    }

    public async Task<List<AnalyticVisitorCountDetails>> GetAnalyticVisitorCountDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/countdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticVisitorCountDetails>();
        }

        var responseObj = JObject.Parse(response);

        var resultNew = responseObj["result"]["new"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();
        var resultLow = responseObj["result"]["low"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();
        var resultFrq = responseObj["result"]["frequent"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();
        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();

        return resultNew.Select(x => x.Key)
            .Concat(resultLow.Select(x => x.Key))
            .Concat(resultFrq.Select(x => x.Key))
            .Concat(resultAll.Select(x => x.Key))
            .Distinct()
            .Select(d => new AnalyticVisitorCountDetails(
                Guid.NewGuid(),
                zoneId,
                d,
                resultNew.ContainsKey(d) ? resultNew[d] : 0,
                resultLow.ContainsKey(d) ? resultLow[d] : 0,
                resultFrq.ContainsKey(d) ? resultFrq[d] : 0,
                resultAll.ContainsKey(d) ? resultAll[d] : 0))
            .ToList();
    }

    public async Task<List<AnalyticVisitorDuration>> GetAnalyticVisitorDurations(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/duration";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticVisitorDuration>();
        }

        var responseObj = JObject.Parse(response);

        var resultAll = responseObj["result"]?.ToObject<Dictionary<string, double>>() ??
                        new Dictionary<string, double>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticVisitorDuration(Guid.NewGuid(), zoneId, d, resultAll[d]))
            .ToList();
    }

    public async Task<List<AnalyticVisitorDurationDetails>> GetAnalyticVisitorDurationDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/durationdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticVisitorDurationDetails>();
        }

        var responseObj = JObject.Parse(response);

        var resultAll = responseObj["result"]?.ToObject<Dictionary<string, JObject>>() ??
                        new Dictionary<string, JObject>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticVisitorDurationDetails(
                Guid.NewGuid(),
                zoneId,
                d,
                resultAll[d]["0"].Value<double>(),
                resultAll[d]["300"].Value<double>(),
                resultAll[d]["900"].Value<double>(),
                resultAll[d]["1800"].Value<double>()))
            .ToList();
    }

    public async Task<AnalyticVisitorBestTimes> GetAnalyticVisitorBestTimes(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/besttimes";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);

        if (response is @"{""result"":{""best"":[],""peak"":[]}}")
        {
            return new AnalyticVisitorBestTimes(zoneId, "n/a", "n/a", "n/a", "n/a");
        }

        var resultObj = responseObj["result"];

        return new AnalyticVisitorBestTimes(
            zoneId,
            resultObj["best"]["startTime"].Value<string>(),
            resultObj["best"]["endTime"].Value<string>(),
            resultObj["peak"]["startTime"].Value<string>(),
            resultObj["peak"]["endTime"].Value<string>());
    }

    public async Task<List<AnalyticVisitorCountHour>> GetVisitorPasserbyHour(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/counthour";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, double>>();

        var result = resultObj
            .Select(x => new AnalyticVisitorCountHour(
                Guid.NewGuid(),
                zoneId,
                x.Key,
                x.Value))
            .ToList();

        return result;
    }

    public async Task<List<AnalyticVisitorCountHourDay>> GetAnalyticVisitorCountHourDay(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/counthourday";
        var result = new List<AnalyticVisitorCountHourDay>();

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return result;
        }

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, JObject>>();

        foreach (var hourObj in resultObj)
        {
            var dayObj = hourObj.Value.ToObject<Dictionary<string, int>>();
            result.AddRange(dayObj.Select(d => new AnalyticVisitorCountHourDay(
                Guid.NewGuid(),
                zoneId,
                d.Key,
                hourObj.Key,
                d.Value
            )));
        }

        return result;
    }

    public async Task<List<AnalyticVisitorCountHourDayDetails>> GetAnalyticVisitorCountHourDayDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/counthourdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticVisitorCountHourDayDetails>();
        }

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, double>>();

        var result = resultObj
            .Select(x => new AnalyticVisitorCountHourDayDetails(
                Guid.NewGuid(),
                zoneId,
                x.Key,
                x.Value))
            .ToList();

        return result;
    }

    public async Task<List<AnalyticVisitorCountHourDayStart>> GetAnalyticVisitorCountHourDayStart(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/counthourdaystart";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticVisitorCountHourDayStart>();
        }

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, int>>();

        var result = resultObj
            .Select(x => new AnalyticVisitorCountHourDayStart(
                Guid.NewGuid(),
                zoneId,
                x.Key,
                x.Value))
            .ToList();

        return result;
    }

    public async Task<List<AnalyticVisitorCountSum>> GetAnalyticVisitorCountSum(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/countsum";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticVisitorCountSum>();
        }

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, double>>();

        var result = resultObj
            .Select(x => new AnalyticVisitorCountSum(
                Guid.NewGuid(),
                zoneId,
                x.Key,
                x.Value))
            .ToList();

        return result;
    }

    public async Task<AnalyticVisitorCountCommon> GetAnalyticVisitorCountCommon(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visitor/countcommon";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new AnalyticVisitorCountCommon(zoneId, null);
        }

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        var result = new AnalyticVisitorCountCommon(zoneId, resultObj.Value<int>());

        return result;
    }

    public async Task<AnalyticDeviceCount> GetAnalyticDeviceCount(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/devices/count";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new AnalyticDeviceCount(zoneId, null, null);
        }

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        var result = new AnalyticDeviceCount(zoneId, resultObj["android"].Value<double>(),
            resultObj["ios"].Value<double>());

        return result;
    }

    public async Task<AnalyticZonesGeneral> GetAnalyticZonesGeneral(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/zone/general";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        var result = new AnalyticZonesGeneral(
            zoneId,
            resultObj["percentage"].Value<int>(),
            resultObj["cnt"]?.Value<string>(),
            resultObj["average"].Value<int>(),
            resultObj["min"].Value<string>(),
            resultObj["max"].Value<string>());

        return result;
    }

    public async Task<List<AnalyticZonesVenn>> GetAnalyticZonesVenn(List<Zone> zones)
    {
        var urlPart = "/1/analytic/zone/venn";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{string.Join(',', zones.Select(x => x.Id))}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, double>>();

        return resultObj
            .Select(x => new AnalyticZonesVenn(
                x.Key,
                x.Value))
            .ToList();
    }

    public async Task<object> GetAnalyticZonesSankey(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/zone/sankey";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        return null;
    }

    public async Task<MediaVisitCount> GetMediaVisitCount(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/media/visit/count";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        return new MediaVisitCount(zoneId, resultObj.Value<int>("facebook"), resultObj.Value<int>("google"), resultObj.Value<int>("teemo"));
    }

    public async Task<List<MediaVisitCountDetails>> GetMediaVisitCountDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/media/visit/countdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<MediaVisitCountDetails>();
        }

        var responseObj = JObject.Parse(response);

        var resultFb = responseObj["result"]["facebook"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();
        var resultGl = responseObj["result"]["google"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();
        var resultTm = responseObj["result"]["teemo"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();

        return resultFb.Select(x => x.Key)
            .Concat(resultGl.Select(x => x.Key))
            .Concat(resultTm.Select(x => x.Key))
            .Distinct()
            .Select(d => new MediaVisitCountDetails(
                Guid.NewGuid(),
                zoneId,
                d,
                resultFb.ContainsKey(d) ? resultFb[d] : 0,
                resultGl.ContainsKey(d) ? resultGl[d] : 0,
                resultTm.ContainsKey(d) ? resultTm[d] : 0))
            .ToList();
    }

    public async Task<List<AnalyticSensorCountDetails>> GetAnalyticSensorCountDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/sensor/countdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithHeader("wts_token", _token)
            .WithTimeout(60 * 60) /* 1 hour */
            .SetQueryParam("zoneId", zoneId)
            .GetStringAsync();

        var responseObj = JObject.Parse(response);

        var resultPr = responseObj["result"]["presence"]?.ToObject<Dictionary<string, double>>() ??
                        new Dictionary<string, double>();
        var resultIn = responseObj["result"]["in"]?.ToObject<Dictionary<string, double>>() ??
                        new Dictionary<string, double>();
        var resultOut = responseObj["result"]["out"]?.ToObject<Dictionary<string, double>>() ??
                        new Dictionary<string, double>();
        var resultAbs = responseObj["result"]["absolute"]?.ToObject<Dictionary<string, double>>() ??
                        new Dictionary<string, double>();

        return resultPr.Select(x => x.Key)
            .Concat(resultIn.Select(x => x.Key))
            .Concat(resultOut.Select(x => x.Key))
            .Concat(resultAbs.Select(x => x.Key))
            .Distinct()
            .Select(d => new AnalyticSensorCountDetails(
                Guid.NewGuid(),
                zoneId,
                d,
                resultPr.ContainsKey(d) ? resultPr[d] : 0,
                resultIn.ContainsKey(d) ? resultIn[d] : 0,
                resultOut.ContainsKey(d) ? resultOut[d] : 0,
                resultAbs.ContainsKey(d) ? resultAbs[d] : 0))
            .ToList();
    }

    public async Task<List<AnalyticSensorCountDetails>> GetAnalyticSensorCountRaw(string sensorId)
    {
        var urlPart = "/1/analytic/sensor/countRaw";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("sensorId", sensorId)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        return null;
    }

    public async Task<AnalyticSystemLastUpdate> GetAnalyticSystemLastUpdate(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/system/lastupdate";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        if (response == @"{""result"":[]}")
        {
            return new AnalyticSystemLastUpdate(zoneId, null, null);
        }

        return new AnalyticSystemLastUpdate(zoneId, resultObj.Value<string>("date"), resultObj["needUpdate"]?.Value<bool>());
    }

    public async Task<AnalyticSystemQuickLastUpdate> GetAnalyticSystemQuickLastUpdate(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/system/lastupdate";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        if (response == @"{""result"":[]}")
        {
            return new AnalyticSystemQuickLastUpdate(zoneId, null, null);
        }

        return new AnalyticSystemQuickLastUpdate(zoneId, resultObj.Value<string>("date"), resultObj["needUpdate"]?.Value<bool>());
    }

    public async Task<AnalyticSystemForceRefresh> GetAnalyticSystemForceRefresh(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/system/forceRefresh";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        if (response == @"{""result"":[]}")
        {
            return new AnalyticSystemForceRefresh(zoneId, null);
        }

        return new AnalyticSystemForceRefresh(zoneId, resultObj.Value<bool>());
    }

    public async Task<AnalyticSystemTemporaryTable> GetAnalyticSystemTemporaryTable(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/system/temporaryTable";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"];

        if (response == @"{""result"":[]}")
        {
            return new AnalyticSystemTemporaryTable(zoneId, null, null);
        }

        return new AnalyticSystemTemporaryTable(zoneId, resultObj["context"]?.Value<string>(), resultObj["table"]?.Value<string>());
    }

    public async Task<List<AnalyticRawPasserby>> GetAnalyticRawServicePasserby(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/raw/passerby";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", start.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", end.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].Values<JObject>();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticRawPasserby>();
        }

        return resultObj.Select(x => new AnalyticRawPasserby(
            Guid.NewGuid(),
            zoneId,
            x["uid"].Value<string>(),
            x["userMac"].Value<string>(),
            x["dateStart"].Value<string>(),
            x["dateEnd"].Value<string>(),
            x["isLocal"].Value<string>())).ToList();
    }

    public async Task<List<AnalyticRawServiceVisitor>> GetAnalyticRawServiceVisitor(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/raw/visitor";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].Values<JObject>();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticRawServiceVisitor>();
        }

        return resultObj.Select(x => new AnalyticRawServiceVisitor(
            Guid.NewGuid(),
            zoneId,
            x["uid"].Value<string>(),
            x["userMac"].Value<string>(),
            x["dateStart"].Value<string>(),
            x["dateEnd"].Value<string>(),
            x["isLocal"].Value<string>(),
            x["cntVisit"].Value<string>(),
            x["zones"].Value<string>(),
            x["deviceType"].Value<string>(),
            x["frequency"].Value<string>(),
            x["maxProximity"].Value<double>()
        )).ToList();
    }

    public async Task<List<AnalyticRawServiceVisitorLight>> GetAnalyticRawServiceVisitorLight(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/raw/visitorLight";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].Values<JObject>();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticRawServiceVisitorLight>();
        }

        return resultObj.Select(x => new AnalyticRawServiceVisitorLight(
            Guid.NewGuid(),
            zoneId,
            x["uid"].Value<string>(),
            x["dateDay"].Value<string>(),
            x["dateStart"].Value<string>(),
            x["dateEnd"].Value<string>()
        )).ToList();
    }

    public async Task<List<AnalyticRawServiceVisitorMacList>> GetAnalyticRawServiceVisitorMacList(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/raw/visitorMacList";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].Values<string>();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticRawServiceVisitorMacList>();
        }

        return resultObj.Select(x => new AnalyticRawServiceVisitorMacList(Guid.NewGuid(), zoneId, x)).ToList();
    }

    public async Task<List<AnalyticRawServicePasserbyMacList>> GetAnalyticRawServicePasserbyMacList(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/raw/passerbyMacList";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].Values<string>();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticRawServicePasserbyMacList>();
        }

        return resultObj.Select(x => new AnalyticRawServicePasserbyMacList(Guid.NewGuid(), zoneId, x)).ToList();
    }

    public async Task<AnalyticVisitCount> GetAnalyticVisitCount(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visit/count";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .WithHeader("wts_token", _token)
            .GetStringAsync();

        var responseObj = JObject.Parse(response);

        var result = new AnalyticVisitCount(zoneId, responseObj["result"]["total"].Value<int>());

        return result;
    }

    public async Task<List<AnalyticVisitCountDetails>> GetAnalyticVisitCountDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visit/countdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":{""all"":[]}}")
        {
            return new List<AnalyticVisitCountDetails>();
        }

        var responseObj = JObject.Parse(response);

        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ??
                        new Dictionary<string, int>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticVisitCountDetails(Guid.NewGuid(), zoneId, d, resultAll[d]))
            .ToList();
    }

    public async Task<List<AnalyticVisitCountHourDetails>> GetAnalyticVisitCountHourDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visit/counthourdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response is @"{""result"":[]}" or @"{""result"":{""all"":[]}}")
        {
            return new List<AnalyticVisitCountHourDetails>();
        }

        var responseObj = JObject.Parse(response);

        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ?? new Dictionary<string, int>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticVisitCountHourDetails(Guid.NewGuid(), zoneId, d, resultAll[d]))
            .ToList();
    }

    public async Task<List<AnalyticVisitDuration>> GetAnalyticVisitDuration(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visit/duration";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticVisitDuration>();
        }

        var responseObj = JObject.Parse(response);

        var resultAll = responseObj["result"]?.ToObject<Dictionary<string, double>>() ??
                        new Dictionary<string, double>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticVisitDuration(Guid.NewGuid(), zoneId, d, resultAll[d]))
            .ToList();
    }

    public async Task<List<AnalyticVisitDurationDetails>> GetAnalyticVisitDurationDetails(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visit/durationdetails";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticVisitDurationDetails>();
        }

        var responseObj = JObject.Parse(response);

        var resultAll = responseObj["result"]?.ToObject<Dictionary<string, JObject>>() ??
                        new Dictionary<string, JObject>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticVisitDurationDetails(
                Guid.NewGuid(),
                zoneId,
                d,
                resultAll[d]["0"].Value<double>(),
                resultAll[d]["300"].Value<double>(),
                resultAll[d]["900"].Value<double>(),
                resultAll[d]["1800"].Value<double>()))
            .ToList();
    }

    public async Task<List<AnalyticVisitCountHour>> GetAnalyticVisitCountHour(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visit/counthour";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, double>>();

        var result = resultObj
            .Select(x => new AnalyticVisitCountHour(
                Guid.NewGuid(),
                zoneId,
                x.Key,
                x.Value))
            .ToList();

        return result;
    }

    public async Task<List<AnalyticVisitCountHourDay>> GetAnalyticVisitCountHourDay(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visit/counthourday";

        var result = new List<AnalyticVisitCountHourDay>();

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return result;
        }

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, JObject>>();

        foreach (var hourObj in resultObj)
        {
            var dayObj = hourObj.Value.ToObject<Dictionary<string, int>>();
            result.AddRange(dayObj.Select(d => new AnalyticVisitCountHourDay(
                Guid.NewGuid(),
                zoneId,
                d.Key,
                hourObj.Key,
                d.Value
            )));
        }

        return result;
    }

    public async Task<List<AnalyticVisitCountHourDayStart>> GetAnalyticVisitCountHourDayStart(string zoneId, DateTime start, DateTime end)
    {
        var urlPart = "/1/analytic/visit/counthourdaystart";

        var response = await _url.AppendPathSegment(urlPart)
            .WithTimeout(60 * 60) /* 1 hour */
            .WithHeader("wts_token", _token)
            .SetQueryParam("zones", $"[{zoneId}]")
            .SetQueryParam("days", "[]")
            .SetQueryParam("openingTimes", 0)
            .SetQueryParam("startDate", _startDateParam.ToString("yyyy-MM-dd hh:mm:ss"))
            .SetQueryParam("endDate", DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"))
            .GetStringAsync();

        if (response == @"{""result"":[]}")
        {
            return new List<AnalyticVisitCountHourDayStart>();
        }

        var responseObj = JObject.Parse(response);
        var resultObj = responseObj["result"].ToObject<Dictionary<string, int>>();

        var result = resultObj
            .Select(x => new AnalyticVisitCountHourDayStart(
                Guid.NewGuid(),
                zoneId,
                x.Key,
                x.Value))
            .ToList();

        return result;
    }
}
