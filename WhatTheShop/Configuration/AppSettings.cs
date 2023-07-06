namespace WhatTheShop.Configuration;

public record ConnectionStringSettings(string Scheduler = default, string Extractor = default);

public record WhatTheShopApiSettings(string URL = default, string UserName = default, string Password = default);

public record AppSettings(ConnectionStringSettings ConnectionStrings = default, WhatTheShopApiSettings WhatTheShopApi = default);