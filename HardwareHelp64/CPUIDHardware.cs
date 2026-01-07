using System;
using System.Collections.Generic;

namespace HardwareHelp
{
    public class CPUIDHardware : Hardware
    {
      private CPUIDSDK _pSdk;

      public override bool Start()
      {
        try
        {
          this.Stop();
          int _version = 0;
          int _errorcode = 0;
          int _extended_errorcode = 0;
          this._pSdk = new CPUIDSDK();
          if (!this._pSdk.CreateInstance())
            throw new Exception("CreateInstance Error");
          bool flag = this._pSdk.Init2((uint) int.MaxValue, ref _errorcode, ref _extended_errorcode);
          if (_errorcode != 0)
          {
            string message;
            switch ((uint) _errorcode)
            {
              case 1:
                switch ((uint) _extended_errorcode)
                {
                  case 1:
                    message = "You are running a trial version of the DLL SDK. In order to make it work, please run CPU-Z at the same time.";
                    break;
                  case 2:
                    message = "Evaluation version has expired.";
                    break;
                  default:
                    message = "Eval version error " + _extended_errorcode.ToString();
                    break;
                }
                break;
              case 2:
                message = "Driver error " + _extended_errorcode.ToString();
                break;
              case 4:
                message = "Virtual machine detected.";
                break;
              case 8:
                message = "SDK mutex locked.";
                break;
              default:
                message = "Error code 0x%X" + _errorcode.ToString();
                break;
            }
            throw new Exception(message);
          }
          if (!flag)
            throw new Exception("Init Error");
          if (flag)
            this._pSdk.GetDllVersion(ref _version);
          return base.Start();
        }
        catch (Exception ex)
        {
          Log.WriteLog("CPUIDHelp Start Error=" + ex.Message);
          this.Stop();
          return false;
        }
      }

      public override void Stop()
      {
        try
        {
          base.Stop();
          if (this._pSdk == null)
            return;
          this._pSdk.Close();
          this._pSdk.DestroyInstance();
          this._pSdk = (CPUIDSDK) null;
        }
        catch
        {
        }
      }

      public override bool ReadRunData()
      {
        try
        {
          if (this.sharedRunData.CreateNew)
            this._pSdk.RefreshInformation();
          return base.ReadRunData();
        }
        catch
        {
          return false;
        }
      }

      public override List<Dictionary<string, object>> GetFanSpeed()
      {
        try
        {
          List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
          string _szName = "";
          int _raw_value = 0;
          float _min_value = 0.0f;
          float _max_value = 0.0f;
          int _sensor_id = 0;
          float num = 0.0f;
          int numberOfDevices = this._pSdk.GetNumberOfDevices();
          for (int _device_index = 0; _device_index < numberOfDevices; ++_device_index)
          {
            int numberOfSensors = this._pSdk.GetNumberOfSensors(_device_index, 12288 /*0x3000*/);
            if (numberOfSensors > 0)
            {
              this._pSdk.GetDeviceName(_device_index);
              if (this._pSdk.GetDeviceClass(_device_index) == 1024 /*0x0400*/)
              {
                for (int _sensor_index = 0; _sensor_index < numberOfSensors; ++_sensor_index)
                {
                  if (this._pSdk.GetSensorInfos(_device_index, _sensor_index, 12288 /*0x3000*/, ref _sensor_id, ref _szName, ref _raw_value, ref num, ref _min_value, ref _max_value) && Math.Round((double) num, 0) >= 0.0)
                  {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("name", (object) _szName);
                    dictionary.Add("speed", (object) num);
                    if (_szName.ToLower().IndexOf("cpu") >= 0)
                      dictionaryList.Insert(0, dictionary);
                    else
                      dictionaryList.Add(dictionary);
                  }
                }
              }
            }
          }
          return dictionaryList.Count >= 1 ? dictionaryList : (List<Dictionary<string, object>>) null;
        }
        catch (Exception ex)
        {
          Log.WriteLog("GetFanSpeed Error:" + ex.ToString());
          return (List<Dictionary<string, object>>) null;
        }
      }

