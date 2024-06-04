using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using Makaretu.Dns;
using System.Linq;
using System.Net.Sockets;
using System;
using extOSC;
using System.Net;
using UDP.GetAddress;
using UnityEditor.ShaderGraph;


public class ButtonInteraction : MonoBehaviour
{
    
    //public TextMeshProUGUI simpleUIText;
    [SerializeField] private string LogicIp;

    [Header("OSC Settings")]
    //public OSCTransmitter Transmitter;
    public GameObject ReceiverObject;
    public testlerp lerp;
    public OSCReceiver Receiver;
    private MulticastService mdns;
    private ServiceDiscovery sd;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OnButton1Clicked();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnButton2Clicked();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnButton3Clicked();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnButton4Clicked();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnButton5Clicked();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnButton6Clicked();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnButton7Clicked();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            OnButton8Clicked();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnButton9Clicked();
        }
        if (Input.GetKeyDown(KeyCode.X))
        { 
            OnButton10Clicked();
        }
    }

    private void OnButton9Clicked()
    {

        ReceiverObject.SetActive(true);
        var scale = new Vector3(1.0f, 1.0f, 1.0f);
        lerp.Grow(scale);
    }
    private void OnButton10Clicked()
    {

        
        
        lerp.Shrink();
        //ReceiverObject.SetActive(false);
    }

    public void OnButton1Clicked()
    {
        Debug.Log("Button1 is clicked");
        var ni = NetworkInterface
        .GetAllNetworkInterfaces();
        sd = new ServiceDiscovery();
        IPAddress iPAddress = null;
        
        var address = ni[1].GetIPProperties().UnicastAddresses.Where(o => o.Address.AddressFamily == AddressFamily.InterNetwork).Select(o => o.Address);
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach(var ip in host.AddressList)
        {
            Debug.Log(ip.ToString());
            iPAddress = ip;
        }
        //Receiver.LocalPort = 6000;
        //Receiver.Bind("*", MessageReceived);
        //foreach ( var address in addresses)
        //{
        //    var test = address.Address;
        //}
        var serviceProfile = new ServiceProfile("MasterBitch[Killer](TestStuff)", "_osc._udp", 6000, host.AddressList);  
        sd.Advertise(serviceProfile);
        //var test2 = 1;
        
       
    }

    public void Found(string test)
    {
        Debug.Log("found");
    }
    public void OnButton2Clicked()
    {
    
        Debug.Log("Button2 is clicked");
        mdns = new MulticastService();
        sd = new ServiceDiscovery(mdns);

        mdns.NetworkInterfaceDiscovered += (s, e) =>
        {
            foreach (var nic in e.NetworkInterfaces)
            {
                Debug.Log($"NIC '{nic.Name}'");
            }

            // Ask for the name of all services.
            sd.QueryAllServices();
        };

        sd.ServiceDiscovered += (s, serviceName) =>
        {
            Debug.Log($"service '{serviceName}'");

            // Ask for the name of instances of the service.
            mdns.SendQuery(serviceName, type: DnsType.PTR);
        };

        sd.ServiceInstanceDiscovered += (s, e) =>
        {
            Debug.Log($"service instance '{e.ServiceInstanceName}'");

            // Ask for the service instance details.
            mdns.SendQuery(e.ServiceInstanceName, type: DnsType.SRV);
        };

        mdns.AnswerReceived += (s, e) =>
        {
            // Is this an answer to a service instance details?
            var servers = e.Message.Answers.OfType<SRVRecord>();
            foreach (var server in servers)
            {
                Debug.Log($"host '{server.Target}' for '{server.Name}'");

                // Ask for the host IP addresses.
                mdns.SendQuery(server.Target, type: DnsType.A);
                mdns.SendQuery(server.Target, type: DnsType.AAAA);
            }

            // Is this an answer to host addresses?
            var addresses = e.Message.Answers.OfType<AddressRecord>();
            foreach (var address in addresses)
            {
                Debug.Log($"host '{address.Name}' at {address.Address}");
            }

        };

        try
        {
            mdns.Start();
            Console.ReadKey();
        }
        finally
        {
            sd.Dispose();
            mdns.Stop();
        }
    

        //var mdns = new MulticastService();
        
        //var sd = new ServiceDiscovery(mdns);
        //sd.ServiceDiscovered += (s, serviceName) =>
        //{
        //    Console.WriteLine($"service '{serviceName}'");

        //    // Ask for the name of instances of the service.
        //    // mdns.SendQuery(serviceName, type: DnsType.PTR);
        //};


        //sd.ServiceInstanceDiscovered += (s, e) =>
        //{
        //    Console.WriteLine($"service instance '{e.ServiceInstanceName}'");

        //    // Ask for the service instance details.
        //    // mdns.SendQuery(e.ServiceInstanceName, type: DnsType.SRV);
        //};

        //// Ask for the name of all services.
        //sd.QueryServiceInstances("_udp");
        //var service = new ServiceProfile("LogicControl", "_osc._udp", 6000);
        //var sd = new ServiceDiscovery();
        //sd.Advertise(service);



        //mdns.AnswerReceived += (s, e) =>
        //{
        //    // Is this an answer to a service instance details?
        //    var servers = e.Message.Answers.OfType<SRVRecord>();
        //    foreach (var server in servers)
        //    {
        //        Console.WriteLine($"host '{server.Target}' for '{server.Name}'");

        //        // Ask for the host IP addresses.
        //        mdns.SendQuery(server.Target, type: DnsType.A);
        //        mdns.SendQuery(server.Target, type: DnsType.AAAA);
        //    }

        //    // Is this an answer to host addresses?
        //    var addresses = e.Message.Answers.OfType<AddressRecord>();
        //    foreach (var address in addresses)
        //    {
        //        Console.WriteLine($"host '{address.Name}' at {address.Address}");
        //    }
        //};
        //mdns.Start();
        //var serviceDiscovery = new ServiceDiscovery();
        //serviceDiscovery.ServiceDiscovered += (s, serviceName) =>
        //{
        //    Debug.Log($"Service '{serviceName}' discovered.");
        //};
        //serviceDiscovery.ServiceInstanceDiscovered += (s, e) =>
        //{
        //    Debug.Log($"Service instance '{e.ServiceInstanceName}' discovered.");
        //    if (e.ServiceInstanceName.ToString().Contains("Logic Pro"))
        //    {
        //        var addresses = e.Message.AdditionalRecords
        //            .OfType<ARecord>()
        //            .Select(a => a.Address)
        //            .ToArray();
        //        Debug.Log($"IP addresses: {String.Join(", ", addresses.ToList())}");
        //        LogicIp = addresses[0].ToString();
        //    }
        
        //serviceDiscovery.QueryServiceInstances("_udp");
    }


    public void OnButton3Clicked()
    {
        //simpleUIText.text = "Button3 is clicked";
        //LogicIp = networkManager.Client.ServerIP;
        Debug.Log("Button3 clicked");
        string Address = "/1/volume1";
        var message = new OSCMessage(Address);
        message.AddValue(OSCValue.Float(0.5f));
        //Transmitter.RemoteHost = LogicIp;

        //// Set remote port;
        //Transmitter.RemotePort = 7000;
        //Transmitter.Send(message);
    }

    public void MessageReceived(OSCMessage message)
    {
        Debug.Log(message);
    }

    public void DisplayIP()
    {
        
        Debug.Log(LogicIp);

    }

    public void OnButton4Clicked()
    {
        string ip = MyUdpClient.Start();
        LogicIp = ip;
        DisplayIP();
    }

    public void OnButton8Clicked()
    {
        sd.Dispose();
    }

    public void OnButton5Clicked()
    {
        Debug.Log("Button5 clicked");
        string Address = "/test";
        var message = new OSCMessage(Address);
        var noteOn = "On";
        var note = 60;
        var velocity = 0.56f;
        message.AddValue(OSCValue.String(noteOn));
        message.AddValue(OSCValue.Int(note));
        message.AddValue(OSCValue.Float(velocity));
        //Transmitter.RemoteHost = LogicIp;

        // Set remote port;
        //Transmitter.RemotePort = 7001;
        //Transmitter.Send(message);

    }

    public void OnButton6Clicked()
    {
        Debug.Log("Button6 clicked");
        string Address = "/test";
        var message = new OSCMessage(Address);
        var noteOn = "Off";
        var note = 60;
        var velocity = 100.0f;
        message.AddValue(OSCValue.String(noteOn));
        message.AddValue(OSCValue.Int(note));
        message.AddValue(OSCValue.Float(velocity));
        //Transmitter.RemoteHost = LogicIp;

        //// Set remote port;
        //Transmitter.RemotePort = 7001;
        //Transmitter.Send(message);

    }

    public void OnButton7Clicked()
    {
        Debug.Log("Button6 clicked");
        string Address = "/cs/mixer/level";
        var message = new OSCMessage(Address);
        var noteOn = "";
        //var note = 60;
        //var velocity = 100.0f;
        message.AddValue(OSCValue.String(noteOn));
        //message.AddValue(OSCValue.Int(note));
        //message.AddValue(OSCValue.Float(velocity));
        //Transmitter.RemoteHost = LogicIp;

        //// Set remote port;
        //Transmitter.RemotePort = 7000;
        //Transmitter.Send(message);

    }

    //void Start()
    //{
    //    serviceBrowser = new ServiceBrowser();
    //    serviceBrowser.ServiceAdded += ServiceAdded;
    //    serviceBrowser.Browse("_http._tcp", "local");
    //}

    //private void ServiceAdded(object o, ServiceBrowseEventArgs args)
    //{
    //    IResolvableService service = args.Service;
    //    service.Resolved += ServiceResolved;
    //    service.Resolve();
    //}

    //private void ServiceResolved(object o, ServiceResolvedEventArgs args)
    //{
    //    IResolvableService service = args.Service;
    //    Debug.Log("Service Resolved: " + service.FullName);
    //}

    public void OnDestroy()
    {
       // sd.Dispose();
    }

    public void OnApplicationQuit()
    {
        //sd.Dispose();
    }
    


}
