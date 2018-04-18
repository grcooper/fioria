using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Combo {
    public enum Key {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public string name;
    public List<Key> comboList;

    public Combo(string name, List<Key> comboList) {
        this.name = name;
        this.comboList = comboList;
    }

    public bool Equal(List<Key> c2) {
        return comboList.SequenceEqual(c2);
    }
}