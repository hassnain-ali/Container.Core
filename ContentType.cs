namespace Container.Core;
//
// Summary:
//     A list of internet media types, which are a standard identifier used on the Internet
//     to indicate the type of data that a file contains. Web browsers use them to determine
//     how to display, output or handle files and search engines use them to classify
//     data files on the web.
public static class ContentType
{
    //
    // Summary:
    //     Atom feeds.
    public const string Atom = "application/atom+xml";

    //
    // Summary:
    //     HTML; Defined in RFC 2854.
    public const string Html = "text/html";

    //
    // Summary:
    //     Form URL Encoded.
    public const string FormUrlEncoded = "application/x-www-form-urlencoded";

    //
    // Summary:
    //     GIF image; Defined in RFC 2045 and RFC 2046.
    public const string Gif = "image/gif";

    //
    // Summary:
    //     JPEG JFIF image; Defined in RFC 2045 and RFC 2046.
    public const string Jpg = "image/jpeg";

    //
    // Summary:
    //     JavaScript Object Notation JSON; Defined in RFC 4627.
    public const string Json = "application/json";

    //
    // Summary:
    //     JSON Patch; Defined at http://jsonpatch.com/.
    public const string JsonPatch = "application/json-patch+json";

    //
    // Summary:
    //     Web App Manifest.
    public const string Manifest = "application/manifest+json";

    //
    // Summary:
    //     Multi-part form daata; Defined in RFC 2388.
    public const string MultipartFormData = "multipart/form-data";

    //
    // Summary:
    //     Portable Network Graphics; Registered,[8] Defined in RFC 2083.
    public const string Png = "image/png";

    //
    // Summary:
    //     Problem Details JavaScript Object Notation (JSON); Defined at https://tools.ietf.org/html/rfc7807.
    public const string ProblemJson = "application/problem+json";

    //
    // Summary:
    //     Problem Details Extensible Markup Language (XML); Defined at https://tools.ietf.org/html/rfc7807.
    public const string ProblemXml = "application/problem+xml";

    //
    // Summary:
    //     REST'ful JavaScript Object Notation (JSON); Defined at http://restfuljson.org/.
    public const string RestfulJson = "application/vnd.restful+json";

    //
    // Summary:
    //     Rich Site Summary; Defined by Harvard Law.
    public const string Rss = "application/rss+xml";

    //
    // Summary:
    //     Textual data; Defined in RFC 2046 and RFC 3676.
    public const string Text = "text/plain";

    //
    // Summary:
    //     Extensible Markup Language; Defined in RFC 3023.
    public const string Xml = "application/xml";

    //
    // Summary:
    //     Compressed ZIP.
    public const string Zip = "application/zip";
}