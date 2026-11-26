using System.Xml;
using System.Xml.Linq;

namespace POS;

internal sealed record ApplicationConfig(
    string AdminPassword,
    string StoreName,
    string Branch)
{
    private const string ConfigFileName = "application_config";

    internal static string GetConfigPath()
    {
        string currentDirectoryPath = Path.Combine(Environment.CurrentDirectory, ConfigFileName);
        return File.Exists(currentDirectoryPath)
            ? currentDirectoryPath
            : Path.Combine(AppContext.BaseDirectory, ConfigFileName);
    }

    internal static ApplicationConfig Load(string configPath)
    {
        const string openingTag = "<admincreds>";
        const string closingTag = "</admincreds>";
        string contents = File.ReadAllText(configPath).Trim();
        if (contents.StartsWith(openingTag, StringComparison.Ordinal) &&
            contents.EndsWith(closingTag, StringComparison.Ordinal) &&
            contents.Length > openingTag.Length + closingTag.Length)
        {
            string password = contents.Substring(
                openingTag.Length,
                contents.Length - openingTag.Length - closingTag.Length);
            return new ApplicationConfig(password, string.Empty, string.Empty);
        }

        XDocument document;
        try
        {
            document = XDocument.Parse(contents);
        }
        catch (XmlException exception)
        {
            throw new InvalidDataException("The application_config file is not valid XML.", exception);
        }

        XElement root = document.Root
            ?? throw new InvalidDataException("The application_config file is empty.");

        if (root.Name != "applicationconfig")
        {
            throw new InvalidDataException("The application_config file has an unsupported format.");
        }

        return new ApplicationConfig(
            ReadRequiredValue(root, "admincreds"),
            ReadRequiredValue(root, "storename"),
            ReadRequiredValue(root, "branch"));
    }

    internal static void Save(string configPath, ApplicationConfig configuration)
    {
        XDocument document = new(
            new XElement("applicationconfig",
                new XElement("admincreds", VerifyXmlText(configuration.AdminPassword)),
                new XElement("storename", VerifyXmlText(configuration.StoreName)),
                new XElement("branch", VerifyXmlText(configuration.Branch))));

        string directory = Path.GetDirectoryName(configPath)
            ?? throw new InvalidDataException("The application_config path has no parent directory.");
        string temporaryPath = Path.Combine(
            directory,
            $".{ConfigFileName}.{Guid.NewGuid():N}.tmp");

        try
        {
            document.Save(temporaryPath);
            File.Move(temporaryPath, configPath);
        }
        finally
        {
            if (File.Exists(temporaryPath))
            {
                File.Delete(temporaryPath);
            }
        }
    }

    private static string ReadRequiredValue(XElement root, string elementName)
    {
        string? value = root.Element(elementName)?.Value;
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidDataException($"The {elementName} value is missing from application_config.");
        }

        return value;
    }

    private static string VerifyXmlText(string value)
    {
        try
        {
            return XmlConvert.VerifyXmlChars(value);
        }
        catch (XmlException exception)
        {
            throw new InvalidDataException("Configuration values contain characters that cannot be saved.", exception);
        }
    }
}
