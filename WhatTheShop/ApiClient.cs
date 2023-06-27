using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using WhatTheShop.Models;

namespace WhatTheShop;

public class ApiClient
{
    /*
     https://api.whattheshop.net/doc/1/index.html
     */
    private readonly string _url;
    private readonly string _userName;
    private readonly string _userPassword;
    private readonly DateTime _startDateParam;

    private readonly string _token;

    public ApiClient()
    {
        _url = "https://api.whattheshop.net";
        
        _userName = Environment.GetEnvironmentVariable("whattheshop_UserName", EnvironmentVariableTarget.User)!;
        _userPassword = Environment.GetEnvironmentVariable("whattheshop_Password", EnvironmentVariableTarget.User)!;

        _startDateParam = DateTime.Now.AddYears(-2).AddDays(1); // the only 2 last years available
        //_startDateParam = DateTime.Now.AddMonths(0).AddDays(-2); // the only 2 last years available

        _token = this.GetToken().GetAwaiter().GetResult();
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
    public async Task<AnalyticPasserbyCount> GetAnalyticPasserbyCount(string zoneId)
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

    public async Task<List<AnalyticPasserbyCountDetails>> GetAnalyticPasserbyCountDetails(string zoneId)
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

        var resultNew = responseObj["result"]["new"]?.ToObject<Dictionary<string, int>>() ??  new Dictionary<string, int>();
        var resultLow = responseObj["result"]["low"]?.ToObject<Dictionary<string, int>>() ?? new Dictionary<string, int>();
        var resultFrq = responseObj["result"]["frequent"]?.ToObject<Dictionary<string, int>>() ?? new Dictionary<string, int>();
        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ?? new Dictionary<string, int>();

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

    public async Task<List<AnalyticPasserbyCountHour>> GetAnalyticPasserbyHour(string zoneId)
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

    public async Task<object> GetAnalyticSensorCount(List<Zone> zones)
    {
        var urlPart = "/1/analytic/sensor/count";

        foreach (var zone in zones)
        {
            var response = await _url.AppendPathSegment(urlPart)
                .WithTimeout(60 * 60) /* 1 hour */
                .SetQueryParam("zoneId", zone.Id)
                .WithHeader("wts_token", _token)
                .GetStringAsync();
        }

        return null;
    }

    public async Task<AnalyticPassingCount> GetAnalyticPassingCount(string zoneId)
    {
        var urlPart = "/1/analytic/passing/count";

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

    public async Task<List<CampaignCount>> GetCampaignCount(List<Zone> zones, List<CampaignList> campaignLists)
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

    public async Task<List<AnalyticPasserbyCountDay>> GetAnalyticPasserbyCountDay(string zoneId)
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

    public async Task<List<AnalyticPasserbyCountSum>> GetAnalyticPasserbyCountSum(string zoneId)
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

    public async Task<AnalyticPasserbyBestTimes> GetAnalyticPasserbyBestTimes(string zoneId)
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

    public async Task<AnalyticPasserbyCountCommon> GetAnalyticPasserbyCountCommons(string zoneId)
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

    public async Task<List<AnalyticPassingCountDetails>> GetAnalyticPassingCountDetails(string zoneId)
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

        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ?? new Dictionary<string, int>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticPassingCountDetails(Guid.NewGuid(), zoneId, d, resultAll[d]))
            .ToList();
    }

    public async Task<List<AnalyticPassingCountHourDetails>> GetAnalyticPassingCountHourDetails(string zoneId)
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

        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ?? new Dictionary<string, int>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticPassingCountHourDetails(Guid.NewGuid(), zoneId, d, resultAll[d]))
            .ToList();
    }

    public async Task<AnalyticVisitorCount> GetAnalyticVisitorCount(string zoneId)
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

    public async Task<List<AnalyticVisitorCountDetails>> GetAnalyticVisitorCountDetails(string zoneId)
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

        var resultNew = responseObj["result"]["new"]?.ToObject<Dictionary<string, int>>() ??  new Dictionary<string, int>();
        var resultLow = responseObj["result"]["low"]?.ToObject<Dictionary<string, int>>() ?? new Dictionary<string, int>();
        var resultFrq = responseObj["result"]["frequent"]?.ToObject<Dictionary<string, int>>() ?? new Dictionary<string, int>();
        var resultAll = responseObj["result"]["all"]?.ToObject<Dictionary<string, int>>() ?? new Dictionary<string, int>();

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

    public async Task<List<AnalyticVisitorDuration>> GetAnalyticVisitorDurations(string zoneId)
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

        var resultAll = responseObj["result"]?.ToObject<Dictionary<string, double>>() ?? new Dictionary<string, double>();

        return resultAll.Select(x => x.Key)
            .Distinct()
            .Select(d => new AnalyticVisitorDuration(Guid.NewGuid(), zoneId, d, resultAll[d]))
            .ToList();
    }

    public async Task<List<AnalyticVisitorDurationDetails>> GetAnalyticVisitorDurationDetails(string zoneId)
    {
        var urlPart = "/1/analytic/visitor/durationdetails";
        zoneId = "10025";
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

        var resultAll = responseObj["result"]?.ToObject<Dictionary<string, JObject>>() ?? new Dictionary<string, JObject>();

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

    public async Task<AnalyticVisitorBestTimes> GetAnalyticVisitorBestTimes(string zoneId)
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

    public async Task<List<AnalyticVisitorCountHour>> GetVisitorPasserbyHour(string zoneId)
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

    public async Task<List<AnalyticVisitorCountHourDay>> GetAnalyticVisitorCountHourDay(string zoneId)
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

    public async Task<List<AnalyticVisitorCountHourDayDetails>> GetAnalyticVisitorCountHourDayDetails(string zoneId)
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

    public async Task<List<AnalyticVisitorCountHourDayStart>> GetAnalyticVisitorCountHourDayStart(string zoneId)
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

    public async Task<List<AnalyticVisitorCountSum>> GetAnalyticVisitorCountSum(string zoneId)
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

    public async Task<AnalyticVisitorCountCommon> GetAnalyticVisitorCountCommon(string zoneId)
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

    public async Task<AnalyticDeviceCount> GetAnalyticDeviceCount(string zoneId)
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

        var result = new AnalyticDeviceCount(zoneId, resultObj["android"].Value<double>(), resultObj["ios"].Value<double>());

        return result;
    }
}