using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : Singleton<MobManager> {

	protected MobManager() { }

    public string myGlovalVar = "whatever";
    public int MobCount() { return 100; }
}
