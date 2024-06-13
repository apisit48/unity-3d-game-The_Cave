using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint
{
    public string ItemName;
    public string Req1;
    public string Req2;
    public string Req3;
    public int Req1Amount;
    public int Req2Amount;
    public int Req3Amount;

    public int numOfReq;

    public Blueprint(string name, int reqNum, string r1, int r1num , string r2, int r2num ){
        ItemName = name;
        numOfReq = reqNum;
        Req1 = r1;
        Req2 = r2;

        Req1Amount = r1num;
        Req2Amount = r2num;
    
    }
    public Blueprint(string name, int reqNum, string r1, int r1num ){
        ItemName = name;
        numOfReq = reqNum;
        Req1 = r1;
        Req1Amount = r1num;
        
    
    }

        public Blueprint(string name, int reqNum, string r1, int r1num , string r2, int r2num , string r3, int r3num ){
        ItemName = name;
        numOfReq = reqNum;
        Req1 = r1;
        Req2 = r2;
        Req3 = r3;

        Req1Amount = r1num;
        Req2Amount = r2num;
        Req3Amount = r3num;
    
    }


}
