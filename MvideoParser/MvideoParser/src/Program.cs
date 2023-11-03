using MvideoParser.src.main;
using MvideoParser.src.model;
using MvideoParser.src.parameters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Web.Helpers;

internal class Program
{
    public static List<Product> productInfo = new List<Product>();
    private static void Main(string[] args)
    {
        var ParserWork = new ParseProcess<int[]>(new Parser());
        ParserWork.DataChangeEvent += NewElementInfo;
        ParserWork.SatusEvent += NewSatus;
        Start(ParserWork);
        Console.ReadLine();
    }

    private static void CreateJsonFiles()
    {
        var json = JsonConvert.SerializeObject(productInfo.ToArray());
        File.WriteAllText(@"F:\mvideo\MvideoParser\MvideoParser\Json\JsonAnswer.json", json);
    }


    private static void Start(ParseProcess<int[]> parserWork)
    {
        parserWork.ParamsParser = new ParserSettings(2, 3, "brand/apple-685/f/page={CurrentId}");
        parserWork.Started();
    }

    private static void NewSatus(object obj)
    {
        Console.WriteLine("Connection close...");
    }

    private static void NewElementInfo(object arg1, int[] arg2)
    {
        Random random = new Random();
        foreach (var item in arg2)
        {
            Console.WriteLine(item);
            var urlInfo = $"https://www.mvideo.ru/bff/product-details?productId={item}";
            var urlPrice = $"https://www.mvideo.ru/bff/products/prices?productIds={item}";
            Console.WriteLine(urlInfo);
            var info = MakeRequest(urlInfo, item, "body");
            Thread.Sleep(random.Next(1000, 5000));
            var price = MakeRequest(urlPrice, item, "body.materialPrices[0].price");
            Thread.Sleep(random.Next(1000, 5000));
            if (info != null && price != null)
            {
                var product = new Product
                {
                    Code = info.SelectToken("productId").ToString(),
                    Name = info.SelectToken("name").ToString(),
                    Price = (decimal)price.SelectToken("basePrice"),
                    Link = $"https://www.mvideo.ru/products/{info.SelectToken("productId")}",
                };
                productInfo.Add(product);
                Console.WriteLine("product add");
            }
            else
            {
                Console.WriteLine("RequestError");
                ParseProcess<int[]> process = arg1 as ParseProcess<int[]>;
                process.Abort();
                return;
            }
        }
        CreateJsonFiles();
    }

    private static JObject MakeRequest(string url, int item, string jsonElement)
    {
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 2,
        };

