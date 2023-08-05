//Anti luna send message and more
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // URLs a bloquear
        string[] urlsToBlock =
        {
            "api.ipify.org", "ipapi.co", "ifconfig.co", "www.iplocation.net", "ipwhois.io", "www.whatismyip.com",
            "www.ipchicken.com", "www.myip.com", "www.ipfingerprints.com", "ip.gs", "iplookup.flagfox.net",
            "www.ipaddress.com", "www.ip2location.com", "www.ip-tracker.org", "www.iplocation.net",
            "www.findip-address.com", "www.ipaddressguide.com", "www.ip2whois.com", "www.ip-adress.com", "www.whois.com",
            "www.dnschecker.org", "www.whatismyip.net", "www.whatismyip.org", "www.whatismyip.live",
            "www.whatismyip-address.com", "www.whatismyip.co", "www.whatismyip.host", "www.whatismyip.online",
            "www.whatismyip.site", "www.whatismyip.today", "www.whatismyip.world", "www.whatismyipaddress.com",
            "www.whatismyiplocation.com", "www.whatismypublicip.com", "www.myipnumber.com", "www.checkmyip.com",
            "www.ip2.me", "www.myexternalip.com", "www.myipaddress.net", "www.showmyip.com", "www.yougetsignal.com",
            "www.browserleaks.com", "www.speedguide.net", "www.tcpiputils.com", "www.shodan.io", "www.censys.io",
            "dnslytics.com", "www.projecthoneypot.org", "ipdata.co", "ipgeolocation.io", "ipstack.com", "ipify.com",
            "whatismyipaddress.com", "iplocation.net", "whoer.net", "ipinfo.io", "ip-lookup.net", "geoplugin.com",
            "ip2country.info", "hostip.info", "iplocationtools.com", "ipaddressguide.com", "ip-tracker.org",
            "ipgeolocation.com", "freegeoip.app", "ipwhois.app", "ipdata.co", "geoip.nekudo.com", "ip-api.com",
            "ipapi.com", "ipstack.com", "iplocation.com", "ip-api.io", "ip-tracker.org", "iplookup.appliedops.net",
            "iplocation.netlify.app", "ipify.org", "ipgp.net", "ipinfodb.com", "ipaddress.com", "ifconfig.io",
            "ipgeolocation.com", "ipgeolocationapi.com", "ipgeolocation.xyz", "ipregistry.co", "ip2whois.com",
            "ipapi.net", "www.iplocation.info", "www.ip-tracker.com", "www.ip-lookup.org", "www.whois.net",
            "www.maxmind.com", "www.infosniper.net", "www.geobytes.com", "www.ip2country.net", "www.ipaddressguide.org",
            "www.iplocationlookup.com", "api.seeip.org", "api64.ipify.org","discord.com"
        };

        // Ruta del archivo hosts
        string hostsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers", "etc", "hosts");

        // Preguntar al usuario si desea bloquear las URLs
        Console.WriteLine("¿Desea bloquear las URLs para evitar el anti IP grabber? [Y/N]");
        string answer = Console.ReadLine().ToLower();

        if (answer == "y" || answer == "yes")
        {
            // Añadir las entradas para bloquear las URLs
            foreach (string urlToBlock in urlsToBlock)
            {
                if (!File.ReadLines(hostsFilePath).Any(line => line.Contains(urlToBlock)))
                {
                    using (StreamWriter sw = File.AppendText(hostsFilePath))
                    {
                        sw.WriteLine($"0.0.0.0 {urlToBlock}");
                    }

                    Console.WriteLine($"La URL {urlToBlock} ha sido bloqueada en el archivo hosts.");
                }
                else
                {
                    Console.WriteLine($"La URL {urlToBlock} ya se encuentra bloqueada, saltando paso...");
                }
            }
        }
        else if (answer == "n" || answer == "no")
        {
            Console.WriteLine("No se bloquearán las URLs.");
        }
        else
        {
            Console.WriteLine("Respuesta inválida.");
        }

        // Preguntar al usuario si desea borrar las URLs bloqueadas
        Console.WriteLine("¿Desea borrar todas las URLs bloqueadas? [Y/N]");
        string deleteAnswer = Console.ReadLine().ToLower();
        if (deleteAnswer == "y" || deleteAnswer == "yes")
        {

    // Borrar las entradas de las URLs bloqueadas
    foreach (string urlToBlock in urlsToBlock)
    {
        string lineToRemove = $"0.0.0.0 {urlToBlock}";
        string[] lines = File.ReadAllLines(hostsFilePath);
        lines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
        bool lineRemoved = false;

        // Buscar la línea a remover y borrarla
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains(lineToRemove))
            {
                lines[i] = "";
                lineRemoved = true;
            }
        }

        if (lineRemoved)
        {
            // Escribir el archivo hosts sin la línea eliminada
            File.WriteAllLines(hostsFilePath, lines);
            Console.WriteLine($"La URL {urlToBlock} ha sido desbloqueada en el archivo hosts.");
        }
        else
        {
            Console.WriteLine($"La URL {urlToBlock} no se encontró en el archivo hosts, saltando paso...");
        }
    }

        }
        else if (deleteAnswer == "n" || deleteAnswer == "no")
        {
            Console.WriteLine("No se borrarán las URLs bloqueadas.");
        }
        else
        {
            Console.WriteLine("Respuesta inválida.");
        }

        // Esperar a que el usuario presione una tecla
        Console.WriteLine("Presione una tecla para salir...");
        Console.ReadKey();
    }
}
