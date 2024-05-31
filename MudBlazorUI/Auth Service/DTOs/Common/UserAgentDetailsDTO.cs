using System.Text.RegularExpressions;

public class UserAgentDetailsDTO
{
    public string BrowserName { get; set; }
    public string BrowserVersion { get; set; }
    public string OperatingSystem { get; set; }
    public string RenderingEngine { get; set; }
    public string DeviceType { get; set; }

    public static UserAgentDetailsDTO GetBrowser(string userAgent)
    {
        UserAgentDetailsDTO browserDetails = new UserAgentDetailsDTO();

        // Detect browser name
        string browser = "unknown";
        if (Regex.IsMatch(userAgent, @"ucbrowser", RegexOptions.IgnoreCase))
        {
            browser = "UCBrowser";
        }
        else if (Regex.IsMatch(userAgent, @"edg", RegexOptions.IgnoreCase))
        {
            browser = "Edge";
        }
        else if (Regex.IsMatch(userAgent, @"googlebot", RegexOptions.IgnoreCase))
        {
            browser = "GoogleBot";
        }
        else if (Regex.IsMatch(userAgent, @"chromium", RegexOptions.IgnoreCase))
        {
            browser = "Chromium";
        }
        else if (Regex.IsMatch(userAgent, @"firefox|fxios", RegexOptions.IgnoreCase) && !Regex.IsMatch(userAgent, @"seamonkey", RegexOptions.IgnoreCase))
        {
            browser = "Firefox";
        }
        else if (Regex.IsMatch(userAgent, @"; msie|trident", RegexOptions.IgnoreCase) && !Regex.IsMatch(userAgent, @"ucbrowser", RegexOptions.IgnoreCase))
        {
            browser = "IE";
        }
        else if (Regex.IsMatch(userAgent, @"chrome|crios", RegexOptions.IgnoreCase) && !Regex.IsMatch(userAgent, @"opr|opera|chromium|edg|ucbrowser|googlebot", RegexOptions.IgnoreCase))
        {
            browser = "Chrome";
        }
        else if (Regex.IsMatch(userAgent, @"safari", RegexOptions.IgnoreCase) && !Regex.IsMatch(userAgent, @"chromium|edg|ucbrowser|chrome|crios|opr|opera|fxios|firefox", RegexOptions.IgnoreCase))
        {
            browser = "Safari";
        }
        else if (Regex.IsMatch(userAgent, @"opr|opera", RegexOptions.IgnoreCase))
        {
            browser = "Opera";
        }

        // Detect browser version
        string browserVersion = browser switch
        {
            "UCBrowser" => GetVersion(userAgent, @"(ucbrowser)\/([\d\.]+)", 2),
            "Edge" => GetVersion(userAgent, @"(edge|edga|edgios|edg)\/([\d\.]+)", 2),
            "GoogleBot" => GetVersion(userAgent, @"(googlebot)\/([\d\.]+)", 2),
            "Chromium" => GetVersion(userAgent, @"(chromium)\/([\d\.]+)", 2),
            "Firefox" => GetVersion(userAgent, @"(firefox|fxios)\/([\d\.]+)", 2),
            "Chrome" => GetVersion(userAgent, @"(chrome|crios)\/([\d\.]+)", 2),
            "Safari" => GetVersion(userAgent, @"(safari)\/([\d\.]+)", 2),
            "Opera" => GetVersion(userAgent, @"(opera|opr)\/([\d\.]+)", 2),
            "IE" => GetIEVersion(userAgent),
            _ => "0.0.0.0"
        };

        // Detect operating system
        string operatingSystem = GetOperatingSystem(userAgent);

        // Detect rendering engine
        string renderingEngine = GetRenderingEngine(userAgent);

        // Detect device type
        string deviceType = GetDeviceType(userAgent);

        browserDetails.BrowserName = browser;
        browserDetails.BrowserVersion = browserVersion;
        browserDetails.OperatingSystem = operatingSystem;
        browserDetails.RenderingEngine = renderingEngine;
        browserDetails.DeviceType = deviceType;

        return browserDetails;
    }

    private static string GetVersion(string userAgent, string pattern, int versionGroup)
    {
        Match match = Regex.Match(userAgent, pattern, RegexOptions.IgnoreCase);
        if (match.Success && match.Groups.Count > versionGroup)
        {
            return match.Groups[versionGroup].Value;
        }
        return "0.0";
    }

    private static string GetIEVersion(string userAgent)
    {
        Match match = Regex.Match(userAgent, @"(trident)\/([\d\.]+)", RegexOptions.IgnoreCase);
        if (match.Success && match.Groups.Count > 2)
        {
            double tridentVersion = double.Parse(match.Groups[2].Value);
            return $"{tridentVersion + 4.0}";
        }
        return "7.0"; // Default IE version
    }

    private static string GetOperatingSystem(string userAgent)
    {
        // Parse operating system
        Match osMatch = Regex.Match(userAgent, @"\(([^;]+); [^;]+; [^)]+\)");
        if (osMatch.Success)
        {
            return osMatch.Groups[1].Value.Trim();
        }
        return "Unknown";
    }

    private static string GetRenderingEngine(string userAgent)
    {
        Match renderingEngineMatch = Regex.Match(userAgent, @"AppleWebKit/([\d.]+)");
        if (renderingEngineMatch.Success)
        {
            return "WebKit " + renderingEngineMatch.Groups[1].Value;
        }
        return "Unknown";
    }

    private static string GetDeviceType(string userAgent)
    {
        if (userAgent.Contains("Mobile"))
        {
            return "Mobile";
        }
        else if (userAgent.Contains("Tablet"))
        {
            return "Tablet";
        }
        return "Desktop";
    }
}