using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class GetData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JObject json = new JObject();
        json["login_id"] = "user123";
        json["score"] = 6;

        JArray jsonArray = new JArray();

        jsonArray.Add(json);

        for (int i = 0; i < 2; i++)
        {
            json = new JObject();
            json["name"] = "brokendish";
            json["category"] = "normal";
            json["count"] = i;
            json["clear"] = i;

            jsonArray.Add(json);
        }

        print(jsonArray.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

}
