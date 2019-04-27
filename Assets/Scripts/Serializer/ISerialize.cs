using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerialize
{
    string Serialize();
    void Deserialize(string json);
}
