using System;
using System.Collections.Generic;

// Non-OOP code on C# :p

bool CheckSequence(string ip)
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

List<int> SequenceToInt(string ip)
{
    if (!CheckSequence(ip))
    {
        throw new Exception("Invalid sequence!");
    }

    string[] nums = ip.Split('.');
    var ipNums = new List<int>();

    foreach (string num in nums)
    {
        ipNums.Add(Convert.ToInt32(num));
    }

    return ipNums;
}

bool CheckMask(List<int> mask)
{
    foreach (int maskByte in mask)
    {
        string binary = Convert.ToString(maskByte, 2);
        bool metZero = false;
        foreach(char bin in binary)
        {
            if (bin == '0' && !metZero)
            {
                metZero = true;
            } else if (metZero)
            {
                return false;
            }
        }
    }

    return true;
}

List<int> GetId(bool isHost, string ip, string mask)
{
    var intMask = SequenceToInt(mask);
    if (!CheckMask(intMask))
    {
        throw new Exception("Invalid mask!");
    }

    var adress = SequenceToInt(ip);
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

foreach (var val in GetId(false, "127.0.0.1", "255.255.0.0"))
    Console.Write($"{val} ");