using System;
using System.Collections.Generic;

bool CheckAdress(string ip)
{
    for (int i = 0; i < ip.Length; i++)
    {
        char ch = ip[i];
        if (!Char.IsDigit(ch) && ch != '.')
        {
            return false;
        }
    }

    string[] nums = ip.Split('.');

    if (nums.Length != 4)
    {
        return false;
    }

    foreach (string num in nums)
    {
        int val = Convert.ToInt32(num);
        if (val < 0 || val > 255)
        {
            return false;
        }
    }
    return true;
}

List<int> GetAdress(string ip)
{
    if (!CheckAdress(ip))
    {
        throw new Exception("Невалидный адресс");
    }

    string[] nums = ip.Split('.');
    var ipNums = new List<int>();

    foreach (string num in nums)
    {
        ipNums.Add(Convert.ToInt32(num));
    }

    return ipNums;
}

List<int> GetId(bool isHost, string ip, string mask)
{
    if (!CheckAdress(mask)) // Вроде для маски такие же правила валидности, как и для айпишника
    {
        throw new Exception("Невалидная маска");
    }

    var adress = GetAdress(ip);
    var intMask = GetAdress(mask); // Та же картина
    var hostId = new List<int>();

    for (int i = 0; i < adress.Count; i++)
    {
        int ipByte = adress[i];
        int maskByte = intMask[i];

        int val = isHost ? ipByte & ~maskByte : ipByte & maskByte;
        hostId.Add(val);
    }

    return hostId;
}

foreach (var val in GetId(true, "127.0.0.1", "255.255.0.0"))
    Console.WriteLine(val);