        using (var client = new HttpClient(handler))
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("authority", "www.mvideo.ru");
            request.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.Headers.Add("accept-language", "ru,en;q=0.9");
            request.Headers.Add("cache-control", "max-age=0");
            request.Headers.Add("cookie", "SMSError=; authError=; __lhash_=a5aaa941143b863153bdb7ea6764d720; MVID_ALFA_PODELI_NEW=true; MVID_CASCADE_CMN=true; MVID_CHAT_VERSION=4.16.4; MVID_CITY_ID=CityCZ_6273; MVID_CREDIT_DIGITAL=true; MVID_CREDIT_SERVICES=true; MVID_CRITICAL_GTM_INIT_DELAY=3000; MVID_DISPLAY_ACCRUED_BR=true; MVID_EMPLOYEE_DISCOUNT=true; MVID_FILTER_CODES=true; MVID_FILTER_TOOLTIP=1; MVID_FLOCKTORY_ON=true; MVID_GEOLOCATION_NEEDED=true; MVID_GTM_ENABLED=011; MVID_INTERVAL_DELIVERY=true; MVID_IS_NEW_BR_WIDGET=true; MVID_KLADR_ID=1800000100000; MVID_LAYOUT_TYPE=1; MVID_MINDBOX_DYNAMICALLY=true; MVID_NEW_LK_CHECK_CAPTCHA=true; MVID_NEW_LK_OTP_TIMER=true; MVID_NEW_MBONUS_BLOCK=true; MVID_PODELI_PDP=true; MVID_REGION_ID=21; MVID_REGION_SHOP=S929; MVID_SERVICES=111; MVID_SINGLE_CHECKOUT=true; MVID_SP=true; MVID_TIMEZONE_OFFSET=4; MVID_TYP_CHAT=true; MVID_WEB_SBP=true; SENTRY_ERRORS_RATE=0.1; SENTRY_TRANSACTIONS_RATE=0.5; _ym_uid=1698239091521977500; _ym_d=1698239091; MVID_GUEST_ID=23130303172; MVID_VIEWED_PRODUCTS=; wurfl_device_id=generic_web_browser; MVID_CALC_BONUS_RUBLES_PROFIT=false; NEED_REQUIRE_APPLY_DISCOUNT=true; MVID_CART_MULTI_DELETE=false; MVID_YANDEX_WIDGET=true; PROMOLISTING_WITHOUT_STOCK_AB_TEST=2; MVID_GET_LOCATION_BY_DADATA=DaData; PRESELECT_COURIER_DELIVERY_FOR_KBT=true; HINTS_FIO_COOKIE_NAME=2; searchType2=2; COMPARISON_INDICATOR=false; MVID_NEW_OLD=eyJjYXJ0IjpmYWxzZSwiZmF2b3JpdGUiOnRydWUsImNvbXBhcmlzb24iOnRydWV9; MVID_OLD_NEW=eyJjb21wYXJpc29uIjogdHJ1ZSwgImZhdm9yaXRlIjogdHJ1ZSwgImNhcnQiOiB0cnVlfQ==; tmr_lvid=2763b74fbfefbf59039e8cdd44498871; tmr_lvidTS=1698239091986; gdeslon.ru.__arc_domain=gdeslon.ru; gdeslon.ru.user_id=c242ddc2-c1ab-4e8c-8bba-983fca1a22b4; advcake_track_id=e7c1fe82-082a-7eac-6b52-fa6d544a1a64; advcake_session_id=8ef9e69a-43eb-a5bf-225f-e7a1be1566f2; adrcid=AnxYx1rk-mHQ4-em7gBEnNQ; flocktory-uuid=2524e797-c76b-4741-bbbe-2aeb444b9910-8; _gpVisits={\"isFirstVisitDomain\":true,\"idContainer\":\"100025D5\"}; uxs_uid=1614ee10-7337-11ee-8faa-93aed9614f63; afUserId=f9076274-fb79-4a3c-a7c9-436169f6a8cd-p; AF_SYNC=1698239092807; _ga=GA1.1.1920521422.1698239092; cookie_ip_add=92.39.216.66; _ym_isad=2; __SourceTracker=https%3A%2F%2Fya.ru%2F__referral; admitad_deduplication_cookie=other_referral; MVID_ENVCLOUD=prod1; flacktory=no; BIGipServeratg-ps-prod_tcp80=2483346442.20480.0000; bIPs=2105588670; MVID_GTM_BROWSER_THEME=1; deviceType=desktop; SMSError=; authError=; JSESSIONID=NgGSl7sMFyRS9DH3q2GhFhXw7VrF22YZzKmny4J6ZfwyH2CHwfPv!255883261; BIGipServeratg-ps-prod_tcp80_clone=2483346442.20480.0000; CACHE_INDICATOR=true; mindboxDeviceUUID=513cfdc7-f03a-48b4-9a73-8588404f37ba; directCrm-session=%7B%22deviceGuid%22%3A%22513cfdc7-f03a-48b4-9a73-8588404f37ba%22%7D; _ga_CFMZTSS5FM=GS1.1.1698403197.9.1.1698411993.0.0.0; _ga_BNX5WPP3YK=GS1.1.1698403197.9.1.1698411993.56.0.0; _sp_id.d61c=dfbe9662-0ff0-47bd-bac2-ee5a13b8a654.1698239091.10.1698411994.1698399111.0915cf62-6b1b-41e6-866e-bb3629bfff3c.57d2884f-1fe3-480e-99fb-bda74100bf4b.fbe24506-82f1-4799-a6da-e3c9122f6825.1698403200978.173; _gp100025D5={\"hits\":53,\"vc\":1,\"ac\":1,\"a6\":1}; tmr_detect=0%7C1698411999470; gssc218=; cfidsgib-w-mvideo=l2gPIpDeSfGuz3+7rWmz3LsTo+IKWpEhZcVlRWT9K3IM90z7JSr81BcBMp3ZCqdPzADpZh9viKLnWpsRH+Atum+hO0XmT+qMcLxd/+fKTbWn94cDBx8qJ46niuj1pDIAO1iYG5miST/m1aacFFRBrOUqCCQ1mzBSmGKi7y0=; gsscgib-w-mvideo=qMvYOeO7clcx4yXFGA6/u/rpftXlUAGkvoV6ZFk7PkksKI3IHhv5MLr/Povt249oQYmAXp/F9d7+gRNz60L9nDGc9SG+WGc+/ipNVBrXcfTQ/hNhFAiTMqeMRPfOWldaoibX0A1CQrbHmeYokeGifHEkShTpolTzJld68iS1jDXms62ALnEv9vGQrI2ImNlMgNZL/7eNw82egpbtGzYa9DndkO1bzS7t4GArkQOc5h0Mh5do3KcpttJWdAx7lX8=; gsscgib-w-mvideo=qMvYOeO7clcx4yXFGA6/u/rpftXlUAGkvoV6ZFk7PkksKI3IHhv5MLr/Povt249oQYmAXp/F9d7+gRNz60L9nDGc9SG+WGc+/ipNVBrXcfTQ/hNhFAiTMqeMRPfOWldaoibX0A1CQrbHmeYokeGifHEkShTpolTzJld68iS1jDXms62ALnEv9vGQrI2ImNlMgNZL/7eNw82egpbtGzYa9DndkO1bzS7t4GArkQOc5h0Mh5do3KcpttJWdAx7lX8=; fgsscgib-w-mvideo=X7n3add68c34bd68c64246d2927c9a8820bce794; fgsscgib-w-mvideo=X7n3add68c34bd68c64246d2927c9a8820bce794");
            request.Headers.Add("if-modified-since", "Wed, 25 Oct 2023 20:47:10 GMT");
            request.Headers.Add("if-none-match", "W/\"65397ece-a154\"");
            request.Headers.Add("referer", "https://www.mvideo.ru/brand/apple-685/f/brand/apple-685/f/page=2");
            request.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"116\", \"Not)A;Brand\";v=\"24\", \"YaBrowser\";v=\"23\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "document");
            request.Headers.Add("sec-fetch-mode", "navigate");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("sec-fetch-user", "?1");
            request.Headers.Add("upgrade-insecure-requests", "1");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.5845.931 YaBrowser/23.9.3.931 Yowser/2.5 Safari/537.36");



            HttpResponseMessage response = client.SendAsync(request).Result;
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var statusCode = (int)response.StatusCode;
                var content = response.Content.ReadAsStringAsync().Result;
                var result = CreateJsonAnswer(content, jsonElement);
                return JObject.Parse(result);
            }
            return null;
        }
    }

    private static string CreateJsonAnswer(string json, string tokenName)
    {
        JObject jsonAnswer = JObject.Parse(json);
        JToken token = jsonAnswer.SelectToken(tokenName);
        return token.ToString();
    }
}