      public override List<Dictionary<string, object>> GetCpuRunInfo()
      {
        try
        {
          List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
          Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
          Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
          Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
          Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
          string _szName = "";
          float num1 = 0.0f;
          int num2 = 0;
          int _raw_value = 0;
          float _min_value = 0.0f;
          float _max_value = 0.0f;
          int _sensor_id = 0;
          float _f = 0.0f;
          List<string> stringList = new List<string>();
          dictionary1.Add("name", (object) "packge");
          int numberOfDevices = this._pSdk.GetNumberOfDevices();
          for (int _device_index = 0; _device_index < numberOfDevices; ++_device_index)
          {
            this._pSdk.GetDeviceName(_device_index);
            if (this._pSdk.GetDeviceClass(_device_index) == 4)
            {
              int numberOfSensors1 = this._pSdk.GetNumberOfSensors(_device_index, 8192 /*0x2000*/);
              for (int _sensor_index = 0; _sensor_index < numberOfSensors1; ++_sensor_index)
              {
                if (this._pSdk.GetSensorInfos(_device_index, _sensor_index, 8192 /*0x2000*/, ref _sensor_id, ref _szName, ref _raw_value, ref _f, ref _min_value, ref _max_value) && this._pSdk.IS_F_DEFINED(_f))
                {
                  if (_szName.ToLower() == "package")
                    dictionary1["temperature"] = (object) _f;
                  else if (_szName.IndexOf('#') >= 0)
                  {
                    _szName = _szName.Replace("Core", "CPU");
                    if (!stringList.Contains(_szName))
                      stringList.Add(_szName);
                    if (dictionary3.ContainsKey(_szName))
                      dictionary3[_szName] = (object) _f;
                    else
                      dictionary3.Add(_szName, (object) _f);
                  }
                }
              }
              int numberOfSensors2 = this._pSdk.GetNumberOfSensors(_device_index, 57344 /*0xE000*/);
              for (int _sensor_index = 0; _sensor_index < numberOfSensors2; ++_sensor_index)
              {
                if (this._pSdk.GetSensorInfos(_device_index, _sensor_index, 57344 /*0xE000*/, ref _sensor_id, ref _szName, ref _raw_value, ref _f, ref _min_value, ref _max_value) && Math.Round((double) _f, 0) >= 0.0)
                {
                  if ((double) _f > 100.0)
                    _f = 100f;
                  if (_szName.ToLower() == "processor")
                    dictionary1["load"] = (object) _f;
                  else if (_szName.IndexOf('#') >= 0)
                  {
                    if (!stringList.Contains(_szName))
                      stringList.Add(_szName);
                    if (dictionary4.ContainsKey(_szName))
                      dictionary4[_szName] = (object) _f;
                    else
                      dictionary4.Add(_szName, (object) _f);
                  }
                }
              }
              int numberOfSensors3 = this._pSdk.GetNumberOfSensors(_device_index, 61440 /*0xF000*/);
              for (int _sensor_index = 0; _sensor_index < numberOfSensors3; ++_sensor_index)
              {
                if (this._pSdk.GetSensorInfos(_device_index, _sensor_index, 61440 /*0xF000*/, ref _sensor_id, ref _szName, ref _raw_value, ref _f, ref _min_value, ref _max_value) && Math.Round((double) _f, 0) >= 0.0 && _szName.IndexOf('#') >= 0)
                {
                  _szName = _szName.Replace("Core", "CPU");
                  if (!stringList.Contains(_szName))
                    stringList.Add(_szName);
                  if (dictionary2.ContainsKey(_szName))
                    dictionary2[_szName] = (object) _f;
                  else
                    dictionary2.Add(_szName, (object) _f);
                }
              }
              int numberOfSensors4 = this._pSdk.GetNumberOfSensors(_device_index, 4096 /*0x1000*/);
              for (int _sensor_index = 0; _sensor_index < numberOfSensors4; ++_sensor_index)
              {
                if (this._pSdk.GetSensorInfos(_device_index, _sensor_index, 4096 /*0x1000*/, ref _sensor_id, ref _szName, ref _raw_value, ref _f, ref _min_value, ref _max_value) && Math.Round((double) _f, 0) >= 0.0)
                {
                  if (_szName.ToLower() == "vid (max)")
                    num1 = _f;
                  else if ((double) num1 == 0.0)
                    num1 = _f;
                }
              }
            }
          }
          float sensorTypeValue = this._pSdk.GetSensorTypeValue(4214784 /*0x405000*/, ref num2, ref num2);
          float num3 = !this._pSdk.IS_F_DEFINED(sensorTypeValue) ? 0.0f : sensorTypeValue;
          dictionary1.Add("powers", (object) num3);
          dictionary1.Add("voltage", (object) num1);
          float num4 = 0.0f;
          if (dictionary2.Values.Count > 0)
          {
            foreach (float num5 in dictionary2.Values)
            {
              if ((double) num4 < (double) num5)
                num4 = num5;
            }
          }
          dictionary1.Add("clock", (object) num4.ToString("0.00"));
          if (!dictionary1.ContainsKey("load"))
          {
            float num6 = 0.0f;
            if (dictionary4.Values.Count > 0)
            {
              foreach (object obj in dictionary4.Values)
                num6 += float.Parse(obj.ToString());
              num6 /= (float) dictionary4.Values.Count;
            }
            dictionary1.Add("load", (object) num6.ToString("0.00"));
          }
          if (!dictionary1.ContainsKey("temperature"))
          {
            float num7 = 0.0f;
            if (dictionary3.Values.Count > 0)
            {
              foreach (object obj in dictionary3.Values)
                num7 += float.Parse(obj.ToString());
              num7 /= (float) dictionary3.Values.Count;
            }
            dictionary1.Add("temperature", (object) num7.ToString("0.00"));
          }
          dictionaryList.Add(dictionary1);
          foreach (string key in stringList)
          {
            Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
            dictionary5.Add("name", (object) key);
            if (dictionary2.ContainsKey(key))
              dictionary5.Add("clock", dictionary2[key]);
            if (dictionary3.ContainsKey(key))
              dictionary5.Add("temperature", dictionary3[key]);
            if (dictionary4.ContainsKey(key))
              dictionary5.Add("load", dictionary4[key]);
            if (dictionary5.Keys.Count == 4)
              dictionaryList.Add(dictionary5);
          }
          return dictionaryList.Count >= 1 ? dictionaryList : (List<Dictionary<string, object>>) null;
        }
        catch (Exception ex)
        {
          return (List<Dictionary<string, object>>) null;
        }
      }

      public override List<Dictionary<string, object>> GetGpuRunInfo()
      {
        try
        {
          List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
          string _szName = "";
          int _raw_value = 0;
          float _min_value = 0.0f;
          float _max_value = 0.0f;
          int _sensor_id = 0;
          float num = 0.0f;
          int numberOfDevices = this._pSdk.GetNumberOfDevices();
          for (int _device_index = 0; _device_index < numberOfDevices; ++_device_index)
          {
            string deviceName = this._pSdk.GetDeviceName(_device_index);
            if (this._pSdk.GetDeviceClass(_device_index) == 32 /*0x20*/)
            {
              Dictionary<string, object> dictionary = new Dictionary<string, object>();
              dictionary.Add("name", (object) deviceName);
              int numberOfSensors1 = this._pSdk.GetNumberOfSensors(_device_index, 8192 /*0x2000*/);
              for (int _sensor_index = 0; _sensor_index < numberOfSensors1; ++_sensor_index)
              {
                if (this._pSdk.GetSensorInfos(_device_index, _sensor_index, 8192 /*0x2000*/, ref _sensor_id, ref _szName, ref _raw_value, ref num, ref _min_value, ref _max_value) && this._pSdk.IS_F_DEFINED(num))
                {
                  if (_szName.ToUpper() == "GPU" || _szName.ToUpper() == "GLOBAL")
                  {
                    if (!dictionary.ContainsKey("temperature"))
                    {
                      dictionary.Add("temperature", (object) num);
                      break;
                    }
                    dictionary["temperature"] = (object) num;
                    break;
                  }
                  if (_szName.ToUpper().IndexOf("HOT") < 0 && _szName.ToUpper().IndexOf("MEMORY") < 0 && !dictionary.ContainsKey("temperature"))
                    dictionary.Add("temperature", (object) num);
                }
              }
              int numberOfSensors2 = this._pSdk.GetNumberOfSensors(_device_index, 57344 /*0xE000*/);
              float val1 = 0.0f;
              for (int _sensor_index = 0; _sensor_index < numberOfSensors2; ++_sensor_index)
              {
                if (this._pSdk.GetSensorInfos(_device_index, _sensor_index, 57344 /*0xE000*/, ref _sensor_id, ref _szName, ref _raw_value, ref num, ref _min_value, ref _max_value) && Math.Round((double) num, 0) >= 0.0)
                {
                  string upper = _szName.ToUpper();
                  if (upper == "GPU" || upper == "VIDEO ENGINE" || upper == "3D")
                  {
                    if ((double) num > 100.0)
                      num = 100f;
                    val1 = Math.Max(val1, num);
                    if (!dictionary.ContainsKey("load"))
                      dictionary.Add("load", (object) val1);
                    else
                      dictionary["load"] = (object) val1;
                  }
                }
              }
              int numberOfSensors3 = this._pSdk.GetNumberOfSensors(_device_index, 61440 /*0xF000*/);
              for (int _sensor_index = 0; _sensor_index < numberOfSensors3; ++_sensor_index)
              {
                if (this._pSdk.GetSensorInfos(_device_index, _sensor_index, 61440 /*0xF000*/, ref _sensor_id, ref _szName, ref _raw_value, ref num, ref _min_value, ref _max_value) && Math.Round((double) num, 0) >= 0.0)
                {
                  if (_szName == "Graphics")
                  {
                    if (!dictionary.ContainsKey("clock"))
                    {
                      dictionary.Add("clock", (object) num);
                      break;
                    }
                    dictionary["clock"] = (object) num;
                    break;
                  }
                  if (!dictionary.ContainsKey("clock"))
                    dictionary.Add("clock", (object) num);
                }
              }
              int numberOfSensors4 = this._pSdk.GetNumberOfSensors(_device_index, 24576 /*0x6000*/);
              for (int _sensor_index = 0; _sensor_index < numberOfSensors4; ++_sensor_index)
              {
                if (this._pSdk.GetSensorInfos(_device_index, _sensor_index, 24576 /*0x6000*/, ref _sensor_id, ref _szName, ref _raw_value, ref num, ref _min_value, ref _max_value) && Math.Round((double) num, 0) >= 0.0 && _szName.ToLower().IndexOf("fanpwm") >= 0)
                {
                  dictionary.Add("fan", (object) num);
                  break;
                }
              }
              if (dictionary.ContainsKey("temperature") && dictionary.ContainsKey("load"))
              {
                if (dictionary["name"].ToString().ToUpper().IndexOf("NVIDIA") >= 0)
                  dictionaryList.Insert(0, dictionary);
                else if (dictionary["name"].ToString().IndexOf("AMD Radeon") >= 0 && dictionary["name"].ToString().IndexOf("AMD Radeon(TM)") < 0)
                  dictionaryList.Insert(0, dictionary);
                else
                  dictionaryList.Add(dictionary);
              }
            }
          }
          return dictionaryList.Count >= 1 ? dictionaryList : (List<Dictionary<string, object>>) null;
        }
        catch (Exception ex)
        {
          Log.WriteLog("gpu error:" + ex.Message);
          return (List<Dictionary<string, object>>) null;
        }
      }
    }
}
